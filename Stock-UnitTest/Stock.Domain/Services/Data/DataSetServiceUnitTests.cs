using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Stock.Domain.Entities;
using Stock.Domain.Services;
using Stock.Domain.Enums;
using Stock.Utils;
using Stock.Core;
using Stock_UnitTest.Helpers;

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class DataSetServiceUnitTests
    {

        private UTFactory utf = new UTFactory();


        #region GET_DATA_SETS

        [TestMethod]
        public void GetDataSets_ReturnsEmptyCollection_IfThereIsNoQuotationForGivenDateRange()
        {

            //Arrange
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Quotations, AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(UTDefaulter.DEFAULT_ASSET_ID, UTDefaulter.DEFAULT_TIMEFRAME_ID) { AnalysisTypes = analysisTypes };
            Mock<IQuotationRepository> quotationRepository = new Mock<IQuotationRepository>();
            Mock<IPriceRepository> priceRepository = new Mock<IPriceRepository>();
            IEnumerable<PriceDto> priceDtos = utf.getDefaultPriceDtosCollection();
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(new List<QuotationDto>());
            priceRepository.Setup(q => q.GetPrices(queryDef)).Returns(priceDtos);
            
            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationRepository(quotationRepository.Object);
            service.InjectPriceRepository(priceRepository.Object);
            var dataSets = service.GetDataSets(queryDef);

            //Assert
            var isEmpty = (dataSets.Count() == 0);
            Assert.IsTrue(isEmpty);

        }

        [TestMethod]
        public void GetDataSets_ReturnsCollectionWithQuotationsOnly_IfGivenAnalysisTypesParameterIsInvalid()
        {

            //Arrange:QueryDef
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(UTDefaulter.DEFAULT_ASSET_ID, UTDefaulter.DEFAULT_TIMEFRAME_ID) { AnalysisTypes = analysisTypes };

            //Arrange:Quotations
            Mock<IQuotationRepository> quotationRepository = new Mock<IQuotationRepository>();
            IEnumerable<QuotationDto> quotationDtos = utf.getDefaultQuotationDtosCollection();
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(quotationDtos);

            //Arrange:Prices
            Mock<IPriceRepository> priceRepository = new Mock<IPriceRepository>();
            IEnumerable<PriceDto> priceDtos = utf.getDefaultPriceDtosCollection();
            priceRepository.Setup(p => p.GetPrices(queryDef)).Returns(priceDtos);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationRepository(quotationRepository.Object);
            service.InjectPriceRepository(priceRepository.Object);
            var dataSets = service.GetDataSets(queryDef);

            //Assert
            DataSet ds1 = utf.getDataSet(1);
            ds1.SetQuotation(utf.getQuotation(ds1));
            DataSet ds2 = utf.getDataSet(2);
            ds2.SetQuotation(utf.getQuotation(ds2));
            DataSet ds3 = utf.getDataSet(3);
            ds3.SetQuotation(utf.getQuotation(ds3));
            DataSet ds4 = utf.getDataSet(4);
            ds4.SetQuotation(utf.getQuotation(ds4));

            IEnumerable<DataSet> expectedDataSets = new DataSet[] { ds1, ds2, ds3, ds4 };
            var areEqual = expectedDataSets.HasEqualItems(dataSets);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetDataSets_ReturnsCollectionWithAllGivenAnalysisTypes()
        {

            //Arrange:QueryDef
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(UTDefaulter.DEFAULT_ASSET_ID, UTDefaulter.DEFAULT_TIMEFRAME_ID) { AnalysisTypes = analysisTypes };
            
            //Arrange:Quotations
            Mock<IQuotationRepository> quotationRepository = new Mock<IQuotationRepository>();
            IEnumerable<QuotationDto> quotationDtos = utf.getDefaultQuotationDtosCollection();
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(quotationDtos);

            //Arrange:Prices
            Mock<IPriceRepository> priceRepository = new Mock<IPriceRepository>();
            IEnumerable<PriceDto> priceDtos = new PriceDto[] { utf.getPriceDto(1), utf.getPriceDto(2), utf.getPriceDto(3), utf.getPriceDto(4) }; 
            priceRepository.Setup(p => p.GetPrices(queryDef)).Returns(priceDtos);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationRepository(quotationRepository.Object);
            service.InjectPriceRepository(priceRepository.Object);
            var dataSets = service.GetDataSets(queryDef);

            //Assert
            DataSet ds1 = utf.getDataSet(1);
            ds1.SetQuotation(utf.getQuotation(ds1)).SetPrice(utf.getPrice(ds1));
            DataSet ds2 = utf.getDataSet(2);
            ds2.SetQuotation(utf.getQuotation(ds2)).SetPrice(utf.getPrice(ds2));
            DataSet ds3 = utf.getDataSet(3);
            ds3.SetQuotation(utf.getQuotation(ds3)).SetPrice(utf.getPrice(ds3));
            DataSet ds4 = utf.getDataSet(4);
            ds4.SetQuotation(utf.getQuotation(ds4)).SetPrice(utf.getPrice(ds4));
            IEnumerable<DataSet> expectedDataSets = new DataSet[] { ds1, ds2, ds3, ds4 };
            var areEqual = expectedDataSets.HasEqualItems(dataSets);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetDataSets_ReturnsCollectionWithoutSomeAnalysisType_IfThereIsNoDataForThisType()
        {

            //Arrange:QueryDef
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(UTDefaulter.DEFAULT_ASSET_ID, UTDefaulter.DEFAULT_TIMEFRAME_ID) { AnalysisTypes = analysisTypes };

            //Arrange:Quotations
            Mock<IQuotationRepository> quotationRepository = new Mock<IQuotationRepository>();
            IEnumerable<QuotationDto> quotationDtos = new QuotationDto[] { utf.getQuotationDto(2), utf.getQuotationDto(3), utf.getQuotationDto(4) };
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(quotationDtos);

            //Arrange:Prices
            Mock<IPriceRepository> priceRepository = new Mock<IPriceRepository>();
            IEnumerable<PriceDto> priceDtos = new PriceDto[] { utf.getPriceDto(3), utf.getPriceDto(4), utf.getPriceDto(5), utf.getPriceDto(6) }; 
            priceRepository.Setup(p => p.GetPrices(queryDef)).Returns(priceDtos);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationRepository(quotationRepository.Object);
            service.InjectPriceRepository(priceRepository.Object);
            var dataSets = service.GetDataSets(queryDef);

            //Assert
            DataSet ds2 = utf.getDataSet(2);
            ds2.SetQuotation(utf.getQuotation(ds2));
            DataSet ds3 = utf.getDataSet(3);
            ds3.SetQuotation(utf.getQuotation(ds3)).SetPrice(utf.getPrice(ds3));
            DataSet ds4 = utf.getDataSet(4);
            ds4.SetQuotation(utf.getQuotation(ds4)).SetPrice(utf.getPrice(ds4));
            IEnumerable<DataSet> expectedDataSets = new DataSet[] { ds2, ds3, ds4 };
            var areEqual = expectedDataSets.HasEqualItems(dataSets);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetDataSets_ReturnsCollectionWithExistingItems()
        {

            //Arrange:QueryDef
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices };
            DateTime startDate = new DateTime(2016, 1, 15, 22, 30, 0);
            DateTime endDate = new DateTime(2016, 1, 15, 22, 50, 0);
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1) { AnalysisTypes = analysisTypes, StartDate = startDate, EndDate = endDate };

            //Arrange:Quotations
            Mock<IQuotationRepository> quotationRepository = new Mock<IQuotationRepository>();
            IEnumerable<QuotationDto> quotationDtos = new QuotationDto[] { utf.getQuotationDto(1), utf.getQuotationDto(2), utf.getQuotationDto(3), utf.getQuotationDto(4) };
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(quotationDtos);

            //Arrange:Prices
            Mock<IPriceRepository> priceRepository = new Mock<IPriceRepository>();
            IEnumerable<PriceDto> priceDtos = new PriceDto[] { utf.getPriceDto(1), utf.getPriceDto(2), utf.getPriceDto(3), utf.getPriceDto(4) };
            priceRepository.Setup(p => p.GetPrices(queryDef)).Returns(priceDtos);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationRepository(quotationRepository.Object);
            service.InjectPriceRepository(priceRepository.Object);
            var dataSets = service.GetDataSets(queryDef);
            DataSet baseDataSet = dataSets.SingleOrDefault(ds => ds.IndexNumber == 1);

            queryDef.StartDate = new DateTime(2016, 1, 15, 22, 25, 0);
            dataSets = service.GetDataSets(queryDef);
            DataSet comparedDataSet = dataSets.SingleOrDefault(ds => ds.IndexNumber == 1);

            //Assert
            var areTheSameObject = (baseDataSet == comparedDataSet);
            Assert.IsTrue(areTheSameObject);

        }

        [TestMethod]
        public void GetDataSets_UseAlwaysExistingInstancesOfObjects_EvenIfThereIsAnotherOneInRepository()
        {

            //Arrange:QueryDef
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1) { AnalysisTypes = analysisTypes };

            //Arrange:Quotations
            Mock<IQuotationRepository> quotationRepository = new Mock<IQuotationRepository>();
            IEnumerable<QuotationDto> quotationDtos = new QuotationDto[] { utf.getQuotationDto(1), utf.getQuotationDto(2), utf.getQuotationDto(3), utf.getQuotationDto(4) };
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(quotationDtos);

            //Arrange:Prices
            Mock<IPriceRepository> priceRepository = new Mock<IPriceRepository>();
            IEnumerable<PriceDto> priceDtos = new PriceDto[] { utf.getPriceDto(1), utf.getPriceDto(2), utf.getPriceDto(3), utf.getPriceDto(4) };
            priceRepository.Setup(p => p.GetPrices(queryDef)).Returns(priceDtos);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationRepository(quotationRepository.Object);
            service.InjectPriceRepository(priceRepository.Object);
            var dataSets = service.GetDataSets(queryDef);
            DataSet baseDataSet = dataSets.SingleOrDefault(ds => ds.IndexNumber == 2);

            Quotation stubQuotation = utf.getQuotation(2);
            stubQuotation.Open = stubQuotation.Open + 3;
            IEnumerable<Quotation> quotations = new Quotation[] { utf.getQuotation(1), stubQuotation, utf.getQuotation(3), utf.getQuotation(4) };
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(quotationDtos);

            service.InjectQuotationRepository(quotationRepository.Object);
            dataSets = service.GetDataSets(queryDef);
            DataSet comparedDataSet = dataSets.SingleOrDefault(ds => ds.IndexNumber == 2);

            //Assert
            var areTheSameObject = (baseDataSet.GetQuotation() == comparedDataSet.GetQuotation());
            Assert.IsTrue(areTheSameObject);

        }

        [TestMethod]
        public void GetDataSets_DoesntOverrideExistingObjectsWithNulls_IfTheyAreNotInSpecificRepositories()
        {

            //Arrange:QueryDef
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(UTDefaulter.DEFAULT_ASSET_ID, UTDefaulter.DEFAULT_TIMEFRAME_ID) { AnalysisTypes = analysisTypes };

            //Arrange:Quotations
            Mock<IQuotationRepository> quotationRepository = new Mock<IQuotationRepository>();
            IEnumerable<QuotationDto> quotationDtos = new QuotationDto[] { utf.getQuotationDto(1), utf.getQuotationDto(2), utf.getQuotationDto(3), utf.getQuotationDto(4) };
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(quotationDtos);

            //Arrange:Prices
            Mock<IPriceRepository> priceRepository = new Mock<IPriceRepository>();
            IEnumerable<PriceDto> priceDtos = new PriceDto[] { utf.getPriceDto(1), utf.getPriceDto(2), utf.getPriceDto(3), utf.getPriceDto(4) };
            priceRepository.Setup(p => p.GetPrices(queryDef)).Returns(priceDtos);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationRepository(quotationRepository.Object);
            service.InjectPriceRepository(priceRepository.Object);
            var dataSets = service.GetDataSets(queryDef);
            if (dataSets == null) throw new Exception("Collection should not be null");

            DataSet baseDataSet = dataSets.SingleOrDefault(ds => ds.IndexNumber == 2);
            if (baseDataSet == null) throw new Exception("Base data set shouldn't be null");

            Price basePrice = (baseDataSet == null ? null : baseDataSet.GetPrice());
            if (basePrice == null) throw new Exception("Base price shouldn't be null");
            

            priceRepository.Setup(q => q.GetPrices(queryDef)).Returns(new PriceDto[] { });
            service.InjectPriceRepository(priceRepository.Object);
            dataSets = service.GetDataSets(queryDef);
            DataSet comparedDataSet = dataSets.SingleOrDefault(ds => ds.IndexNumber == 2);
            Price comparedPrice = (comparedDataSet == null ? null : comparedDataSet.GetPrice());

            //Assert
            var areTheSameObject = (basePrice == comparedPrice);
            Assert.IsTrue(areTheSameObject);

        }

        #endregion GET_DATA_SETS



        #region GET_DATA_SETS_WITH_INITIAL_COLLECTION

        [TestMethod]
        public void GetDataSetsWithInitialCollection_ReturnsNullArray_IfNullArrayWasPassedAndThereWereNoDataToBeAppended()
        {

            //Arrange
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Quotations, AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(UTDefaulter.DEFAULT_ASSET_ID, UTDefaulter.DEFAULT_TIMEFRAME_ID) { AnalysisTypes = analysisTypes };
            Mock<IQuotationRepository> quotationRepository = new Mock<IQuotationRepository>();
            Mock<IPriceRepository> priceRepository = new Mock<IPriceRepository>();
            IEnumerable<PriceDto> priceDtos = utf.getDefaultPriceDtosCollection();
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(new List<QuotationDto>());
            priceRepository.Setup(p => p.GetPrices(queryDef)).Returns(priceDtos);

            //Act
            DataSet[] previous = new DataSet[] { };
            IDataSetService service = new DataSetService();
            service.InjectQuotationRepository(quotationRepository.Object);
            service.InjectPriceRepository(priceRepository.Object);
            var dataSets = service.GetDataSets(queryDef, previous).ToArray();

            //Assert
            var isEmpty = (dataSets.Length == 0 || (dataSets.Length == 1 && dataSets[0] == null));
            Assert.IsTrue(isEmpty);

        }

        [TestMethod]
        public void GetDataSetsWithInitialCollection_ReturnsArrayWithoutChanges_IfNonEmptyArrayWasPassedAndThereWereNoDataToBeAppended()
        {

            //Arrange
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Quotations, AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(UTDefaulter.DEFAULT_ASSET_ID, UTDefaulter.DEFAULT_TIMEFRAME_ID) { AnalysisTypes = analysisTypes };
            Mock<IQuotationRepository> quotationRepository = new Mock<IQuotationRepository>();
            Mock<IPriceRepository> priceRepository = new Mock<IPriceRepository>();
            IEnumerable<PriceDto> priceDtos = utf.getDefaultPriceDtosCollection();
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(new List<QuotationDto>());
            priceRepository.Setup(p => p.GetPrices(queryDef)).Returns(priceDtos);

            //Act
            DataSet[] baseDataSets = new DataSet[11];
            baseDataSets[7] = utf.getDataSet(7);
            baseDataSets[8] = utf.getDataSet(8);
            baseDataSets[9] = utf.getDataSet(9);
            baseDataSets[10] = utf.getDataSet(10);
            IDataSetService service = new DataSetService();
            service.InjectQuotationRepository(quotationRepository.Object);
            service.InjectPriceRepository(priceRepository.Object);
            var dataSets = service.GetDataSets(queryDef, baseDataSets).ToArray();

            //Assert
            var areEqual = baseDataSets.HasEqualItemsInTheSameOrder(dataSets);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetDataSetsWithInitialCollection_ReturnsProperlyFilledAndIndexedArray_IfNullArrayWasPassedAndSomeDataWereAppended()
        {

            //Arrange
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Quotations, AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(UTDefaulter.DEFAULT_ASSET_ID, UTDefaulter.DEFAULT_TIMEFRAME_ID) { AnalysisTypes = analysisTypes };
            Mock<IQuotationRepository> quotationRepository = new Mock<IQuotationRepository>();
            Mock<IPriceRepository> priceRepository = new Mock<IPriceRepository>();

            //Act
            IDataSetService service = new DataSetService();
            IEnumerable<QuotationDto> quotationDtos = new QuotationDto[] { utf.getQuotationDto(101), utf.getQuotationDto(102), utf.getQuotationDto(103) };
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(quotationDtos);
            IEnumerable<PriceDto> priceDtos = new PriceDto[] { utf.getPriceDto(101), utf.getPriceDto(102), utf.getPriceDto(103) };
            priceRepository.Setup(q => q.GetPrices(queryDef)).Returns(priceDtos);
            service.InjectQuotationRepository(quotationRepository.Object);
            service.InjectPriceRepository(priceRepository.Object);
            DataSet[] result = service.GetDataSets(queryDef, new List<DataSet>()).ToArray();

            //Assert
            DataSet[] expectedDataSets = new DataSet[104];
            DataSet ds101 = utf.getDataSet(101);
            ds101.SetQuotation(utf.getQuotation(ds101)).SetPrice(utf.getPrice(ds101));
            DataSet ds102 = utf.getDataSet(102);
            ds102.SetQuotation(utf.getQuotation(ds102)).SetPrice(utf.getPrice(ds102));
            DataSet ds103 = utf.getDataSet(103);
            ds103.SetQuotation(utf.getQuotation(ds103)).SetPrice(utf.getPrice(ds103));
            expectedDataSets[101] = ds101;
            expectedDataSets[102] = ds102;
            expectedDataSets[103] = ds103;
            var areEqual = expectedDataSets.HasEqualItemsInTheSameOrder(result);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetDataSetsWithInitialCollection_ReturnsProperlyFilledArray_IfNonEmptyArrayWasPassedAndSomeDataWereAppended()
        {

            //Arrange.
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Quotations, AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(UTDefaulter.DEFAULT_ASSET_ID, UTDefaulter.DEFAULT_TIMEFRAME_ID) { AnalysisTypes = analysisTypes };

            //Quotation repository.
            Mock<IQuotationRepository> quotationRepository = new Mock<IQuotationRepository>();
            IEnumerable<QuotationDto> quotationDtos = new QuotationDto[] { utf.getQuotationDto(1), utf.getQuotationDto(2), utf.getQuotationDto(3) };
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(quotationDtos);

            //Price repository.
            Mock<IPriceRepository> priceRepository = new Mock<IPriceRepository>();
            IEnumerable<PriceDto> priceDtos = new PriceDto[] { utf.getPriceDto(1), utf.getPriceDto(2), utf.getPriceDto(3) };
            priceRepository.Setup(q => q.GetPrices(queryDef)).Returns(priceDtos);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationRepository(quotationRepository.Object);
            service.InjectPriceRepository(priceRepository.Object);
            var dataSets = service.GetDataSets(queryDef);

            IEnumerable<QuotationDto> quotationDtos2 = new QuotationDto[] { utf.getQuotationDto(101), utf.getQuotationDto(102), utf.getQuotationDto(103) };
            quotationRepository.Setup(q => q.GetQuotations(queryDef)).Returns(quotationDtos2);

            IEnumerable<PriceDto> priceDtos2 = new PriceDto[] { utf.getPriceDto(101), utf.getPriceDto(102), utf.getPriceDto(103) };
            priceRepository.Setup(q => q.GetPrices(queryDef)).Returns(priceDtos2);
            service.InjectQuotationRepository(quotationRepository.Object);
            service.InjectPriceRepository(priceRepository.Object);
            DataSet[] result = service.GetDataSets(queryDef, dataSets).ToArray();

            //Assert
            DataSet[] expectedDataSets = new DataSet[104];

            DataSet ds1 = utf.getDataSet(1);
            ds1.SetQuotation(utf.getQuotation(ds1)).SetPrice(utf.getPrice(ds1));
            DataSet ds2 = utf.getDataSet(2);
            ds2.SetQuotation(utf.getQuotation(ds2)).SetPrice(utf.getPrice(ds2)); ;
            DataSet ds3 = utf.getDataSet(3);
            ds3.SetQuotation(utf.getQuotation(ds3)).SetPrice(utf.getPrice(ds3));
            DataSet ds101 = utf.getDataSet(101);
            ds101.SetQuotation(utf.getQuotation(ds101)).SetPrice(utf.getPrice(ds101));
            DataSet ds102 = utf.getDataSet(102);
            ds102.SetQuotation(utf.getQuotation(ds102)).SetPrice(utf.getPrice(ds102));
            DataSet ds103 = utf.getDataSet(103);
            ds103.SetQuotation(utf.getQuotation(ds103)).SetPrice(utf.getPrice(ds103));

            expectedDataSets[1] = ds1;
            expectedDataSets[2] = ds2;
            expectedDataSets[3] = ds3;
            expectedDataSets[101] = ds101;
            expectedDataSets[102] = ds102;
            expectedDataSets[103] = ds103;
            var areEqual = expectedDataSets.HasEqualItemsInTheSameOrder(result);
            Assert.IsTrue(areEqual);

        }

        #endregion GET_DATA_SETS_WITH_INITIAL_COLLECTION



        #region UPDATE_DATA_SETS

        [Ignore]
        [TestMethod]
        public void UpdateDataSets_UnitTestsTODO()
        {
            Assert.Fail("Not implemented yet");
            //UpdateDataSets_SendUpdateCommandToAllSubservices
        }

        #endregion UPDATE_DATA_SETS



        #region INHERITED FROM QUOTATIONS SERVICE

        [TestMethod]
        public void GetQuotations_ReturnsProperCollection_IfQueryDefIsGiven()
        {

            ////Arrange
            //Mock<IQuotationRepository> mockedRepository = new Mock<IQuotationRepository>();
            //QuotationDto dto1 = new QuotationDto() { QuotationId = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09191, HighPrice = 1.09218, LowPrice = 1.09186, ClosePrice = 1.09194, Volume = 1411, IndexNumber = 2 };
            //QuotationDto dto2 = new QuotationDto() { QuotationId = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 3 };
            //QuotationDto dto3 = new QuotationDto() { QuotationId = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 4 };
            //QuotationDto dto4 = new QuotationDto() { QuotationId = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 5 };
            //mockedRepository.Setup(r => r.GetQuotations(It.IsAny<AnalysisDataQueryDefinition>())).Returns(new QuotationDto[] { dto1, dto2, dto3, dto4 });

            ////Act
            //IQuotationService service = new QuotationService(mockedRepository.Object);
            //var actualQuotations = service.GetQuotations(new AnalysisDataQueryDefinition(1, 1));

            ////Assert
            //List<Quotation> expectedQuotations = new List<Quotation>();
            //Quotation quotation1 = new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 2 };
            //Quotation quotation2 = new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 3 };
            //Quotation quotation3 = new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 4 };
            //Quotation quotation4 = new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 5 };
            //expectedQuotations.AddRange(new Quotation[] { quotation1, quotation2, quotation3, quotation4 });
            //bool areEqual = expectedQuotations.HasEqualItems(actualQuotations);
            //Assert.IsTrue(areEqual);

            Assert.Fail("Not implemented yet");

        }

        [TestMethod]
        public void GetQuotations_ForAlreadyExistingInstances_ThoseInstancesAreReturned()
        {

            ////Arrange
            //Mock<IQuotationRepository> mockedRepository = new Mock<IQuotationRepository>();
            //List<QuotationDto> dtos = new List<QuotationDto>();
            //QuotationDto dto1 = new QuotationDto() { QuotationId = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09191, HighPrice = 1.09218, LowPrice = 1.09186, ClosePrice = 1.09194, Volume = 1411, IndexNumber = 2 };
            //QuotationDto dto2 = new QuotationDto() { QuotationId = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 3 };
            //QuotationDto dto3 = new QuotationDto() { QuotationId = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 4 };
            //QuotationDto dto4 = new QuotationDto() { QuotationId = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 5 };
            //AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1);
            //mockedRepository.Setup(r => r.GetQuotations(queryDef)).Returns(new QuotationDto[] { dto1, dto2, dto3 });

            ////Act
            //IQuotationService service = new QuotationService(mockedRepository.Object);
            //var actualQuotations = service.GetQuotations(queryDef);
            //Quotation baseQuotation = actualQuotations.SingleOrDefault(q => q.IndexNumber == 3);

            ////Change mocking.
            //mockedRepository.Setup(r => r.GetQuotations(queryDef)).Returns(new QuotationDto[] { dto1, dto2, dto3, dto4 });
            //actualQuotations = service.GetQuotations(queryDef);
            //Quotation comparedQuotation = actualQuotations.SingleOrDefault(q => q.IndexNumber == 3);

            ////Assert
            //bool areTheSameObject = (baseQuotation == comparedQuotation);
            //Assert.IsTrue(areTheSameObject);

            Assert.Fail("Not implemented yet");

        }

        [TestMethod]
        public void UpdateQuotations_OnlyItemsFlaggedAsUpdatedAreSendToRepository()
        {
            ////Arrange
            //QuotationUpdateTester mockedRepository = new QuotationUpdateTester();
            //List<Quotation> quotations = new List<Quotation>();
            //Quotation quotation1 = new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 2 };
            //Quotation quotation2 = new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 3 };
            //Quotation quotation3 = new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 4 };
            //Quotation quotation4 = new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 5 };
            //quotations.AddRange(new Quotation[] { quotation1, quotation2, quotation3, quotation4 });

            ////Act
            //quotation1.Updated = true;
            //quotation3.Updated = true;
            //quotation4.IsNew = true;
            //IQuotationService service = new QuotationService(mockedRepository);
            //service.UpdateQuotations(quotations);

            ////Assert
            //IEnumerable<QuotationDto> passedDtos = mockedRepository.GetDtosPassedAsParameters();
            //List<QuotationDto> expectedDtos = new List<QuotationDto>();
            //QuotationDto dto1 = new QuotationDto() { QuotationId = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09191, HighPrice = 1.09218, LowPrice = 1.09186, ClosePrice = 1.09194, Volume = 1411, IndexNumber = 2 };
            //QuotationDto dto2 = new QuotationDto() { QuotationId = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 3 };
            //QuotationDto dto3 = new QuotationDto() { QuotationId = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 4 };
            //QuotationDto dto4 = new QuotationDto() { QuotationId = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 5 };
            //expectedDtos.AddRange(new QuotationDto[] { dto1, dto3, dto4 });
            //bool areEqual = expectedDtos.HasEqualItems(passedDtos);
            //Assert.IsTrue(areEqual);

            Assert.Fail("Not implemented yet");

        }

        #endregion INHERITED FROM QUOTATIONS SERVICE



        #region INHERITED FROM PRICES SERVICE

        [TestMethod]
        public void GetPrices_ReturnsProperCollection_IfQueryDefIsGiven()
        {

            //Mock<IPriceRepository> mockedRepository = new Mock<IPriceRepository>();

            ////Arrange:Prices
            //PriceDto dto1 = new PriceDto() { Id = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.05, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //PriceDto dto2 = new PriceDto() { Id = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.06, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //PriceDto dto3 = new PriceDto() { Id = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.07, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //PriceDto dto4 = new PriceDto() { Id = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.08, PriceDirection2D = -1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //mockedRepository.Setup(r => r.GetPrices(It.IsAny<AnalysisDataQueryDefinition>())).Returns(new PriceDto[] { dto1, dto2, dto3, dto4 });

            ////Arrange:Extrema
            //ExtremumDto extremumDto1 = new ExtremumDto() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 1, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Timestamp = DateTime.Now, Value = 123.42 };
            //ExtremumDto extremumDto2 = new ExtremumDto() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 2, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Timestamp = DateTime.Now, Value = 123.42 };
            //ExtremumDto extremumDto3 = new ExtremumDto() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 3, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Timestamp = DateTime.Now, Value = 123.42 };
            //ExtremumDto extremumDto4 = new ExtremumDto() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 4, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Timestamp = DateTime.Now, Value = 123.42 };
            //mockedRepository.Setup(r => r.GetExtrema(It.IsAny<AnalysisDataQueryDefinition>())).Returns(new ExtremumDto[] { extremumDto1, extremumDto2, extremumDto3, extremumDto4 });

            ////Act
            //IPriceService service = new PriceService(mockedRepository.Object);
            //var actualPrices = service.GetPrices(new AnalysisDataQueryDefinition(1, 1));

            ////Assert
            //Extremum extremum1 = new Extremum(1, 1, ExtremumType.PeakByClose, 1) { ExtremumId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Value = 123.42 };
            //Extremum extremum2 = new Extremum(1, 1, ExtremumType.PeakByHigh, 2) { ExtremumId = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Value = 123.42 };
            //Extremum extremum3 = new Extremum(1, 1, ExtremumType.TroughByClose, 3) { ExtremumId = 3, Date = new DateTime(2016, 1, 15, 22, 40, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Value = 123.42 };
            //Extremum extremum4 = new Extremum(1, 1, ExtremumType.TroughByLow, 4) { ExtremumId = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Value = 123.42 };

            //List<Price> expectedPrices = new List<Price>();
            //Price price1 = new Price() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, CloseDelta = 1.05, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34, PeakByClose = extremum1 };
            //Price price2 = new Price() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, CloseDelta = 1.06, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34, PeakByHigh = extremum2 };
            //Price price3 = new Price() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, CloseDelta = 1.07, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //Price price4 = new Price() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, CloseDelta = 1.08, Direction2D = -1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34, TroughByClose = extremum3, TroughByLow = extremum4 };

            //expectedPrices.AddRange(new Price[] { price1, price2, price3, price4 });
            //bool areEqual = expectedPrices.HasEqualItems(actualPrices);
            //Assert.IsTrue(areEqual);

            Assert.Fail("Not implemented yet");

        }

        [TestMethod]
        public void GetPrices_ForAlreadyExistingInstances_ThoseInstancesAreReturned()
        {

            ////Arrange
            //AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1);
            //Mock<IPriceRepository> mockedRepository = new Mock<IPriceRepository>();
            //PriceDto dto1 = new PriceDto() { Id = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.05, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //PriceDto dto2 = new PriceDto() { Id = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.06, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //PriceDto dto3 = new PriceDto() { Id = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.07, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //PriceDto dto4 = new PriceDto() { Id = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.08, PriceDirection2D = -1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //mockedRepository.Setup(r => r.GetPrices(queryDef)).Returns(new PriceDto[] { dto1, dto2, dto3 });

            ////Act
            //IPriceService service = new PriceService(mockedRepository.Object);
            //var actualPrices = service.GetPrices(queryDef);
            //Price price5 = actualPrices.SingleOrDefault(p => p.IndexNumber == 3);

            ////Change mocking.
            //mockedRepository.Setup(r => r.GetPrices(queryDef)).Returns(new PriceDto[] { dto1, dto2, dto3, dto4 });
            //actualPrices = service.GetPrices(queryDef);
            //Price comparedPrice = actualPrices.SingleOrDefault(p => p.IndexNumber == 3);

            ////Assert
            //bool areTheSameObject = (price5 == comparedPrice);
            //Assert.IsTrue(areTheSameObject);

            Assert.Fail("Not implemented yet");

        }

        [TestMethod]
        public void UpdatePrices_OnlyItemsFlaggedAsUpdatedAreSendToRepository()
        {
            ////Arrange
            //PriceUpdateTester mockedRepository = new PriceUpdateTester();

            ////Arrange:Extrema
            //ExtremumDto extremumDto1 = new ExtremumDto() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 1, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Timestamp = DateTime.Now, Value = 123.42 };
            //ExtremumDto extremumDto2 = new ExtremumDto() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 2, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Timestamp = DateTime.Now, Value = 123.42 };
            //ExtremumDto extremumDto3 = new ExtremumDto() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 3, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Timestamp = DateTime.Now, Value = 123.42 };
            //ExtremumDto extremumDto4 = new ExtremumDto() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 4, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Timestamp = DateTime.Now, Value = 123.42 };

            //Extremum extremum1 = new Extremum(1, 1, ExtremumType.PeakByClose, 1) { ExtremumId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Value = 123.42 };
            //Extremum extremum2 = new Extremum(1, 1, ExtremumType.PeakByHigh, 2) { ExtremumId = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Value = 123.42 };
            //Extremum extremum3 = new Extremum(1, 1, ExtremumType.TroughByClose, 3) { ExtremumId = 3, Date = new DateTime(2016, 1, 15, 22, 40, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Value = 123.42 };
            //Extremum extremum4 = new Extremum(1, 1, ExtremumType.TroughByLow, 4) { ExtremumId = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, Open = true, Value = 123.42 };

            ////Arrange:Prices
            //PriceDto dto1 = new PriceDto() { Id = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.05, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //PriceDto dto2 = new PriceDto() { Id = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.06, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //PriceDto dto3 = new PriceDto() { Id = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.07, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //PriceDto dto4 = new PriceDto() { Id = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.08, PriceDirection2D = -1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };

            //Price price1 = new Price() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, CloseDelta = 1.05, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34, PeakByClose = extremum1 };
            //Price price2 = new Price() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, CloseDelta = 1.06, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34, PeakByHigh = extremum2 };
            //Price price3 = new Price() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, CloseDelta = 1.07, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            //Price price4 = new Price() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, CloseDelta = 1.08, Direction2D = -1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34, TroughByClose = extremum3, TroughByLow = extremum4 };

            ////Act
            //price1.Updated = true;
            //price3.New = true;
            //price4.Updated = true;
            //extremum2.Updated = true;
            //extremum3.Updated = true;
            //IPriceService service = new PriceService(mockedRepository);
            //service.UpdatePrices(new Price[] { price1, price2, price3, price4 });
            //IEnumerable<PriceDto> passedPriceDtos = mockedRepository.GetPriceDtosPassedAsParameters();
            //IEnumerable<ExtremumDto> passedExtremumDtos = mockedRepository.GetExtremumDtosPassedAsParameters();

            ////Assert
            //IEnumerable<PriceDto> expectedPriceDtos = new PriceDto[] { dto1, dto3, dto4 };
            //IEnumerable<ExtremumDto> expectedExtremumDtos = new ExtremumDto[] { extremumDto2, extremumDto3 };
            //bool arePriceDtosEqual = expectedPriceDtos.HasEqualItems(passedPriceDtos);
            //bool areExtremumDtosEqual = expectedExtremumDtos.HasEqualItems(passedExtremumDtos);
            //bool areEqual = arePriceDtosEqual && areExtremumDtosEqual;
            //Assert.IsTrue(areEqual);

            Assert.Fail("Not implemented yet");

        }

        #endregion GET_PRICES




    }
}


