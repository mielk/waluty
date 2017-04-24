using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.DAL.Repositories;
using Moq;
using Stock.Domain.Entities;
using Stock.DAL.TransferObjects;
using Stock.Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace Stock_UnitTest.Stock.Domain.Services.MarketServices
{
    [TestClass]
    public class CurrencyServiceUnitTest
    {
        private const int DEFAULT_ID = 1;
        private const string DEFAULT_NAME = "US Dollar";
        private const string DEFAULT_SYMBOL = "USD";



        private ICurrencyService testServiceInstance(Mock<ICurrencyRepository> mockedRepository)
        {
            ICurrencyService service = CurrencyService.Instance(true);
            service.InjectRepository(mockedRepository.Object);
            return service;
        }

        private CurrencyDto[] getCurrencyDtos()
        {
            List<CurrencyDto> list = new List<CurrencyDto>();
            list.Add(new CurrencyDto { Id = 1, Name = "US Dollar", Symbol = "USD" });
            list.Add(new CurrencyDto { Id = 2, Name = "Euro", Symbol = "EUR" });
            list.Add(new CurrencyDto { Id = 3, Name = "Japanese Yen", Symbol = "JPY" });
            return list.ToArray();
        }



        [TestMethod]
        public void GetCurrencyById_ReturnsCurrency_IfExists()
        {

            //Arrange
            Mock<ICurrencyRepository> mockedRepository = new Mock<ICurrencyRepository>();
            CurrencyDto dto = new CurrencyDto { Id = DEFAULT_ID, Name = DEFAULT_NAME, Symbol = DEFAULT_SYMBOL };
            mockedRepository.Setup(c => c.GetCurrencyById(DEFAULT_ID)).Returns(dto);
            Currency expectedCurrency = new Currency(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);
            
            //Act
            ICurrencyService service = testServiceInstance(mockedRepository);
            Currency currency = service.GetCurrencyById(DEFAULT_ID);

            //Assert
            Assert.AreEqual(currency, expectedCurrency);

        }

        [TestMethod]
        public void GetCurrencyById_ReturnsNull_IfDoesntExist()
        {

            //Arrange
            Mock<ICurrencyRepository> mockedRepository = new Mock<ICurrencyRepository>();
            CurrencyDto dto = null;
            mockedRepository.Setup(c => c.GetCurrencyById(DEFAULT_ID)).Returns(dto);

            //Act
            ICurrencyService service = testServiceInstance(mockedRepository);
            Currency currency = service.GetCurrencyById(DEFAULT_ID);

            //Assert
            Assert.IsNull(currency);

        }

        [TestMethod]
        public void GetCurrencyById_ReturnsAlwaysTheSameInstance()
        {

            //Arrange
            Mock<ICurrencyRepository> mockedRepository = new Mock<ICurrencyRepository>();
            CurrencyDto dto = new CurrencyDto { Id = DEFAULT_ID, Name = DEFAULT_NAME, Symbol = DEFAULT_SYMBOL };
            mockedRepository.Setup(c => c.GetCurrencyById(DEFAULT_ID)).Returns(dto);

            //Act
            ICurrencyService service = testServiceInstance(mockedRepository);
            Currency currency1 = service.GetCurrencyById(DEFAULT_ID);
            Currency currency2 = service.GetCurrencyById(DEFAULT_ID);

            //Assert
            Assert.AreSame(currency1, currency2);

        }



        [TestMethod]
        public void GetCurrencyByName_ReturnsCurrency_IfExists()
        {

            //Arrange
            Mock<ICurrencyRepository> mockedRepository = new Mock<ICurrencyRepository>();
            CurrencyDto dto = new CurrencyDto { Id = DEFAULT_ID, Name = DEFAULT_NAME, Symbol = DEFAULT_SYMBOL };
            mockedRepository.Setup(c => c.GetCurrencyByName(DEFAULT_NAME)).Returns(dto);
            Currency expectedCurrency = new Currency(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);

            //Act
            ICurrencyService service = testServiceInstance(mockedRepository);
            Currency currency = service.GetCurrencyByName(DEFAULT_NAME);

            //Assert
            Assert.AreEqual(currency, expectedCurrency);

        }

        [TestMethod]
        public void GetCurrencyByName_ReturnsNull_IfDoesntExist()
        {

            //Arrange
            Mock<ICurrencyRepository> mockedRepository = new Mock<ICurrencyRepository>();
            CurrencyDto dto = null;
            mockedRepository.Setup(c => c.GetCurrencyByName(DEFAULT_NAME)).Returns(dto);

            //Act
            ICurrencyService service = testServiceInstance(mockedRepository);
            Currency currency = service.GetCurrencyByName(DEFAULT_NAME);

            //Assert
            Assert.IsNull(currency);

        }

        [TestMethod]
        public void GetCurrencyByName_ReturnsAlwaysTheSameInstance()
        {

            //Arrange
            Mock<ICurrencyRepository> mockedRepository = new Mock<ICurrencyRepository>();
            CurrencyDto dto = new CurrencyDto { Id = DEFAULT_ID, Name = DEFAULT_NAME, Symbol = DEFAULT_SYMBOL };
            mockedRepository.Setup(c => c.GetCurrencyByName(DEFAULT_NAME)).Returns(dto);

            //Act
            ICurrencyService service = testServiceInstance(mockedRepository);
            Currency currency1 = service.GetCurrencyByName(DEFAULT_NAME);
            Currency currency2 = service.GetCurrencyByName(DEFAULT_NAME);

            //Assert
            Assert.AreSame(currency1, currency2);

        }



        [TestMethod]
        public void GetCurrencyBySymbol_ReturnsCurrency_IfExists()
        {

            //Arrange
            Mock<ICurrencyRepository> mockedRepository = new Mock<ICurrencyRepository>();
            CurrencyDto dto = new CurrencyDto { Id = DEFAULT_ID, Name = DEFAULT_NAME, Symbol = DEFAULT_SYMBOL };
            mockedRepository.Setup(c => c.GetCurrencyBySymbol(DEFAULT_SYMBOL)).Returns(dto);
            Currency expectedCurrency = new Currency(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);

            //Act
            ICurrencyService service = testServiceInstance(mockedRepository);
            Currency currency = service.GetCurrencyBySymbol(DEFAULT_SYMBOL);

            //Assert
            Assert.AreEqual(currency, expectedCurrency);

        }

        [TestMethod]
        public void GetCurrencyBySymbol_ReturnsNull_IfDoesntExist()
        {

            //Arrange
            Mock<ICurrencyRepository> mockedRepository = new Mock<ICurrencyRepository>();
            CurrencyDto dto = null;
            mockedRepository.Setup(c => c.GetCurrencyBySymbol(DEFAULT_SYMBOL)).Returns(dto);

            //Act
            ICurrencyService service = testServiceInstance(mockedRepository);
            Currency currency = service.GetCurrencyBySymbol(DEFAULT_SYMBOL);

            //Assert
            Assert.IsNull(currency);

        }

        [TestMethod]
        public void GetCurrencyBySymbol_ReturnsAlwaysTheSameInstance()
        {

            //Arrange
            Mock<ICurrencyRepository> mockedRepository = new Mock<ICurrencyRepository>();
            CurrencyDto dto = new CurrencyDto { Id = DEFAULT_ID, Name = DEFAULT_NAME, Symbol = DEFAULT_SYMBOL };
            mockedRepository.Setup(c => c.GetCurrencyBySymbol(DEFAULT_SYMBOL)).Returns(dto);

            //Act
            ICurrencyService service = testServiceInstance(mockedRepository);
            Currency currency1 = service.GetCurrencyBySymbol(DEFAULT_SYMBOL);
            Currency currency2 = service.GetCurrencyBySymbol(DEFAULT_SYMBOL);

            //Assert
            Assert.AreSame(currency1, currency2);

        }

        [TestMethod]
        public void GetCurrency_ReturnsAlwaysTheSameInstance()
        {

            //Arrange
            Mock<ICurrencyRepository> mockedRepository = new Mock<ICurrencyRepository>();
            CurrencyDto dto = new CurrencyDto { Id = DEFAULT_ID, Name = DEFAULT_NAME, Symbol = DEFAULT_SYMBOL };
            mockedRepository.Setup(r => r.GetCurrencyById(DEFAULT_ID)).Returns(dto);
            mockedRepository.Setup(r => r.GetCurrencyBySymbol(DEFAULT_SYMBOL)).Returns(dto);

            //Act
            ICurrencyService service = testServiceInstance(mockedRepository);
            Currency currency1 = service.GetCurrencyById(DEFAULT_ID);
            Currency currency2 = service.GetCurrencyBySymbol(DEFAULT_SYMBOL);

            //Assert
            Assert.AreSame(currency1, currency2);

        }



        [TestMethod]
        public void GetAllCurrencies_ReturnsProperNumberOfItems()
        {
            //Arrange
            Mock<ICurrencyRepository> mockedRepository = new Mock<ICurrencyRepository>();
            CurrencyDto[] dtos = getCurrencyDtos();
            mockedRepository.Setup(r => r.GetCurrencies()).Returns(dtos);

            //Act
            ICurrencyService service = testServiceInstance(mockedRepository);
            IEnumerable<Currency> currencies = service.GetAllCurrencies();

            //Assert
            Assert.AreEqual(dtos.Length, ((List<Currency>)currencies).Count);

        }

        [TestMethod]
        public void GetAllCurrencies_AlreadyExistingCurrencyInstancesAreUsed()
        {
            //Arrange
            Mock<ICurrencyRepository> mockedRepository = new Mock<ICurrencyRepository>();
            CurrencyDto[] dtos = getCurrencyDtos();
            CurrencyDto dto = dtos[1];
            mockedRepository.Setup(r => r.GetCurrencyById(dto.Id)).Returns(dto);
            mockedRepository.Setup(r => r.GetCurrencies()).Returns(dtos);

            //Act
            ICurrencyService service = testServiceInstance(mockedRepository);
            Currency currency = service.GetCurrencyById(dto.Id);
            IEnumerable<Currency> currencies = service.GetAllCurrencies();

            //Assert
            Currency fromResultCollection = currencies.SingleOrDefault(c => c.Id == dto.Id);
            Assert.AreSame(fromResultCollection, currency);

        }


    }

}