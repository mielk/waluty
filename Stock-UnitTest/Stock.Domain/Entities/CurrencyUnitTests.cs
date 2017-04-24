using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.DAL.TransferObjects;
using System.Collections.Generic;
using Moq;
using Stock.Domain.Services;
using System.Linq;
using Stock.Utils;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class CurrencyUnitTests
    {

        private const int DEFAULT_ID = 1;
        private const string DEFAULT_NAME = "US Dollar";
        private const string DEFAULT_SYMBOL = "USD";

        //Arrange.
        private IEnumerable<Currency> getCurrenciesCollection()
        {
            List<Currency> currencies = new List<Currency>();
            currencies.Add(new Currency(1, "USD", "US Dollar"));
            currencies.Add(new Currency(2, "EUR", "Euro"));
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


        [TestMethod]
        public void Equals_returnFalse_forObjectOfOtherType()
        {
            var baseObject = new Currency(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);
            var comparedObject = new Market(1, "market");
            Assert.IsFalse(baseObject.Equals(comparedObject));
        }

        [TestMethod]
        public void Equals_returnFalse_ifIdDifferent()
        {
            var baseObject = new Currency(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);
            var comparedObject = new Currency(DEFAULT_ID + 1, DEFAULT_SYMBOL, DEFAULT_NAME);
            Assert.IsFalse(baseObject.Equals(comparedObject));
        }

        [TestMethod]
        public void Equals_returnFalse_ifNameDifferent()
        {
            var baseObject = new Currency(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);
            var comparedObject = new Currency(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME + 'a');
            Assert.IsFalse(baseObject.Equals(comparedObject));
        }

        [TestMethod]
        public void Equals_returnFalse_ifSymbolDifferent()
        {
            var baseObject = new Currency(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);
            var comparedObject = new Currency(DEFAULT_ID, DEFAULT_SYMBOL + 'a', DEFAULT_NAME);
            Assert.IsFalse(baseObject.Equals(comparedObject));
        }

        [TestMethod]
        public void Equals_returnTrue_ifCurrenciesEqual()
        {
            var baseObject = new Currency(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);
            var comparedObject = new Currency(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);
            Assert.IsTrue(baseObject.Equals(comparedObject));
        }



        [TestMethod]
        public void ById_returnsTheSameInstance_eachTime()
        {

            //Arrange
            Mock<ICurrencyService> mockService = new Mock<ICurrencyService>();
            mockService.Setup(c => c.GetCurrencyById(DEFAULT_ID)).Returns(new Currency(DEFAULT_ID, DEFAULT_NAME, DEFAULT_SYMBOL));
            Currency.injectService(mockService.Object);

            //Act
            var usd1 = Currency.ById(DEFAULT_ID);
            var usd2 = Currency.BySymbol(DEFAULT_SYMBOL);

            //Assert
            Assert.AreSame(usd1, usd2);

        }

        [TestMethod]
        public void ById_returnsNull_ifNotExistInRepository()
        {

            //Arrange
            Mock<ICurrencyService> mockService = new Mock<ICurrencyService>();
            Currency nullCurrency = null;
            mockService.Setup(c => c.GetCurrencyById(DEFAULT_ID)).Returns(nullCurrency);
            Currency.injectService(mockService.Object);

            //Act.
            Currency currency = Currency.ById(DEFAULT_ID);

            //Assert.
            Assert.IsNull(currency);

        }

        [TestMethod]
        public void ById_returnsExistingInstance()
        {

            //Arrange
            Mock<ICurrencyService> mockService = new Mock<ICurrencyService>();
            Currency expectedCurrency = new Currency(DEFAULT_ID, DEFAULT_NAME, DEFAULT_SYMBOL);
            mockService.Setup(c => c.GetCurrencyById(DEFAULT_ID)).Returns(expectedCurrency);
            Currency.injectService(mockService.Object);

            //Act.
            Currency currency = Currency.ById(DEFAULT_ID);

            //Assert.
            Assert.AreSame(currency, expectedCurrency);

        }

        [TestMethod]
        public void ByName_returnsExistingInstance()
        {

            //Arrange
            Mock<ICurrencyService> mockService = new Mock<ICurrencyService>();
            Currency expectedCurrency = new Currency(DEFAULT_ID, DEFAULT_NAME, DEFAULT_SYMBOL);
            mockService.Setup(c => c.GetCurrencyByName(DEFAULT_NAME)).Returns(expectedCurrency);
            Currency.injectService(mockService.Object);

            //Act.
            Currency currency = Currency.ByName(DEFAULT_NAME);

            //Assert.
            Assert.AreSame(currency, expectedCurrency);

        }

        [TestMethod]
        public void BySymbol_returnsExistingInstance()
        {

            //Arrange
            Mock<ICurrencyService> mockService = new Mock<ICurrencyService>();
            Currency expectedCurrency = new Currency(DEFAULT_ID, DEFAULT_NAME, DEFAULT_SYMBOL);
            mockService.Setup(c => c.GetCurrencyBySymbol(DEFAULT_SYMBOL)).Returns(expectedCurrency);
            Currency.injectService(mockService.Object);

            //Act.
            Currency currency = Currency.BySymbol(DEFAULT_SYMBOL);

            //Assert.
            Assert.AreSame(currency, expectedCurrency);

        }

        [TestMethod]
        public void ById_returnsExistingInstance_afterAddingNewItem()
        {

            //Arrange
            Mock<ICurrencyService> mockService = new Mock<ICurrencyService>();
            mockService.Setup(c => c.GetCurrencyById(1)).Returns(new Currency(1, "USD", "US Dollar"));
            mockService.Setup(c => c.GetCurrencyById(2)).Returns(new Currency(2, "EUR", "Euro"));
            Currency.injectService(mockService.Object);

            //Act
            Currency eur = Currency.ById(2);
            Currency usd = Currency.ById(1);
            Currency eurAgain = Currency.ById(2);

            //Assert
            Assert.AreSame(eur, eurAgain);

        }

        [TestMethod]
        public void GetAllCurrencies_returnsProperCollection()
        {

            //Arrange
            Mock<ICurrencyService> mockService = new Mock<ICurrencyService>();
            var expectedCurrencies = getCurrenciesCollection();
            mockService.Setup(c => c.GetAllCurrencies()).Returns(expectedCurrencies);
            Currency.injectService(mockService.Object);

            //Act.
            var currencies = Currency.GetAllCurrencies();

            //Assert.
            bool areEquivalent = currencies.ScrambledEquals(expectedCurrencies);
            Assert.IsTrue(areEquivalent);

        }

    }
}
