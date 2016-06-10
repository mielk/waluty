using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using System.Collections.Generic;
using Moq;
using Stock.Domain.Services;
using Stock.DAL.TransferObjects;
using System.Linq;

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
            return getMarket(DEFAULT_ID);
        }

        private Market getMarket(int id)
        {
            IEnumerable<Market> markets = marketsCollection();
            Market market = markets.SingleOrDefault(m => m.Id == id);
            return market;
        }

        private IEnumerable<Market> marketsCollection()
        {
            List<Market> markets = new List<Market>();
            markets.Add(new Market(DEFAULT_ID, DEFAULT_NAME) { ShortName = DEFAULT_SHORT_NAME });
            markets.Add(new Market(2, "United Kingdom") { ShortName = "UK" });
            markets.Add(new Market(3, "United States") { ShortName = "US" });
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

        [TestMethod]
        public void marketFromDto_has_the_same_properties_as_dto()
        {
            MarketDto dto = new MarketDto { 
                Id = DEFAULT_ID, 
                Name = DEFAULT_NAME, 
                ShortName = DEFAULT_SHORT_NAME, 
                StartTime = DEFAULT_START_TIME, 
                EndTime = DEFAULT_END_TIME 
            };

            Market market = Market.FromDto(dto);

            Assert.AreEqual(DEFAULT_ID, market.Id);
            Assert.AreEqual(DEFAULT_NAME, market.Name);
            Assert.AreEqual(DEFAULT_SHORT_NAME, market.ShortName);
            //Assert.Fail("Dodać porównywanie godzin");

        }


        #endregion Constructor




        #region setTimes

        //[TestMethod]
        //public void setTimes_byString_times_are_correctly_set()
        //{
        //    Assert.Fail("Not implemented");
        //}

        //[TestMethod]
        //public void setTimes_byString_throws_exception_if_illegal_strings_gaven()
        //{
        //    Assert.Fail("Not implemented");
        //}

        //[TestMethod]
        //public void setTimes_byDateTime_times_are_correctly_set()
        //{
        //    Assert.Fail("Not implemented");
        //}

        #endregion setTimes




        #region isOpen
        //[TestMethod]
        //public void isOpen_returns_false_if_before_open_time()
        //{
        //    Assert.Fail("Not implemented");
        //}

        //[TestMethod]
        //public void isOpen_returns_false_if_after_close_time()
        //{
        //    Assert.Fail("Not implemented");
        //}

        //[TestMethod]
        //public void isOpen_returns_true_if_equal_to_open_time()
        //{
        //    Assert.Fail("Not implemented");
        //}

        //[TestMethod]
        //public void isOpen_returns_true_if_equal_to_close_time()
        //{
        //    Assert.Fail("Not implemented");
        //}

        //[TestMethod]
        //public void isOpen_returns_true_if_between_open_and_close_time()
        //{
        //    Assert.Fail("Not implemented");
        //}

        //[TestMethod]
        //public void isOpen_returns_true_if_midnight_for_247_market()
        //{
        //    Assert.Fail("Not implemented");
        //}

        //[TestMethod]
        //public void isOpen_returns_false_if_saturday()
        //{
        //    Assert.Fail("Not implemented");
        //}

        //[TestMethod]
        //public void isOpen_returns_false_if_sunday()
        //{
        //    Assert.Fail("Not implemented");
        //}

        //[TestMethod]
        //public void isOpen_returns_false_if_newYear()
        //{
        //    Assert.Fail("Not implemented");
        //}
        #endregion isOpen




        #region getting markets
        [TestMethod]
        public void getMarket_returns_the_same_instance_each_time()
        {
            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetMarketById(It.IsAny<int>())).Returns((int a) => getMarket(a));
            Market.injectService(mockService.Object);

            //Act
            var fx1 = Market.GetMarketById(1);
            var uk1 = Market.GetMarketById(2);
            var us1 = Market.GetMarketById(3);
            var fx2 = Market.GetMarketByName("Forex");
            var uk2 = Market.GetMarketBySymbol("UK");
            var fx3 = Market.GetMarketBySymbol("FX");
            var us2 = Market.GetMarketBySymbol("US");
            var fx4 = Market.GetMarketById(1);

            Assert.IsTrue(fx1 == fx2);
            Assert.IsTrue(fx1 == fx3);
            Assert.IsTrue(fx1 == fx4);
            Assert.IsTrue(uk1 == uk2);
            Assert.IsTrue(us1 == us2);

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
            mockService.Setup(c => c.GetMarketById(It.IsAny<int>())).Returns((int a) => getMarket(a));
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

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetMarketById(It.IsAny<int>())).Returns((int a) => getMarket(a));
            Market.injectService(mockService.Object);

            //Act.
            Market baseMarket = Market.GetMarketById(1);
            Market newMarket = Market.GetMarketById(2);
            Market baseMarketAgain = Market.GetMarketById(1);

            //Assert.
            Assert.AreSame(baseMarket, baseMarketAgain);

        }


        [TestMethod]
        public void getAllMarkets_returns_existing_instances()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetMarkets()).Returns(marketsCollection());
            mockService.Setup(c => c.GetMarketById(It.IsAny<int>())).Returns((int a) => getMarket(a));
            Market.injectService(mockService.Object);

            //Act.
            var fx = Market.GetMarketById(1);
            var uk = Market.GetMarketById(2);
            var markets = Market.GetAllMarkets();

            //Assert.
            Assert.IsTrue(fx == markets.SingleOrDefault(m => m.ShortName.Equals("FX")));
            Assert.IsTrue(uk == markets.SingleOrDefault(m => m.ShortName.Equals("UK")));

        }


        [TestMethod]
        public void getAllMarkets_returns_proper_number_of_markets()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetMarkets()).Returns(marketsCollection());
            mockService.Setup(c => c.GetMarketById(It.IsAny<int>())).Returns((int a) => getMarket(a));
            Market.injectService(mockService.Object);

            //Act.
            var fx = Market.GetMarketById(1);
            var uk = Market.GetMarketById(2);
            var markets = Market.GetAllMarkets();

            //Asssert.
            Assert.AreEqual(3, markets.Count());

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
