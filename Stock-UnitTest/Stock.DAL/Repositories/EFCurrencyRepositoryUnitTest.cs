using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;

namespace Stock_UnitTest.Stock.DAL.Repositories
{
    [TestClass]
    public class EFCurrencyRepositoryUnitTest
    {

        private const int DEFAULT_ID = 1;
        private const string DEFAULT_NAME = "US Dollar";
        private const string DEFAULT_SYMBOL = "USD";


        [TestCategory("DB")]
        [TestMethod]
        public void GetCurrencyById_returnProperDto_forExistingItem()
        {

            //Arrange
            EFCurrencyRepository repository = new EFCurrencyRepository();

            //Act
            CurrencyDto dto = repository.GetCurrencyById(DEFAULT_ID);

            //Assert
            Assert.AreEqual(dto.Id, DEFAULT_ID);
            Assert.AreEqual(dto.Name, DEFAULT_NAME);
            Assert.AreEqual(dto.Symbol, DEFAULT_SYMBOL);

        }

        [TestCategory("DB")]
        [TestMethod]
        public void GetCurrencyById_returnNull_forNonExistingItem()
        {

            //Arrange
            EFCurrencyRepository repository = new EFCurrencyRepository();

            //Act
            if (repository == null)
            {
                var x = 1;
            }

            CurrencyDto dto = repository.GetCurrencyById(0);

            //Assert
            Assert.IsNull(dto);

        }


    }

}