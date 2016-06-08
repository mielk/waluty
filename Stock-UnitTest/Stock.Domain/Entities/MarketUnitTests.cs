using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using System.Collections.Generic;
using Moq;
using Stock.Domain.Services;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class MarketUnitTests
    {

        private const int DEFAULT_ID = 1;
        private const string DEFAULT_NAME = "Forex";
        private const string DEFAULT_SHORT_NAME = "FX";
        private const string DEFAULT_START_TIME = "00:00";
        private const string DEFAULT_END_TIME = "00:00";
        

        //Arrange
        private Market defaultMarket()
        {
            var market = new Market(DEFAULT_ID, DEFAULT_NAME) { ShortName = DEFAULT_SHORT_NAME };
            return market;
        }

        private IEnumerable<Market> marketsCollection(){
            List<Market> markets = new List<Market>();
            markets.Add(defaultMarket());
            markets.Add(new Market(2, "United Kingdom") { ShortName = "UK" });
            return markets;
        }



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

            const int NOT_EXISTING_ID = 20;

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            Market.injectService(mockService.Object);

            //Act.
            Market market = Market.GetMarketById(NOT_EXISTING_ID);

            //Assert.
            Assert.IsNull(market);

        }


        [TestMethod]
        public void getMarket_returns_existing_instance_by_id()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(a => a.GetMarketById(DEFAULT_ID)).Returns(defaultMarket());
            Market.injectService(mockService.Object);

            //Act.
            Market market = Market.GetMarketById(DEFAULT_ID);

            //Assert.
            Assert.IsNotNull(market);
            Assert.AreEqual(DEFAULT_NAME, market.Name);
            Assert.AreEqual(DEFAULT_SHORT_NAME, market.ShortName);
            Assert.AreEqual(DEFAULT_ID, market.Id);

        }

        [TestMethod]
        public void getMarket_returns_existing_instance_by_name()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(a => a.GetMarketByName(DEFAULT_NAME)).Returns(defaultMarket());
            Market.injectService(mockService.Object);

            //Act.
            Market market = Market.GetMarketByName(DEFAULT_NAME);

            //Assert.
            Assert.IsNotNull(market);
            Assert.AreEqual(DEFAULT_NAME, market.Name);
            Assert.AreEqual(DEFAULT_SHORT_NAME, market.ShortName);
            Assert.AreEqual(DEFAULT_ID, market.Id);

        }

        [TestMethod]
        public void getMarket_returns_existing_instance_by_symbol()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(a => a.GetMarketBySymbol(DEFAULT_SHORT_NAME)).Returns(defaultMarket());
            Market.injectService(mockService.Object);

            //Act.
            Market market = Market.GetMarketBySymbol(DEFAULT_SHORT_NAME);

            //Assert.
            Assert.IsNotNull(market);
            Assert.AreEqual(DEFAULT_NAME, market.Name);
            Assert.AreEqual(DEFAULT_SHORT_NAME, market.ShortName);
            Assert.AreEqual(DEFAULT_ID, market.Id);

        }

        [TestMethod]
        public void getMarket_after_adding_new_item_returns_existing_instance()
        {

            const int NEW_MARKET_ID = 2;
            const string NEW_MARKET_NAME = "market2";

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(a => a.GetMarketById(DEFAULT_ID)).Returns(defaultMarket());
            mockService.Setup(a => a.GetMarketById(NEW_MARKET_ID)).Returns(new Market(NEW_MARKET_ID, NEW_MARKET_NAME));
            Market.injectService(mockService.Object);

            //Act.
            Market baseMarket = Market.GetMarketById(DEFAULT_ID);
            Market newMarket = Market.GetMarketById(NEW_MARKET_ID);
            Market baseMarketAgain = Market.GetMarketById(DEFAULT_ID);

            //Assert.
            Assert.AreSame(baseMarket, baseMarketAgain);

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
