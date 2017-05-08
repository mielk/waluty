using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using Stock.DAL.Infrastructure;
using Stock.DAL.Helpers;
using Stock.Utils;


namespace Stock_UnitTest.Stock.DAL.Repositories
{
    [TestClass]
    public class EFAnalysisRepositoryUnitTest
    {

        private const string UNIT_TEST_DB_NAME = "fx_unittests";
        private const string ANALYSIS_TYPES_TABLE_NAME = "analysis_types";
        private const int DEFAULT_ID = 1;
        private const string DEFAULT_NAME = "quotations";


        #region INFRASTRUCTURE

        [ClassInitialize()]
        public static void InitTestSuite(TestContext testContext)
        {
            DbContext context = new OriginalDbContext();
            context.Database.ExecuteSqlCommand("recreateDb");
        }

        [ClassCleanup()]
        public static void CleanupTestSuite()
        {
            DbContext context = new OriginalDbContext();
            context.Database.ExecuteSqlCommand("DROP DATABASE fx_unittests");
        }



        private AnalysisTypeDto getDefaultAnalysisTypeDto()
        {
            return new AnalysisTypeDto()
            {
                Id = DEFAULT_ID,
                Name = DEFAULT_NAME
            };
        }

        private IEnumerable<AnalysisTypeDto> getDefaultAnalysisTypeDtos()
        {
            List<AnalysisTypeDto> list = new List<AnalysisTypeDto>();
            list.Add(new AnalysisTypeDto() { Id = 1, Name = "quotations" });
            list.Add(new AnalysisTypeDto() { Id = 2, Name = "prices" });
            list.Add(new AnalysisTypeDto() { Id = 3, Name = "macd" });
            list.Add(new AnalysisTypeDto() { Id = 4, Name = "adx" });
            list.Add(new AnalysisTypeDto() { Id = 5, Name = "trendlines" });
            list.Add(new AnalysisTypeDto() { Id = 6, Name = "candlesticks" });
            return list;
        }

        private void insertAnalysisTypeToTestDb(AnalysisTypeDto dto)
        {
            const string INSERT_SQL_PATTERN = "INSERT INTO {0}.{1}(AnalysisTypeId, AnalysisTypeName) VALUES({2}, {3});";
            string insertSql = string.Format(INSERT_SQL_PATTERN, UNIT_TEST_DB_NAME, ANALYSIS_TYPES_TABLE_NAME, dto.Id, dto.Name.ToDbString());

            DbContext context = new UnitTestsDbContext();
            try
            {
                context.Database.BeginTransaction();
                context.ClearTable(UNIT_TEST_DB_NAME, ANALYSIS_TYPES_TABLE_NAME);
                context.Database.ExecuteSqlCommand(insertSql);
                context.Database.CurrentTransaction.Commit();
            }
            catch (Exception ex)
            {
                context.Database.CurrentTransaction.Rollback();
            }

        }

        private void insertAnalysisTypesToTestDb(IEnumerable<AnalysisTypeDto> analysisTypes)
        {
            const string INSERT_SQL_PATTERN = "INSERT INTO {0}.{1}(AnalysisTypeId, AnalysisTypeName) VALUES({2}, {3});";

            DbContext context = new UnitTestsDbContext();
            try
            {
                context.Database.BeginTransaction();
                context.ClearTable(UNIT_TEST_DB_NAME, ANALYSIS_TYPES_TABLE_NAME);
                foreach (var analysisType in analysisTypes)
                {
                    string insertSql = string.Format(INSERT_SQL_PATTERN, UNIT_TEST_DB_NAME, ANALYSIS_TYPES_TABLE_NAME, analysisType.Id, analysisType.Name.ToDbString());
                    context.Database.ExecuteSqlCommand(insertSql);
                }
                context.Database.CurrentTransaction.Commit();
            }
            catch (Exception ex)
            {
                context.Database.CurrentTransaction.Rollback();
            }

        }

        #endregion INFRASTRUCTURE


        #region GET_ANALYSIS_TYPES

        [TestMethod]
        public void GetAllAnalysisTypes_returnProperDtoCollection()
        {

            //Arrange
            EFAnalysisRepository repository = new EFAnalysisRepository();
            IEnumerable<AnalysisTypeDto> analysisTypeDtos = getDefaultAnalysisTypeDtos();
            insertAnalysisTypesToTestDb(analysisTypeDtos);

            //Act
            AnalysisTypeDto[] dtos = repository.GetAnalysisTypes().ToArray();

            //Assert
            bool areEqualArrays = analysisTypeDtos.HasEqualItems(dtos);
            Assert.IsTrue(areEqualArrays);

        }

        #endregion GET_ANALYSIS_TYPES


        #region GET_ANALYSIS_TYPE_BY_ID

        [TestMethod]
        public void GetAnalysisTypeById_returnProperDto_forExistingItem()
        {

            //Arrange
            EFAnalysisRepository repository = new EFAnalysisRepository();
            AnalysisTypeDto baseDto = getDefaultAnalysisTypeDto();
            insertAnalysisTypeToTestDb(baseDto);

            //Act
            AnalysisTypeDto dto = repository.GetAnalysisTypeById(baseDto.Id);

            //Assert
            var areEqual = baseDto.Equals(dto);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetAnalysisTypeById_returnNull_forNonExistingItem()
        {

            //Arrange
            EFAnalysisRepository repository = new EFAnalysisRepository();
            AnalysisTypeDto baseDto = getDefaultAnalysisTypeDto();
            insertAnalysisTypeToTestDb(baseDto);

            //Act
            AnalysisTypeDto dto = repository.GetAnalysisTypeById(baseDto.Id + 1);

            //Assert
            Assert.IsNull(dto);

        }

        #endregion GET_ANALYSIS_TYPE_BY_ID


        #region GET_ANALYSIS_TYPE_BY_SYMBOL

        [TestMethod]
        public void GetAnalysisTypeBySymbol_returnProperDto_forExistingItem()
        {

            //Arrange
            EFAnalysisRepository repository = new EFAnalysisRepository();
            AnalysisTypeDto baseDto = getDefaultAnalysisTypeDto();
            insertAnalysisTypeToTestDb(baseDto);

            //Act
            AnalysisTypeDto dto = repository.GetAnalysisTypeByName(baseDto.Name);

            //Assert
            var areEqual = baseDto.Equals(dto);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetAnalysisTypeBySymbol_returnNull_forNonExistingItem()
        {

            //Arrange
            EFAnalysisRepository repository = new EFAnalysisRepository();
            AnalysisTypeDto baseDto = getDefaultAnalysisTypeDto();
            insertAnalysisTypeToTestDb(baseDto);

            //Act
            AnalysisTypeDto dto = repository.GetAnalysisTypeByName(baseDto.Name + "a");

            //Assert
            Assert.IsNull(dto);

        }

        #endregion GET_ANALYSIS_TYPE_BY_SYMBOL



    }

}