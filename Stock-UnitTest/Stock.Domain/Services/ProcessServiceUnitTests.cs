using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.Domain.Services;
using Moq;

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class ProcessServiceUnitTests
    {


        public IDataService mockDataService()
        {
            //Mock<IDataService> dataService = new Mock<IDataService>();
            return null;
        }


        [TestMethod]
        public void loadAssetTimeframe_asset_from_object_is_properly_loaded_()
        {
            Assert.Fail("Not implemented yet");
        }


        [TestMethod]
        public void loadAssetTimeframe_asset_from_string_is_properly_loaded_()
        {
            Assert.Fail("Not implemented yet");
        }


        [TestMethod]
        public void loadAssetTimeframe_timeframe_from_object_is_properly_loaded_()
        {
            Assert.Fail("Not implemented yet");
        }


        [TestMethod]
        public void loadAssetTimeframe_timeframe_from_string_is_properly_loaded_()
        {
            Assert.Fail("Not implemented yet");
        }


        [TestMethod]
        public void loadAssetTimeframe_timeframe_from_enum_is_properly_loaded_()
        {
            Assert.Fail("Not implemented yet");
        }


        [TestMethod]
        public void loadAssetTimeframe_symbol_is_properly_converted_to_asset_and_timeframe()
        {
            Assert.Fail("Not implemented yet");
        }


        [TestMethod]
        public void loadAssetTimeframe_if_asset_not_found_throw_exception()
        {
            Assert.Fail("Not implemented yet");
        }


        [TestMethod]
        public void loadAssetTimeframe_if_timeframe_not_found_throw_exception()
        {
            Assert.Fail("Not implemented yet");
        }


        [TestMethod]
        public void run_throws_exception_if_asset_is_not_set()
        {

            //dodać mock up do IDataRepository.

            //IProcessService service = new ProcessService();
            //service.LoadAssetTimeframe(null, Timeframe.GetTimeframe(TimeframeSymbol.D1));

            Assert.Fail("Not implemented yet");

        }


        [TestMethod]
        public void run_throws_exception_if_timeframe_is_not_set()
        {
            Assert.Fail("Not implemented yet");
        }


    }
}
