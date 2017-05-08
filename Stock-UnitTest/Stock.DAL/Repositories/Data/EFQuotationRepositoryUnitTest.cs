using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_UnitTest.Stock.DAL.Repositories.Data
{
    public class EFQuotationRepositoryUnitTest
    {

        private const string UNIT_TEST_DB_NAME = "fx_unittests";
        private const string QUOTATIONS_TABLE_NAME = "quotations";
        //private const int DEFAULT_ID = 1;
        //private const string DEFAULT_NAME = "quotations";


        //#region INFRASTRUCTURE

        //[ClassInitialize()]
        //public static void InitTestSuite(TestContext testContext)
        //{
        //    DbContext context = new OriginalDbContext();
        //    context.Database.ExecuteSqlCommand("recreateDb");
        //}

        //[ClassCleanup()]
        //public static void CleanupTestSuite()
        //{
        //    DbContext context = new OriginalDbContext();
        //    context.Database.ExecuteSqlCommand("DROP DATABASE fx_unittests");
        //}



        //private QuotationDto getDefaultQuotationDto()
        //{
        //    return new QuotationDto()
        //    {
        //        Id = DEFAULT_ID,
        //        Name = DEFAULT_NAME
        //    };
        //}

        //private IEnumerable<QuotationDto> getDefaultQuotationDtos()
        //{
        //    List<QuotationDto> list = new List<QuotationDto>();
        //    list.Add(new QuotationDto() { Id = 1, Name = "quotations" });
        //    list.Add(new QuotationDto() { Id = 2, Name = "prices" });
        //    list.Add(new QuotationDto() { Id = 3, Name = "macd" });
        //    list.Add(new QuotationDto() { Id = 4, Name = "adx" });
        //    list.Add(new QuotationDto() { Id = 5, Name = "trendlines" });
        //    list.Add(new QuotationDto() { Id = 6, Name = "candlesticks" });
        //    return list;
        //}

        //private void insertQuotationToTestDb(QuotationDto dto)
        //{
        //    const string INSERT_SQL_PATTERN = "INSERT INTO {0}.{1}(QuotationId, QuotationName) VALUES({2}, {3});";
        //    string insertSql = string.Format(INSERT_SQL_PATTERN, UNIT_TEST_DB_NAME, QUOTATIONS_TABLE_NAME, dto.Id, dto.Name.ToDbString());

        //    DbContext context = new UnitTestsDbContext();
        //    try
        //    {
        //        context.Database.BeginTransaction();
        //        context.ClearTable(UNIT_TEST_DB_NAME, QUOTATIONS_TABLE_NAME);
        //        context.Database.ExecuteSqlCommand(insertSql);
        //        context.Database.CurrentTransaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        context.Database.CurrentTransaction.Rollback();
        //    }

        //}

        //private void insertQuotationsToTestDb(IEnumerable<QuotationDto> quotations)
        //{
        //    const string INSERT_SQL_PATTERN = "INSERT INTO {0}.{1}(QuotationId, QuotationName) VALUES({2}, {3});";

        //    DbContext context = new UnitTestsDbContext();
        //    try
        //    {
        //        context.Database.BeginTransaction();
        //        context.ClearTable(UNIT_TEST_DB_NAME, QUOTATIONS_TABLE_NAME);
        //        foreach (var quotation in quotations)
        //        {
        //            string insertSql = string.Format(INSERT_SQL_PATTERN, UNIT_TEST_DB_NAME, QUOTATIONS_TABLE_NAME, quotation.Id, quotation.Name.ToDbString());
        //            context.Database.ExecuteSqlCommand(insertSql);
        //        }
        //        context.Database.CurrentTransaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        context.Database.CurrentTransaction.Rollback();
        //    }

        //}

        //#endregion INFRASTRUCTURE


        //#region GET_QUOTATIONS

        //[TestMethod]
        //public void GetAllQuotations_returnProperDtoCollection()
        //{

        //    //Arrange
        //    EFAnalysisRepository repository = new EFAnalysisRepository();
        //    IEnumerable<QuotationDto> quotationDtos = getDefaultQuotationDtos();
        //    insertQuotationsToTestDb(quotationDtos);

        //    //Act
        //    QuotationDto[] dtos = repository.GetQuotations().ToArray();

        //    //Assert
        //    bool areEqualArrays = quotationDtos.HasEqualItems(dtos);
        //    Assert.IsTrue(areEqualArrays);

        //}

        //#endregion GET_QUOTATIONS


        //#region GET_QUOTATION_BY_ID

        //[TestMethod]
        //public void GetQuotationById_returnProperDto_forExistingItem()
        //{

        //    //Arrange
        //    EFAnalysisRepository repository = new EFAnalysisRepository();
        //    QuotationDto baseDto = getDefaultQuotationDto();
        //    insertQuotationToTestDb(baseDto);

        //    //Act
        //    QuotationDto dto = repository.GetQuotationById(baseDto.Id);

        //    //Assert
        //    var areEqual = baseDto.Equals(dto);
        //    Assert.IsTrue(areEqual);

        //}

        //[TestMethod]
        //public void GetQuotationById_returnNull_forNonExistingItem()
        //{

        //    //Arrange
        //    EFAnalysisRepository repository = new EFAnalysisRepository();
        //    QuotationDto baseDto = getDefaultQuotationDto();
        //    insertQuotationToTestDb(baseDto);

        //    //Act
        //    QuotationDto dto = repository.GetQuotationById(baseDto.Id + 1);

        //    //Assert
        //    Assert.IsNull(dto);

        //}

        //#endregion GET_QUOTATION_BY_ID


        //#region GET_QUOTATION_BY_SYMBOL

        //[TestMethod]
        //public void GetQuotationBySymbol_returnProperDto_forExistingItem()
        //{

        //    //Arrange
        //    EFAnalysisRepository repository = new EFAnalysisRepository();
        //    QuotationDto baseDto = getDefaultQuotationDto();
        //    insertQuotationToTestDb(baseDto);

        //    //Act
        //    QuotationDto dto = repository.GetQuotationByName(baseDto.Name);

        //    //Assert
        //    var areEqual = baseDto.Equals(dto);
        //    Assert.IsTrue(areEqual);

        //}

        //[TestMethod]
        //public void GetQuotationBySymbol_returnNull_forNonExistingItem()
        //{

        //    //Arrange
        //    EFAnalysisRepository repository = new EFAnalysisRepository();
        //    QuotationDto baseDto = getDefaultQuotationDto();
        //    insertQuotationToTestDb(baseDto);

        //    //Act
        //    QuotationDto dto = repository.GetQuotationByName(baseDto.Name + "a");

        //    //Assert
        //    Assert.IsNull(dto);

        //}

        //#endregion GET_QUOTATION_BY_SYMBOL




        //GetQuotationsWithAssetAndTimeframe_ReturnsEmptyContainer_IfThereIsNoDataForSuchCombination
        //GetQuotationsWithAssetAndTimeframe_ReturnsAllDataAvailableForGivenCombinationOfAssetAndTimeframe
        //GetQuotationsWithQueryDef_ReturnsAllDataAvailableForGivenCombinationOfAssetAndTimeframe
        //GetQuotationsWithQueryDef_ReturnsEmptyContainer_IfThereIsNoDataForSuchCombinationOfAssetAndTimeframe
        //GetQuotationsWithQueryDef_ReturnsDataFromGivenStartDateOnly
        //GetQuotationsWithQueryDef_ReturnsDataToGivenEndDateOnly
        //GetQuotationsWithQueryDef_ReturnsDataFromGivenStartDateAndToGivenEndDateOnly
        //GetQuotationsWithQueryDef_ReturnsEmptyContainer_IfThereIsNoDataInGivenDateRange
        //GetQuotationsWithQueryDef_ReturnsEmptyContainer_ReturnsOnlyLimitedNumberOfQuotations_EvenIfThereIsMoreQuotationsInDateRange


    }
}
