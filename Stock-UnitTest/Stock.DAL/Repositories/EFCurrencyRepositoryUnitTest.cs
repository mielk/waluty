using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using System.Data.Entity;
using Stock.DAL.Infrastructure;
using System.Collections.Generic;
using Stock.Utils;

namespace Stock_UnitTest.Stock.DAL.Repositories
{
    [TestClass]
    public class EFCurrencyRepositoryUnitTest
    {

        private const string UNIT_TEST_DB_NAME = "fx_unittests";
        private const string CURRENCIES_TABLE_NAME = "currencies";
        private const int DEFAULT_ID = 1;
        private const string DEFAULT_NAME = "US Dollar";
        private const string DEFAULT_SYMBOL = "USD";


        #region TEST_CLASS_INITIALIZATION

        [ClassInitialize()]
        public static void InitTestSuite(TestContext testContext)
        {
            DbContext context = new OriginalDbContext();
            context.Database.ExecuteSqlCommand("recreateDb");
        }

        private void insertCurrencyToTestDb(int id, string name, string fullName)
        {
            const string DELETE_SQL_PATTERN = "DELETE FROM {0}.{1}";
            const string INSERT_SQL_PATTERN = "INSERT INTO {0}.{1}(Id, CurrencySymbol, CurrencyFullName) VALUES({2}, '{3}', '{4}');";
            string insertSql = string.Format(INSERT_SQL_PATTERN, UNIT_TEST_DB_NAME, CURRENCIES_TABLE_NAME, id, name, fullName);
            string deleteSql = string.Format(DELETE_SQL_PATTERN, UNIT_TEST_DB_NAME, CURRENCIES_TABLE_NAME);

            DbContext context = new UnitTestsDbContext();
            try
            {
                context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand(deleteSql);
                context.Database.ExecuteSqlCommand(insertSql);
                context.Database.CurrentTransaction.Commit();
            }
            catch (Exception ex)
            {
                context.Database.CurrentTransaction.Rollback();
            }

        }

        private void insertCurrenciesToTestDb(IEnumerable<CurrencyDto> currencies)
        {
            const string DELETE_SQL_PATTERN = "DELETE FROM {0}.{1}";
            const string INSERT_SQL_PATTERN = "INSERT INTO {0}.{1}(Id, CurrencySymbol, CurrencyFullName) VALUES({2}, '{3}', '{4}');";
            string deleteSql = string.Format(DELETE_SQL_PATTERN, UNIT_TEST_DB_NAME, CURRENCIES_TABLE_NAME);

            DbContext context = new UnitTestsDbContext();
            try
            {
                context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand(deleteSql);
                foreach (var currency in currencies)
                {
                    string insertSql = string.Format(INSERT_SQL_PATTERN, UNIT_TEST_DB_NAME, CURRENCIES_TABLE_NAME, currency.Id, currency.Symbol, currency.Name);
                    context.Database.ExecuteSqlCommand(insertSql);
                }
                context.Database.CurrentTransaction.Commit();
            }
            catch (Exception ex)
            {
                context.Database.CurrentTransaction.Rollback();
            }

        }

        #endregion TEST_CLASS_INITIALIZATION


        #region GET_CURRENCIES

        [TestMethod]
        public void GetCurrencies_returnProperDtoCollection()
        {

            //Arrange
            EFCurrencyRepository repository = new EFCurrencyRepository();
            CurrencyDto usd = new CurrencyDto() { Id = 1, Name = "US Dollar", Symbol = "USD" };
            CurrencyDto eur = new CurrencyDto() { Id = 2, Name = "Euro", Symbol = "EUR" };
            CurrencyDto gbp = new CurrencyDto() { Id = 3, Name = "British Pound", Symbol = "GBP" };
            CurrencyDto jpy = new CurrencyDto() { Id = 4, Name = "Japanese Yen", Symbol = "JPY" };
            insertCurrenciesToTestDb(new CurrencyDto[] { usd, eur, gbp, jpy });

            //Act
            CurrencyDto[] dtos = repository.GetCurrencies().ToArray();

            //Assert
            Assert.AreEqual(4, dtos.Count());
            Assert.AreEqual(usd, dtos[0]);
            Assert.AreEqual(eur, dtos[1]);
            Assert.AreEqual(gbp, dtos[2]);
            Assert.AreEqual(jpy, dtos[3]);

        }

        #endregion GET_CURRENCIES


        #region GET_CURRENCY_BY_ID

        [TestMethod]
        public void GetCurrencyById_returnProperDto_forExistingItem()
        {

            //Arrange
            EFCurrencyRepository repository = new EFCurrencyRepository();
            insertCurrencyToTestDb(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);

            //Act
            CurrencyDto dto = repository.GetCurrencyById(DEFAULT_ID);

            //Assert
            Assert.AreEqual(dto.Id, DEFAULT_ID);
            Assert.AreEqual(dto.Name, DEFAULT_NAME);
            Assert.AreEqual(dto.Symbol, DEFAULT_SYMBOL);

        }

        [TestMethod]
        public void GetCurrencyById_returnNull_forNonExistingItem()
        {

            //Arrange
            EFCurrencyRepository repository = new EFCurrencyRepository();
            insertCurrencyToTestDb(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);

            //Act
            CurrencyDto dto = repository.GetCurrencyById(2);

            //Assert
            Assert.IsNull(dto);

        }

        #endregion GET_CURRENCY_BY_ID


        #region GET_CURRENCY_BY_NAME

        [TestMethod]
        public void GetCurrencyByName_returnProperDto_forExistingItem()
        {

            //Arrange
            EFCurrencyRepository repository = new EFCurrencyRepository();
            insertCurrencyToTestDb(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);

            //Act
            CurrencyDto dto = repository.GetCurrencyByName(DEFAULT_NAME);

            //Assert
            Assert.AreEqual(dto.Id, DEFAULT_ID);
            Assert.AreEqual(dto.Name, DEFAULT_NAME);
            Assert.AreEqual(dto.Symbol, DEFAULT_SYMBOL);

        }

        [TestMethod]
        public void GetCurrencyByName_returnNull_forNonExistingItem()
        {

            //Arrange
            EFCurrencyRepository repository = new EFCurrencyRepository();
            insertCurrencyToTestDb(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);

            //Act
            CurrencyDto dto = repository.GetCurrencyByName("Euro");

            //Assert
            Assert.IsNull(dto);

        }

        #endregion GET_CURRENCY_BY_NAME


        #region GET_CURRENCY_BY_SYMBOL

        [TestMethod]
        public void GetCurrencyBySymbol_returnProperDto_forExistingItem()
        {

            //Arrange
            EFCurrencyRepository repository = new EFCurrencyRepository();
            insertCurrencyToTestDb(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);

            //Act
            CurrencyDto dto = repository.GetCurrencyBySymbol(DEFAULT_SYMBOL);

            //Assert
            Assert.AreEqual(dto.Id, DEFAULT_ID);
            Assert.AreEqual(dto.Name, DEFAULT_NAME);
            Assert.AreEqual(dto.Symbol, DEFAULT_SYMBOL);

        }

        [TestMethod]
        public void GetCurrencyBySymbol_returnNull_forNonExistingItem()
        {

            //Arrange
            EFCurrencyRepository repository = new EFCurrencyRepository();
            insertCurrencyToTestDb(DEFAULT_ID, DEFAULT_SYMBOL, DEFAULT_NAME);

            //Act
            CurrencyDto dto = repository.GetCurrencyBySymbol("EUR");

            //Assert
            Assert.IsNull(dto);

        }

        #endregion GET_CURRENCY_BY_SYMBOL


        #region TEST_CLASS_TERMINATION

        [ClassCleanup()]
        public static void CleanupTestSuite()
        {
            DbContext context = new OriginalDbContext();
            context.Database.ExecuteSqlCommand("DROP DATABASE fx_unittests");
        }

        #endregion TEST_CLASS_TERMINATION

    }

}