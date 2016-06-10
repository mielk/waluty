using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.DAL.TransferObjects;
using System.Collections.Generic;
using Moq;
using Stock.Domain.Services;
using System.Linq;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class CurrencyUnitTests
    {

        private const int DEFAULT_ID = 1;
        private const string DEFAULT_NAME = "US Dollar";
        private const string DEFAULT_SYMBOL = "USD";
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

        [TestMethod]
        public void constructor_new_instance_has_proper_id_and_name()
        {
            var currency = new Currency(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);

            Assert.AreEqual(DEFAULT_ID, currency.Id);
            Assert.AreEqual(DEFAULT_NAME, currency.Name);
            Assert.AreEqual(DEFAULT_SYMBOL, currency.Symbol);

        }


        [TestMethod]
        public void currencyFromDto_has_the_same_properties_as_dto()
        {
            CurrencyDto dto = new CurrencyDto { Id = DEFAULT_ID, Symbol = DEFAULT_SYMBOL, Name = DEFAULT_NAME };
            Currency currency = Currency.FromDto(dto);

            Assert.AreEqual(DEFAULT_ID, currency.Id);
            Assert.AreEqual(DEFAULT_SYMBOL, currency.Symbol);
            Assert.AreEqual(DEFAULT_NAME, currency.Name);

        }




        #region getting currencies
        [TestMethod]
        public void getCurrency_returns_the_same_instance_each_time()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            Currency.injectService(mockService.Object);

            //Act
            var eur1 = Currency.GetCurrencyById(2);
            var usd1 = Currency.GetCurrencyById(1);
            var jpy1 = Currency.GetCurrencyById(3);
            var gbp1 = Currency.GetCurrencyById(4);
            var eur2 = Currency.GetCurrencyByName("Euro");
            var usd2 = Currency.GetCurrencyBySymbol("USD");
            var eur3 = Currency.GetCurrencyBySymbol("EUR");
            var jpy2 = Currency.GetCurrencyBySymbol("JPY");
            var eur4 = Currency.GetCurrencyById(2);

            Assert.IsTrue(eur1 == eur2);
            Assert.IsTrue(eur1 == eur3);
            Assert.IsTrue(eur1 == eur4);
            Assert.IsTrue(usd1 == usd2);
            Assert.IsTrue(jpy1 == jpy2);

        }

        [TestMethod]
        public void getCurrency_returns_null_if_not_exist_in_repository()
        {

            const int NOT_EXISTING_ID = 20;

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            Currency.injectService(mockService.Object);

            //Act.
            Currency currency = Currency.GetCurrencyById(NOT_EXISTING_ID);

            //Assert.
            Assert.IsNull(currency);

        }

        [TestMethod]
        public void getCurrency_returns_existing_instance_by_id()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            Currency.injectService(mockService.Object);

            //Act.
            Currency currency = Currency.GetCurrencyById(DEFAULT_ID);

            //Assert.
            Assert.IsNotNull(currency);
            Assert.AreEqual(DEFAULT_ID, currency.Id);
            Assert.AreEqual(DEFAULT_NAME, currency.Name);
            Assert.AreEqual(DEFAULT_SYMBOL, currency.Symbol);

        }

        [TestMethod]
        public void getCurrency_returns_existing_instance_by_name()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            Currency.injectService(mockService.Object);

            //Act.
            Currency currency = Currency.GetCurrencyByName(DEFAULT_NAME);

            //Assert.
            Assert.IsNotNull(currency);
            Assert.AreEqual(DEFAULT_ID, currency.Id);
            Assert.AreEqual(DEFAULT_NAME, currency.Name);
            Assert.AreEqual(DEFAULT_SYMBOL, currency.Symbol);

        }

        [TestMethod]
        public void getCurrency_returns_existing_instance_by_symbol()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            Currency.injectService(mockService.Object);

            //Act.
            Currency currency = Currency.GetCurrencyBySymbol(DEFAULT_SYMBOL);

            //Assert.
            Assert.IsNotNull(currency);
            Assert.AreEqual(DEFAULT_ID, currency.Id);
            Assert.AreEqual(DEFAULT_NAME, currency.Name);
            Assert.AreEqual(DEFAULT_SYMBOL, currency.Symbol);

        }

        [TestMethod]
        public void getCurrency_after_adding_new_item_returns_existing_instance()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            Currency.injectService(mockService.Object);

            //Act.
            Currency eur = Currency.GetCurrencyById(1);
            Currency usd = Currency.GetCurrencyById(2);
            Currency eurAgain = Currency.GetCurrencyById(1);

            //Assert.
            Assert.AreSame(eur, eurAgain);

        }

        [TestMethod]
        public void getAllCurrencies_returns_existing_instances()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencies()).Returns(currenciesCollection());
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            Currency.injectService(mockService.Object);

            //Act.
            var usd = Currency.GetCurrencyById(1);
            var eur = Currency.GetCurrencyById(2);
            var currencies = Currency.GetAllCurrencies();

            //Assert.
            Assert.IsTrue(eur == currencies.SingleOrDefault(c => c.Symbol.Equals("EUR")));
            Assert.IsTrue(usd == currencies.SingleOrDefault(c => c.Symbol.Equals("USD")));

        }


        [TestMethod]
        public void getAllCurrencies_returns_proper_number_of_items()
        {

            //Arrange
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetCurrencies()).Returns(currenciesCollection());
            mockService.Setup(c => c.GetCurrencyById(It.IsAny<int>())).Returns((int a) => getCurrency(a));
            Currency.injectService(mockService.Object);

            //Act.
            var usd = Currency.GetCurrencyById(1);
            var eur = Currency.GetCurrencyById(2);
            var currencies = Currency.GetAllCurrencies();

            //Asssert.
            Assert.AreEqual(4, currencies.Count());

        }

        #endregion getting currencies




    }
}
