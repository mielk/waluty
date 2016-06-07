using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using System.Collections.Generic;
using Moq;
using Stock.DAL.Repositories;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class MarketUnitTests
    {


        #region Constructor 

        [TestMethod]
        public void constructor_proper_name_and_id_is_assigned()
        {
            const int ID = 1;
            const string NAME = "FX";
            Market market = new Market(ID, NAME);

            Assert.AreEqual(ID, market.Id);
            Assert.AreEqual(NAME, market.Name);

        }

        [TestMethod]
        public void constructor_new_instance_empty_list_of_assets_is_created()
        {
            const int ID = 1;
            const string NAME = "FX";
            Market market = new Market(ID, NAME);

            Assert.IsNotNull(market.Assets);
            Assert.IsInstanceOfType(market.Assets, typeof(IEnumerable<Asset>));
        }
        #endregion Constructor




        #region setTimes
        [TestMethod]
        public void setTimes_byString_times_are_correctly_set()
        {
            Assert.Fail("Not implemented");
        }

        public void setTimes_byString_throws_exception_if_illegal_strings_gaven()
        {
            Assert.Fail("Not implemented");
        }

        public void setTimes_byDateTime_times_are_correctly_set()
        {
            Assert.Fail("Not implemented");
        }
        #endregion setTimes




        #region isOpen
        [TestMethod]
        public void isOpen_returns_false_if_before_open_time()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void isOpen_returns_false_if_after_close_time()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void isOpen_returns_true_if_equal_to_open_time()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void isOpen_returns_true_if_equal_to_close_time()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void isOpen_returns_true_if_between_open_and_close_time()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void isOpen_returns_true_if_midnight_for_247_market()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void isOpen_returns_false_if_saturday()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void isOpen_returns_false_if_sunday()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void isOpen_returns_false_if_newYear()
        {
            Assert.Fail("Not implemented");
        }
        #endregion isOpen




        #region getting markets
        [TestMethod]
        public void getMarket_returns_the_same_instance_each_time()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void getMarket_returns_null_if_not_exist_in_repository()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void getMarket_get_from_repository_if_not_loaded()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void getMarket_returns_existing_instance_by_id()
        {
            Assert.Fail("Not implemented");
        }


        [TestMethod]
        public void getMarket_returns_existing_instance_by_name()
        {

            //Create mock-up for IMarketRepository.
            Mock<IMarketRepository> mockRepository = new Mock<IMarketRepository>();
            //mockRepository.

            Assert.Fail("Not implemented");
        }


        [TestMethod]
        public void getAllMarkets_returns_existing_instances()
        {
            Assert.Fail("Not implemented");
        }
        #endregion getting markets




        #region adding assets
        [TestMethod]
        public void addAsset_after_adding_number_increased_by_1()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void addAsset_after_adding_already_existing_number_unchanged()
        {
            Assert.Fail("Not implemented");
        }


        [TestMethod]
        public void addAsset_after_adding_company_can_be_fetched()
        {
            Assert.Fail("Not implemented");
        }
        #endregion adding assets



    }
}
