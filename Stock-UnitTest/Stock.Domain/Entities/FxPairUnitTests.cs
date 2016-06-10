using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using System.Collections.Generic;
using Stock.DAL.TransferObjects;
using System.Linq;
using Moq;
using Stock.Domain.Services;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class FxPairUnitTests
    {

        private const int DEFAULT_ID = 1;
        private const string DEFAULT_NAME = "EURUSD";
        private const string DEFAULT_BASE_CURRENCY_SYMBOL = "USD";
        private const string DEFAULT_QUOTE_CURRENCY_SYMBOL = "EUR";
        private const int DEFAULT_BASE_CURRENCY_ID = 1;
        private const int DEFAULT_QUOTE_CURRENCY_ID = 2;


        //Arrange.
        private Currency getCurrency(int id)
        {
            return currenciesCollection().SingleOrDefault(c => c.Id == id);
        }

        private Currency getCurrencyBySymbol(string symbol)
        {
            return currenciesCollection().SingleOrDefault(c => c.Symbol.Equals(symbol));
        }

        private IEnumerable<Currency> currenciesCollection()
        {
            List<Currency> currencies = new List<Currency>();
            currencies.Add(new Currency(DEFAULT_BASE_CURRENCY_ID, DEFAULT_BASE_CURRENCY_SYMBOL, "US Dollar"));
            currencies.Add(new Currency(DEFAULT_QUOTE_CURRENCY_ID, DEFAULT_QUOTE_CURRENCY_SYMBOL, "Euro"));
            currencies.Add(new Currency(3, "JPY", "Japanese Yen"));
            currencies.Add(new Currency(4, "GBP", "British Pound"));
            return currencies;
        }


        private FxPair getFxPairById(int id)
        {
            return pairsCollection().SingleOrDefault(c => c.Id == id);
        }

        private FxPair getFxPairBySymbol(string symbol)
        {
            return pairsCollection().SingleOrDefault(p => p.Name.Equals(symbol));
        }



        private IEnumerable<FxPair> pairsCollection()
        {
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            Currency.injectService(mockService.Object);

            List<FxPair> fxPairs = new List<FxPair>();
            fxPairs.Add(new FxPair(1, "EURUSD", 2, 1));
            fxPairs.Add(new FxPair(2, "USDJPY", 1, 3));
            fxPairs.Add(new FxPair(3, "GBPUSD", 4, 1));
            fxPairs.Add(new FxPair(4, "EURGBP", 2, 4));
            fxPairs.Add(new FxPair(5, "EURJPY", 2, 3));
            return fxPairs;
        }






        [TestMethod]
        public void constructor_new_instance_has_proper_id_name_and_currencies()
        {

            //Arrange.
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            mockService.Setup(c => c.GetCurrencyBySymbol(It.IsAny<string>())).Returns((string a) => getCurrencyBySymbol(a));
            mockService.Setup(c => c.GetCurrencies()).Returns(currenciesCollection());
            Currency.injectService(mockService.Object);

            //Act.
            var baseCurrency = Currency.GetCurrencyBySymbol(DEFAULT_BASE_CURRENCY_SYMBOL);
            var quoteCurrency = Currency.GetCurrencyBySymbol(DEFAULT_QUOTE_CURRENCY_SYMBOL);
            var pair = new FxPair(DEFAULT_ID, DEFAULT_NAME, baseCurrency, quoteCurrency);

            //Assert.
            Assert.AreEqual(DEFAULT_ID, pair.Id);
            Assert.AreEqual(DEFAULT_NAME, pair.Name);
            Assert.IsTrue(Currency.GetCurrencyById(DEFAULT_BASE_CURRENCY_ID) == pair.BaseCurrency);
            Assert.IsTrue(Currency.GetCurrencyBySymbol(DEFAULT_QUOTE_CURRENCY_SYMBOL) == pair.QuoteCurrency);

        }


        [TestMethod]
        public void constructor_with_currencies_ids_assign_proper_currencies()
        {

            //Arrange.
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            mockService.Setup(c => c.GetCurrencies()).Returns(currenciesCollection());
            Currency.injectService(mockService.Object);

            //Act.
            var pair = new FxPair(DEFAULT_ID, DEFAULT_NAME, DEFAULT_BASE_CURRENCY_ID, DEFAULT_QUOTE_CURRENCY_ID);

            //Assert.
            Assert.IsTrue(Currency.GetCurrencyById(DEFAULT_BASE_CURRENCY_ID) == pair.BaseCurrency);
            Assert.IsTrue(Currency.GetCurrencyById(DEFAULT_QUOTE_CURRENCY_ID) == pair.QuoteCurrency);

        }


        [TestMethod]
        public void constructor_new_instance_empty_list_of_assetTimeframes_is_created()
        {

            //Arrange.
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            mockService.Setup(c => c.GetCurrencies()).Returns(currenciesCollection());
            Currency.injectService(mockService.Object);

            //Act.
            var pair = new FxPair(DEFAULT_ID, DEFAULT_NAME, DEFAULT_BASE_CURRENCY_ID, DEFAULT_QUOTE_CURRENCY_ID);

            //Assert.
            Assert.IsNotNull(pair.AssetTimeframes);
            Assert.IsInstanceOfType(pair.AssetTimeframes, typeof(IEnumerable<AssetTimeframe>));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The given currencies cannot be the same")]
        public void constructor_throw_exception_if_the_same_currency_ids()
        {

            //Arrange.
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            mockService.Setup(c => c.GetCurrencies()).Returns(currenciesCollection());
            Currency.injectService(mockService.Object);

            //Act.
            var pair = new FxPair(DEFAULT_ID, DEFAULT_NAME, DEFAULT_BASE_CURRENCY_ID, DEFAULT_BASE_CURRENCY_ID);

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "One of the given currencies is null")]
        public void constructor_throw_exception_if_given_currency_doesnt_exist()
        {
            //Arrange.
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            Currency.injectService(mockService.Object);

            //Act.
            var pair = new FxPair(DEFAULT_ID, DEFAULT_NAME, DEFAULT_BASE_CURRENCY_ID, 20);

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The given currencies cannot be the same")]
        public void constructor_throw_exception_if_the_same_currencies_given()
        {

            //Arrange.
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            Currency.injectService(mockService.Object);

            //Act.
            Currency currency = Currency.GetCurrencyById(1);
            var pair = new FxPair(DEFAULT_ID, DEFAULT_NAME, currency, currency);

        }


        [TestMethod]
        public void fxPair_fromDto_has_the_same_properties_as_dto()
        {

            //Arrange.
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            Currency.injectService(mockService.Object);
            FxPairDto dto = new FxPairDto { Id = 1, Name = "EURUSD", BaseCurrency = 1, QuoteCurrency = 2, IsActive = true };

            //Act.
            var pair = FxPair.FromDto(dto);

            //Assert.
            Assert.AreEqual(1, pair.Id);
            Assert.AreEqual("EURUSD", pair.Name);
            Assert.AreEqual(true, pair.IsFx);
            Assert.AreEqual(true, pair.IsActive);
            Assert.AreEqual(1, pair.BaseCurrency.Id);
            Assert.AreEqual(2, pair.QuoteCurrency.Id);
            Assert.IsTrue(Currency.GetCurrencyById(1) == pair.BaseCurrency);
            Assert.IsTrue(Currency.GetCurrencyById(2) == pair.QuoteCurrency);
            
        }












        #region getting fx pairs
        [TestMethod]
        public void getFxPair_returns_the_same_instance_each_time()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetFxPair(It.IsAny<int>())).Returns((int a) => getFxPairById(a));
            mockService.Setup(c => c.GetFxPair(It.IsAny<string>())).Returns((string a) => getFxPairBySymbol(a));
            FxPair.injectService(mockService.Object);

            //Act
            var eurusd1 = FxPair.GetFxPairById(1);
            var usdjpy1 = FxPair.GetFxPairById(2);
            var eurjpy1 = FxPair.GetFxPairById(5);
            var gbpusd1 = FxPair.GetFxPairById(3);
            var eurusd2 = FxPair.GetFxPairBySymbol("EURUSD");
            var usdjpy2 = FxPair.GetFxPairBySymbol("USDJPY");
            var eurusd3 = FxPair.GetFxPairBySymbol("EURUSD");
            var eurjpy2 = FxPair.GetFxPairBySymbol("EURJPY");
            var eurusd4 = FxPair.GetFxPairById(1);

            Assert.IsTrue(eurusd1 == eurusd2);
            Assert.IsTrue(eurusd1 == eurusd3);
            Assert.IsTrue(eurusd1 == eurusd4);
            Assert.IsTrue(usdjpy1 == usdjpy2);
            Assert.IsTrue(eurjpy1 == eurjpy2);

        }

        [TestMethod]
        public void getFxPair_returns_null_if_not_exist_in_repository()
        {

            const int NOT_EXISTING_ID = 20;

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetFxPair(It.IsAny<int>())).Returns((int a) => getFxPairById(a));
            mockService.Setup(c => c.GetFxPair(It.IsAny<string>())).Returns((string a) => getFxPairBySymbol(a));

            FxPair.injectService(mockService.Object);

            //Act.
            FxPair pair = FxPair.GetFxPairById(NOT_EXISTING_ID);

            //Assert.
            Assert.IsNull(pair);

        }

        [TestMethod]
        public void getFxPair_returns_existing_instance_by_id()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetFxPair(It.IsAny<int>())).Returns((int a) => getFxPairById(a));
            mockService.Setup(c => c.GetFxPair(It.IsAny<string>())).Returns((string a) => getFxPairBySymbol(a));

            FxPair.injectService(mockService.Object);

            //Act.
            FxPair pair = FxPair.GetFxPairById(DEFAULT_ID);

            //Assert.
            Assert.IsNotNull(pair);
            Assert.AreEqual(DEFAULT_ID, pair.Id);
            Assert.AreEqual(DEFAULT_NAME, pair.Name);

        }


        [TestMethod]
        public void getFxPair_returns_existing_instance_by_symbol()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetFxPair(It.IsAny<int>())).Returns((int a) => getFxPairById(a));
            mockService.Setup(c => c.GetFxPair(It.IsAny<string>())).Returns((string a) => getFxPairBySymbol(a));
            FxPair.injectService(mockService.Object);

            //Act.
            FxPair pair = FxPair.GetFxPairBySymbol(DEFAULT_NAME);

            //Assert.
            Assert.IsNotNull(pair);
            Assert.AreEqual(DEFAULT_ID, pair.Id);
            Assert.AreEqual(DEFAULT_NAME, pair.Name);

        }

        [TestMethod]
        public void getFxPair_after_adding_new_item_returns_existing_instance()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetFxPair(It.IsAny<int>())).Returns((int a) => getFxPairById(a));
            mockService.Setup(c => c.GetFxPair(It.IsAny<string>())).Returns((string a) => getFxPairBySymbol(a));
            FxPair.injectService(mockService.Object);


            //Act.
            FxPair eurusd = FxPair.GetFxPairById(1);
            FxPair usdjpy = FxPair.GetFxPairById(2);
            FxPair eurusdAgain = FxPair.GetFxPairById(1);

            //Assert.
            Assert.AreSame(eurusd, eurusdAgain);

        }

        [TestMethod]
        public void getFxPairs_returns_existing_instances()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetFxPair(It.IsAny<int>())).Returns((int a) => getFxPairById(a));
            mockService.Setup(c => c.GetFxPair(It.IsAny<string>())).Returns((string a) => getFxPairBySymbol(a));
            mockService.Setup(c => c.GetFxPairs()).Returns(pairsCollection());
            FxPair.injectService(mockService.Object);

            //Act.
            var eurusd = FxPair.GetFxPairById(1);
            var usdjpy = FxPair.GetFxPairById(2);
            var fxPairs = FxPair.GetAllFxPairs();

            //Assert.
            Assert.IsTrue(eurusd == fxPairs.SingleOrDefault(c => c.Name.Equals("EURUSD")));
            Assert.IsTrue(usdjpy == fxPairs.SingleOrDefault(c => c.Name.Equals("USDJPY")));

        }


        [TestMethod]
        public void getFxPairs_returns_proper_number_of_items()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetFxPair(It.IsAny<int>())).Returns((int a) => getFxPairById(a));
            mockService.Setup(c => c.GetFxPair(It.IsAny<string>())).Returns((string a) => getFxPairBySymbol(a));
            mockService.Setup(c => c.GetFxPairs()).Returns(pairsCollection());
            FxPair.injectService(mockService.Object);

            //Act.
            var eurusd = FxPair.GetFxPairById(1);
            var usdjpy = FxPair.GetFxPairById(2);
            var fxPairs = FxPair.GetAllFxPairs();

            //Asssert.
            Assert.AreEqual(5, fxPairs.Count());

        }

        #endregion getting fx pairs




    }
}
