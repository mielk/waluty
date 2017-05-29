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
using Stock.Utils;
using Stock.Core;
using Stock.Domain.Enums;

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class DataSetServiceUnitTests
    {

        private const int DEFAULT_ASSET_ID = 1;
        private const int DEFAULT_TIMEFRAME_ID = 1;
        private DateTime DEFAULT_BASE_DATE = new DateTime(2016, 1, 15, 22, 25, 0);
        private Timeframe timeframe = new Timeframe(1, "M5", TimeframeUnit.Minutes, 5);


        #region INFRASTRUCTURE

        private Quotation quotation(int i)
        {
            return quotation(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, i);
        }

        private Quotation quotation(int assetId, int timeframeId, int i)
        {
            return new Quotation()
            {
                Id = i,
                Date = timeframe.AddTimeUnits(DEFAULT_BASE_DATE, i),
                AssetId = assetId,
                TimeframeId = timeframeId,
                Open = 1.09191,
                High = 1.09218,
                Low = 1.09186,
                Close = 1.09194,
                Volume = 1411,
                IndexNumber = i
            };          
        }

        private Price price(int i)
        {
            return price(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, i);
        }

        private Price price(int assetId, int timeframeId, int i)
        {
            return new Price()
            {
                Id = i,
                Date = timeframe.AddTimeUnits(DEFAULT_BASE_DATE, i),
                AssetId = assetId,
                TimeframeId = timeframeId,
                IndexNumber = i,
                CloseDelta = 1.05, 
                Direction2D = 1, 
                Direction3D = 0, 
                PriceGap = 1.23, 
                CloseRatio = 1.23, 
                ExtremumRatio = 2.34 
            };
        }

        private DataSet dataSet(int i)
        {
            return dataSet(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, i);
        }

        private DataSet dataSet(int assetId, int timeframeId, int i)
        {
            return new DataSet(quotation(assetId, timeframeId, i)).SetPrice(price(assetId, timeframeId, i));
        }

        private IEnumerable<Price> getDefaultPricesCollection(){
            return new Price[] { price(1), price(2), price(3), price(4) };
        }

        private IEnumerable<Quotation> getDefaultQuotationsCollection()
        {
            return new Quotation[] { quotation(1), quotation(2), quotation(3), quotation(4) };
        }

        #endregion INFRASTRUCTURE


        #region GET_DATA_SETS

        [TestMethod]
        public void GetDataSets_ReturnsEmptyCollection_IfThereIsNoQuotationForGivenDateRange()
        {

            //Arrange
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Quotations, AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1) { AnalysisTypes = analysisTypes };
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            Mock<IPriceService> priceService = new Mock<IPriceService>();
            IEnumerable<Price> prices = getDefaultPricesCollection();
            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(new List<Quotation>());
            priceService.Setup(q => q.GetUnits(queryDef)).Returns(prices);
            
            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationService(quotationService.Object);
            service.InjectPriceService(priceService.Object);
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
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1) { AnalysisTypes = analysisTypes };

            //Arrange:Quotations
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            IEnumerable<Quotation> quotations = getDefaultQuotationsCollection();
            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(quotations);

            //Arrange:Prices
            Mock<IPriceService> priceService = new Mock<IPriceService>();
            IEnumerable<Price> prices = getDefaultPricesCollection();
            priceService.Setup(q => q.GetUnits(queryDef)).Returns(prices);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationService(quotationService.Object);
            service.InjectPriceService(priceService.Object);
            var dataSets = service.GetDataSets(queryDef);

            //Assert
            IEnumerable<DataSet> expectedDataSets = quotations.Select(q => new DataSet(q));
            var areEqual = expectedDataSets.HasEqualItems(dataSets);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetDataSets_ReturnsCollectionWithAllGivenAnalysisTypes()
        {

            //Arrange:QueryDef
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1) { AnalysisTypes = analysisTypes };
            
            //Arrange:Quotations
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            IEnumerable<Quotation> quotations = new Quotation[] { quotation(1), quotation(2), quotation(3), quotation(4) };
            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(quotations);

            //Arrange:Prices
            Mock<IPriceService> priceService = new Mock<IPriceService>();
            IEnumerable<Price> prices = new Price[] { price(1), price(2), price(3), price(4) };
            priceService.Setup(q => q.GetUnits(queryDef)).Returns(prices);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationService(quotationService.Object);
            service.InjectPriceService(priceService.Object);
            var dataSets = service.GetDataSets(queryDef);

            //Assert
            IEnumerable<DataSet> expectedDataSets = new DataSet[] { new DataSet(quotation(1)).SetPrice(price(1)), new DataSet(quotation(2)).SetPrice(price(2)),
                                                                    new DataSet(quotation(3)).SetPrice(price(3)), new DataSet(quotation(4)).SetPrice(price(4)) };
            var areEqual = expectedDataSets.HasEqualItems(dataSets);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetDataSets_ReturnsCollectionWithoutSomeAnalysisType_IfThereIsNoDataForThisType()
        {

            //Arrange:QueryDef
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1) { AnalysisTypes = analysisTypes };

            //Arrange:Quotations
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            IEnumerable<Quotation> quotations = new Quotation[] { quotation(2), quotation(3), quotation(4) }; 
            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(quotations);

            //Arrange:Prices
            Mock<IPriceService> priceService = new Mock<IPriceService>();
            IEnumerable<Price> prices = new Price[] { price(3), price(4), price(5), price(6) }; 
            priceService.Setup(q => q.GetUnits(queryDef)).Returns(prices);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationService(quotationService.Object);
            service.InjectPriceService(priceService.Object);
            var dataSets = service.GetDataSets(queryDef);

            //Assert
            IEnumerable<DataSet> expectedDataSets = new DataSet[] { new DataSet(quotation(2)), new DataSet(quotation(3)).SetPrice(price(3)), new DataSet(quotation(4)).SetPrice(price(4)) };
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
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            IEnumerable<Quotation> quotations = new Quotation[] { quotation(1), quotation(2), quotation(3), quotation(4) };
            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(quotations);

            //Arrange:Prices
            Mock<IPriceService> priceService = new Mock<IPriceService>();
            IEnumerable<Price> prices = new Price[] { price(1), price(2), price(3), price(4) };
            priceService.Setup(q => q.GetUnits(queryDef)).Returns(prices);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationService(quotationService.Object);
            service.InjectPriceService(priceService.Object);
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
        public void GetDataSets_UseNewInstancesOfAnalysisObjects_IfTheyAreInSpecificRepositories()
        {

            //Arrange:QueryDef
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices };
            DateTime startDate = new DateTime(2016, 1, 15, 22, 30, 0);
            DateTime endDate = new DateTime(2016, 1, 15, 22, 50, 0);
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1) { AnalysisTypes = analysisTypes, StartDate = startDate, EndDate = endDate };

            //Arrange:Quotations
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            IEnumerable<Quotation> quotations = new Quotation[] { quotation(1), quotation(2), quotation(3), quotation(4) };
            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(quotations);

            //Arrange:Prices
            Mock<IPriceService> priceService = new Mock<IPriceService>();
            IEnumerable<Price> prices = new Price[] { price(1), price(2), price(3), price(4) };
            priceService.Setup(q => q.GetUnits(queryDef)).Returns(prices);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationService(quotationService.Object);
            service.InjectPriceService(priceService.Object);
            var dataSets = service.GetDataSets(queryDef);
            DataSet baseDataSet = dataSets.SingleOrDefault(ds => ds.IndexNumber == 2);

            Quotation stubQuotation = quotation(2);
            stubQuotation.Open = stubQuotation.Open + 3;
            quotations = new Quotation[] { quotation(1), stubQuotation, quotation(3), quotation(4) };
            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(quotations);

            service.InjectQuotationService(quotationService.Object);
            dataSets = service.GetDataSets(queryDef);
            DataSet comparedDataSet = dataSets.SingleOrDefault(ds => ds.IndexNumber == 2);

            //Assert
            var areTheSameObject = (stubQuotation == comparedDataSet.GetQuotation());
            Assert.IsTrue(areTheSameObject);

        }

        [TestMethod]
        public void GetDataSets_DoesntOverrideExistingObjectsWithNulls_IfTheyAreNotInSpecificRepositories()
        {

            //Arrange:QueryDef
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1) { AnalysisTypes = analysisTypes };

            //Arrange:Quotations
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            IEnumerable<Quotation> quotations = new Quotation[] { quotation(1), quotation(2), quotation(3), quotation(4) };
            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(quotations);

            //Arrange:Prices
            Mock<IPriceService> priceService = new Mock<IPriceService>();
            IEnumerable<Price> prices = new Price[] { price(1), price(2), price(3), price(4) };
            priceService.Setup(q => q.GetUnits(queryDef)).Returns(prices);

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationService(quotationService.Object);
            service.InjectPriceService(priceService.Object);
            var dataSets = service.GetDataSets(queryDef);
            DataSet baseDataSet = dataSets.SingleOrDefault(ds => ds.IndexNumber == 2);
            Price basePrice = (baseDataSet == null ? null : baseDataSet.GetPrice());

            priceService.Setup(q => q.GetUnits(queryDef)).Returns(new Price[] { });
            service.InjectPriceService(priceService.Object);
            dataSets = service.GetDataSets(queryDef);
            DataSet comparedDataSet = dataSets.SingleOrDefault(ds => ds.IndexNumber == 2);
            Price comparedPrice = (comparedDataSet == null ? null : comparedDataSet.GetPrice());

            //Assert
            var areTheSameObject = (basePrice == comparedPrice);
            Assert.IsTrue(areTheSameObject);

        }

        #endregion GET_DATA_SETS


        #region APPEND_AND_RETURN_AS_ARRAY

        [TestMethod]
        public void AppendAndReturnAsArray_ReturnsNullArray_IfNullArrayWasPassedAndThereWereNoDataToBeAppended()
        {

            //Arrange
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Quotations, AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1) { AnalysisTypes = analysisTypes };
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            Mock<IPriceService> priceService = new Mock<IPriceService>();
            IEnumerable<Price> prices = getDefaultPricesCollection();
            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(new List<Quotation>());
            priceService.Setup(q => q.GetUnits(queryDef)).Returns(prices);

            //Act
            DataSet[] previous = new DataSet[] { };
            IDataSetService service = new DataSetService();
            service.InjectQuotationService(quotationService.Object);
            service.InjectPriceService(priceService.Object);
            var dataSets = service.AppendAndReturnAsArray(previous, queryDef);

            //Assert
            var isEmpty = (dataSets.Length == 0 || dataSets.Length == 1 && dataSets[0] == null);
            Assert.IsTrue(isEmpty);

        }

        [TestMethod]
        public void AppendAndReturnAsArray_ReturnsArrayWithoutChanges_IfNonEmptyArrayWasPassedAndThereWereNoDataToBeAppended()
        {

            //Arrange
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Quotations, AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1) { AnalysisTypes = analysisTypes };
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            Mock<IPriceService> priceService = new Mock<IPriceService>();
            IEnumerable<Price> prices = getDefaultPricesCollection();
            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(new List<Quotation>());
            priceService.Setup(q => q.GetUnits(queryDef)).Returns(prices);

            //Act
            DataSet[] baseDataSets = new DataSet[11];
            baseDataSets[7] = dataSet(7);
            baseDataSets[8] = dataSet(8);
            baseDataSets[9] = dataSet(9);
            baseDataSets[10] = dataSet(10);
            IDataSetService service = new DataSetService();
            service.InjectQuotationService(quotationService.Object);
            service.InjectPriceService(priceService.Object);
            var dataSets = service.AppendAndReturnAsArray(baseDataSets, queryDef);

            //Assert
            var areEqual = baseDataSets.HasEqualItemsInTheSameOrder(dataSets);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void AppendAndReturnAsArray_ReturnsProperlyFilledAndIndexedArray_IfNullArrayWasPassedAndSomeDataWereAppended()
        {

            //Arrange
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Quotations, AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1) { AnalysisTypes = analysisTypes };
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            Mock<IPriceService> priceService = new Mock<IPriceService>();

            //Act
            IDataSetService service = new DataSetService();
            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(new Quotation[] { quotation(101), quotation(102), quotation(103) });
            priceService.Setup(q => q.GetUnits(queryDef)).Returns(new Price[] { price(101), price(102), price(103) });
            service.InjectQuotationService(quotationService.Object);
            service.InjectPriceService(priceService.Object);
            DataSet[] result = service.AppendAndReturnAsArray(new List<DataSet>(), queryDef);

            //Assert
            DataSet[] expectedDataSets = new DataSet[104];
            expectedDataSets[101] = dataSet(101);
            expectedDataSets[102] = dataSet(102);
            expectedDataSets[103] = dataSet(103);
            var areEqual = expectedDataSets.HasEqualItemsInTheSameOrder(result);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void AppendAndReturnAsArray_ReturnsProperlyFilled_IfNonEmptyArrayWasPassedAndSomeDataWereAppended()
        {

            //Arrange
            IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Quotations, AnalysisType.Prices };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1) { AnalysisTypes = analysisTypes };
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            Mock<IPriceService> priceService = new Mock<IPriceService>();
            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(new Quotation[] { quotation(1), quotation(2), quotation(3) });
            priceService.Setup(q => q.GetUnits(queryDef)).Returns(new Price[] { price(1), price(2), price(3) });

            //Act
            IDataSetService service = new DataSetService();
            service.InjectQuotationService(quotationService.Object);
            service.InjectPriceService(priceService.Object);
            var dataSets = service.GetDataSets(queryDef);

            quotationService.Setup(q => q.GetUnits(queryDef)).Returns(new Quotation[] { quotation(101), quotation(102), quotation(103) });
            priceService.Setup(q => q.GetUnits(queryDef)).Returns(new Price[] { price(101), price(102), price(103) });
            service.InjectQuotationService(quotationService.Object);
            service.InjectPriceService(priceService.Object);
            DataSet[] result = service.AppendAndReturnAsArray(dataSets, queryDef);

            //Assert
            DataSet[] expectedDataSets = new DataSet[104];
            expectedDataSets[1] = dataSet(1);
            expectedDataSets[2] = dataSet(2);
            expectedDataSets[3] = dataSet(3);
            expectedDataSets[101] = dataSet(101);
            expectedDataSets[102] = dataSet(102);
            expectedDataSets[103] = dataSet(103);
            var areEqual = expectedDataSets.HasEqualItemsInTheSameOrder(result);
            Assert.IsTrue(areEqual);

        }

        #endregion APPEND_AND_RETURN_AS_ARRAY


        #region UPDATE_DATA_SETS

        [Ignore]
        [TestMethod]
        public void UpdateDataSets_UnitTestsTODO()
        {
            //UpdateDataSets_SendUpdateCommandToAllSubservices
        }

        #endregion UPDATE_DATA_SETS


        #region DATA_SET_INFO

        //Aktualnie zawsze zwraca najwyższy i najniższy poziom dla notowań. Zrobić, żeby podawać jako argument dla jakiego rodzaju analizy ma to wyliczać.

        [Ignore]
        [TestMethod]
        public void GetDataSetInfo_ReturnsProperDataSetInfo_ForEmptyCollectionOfQuotations()
        {

            ////Arrange


            ////Act
            //DataSetInfo expectedInfo = null;

            ////Assert
            //DataSetInfo expectedInfo = new DataSetInfo() { StartDate = null, EndDate = null, MinLevel = null, MaxLevel = null, Counter = 0 };
            //bool isEqual = expectedInfo.Equals(actualInfo);
            //Assert.IsTrue(isEqual);

        }

        [Ignore]
        [TestMethod]
        public void GetDataSetInfo_ReturnsProperDataSetInfo_ForManyItemsCollectionOfQuotations()
        {
            //Napisać testy dla tej metody.
        }

        [Ignore]
        [TestMethod]
        public void GetDataSetInfo_ReturnsProperDataSetInfo_ForSingleItemCollectionOfQuotations()
        {
            //Napisać testy dla tej metody.
        }


        #endregion DATA_SET_INFO

    }
}
