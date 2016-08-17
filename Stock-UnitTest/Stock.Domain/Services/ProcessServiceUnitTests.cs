using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.Domain.Services;
using Moq;
using Stock.DAL.Repositories;
using Stock.Domain.Enums;
using Stock.Domain.Services.Abstract;
using Stock.Domain.Services.Concrete;
using Stock.Domain.Services.Factories;
using System.Collections.Generic;

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class ProcessServiceUnitTests
    {



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



        private Mock<IQuotationService> mockedQuotationService()
        {
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            ProcessServiceFactory.Instance().GetQuotationService(quotationService.Object);
            return quotationService;
        }




        [TestMethod]
        public void quotationProcessor_is_called_once_for_loading_data()
        {
            
            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M15);
            IProcessService service = new ProcessService(asset, timeframe);
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
            var service = new ProcessService(asset, timeframe);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };
            service.Setup(types);

            Assert.IsFalse(service.Run(true));

        }



        private void injectMockedAnalyzerFactory(Dictionary<AnalysisType, Analyzer> analyzers)
        {
            AnalyzerFactory factory = getMockAnalyzerFactory(analyzers);
            AnalyzerFactory.inject(factory);
        }

        private void injectMockedAnalyzerFactory(AssetTimeframe atf)
        {
            Dictionary<AnalysisType, Analyzer> analyzers = getMockAnalyzersDictionary(atf);
            injectMockedAnalyzerFactory(analyzers);
        }

        private void injectMockedAnalyzerFactory()
        {
            Dictionary<AnalysisType, Analyzer> analyzers = getMockAnalyzersDictionary(testAssetTimeframe());
            injectMockedAnalyzerFactory(analyzers);
        }

        private DataItem[] getDataItemsArray(int counter)
        {
            return new DataItem[]{ };
        }

        private Dictionary<AnalysisType, Analyzer> getMockAnalyzersDictionary(AssetTimeframe atf)
        {
            
            return null;
        }

        //private T getMockedAnalyzer<T>(AnalysisType type)
        //{

        //}

        private AnalyzerFactory getMockAnalyzerFactory(Dictionary<AnalysisType, Analyzer> analyzers)
        {
            Mock<AnalyzerFactory> mockAnalyzerFactory = new Mock<AnalyzerFactory>();
            foreach (var analyzer in analyzers.Values)
            {
                mockAnalyzerFactory.Setup(af => af.getAnalyzer(analyzer.getAnalysisType(), analyzer.AssetTimeframe)).Returns(analyzer);
            }
            return mockAnalyzerFactory.Object;
        }

        [TestMethod]
        [Ignore]
        public void if_quotationService_returns_empty_array_of_data_items_analyzers_are_not_run()
        {

            var mockQuotationService = mockedQuotationService();
            DataItem[] items = getDataItemsArray(0);
            mockQuotationService.Setup(q => q.fetchData(It.IsAny<Dictionary<AnalysisType, IAnalyzer>>())).Returns(items);

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M15);
            var service = new ProcessService(asset, timeframe);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price, AnalysisType.MACD };
            service.Setup(types);



            Assert.IsFalse(service.Run(true));

        }



        //if_strings_are_passed_to_setup_and_asset_doesnt_exist_throw_exception()
        //if_strings_are_passed_to_setup_and_asset_doesnt_exist_throw_exception()


    }
}
