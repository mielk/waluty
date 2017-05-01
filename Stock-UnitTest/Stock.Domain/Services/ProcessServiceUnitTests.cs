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
using Stock_UnitTest.tools;

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class ProcessServiceUnitTests
    {


        private Asset assetForTest()
        {
            return new Asset(1, "USD", 1);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Asset is empty")]
        public void run_if_asset_is_empty_exception_is_thrown()
        {

            Asset asset = null;
            TimeframeOld timeframe = TimeframeOld.GetTimeframe(TimeframeSymbol.M15);
            var service = new ProcessService(asset, timeframe);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.MACD, AnalysisType.Price };
            service.Setup(types);
            service.Run(true);
            
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Timeframe is empty")]
        public void run_if_timeframe_is_empty_exception_is_thrown()
        {

            Asset asset = assetForTest();
            TimeframeOld timeframe = null;
            var service = new ProcessService(asset, timeframe);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.MACD, AnalysisType.Price };
            service.Setup(types);
            service.Run(true);

        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Analyzers are not set")]
        public void run_if_there_is_no_analyzers_assigned_exception_is_thrown()
        {

            Asset asset = assetForTest();
            TimeframeOld timeframe = TimeframeOld.GetTimeframe(TimeframeSymbol.M5);
            var service = new ProcessService(asset, timeframe);
            AnalysisType[] types = new AnalysisType[] { };
            service.Setup(types);
            service.Run(true);

        }


        [Ignore]
        [TestMethod]
        public void after_setup_properties_are_correctly_set()
        {

            Asset asset = assetForTest();
            TimeframeOld timeframe = TimeframeOld.GetTimeframe(TimeframeSymbol.M5);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };

            Mock<IQuotationService> mockQuotationService = UnitTestTools.mockedQuotationService();
            mockQuotationService.Setup(q => q.getLastCalculationDate(It.IsAny<string>(), It.IsAny<string>())).Returns(new DateTime());

            var service = new ProcessService(asset, timeframe);
            service.Setup(types);

            Assert.AreEqual(service.getAsset(), asset);
            Assert.AreEqual(service.getTimeframe(), timeframe);

        }


        [Ignore]
        [TestMethod]
        public void after_setup_proper_analyzers_are_assigned()
        {

            Asset asset = assetForTest();
            TimeframeOld timeframe = TimeframeOld.GetTimeframe(TimeframeSymbol.M5);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };

            Mock<IQuotationService> mockQuotationService = UnitTestTools.mockedQuotationService();
            mockQuotationService.Setup(q => q.getLastCalculationDate(It.IsAny<string>(), It.IsAny<string>())).Returns(new DateTime());

            var service = new ProcessService(asset, timeframe);
            service.Setup(types);

            var analyzers = service.getAnalyzers();

            Assert.AreEqual(types.Length, analyzers.Count);

        }


        [Ignore]
        [TestMethod]
        public void quotationProcessor_is_called_once_for_loading_data()
        {

            Asset asset = assetForTest();
            TimeframeOld timeframe = TimeframeOld.GetTimeframe(TimeframeSymbol.M15);
            ProcessService service = new ProcessService(asset, timeframe);
            var mockQuotationService = UnitTestTools.mockedQuotationService();
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };
            service.injectQuotationService(mockQuotationService.Object);
            service.Setup(types);
            service.Run(true);

            mockQuotationService.Verify(x => x.fetchData(It.IsAny<Dictionary<AnalysisType, IAnalyzer>>()), Times.Exactly(1));

        }


        [Ignore]
        [TestMethod]
        public void if_quotationService_returns_empty_array_of_data_items_Run_returns_false()
        {

            var mockQuotationService = UnitTestTools.mockedQuotationService();
            DataItem[] items = new DataItem[] { };
            mockQuotationService.Setup(q => q.fetchData(It.IsAny<Dictionary<AnalysisType, IAnalyzer>>())).Returns(items);

            Asset asset = assetForTest();
            TimeframeOld timeframe = TimeframeOld.GetTimeframe(TimeframeSymbol.M15);
            ProcessService service = new ProcessService(asset, timeframe);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };
            service.Setup(types);

            Assert.IsFalse(service.Run(true));

        }


        [Ignore]
        [TestMethod]
        public void if_quotationService_returns_empty_array_of_data_items_analyzers_are_not_run()
        {

            //Create mocked IQuotationService
            var mockQuotationService = UnitTestTools.mockedQuotationService();
            DataItem[] items = UnitTestTools.getDataItemsArray(0);
            mockQuotationService.Setup(q => q.fetchData(It.IsAny<Dictionary<AnalysisType, IAnalyzer>>())).Returns(items);

            AssetTimeframe atf = UnitTestTools.testAssetTimeframe();
            Dictionary<AnalysisType, IAnalyzer> analyzers = new Dictionary<AnalysisType, IAnalyzer>();
            DateTime laterDate = new DateTime(2016, 8, 1);
            DateTime earlierDate = new DateTime(2016, 7, 5);

            //Create mocked analyzers.
            var mockedPriceAnalyzer = UnitTestTools.generateMockAnalyzer(atf, AnalysisType.Price, laterDate);
            var mockedMacdAnalyzer = UnitTestTools.generateMockAnalyzer(atf, AnalysisType.MACD, laterDate);
            var mockedAdxAnalyzer = UnitTestTools.generateMockAnalyzer(atf, AnalysisType.ADX, laterDate);

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


        [Ignore]
        [TestMethod]
        public void if_quotationService_returns_non_empty_array_of_data_items_analyzers_are_run()
        {

            //Create mocked IQuotationService

            AssetTimeframe atf = UnitTestTools.testAssetTimeframe();
            DateTime startDate = new DateTime(2016, 8, 1);
            DateTime endDate = new DateTime(2016, 8, 14);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price, AnalysisType.MACD, AnalysisType.ADX };
            DataItem[] items = UnitTestTools.getDataItemsArray(atf.Timeframe.Symbol, startDate, endDate, UnitTestTools.createAnalysisTypeList(types));

            var mockQuotationService = UnitTestTools.mockedQuotationService();
            mockQuotationService.Setup(q => q.fetchData(It.IsAny<Dictionary<AnalysisType, IAnalyzer>>())).Returns(items);
            Dictionary<AnalysisType, IAnalyzer> analyzers = new Dictionary<AnalysisType, IAnalyzer>();

            //Create mocked analyzers.
            var mockedPriceAnalyzer = UnitTestTools.generateMockAnalyzer(atf, AnalysisType.Price, null);
            var mockedMacdAnalyzer = UnitTestTools.generateMockAnalyzer(atf, AnalysisType.MACD, null);
            var mockedAdxAnalyzer = UnitTestTools.generateMockAnalyzer(atf, AnalysisType.ADX, null);

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
