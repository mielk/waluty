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
            AnalysisType[] types = new AnalysisType[] { };
            service.Setup(asset, timeframe, types);
            service.Run(true);

            Assert.IsTrue(service.getAsset() == asset);
            
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Timeframe is empty")]
        public void run_if_timeframe_is_empty_exception_is_thrown()
        {
            
            var service = new ProcessService();

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = null;
            AnalysisType[] types = new AnalysisType[] { };
            service.Setup(asset, timeframe, types);
            service.Run(true);

            Assert.IsTrue(service.getTimeframe() == timeframe);

        }




        private Mock<IQuotationService> mockedQuotationService()
        {
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            quotationService.Setup(q => q.Setup(It.IsAny<Asset>(), It.IsAny<Timeframe>(), It.IsAny<AnalysisType[]>()));
            ProcessServiceFactory.Instance().GetQuotationService(quotationService.Object);
            return quotationService;
        }


        [TestMethod]
        public void check_if_quotationProcessor_was_called_for_last_required_date()
        {
            IProcessService service = new ProcessService();
            var mockQuotationService = mockedQuotationService();

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M15);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };
            ProcessServiceFactory.Instance().GetQuotationService(mockQuotationService.Object);
            service.Setup(asset, timeframe, types);
            service.Run(true);

            mockQuotationService.Verify(x => x.findEarliestRequiredDate(It.IsAny<bool>()), Times.Exactly(1));

        }



        [TestMethod]
        public void check_if_quotationProcessor_loading_method_was_called_with_proper_parameter()
        {
            var service = new ProcessService();
            var mockQuotationService = mockedQuotationService();
            DateTime dt = new DateTime();
            mockQuotationService.Setup(q => q.findEarliestRequiredDate(It.IsAny<bool>())).Returns(dt);

            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M15);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };
            service.Setup(asset, timeframe, types);

            service.Run(true);
            mockQuotationService.Verify(x => x.loadData(dt), Times.Exactly(1));

            Assert.IsTrue(service.getTimeframe() == timeframe);

        }


        [TestMethod]
        public void if_quotationService_returns_empty_array_of_data_items_Run_returns_false()
        {
            var service = new ProcessService();
            var mockQuotationService = mockedQuotationService();
            DateTime dt = new DateTime();
            DataItem[] items = new DataItem[] { };
            mockQuotationService.Setup(q => q.loadData(It.IsAny<DateTime>())).Returns(items);
            mockQuotationService.Setup(q => q.findEarliestRequiredDate(It.IsAny<bool>())).Returns(dt);


            Asset asset = new Asset(1, "USD");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M15);
            AnalysisType[] types = new AnalysisType[] { AnalysisType.Price };
            service.Setup(asset, timeframe, types);

            bool result = service.Run(true);
            //mockQuotationService.Verify(x => x.loadData(dt), Times.Exactly(1));

            Assert.IsFalse(result);
        }




        //if_strings_are_passed_to_setup_and_asset_doesnt_exist_throw_exception()
        //if_strings_are_passed_to_setup_and_asset_doesnt_exist_throw_exception()


    }
}
