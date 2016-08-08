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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Asset is empty")]
        public void run_if_asset_is_empty_exception_is_thrown()
        {
            var service = new ProcessService();

            Asset asset = null;
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M15);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.MACD, AnalysisType.Price };
            service.Setup(asset, timeframe, types);
            service.Run(true);
            
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Timeframe is empty")]
        public void run_if_timeframe_is_empty_exception_is_thrown()
        {
            
            var service = new ProcessService();

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = null;
            AnalysisType[] types = new AnalysisType[] { AnalysisType.MACD, AnalysisType.Price };
            service.Setup(asset, timeframe, types);
            service.Run(true);

        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Analyzers are not set")]
        public void run_if_there_is_no_analyzers_assigned_exception_is_thrown()
        {

            var service = new ProcessService();

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
            AnalysisType[] types = new AnalysisType[] { };
            service.Setup(asset, timeframe, types);
            service.Run(true);

        }



        [TestMethod]
        public void after_setup_properties_are_correctly_set()
        {

            var service = new ProcessService();

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.MACD, AnalysisType.Price };
            service.Setup(asset, timeframe, types);

            Assert.AreEqual(service.getAsset(), asset);
            Assert.AreEqual(service.getTimeframe(), timeframe);

        }




        [TestMethod]
        public void after_setup_proper_analyzers_are_assigned()
        {

            var service = new ProcessService();

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.MACD, AnalysisType.Price, AnalysisType.Candlestick };
            service.Setup(asset, timeframe, types);

            var analyzers = service.getAnalyzers();

            Assert.AreEqual(types.Length, analyzers.Count);

        }



        private Mock<IQuotationService> mockedQuotationService()
        {
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            quotationService.Setup(q => q.Setup(It.IsAny<Asset>(), It.IsAny<Timeframe>(), It.IsAny<Dictionary<AnalysisType, IAnalyzer>>()));
            ProcessServiceFactory.Instance().GetQuotationService(quotationService.Object);
            return quotationService;
        }


        


        [TestMethod]
        public void quotationProcessor_is_called_once_for_loading_data()
        {
            IProcessService service = new ProcessService();
            var mockQuotationService = mockedQuotationService();

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M15);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };
            ProcessServiceFactory.Instance().GetQuotationService(mockQuotationService.Object);
            service.Setup(asset, timeframe, types);
            service.Run(true);

            mockQuotationService.Verify(x => x.fetchData(), Times.Exactly(1));

        }



        [TestMethod]
        public void if_quotationService_returns_empty_array_of_data_items_Run_returns_false()
        {
            var service = new ProcessService();
            var mockQuotationService = mockedQuotationService();
            DataItem[] items = new DataItem[] { };
            mockQuotationService.Setup(q => q.fetchData()).Returns(items);


            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M15);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };
            service.Setup(asset, timeframe, types);

            Assert.IsFalse(service.Run(true));

        }




        [TestMethod]
        public void if_quotationService_returns_empty_array_of_data_items_analyzers_are_not_run()
        {

            Assert.Fail("Not created yet");

        }


        //if_strings_are_passed_to_setup_and_asset_doesnt_exist_throw_exception()
        //if_strings_are_passed_to_setup_and_asset_doesnt_exist_throw_exception()


    }
}
