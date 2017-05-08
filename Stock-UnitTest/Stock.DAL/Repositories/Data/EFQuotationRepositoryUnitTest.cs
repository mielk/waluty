using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using Stock.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Helpers;
using Stock.Utils;

namespace Stock_UnitTest.Stock.DAL.Repositories.Data
{
    [TestClass]
    public class EFQuotationRepositoryUnitTest
    {

        private const string UNIT_TEST_DB_NAME = "fx_unittests";
        private const string QUOTATIONS_TABLE_NAME = "quotations";
        //public int Id { get; set; }
        //public DateTime PriceDate { get; set; }
        //public int AssetId { get; set; }
        //public int TimeframeId { get; set; }
        //public double OpenPrice { get; set; }
        //public double HighPrice { get; set; }
        //public double LowPrice { get; set; }
        //public double ClosePrice { get; set; }
        //public double Volume { get; set; }


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

        private void clearQuotationsTable()
        {
            DbContext context = new UnitTestsDbContext();
            context.ClearTable(UNIT_TEST_DB_NAME, QUOTATIONS_TABLE_NAME);
        }

        #endregion INFRASTRUCTURE


        #region UPDATE_QUOTATIONS

        [TestMethod]
        public void UpdateQuotations_WorksProperly_IfItemsAreOnlyAdded()
        {

            //Arrange
            EFQuotationRepository repository = new EFQuotationRepository();
            List<QuotationDto> quotations = new List<QuotationDto>();
            quotations.Add(new QuotationDto() { QuotationId = 2, PriceDate =  new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, OpenPrice =  1.09191, HighPrice =  1.09218, LowPrice =  1.09186, ClosePrice =  1.09194, Volume = 1411, IndexNumber = 2,  });
            quotations.Add(new QuotationDto() { QuotationId = 3, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 3, });

            //Act
            clearQuotationsTable();
            IEnumerable<QuotationDto> recordsBefore = repository.GetQuotations(1, 1);
            repository.UpdateQuotations(quotations);
            IEnumerable<QuotationDto> actualRecords = repository.GetQuotations(1, 1);

            //Assert
            bool areEqual = quotations.HasEqualItems(actualRecords);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void UpdateQuotations_WorksProperly_IfItemsAreOnlyUpdated()
        {

            //Arrange


            //Act

            //Assert
        }

        [TestMethod]
        public void UpdateQuotations_WorksProperly_IfItemsAreAddedAndUpdated()
        {

            //Arrange


            //Act

            //Assert
        }

        #endregion UPDATE_QUOTATIONS

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
