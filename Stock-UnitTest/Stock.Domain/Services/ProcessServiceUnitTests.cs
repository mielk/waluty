using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.Domain.Services;
using Moq;
using Stock.DAL.Repositories;
using Stock.Domain.Enums;
using Stock.Domain.Services.Concrete;
using Stock.Domain.Services.Factories;
using System.Collections.Generic;

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class ProcessServiceUnitTests
    {


        #region testObjects

        private Asset testAsset()
        {
            var asset = new Asset(1, "asset");
            return asset;
        }

        private Timeframe testTimeframe()
        {
            return Timeframe.GetTimeframe(TimeframeSymbol.M5);
        }

        private AssetTimeframe testAssetTimeframe()
        {
            return new AssetTimeframe(testAsset(), testTimeframe());
        }

        private List<AnalysisType> createAnalysisTypeList(AnalysisType[] types)
        {
            List<AnalysisType> list = new List<AnalysisType>();
            foreach (var type in types)
            {
                list.Add(type);
            }
            return list;
        }

        #endregion testObjects


        #region testServices

        private Mock<IAnalyzer> generateMockAnalyzer(AssetTimeframe atf, AnalysisType type, DateTime? firstRequiredDate)
        {
            Mock<IAnalyzer> mock = new Mock<IAnalyzer>();
            mock.Setup(q => q.getFirstRequiredDate()).Returns(firstRequiredDate);
            mock.Setup(q => q.getAssetTimeframe()).Returns(atf);
            mock.Setup(q => q.getAnalysisType()).Returns(type);
            return mock;
        }

        private Mock<IQuotationService> mockedQuotationService()
        {
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            ProcessServiceFactory.Instance().GetQuotationService(quotationService.Object);
            return quotationService;
        }

        private DataItem[] getDataItemsArray(int counter)
        {
            return new DataItem[] { };
        }

        private DataItem[] getDataItemsArray(TimeframeSymbol timeframe, DateTime startDate, DateTime endDate, List<AnalysisType> types)
        {
            DateTime minDate = startDate.CompareTo(endDate) < 0 ? startDate : endDate;
            DateTime d = new DateTime(minDate.Ticks);
            List<DataItem> items = new List<DataItem>();

            while (d.CompareTo(endDate) <= 0)
            {
                var item = generateDataItem(d, types);
                d = d.getNext(timeframe);
                items.Add(item);
            }

            return items.ToArray();

        }

        private DataItem generateDataItem(DateTime d, List<AnalysisType> types)
        {
            var item = new DataItem();
            item.Asset = testAsset();
            item.Timeframe = testTimeframe();

            if (types.Contains(AnalysisType.Price)) item.Price = new Price() { Date = d };
            if (types.Contains(AnalysisType.MACD)) item.Macd = new Macd() { Date = d };
            if (types.Contains(AnalysisType.ADX)) item.Adx = new Adx() { Date = d };

            return item;

        }

        #endregion testServices


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Asset is empty")]
        public void run_if_asset_is_empty_exception_is_thrown()
        {

            Asset asset = null;
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M15);
            var service = new ProcessService(asset, timeframe);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.MACD, AnalysisType.Price };
            service.Setup(types);
            service.Run(true);
            
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Timeframe is empty")]
        public void run_if_timeframe_is_empty_exception_is_thrown()
        {
            
            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = null;
            var service = new ProcessService(asset, timeframe);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.MACD, AnalysisType.Price };
            service.Setup(types);
            service.Run(true);

        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Analyzers are not set")]
        public void run_if_there_is_no_analyzers_assigned_exception_is_thrown()
        {

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
            var service = new ProcessService(asset, timeframe);
            AnalysisType[] types = new AnalysisType[] { };
            service.Setup(types);
            service.Run(true);

        }



        [TestMethod]
        public void after_setup_properties_are_correctly_set()
        {

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };

            Mock<IQuotationService> mockQuotationService = mockedQuotationService();
            mockQuotationService.Setup(q => q.getLastCalculationDate(It.IsAny<string>(), It.IsAny<string>())).Returns(new DateTime());

            var service = new ProcessService(asset, timeframe);
            service.Setup(types);

            Assert.AreEqual(service.getAsset(), asset);
            Assert.AreEqual(service.getTimeframe(), timeframe);

        }



        [TestMethod]
        public void after_setup_proper_analyzers_are_assigned()
        {

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };

            Mock<IQuotationService> mockQuotationService = mockedQuotationService();
            mockQuotationService.Setup(q => q.getLastCalculationDate(It.IsAny<string>(), It.IsAny<string>())).Returns(new DateTime());

            var service = new ProcessService(asset, timeframe);
            service.Setup(types);

            var analyzers = service.getAnalyzers();

            Assert.AreEqual(types.Length, analyzers.Count);

        }



        [TestMethod]
        public void quotationProcessor_is_called_once_for_loading_data()
        {
            
            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M15);
            ProcessService service = new ProcessService(asset, timeframe);
            var mockQuotationService = mockedQuotationService();
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };
            ProcessServiceFactory.Instance().GetQuotationService(mockQuotationService.Object);
            service.Setup(types);
            service.Run(true);

            mockQuotationService.Verify(x => x.fetchData(It.IsAny<Dictionary<AnalysisType, IAnalyzer>>()), Times.Exactly(1));

        }



        [TestMethod]
        public void if_quotationService_returns_empty_array_of_data_items_Run_returns_false()
        {
            
            var mockQuotationService = mockedQuotationService();
            DataItem[] items = new DataItem[] { };
            mockQuotationService.Setup(q => q.fetchData(It.IsAny<Dictionary<AnalysisType, IAnalyzer>>())).Returns(items);

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M15);
            ProcessService service = new ProcessService(asset, timeframe);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };
            service.Setup(types);

            Assert.IsFalse(service.Run(true));

        }



        [TestMethod]
        public void if_quotationService_returns_empty_array_of_data_items_analyzers_are_not_run()
        {

            //Create mocked IQuotationService
            var mockQuotationService = mockedQuotationService();
            DataItem[] items = getDataItemsArray(0);
            mockQuotationService.Setup(q => q.fetchData(It.IsAny<Dictionary<AnalysisType, IAnalyzer>>())).Returns(items);

            AssetTimeframe atf = testAssetTimeframe();
            Dictionary<AnalysisType, IAnalyzer> analyzers = new Dictionary<AnalysisType, IAnalyzer>();
            DateTime laterDate = new DateTime(2016, 8, 1);
            DateTime earlierDate = new DateTime(2016, 7, 5);

            //Create mocked analyzers.
            var mockedPriceAnalyzer = generateMockAnalyzer(atf, AnalysisType.Price, laterDate);
            var mockedMacdAnalyzer = generateMockAnalyzer(atf, AnalysisType.MACD, laterDate);
            var mockedAdxAnalyzer = generateMockAnalyzer(atf, AnalysisType.ADX, laterDate);

            analyzers.Add(AnalysisType.Price, mockedPriceAnalyzer.Object);
            analyzers.Add(AnalysisType.MACD, mockedMacdAnalyzer.Object);
            analyzers.Add(AnalysisType.ADX, mockedAdxAnalyzer.Object);

            var service = new ProcessService(atf);
            service.loadAnalyzers(analyzers);
            service.injectQuotationService(mockQuotationService.Object);
            service.Run(true);

            mockedPriceAnalyzer.Verify(x => x.Analyze(It.IsAny<DataItem[]>()), Times.Exactly(0));
            mockedMacdAnalyzer.Verify(x => x.Analyze(It.IsAny<DataItem[]>()), Times.Exactly(0));
            mockedAdxAnalyzer.Verify(x => x.Analyze(It.IsAny<DataItem[]>()), Times.Exactly(0));

        }



        [TestMethod]
        public void if_quotationService_returns_non_empty_array_of_data_items_analyzers_are_run()
        {

            //Create mocked IQuotationService
            
            AssetTimeframe atf = testAssetTimeframe();
            DateTime startDate = new DateTime(2016, 8, 1);
            DateTime endDate = new DateTime(2016, 8, 14);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price, AnalysisType.MACD, AnalysisType.ADX };
            DataItem[] items = getDataItemsArray(atf.timeframe.Symbol, startDate, endDate, createAnalysisTypeList(types));

            var mockQuotationService = mockedQuotationService();
            mockQuotationService.Setup(q => q.fetchData(It.IsAny<Dictionary<AnalysisType, IAnalyzer>>())).Returns(items);
            Dictionary<AnalysisType, IAnalyzer> analyzers = new Dictionary<AnalysisType, IAnalyzer>();

            //Create mocked analyzers.
            var mockedPriceAnalyzer = generateMockAnalyzer(atf, AnalysisType.Price, null);
            var mockedMacdAnalyzer = generateMockAnalyzer(atf, AnalysisType.MACD, null);
            var mockedAdxAnalyzer = generateMockAnalyzer(atf, AnalysisType.ADX, null);

            analyzers.Add(AnalysisType.Price, mockedPriceAnalyzer.Object);
            analyzers.Add(AnalysisType.MACD, mockedMacdAnalyzer.Object);
            analyzers.Add(AnalysisType.ADX, mockedAdxAnalyzer.Object);

            var service = new ProcessService(atf);
            service.loadAnalyzers(analyzers);
            service.injectQuotationService(mockQuotationService.Object);
            service.Run(true);

            mockedPriceAnalyzer.Verify(x => x.Analyze(items), Times.Exactly(1));
            mockedMacdAnalyzer.Verify(x => x.Analyze(items), Times.Exactly(1));
            mockedAdxAnalyzer.Verify(x => x.Analyze(items), Times.Exactly(1));

        }




    }
}
