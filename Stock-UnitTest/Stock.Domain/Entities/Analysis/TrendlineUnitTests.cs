using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.DAL.TransferObjects;
using Stock.Core;
using Stock.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Stock_UnitTest.Stock.Domain
{
    [TestClass]
    public class TrendlineUnitTests
    {
        private const int DEFAULT_ID = 1;
        private const int DEFAULT_ASSET_ID = 1;
        private const int DEFAULT_TIMEFRAME_ID = 1;
        private const int DEFAULT_SIMULATION_ID = 1;
        private DateTime DEFAULT_BASE_DATE = new DateTime(2017, 5, 1, 12, 0, 0);
        private const int DEFAULT_START_INDEX = 84;
        private const double DEFAULT_START_LEVEL = 1.1654;
        private const int DEFAULT_FOOTHOLD_INDEX = 134;
        private const double DEFAULT_FOOTHOLD_LEVEL = 1.1754;
        private const double DEFAULT_VALUE = 1.234;
        private const int DEFAULT_LAST_UPDATE_INDEX = 154;
        private const int DEFAULT_FOOTHOLD_SLAVE_INDEX = 135;
        private const int DEFAULT_FOOTHOLD_IS_PEAK = 1;
        private const int DEFAULT_FOOTHOLD_TYPE = 1;
        private const bool DEFAULT_INITIAL_IS_PEAK = true;
        private const bool DEFAULT_CURRENT_IS_PEAK = false;



        #region INFRASTRUCTURE

        private DataSet getDataSetWithQuotation(int indexNumber, double open, double high, double low, double close, double volume)
        {
            return getDataSetWithQuotation(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber, open, high, low, close, volume);
        }

        private DataSet getDataSetWithQuotation(int assetId, int timeframeId, int indexNumber, double open, double high, double low, double close, double volume)
        {
            var timeframe = Timeframe.ById(timeframeId);
            DateTime date = timeframe.AddTimeUnits(DEFAULT_BASE_DATE, indexNumber);
            DataSet ds = new DataSet(assetId, timeframeId, date, indexNumber);
            Quotation q = new Quotation(ds) { Open = open, High = high, Low = low, Close = close, Volume = volume };
            return ds;

        }

        private DataSet getDataSetWithQuotationAndPrice(int indexNumber, double open, double high, double low, double close, double volume)
        {
            return getDataSetWithQuotationAndPrice(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber, open, high, low, close, volume);
        }

        private DataSet getDataSetWithQuotationAndPrice(int assetId, int timeframeId, int indexNumber, double open, double high, double low, double close, double volume)
        {
            var timeframe = Timeframe.ById(timeframeId);
            DateTime date = timeframe.AddTimeUnits(DEFAULT_BASE_DATE, indexNumber);
            DataSet ds = new DataSet(assetId, timeframeId, date, indexNumber);
            Quotation q = new Quotation(ds) { Open = open, High = high, Low = low, Close = close, Volume = volume };
            Price p = new Price(ds);
            return ds;
        }


        private DataSet getDataSet(int indexNumber)
        {
            return getDataSet(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber);
        }

        private DataSet getDataSet(int assetId, int timeframeId, int indexNumber)
        {
            var timeframe = Timeframe.ById(timeframeId);
            DateTime date = timeframe.AddTimeUnits(DEFAULT_BASE_DATE, indexNumber);
            DataSet ds = new DataSet(assetId, timeframeId, date, indexNumber);
            return ds;
        }


        private Quotation getQuotation(DataSet ds)
        {
            return new Quotation(ds)
            {
                Id = ds.IndexNumber,
                Open = 1.09191,
                High = 1.09218,
                Low = 1.09186,
                Close = 1.09194,
                Volume = 1411
            };
        }

        private Quotation getQuotation(int indexNumber)
        {
            return getQuotation(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber);
        }

        private Quotation getQuotation(int assetId, int timeframeId, int indexNumber)
        {
            DataSet ds = getDataSet(assetId, timeframeId, indexNumber);
            return new Quotation(ds)
            {
                Id = indexNumber,
                Open = 1.09191,
                High = 1.09218,
                Low = 1.09186,
                Close = 1.09194,
                Volume = 1411
            };
        }

        private QuotationDto getQuotationDto(int indexNumber)
        {
            return getQuotationDto(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber);
        }

        private QuotationDto getQuotationDto(int assetId, int timeframeId, int indexNumber)
        {
            var timeframe = Timeframe.ById(timeframeId);
            DateTime date = timeframe.AddTimeUnits(DEFAULT_BASE_DATE, indexNumber);
            return new QuotationDto()
            {
                PriceDate = date,
                AssetId = assetId,
                TimeframeId = timeframeId,
                OpenPrice = 1.09191,
                HighPrice = 1.09218,
                LowPrice = 1.09186,
                ClosePrice = 1.09194,
                Volume = 1411
            };
        }



        private Price getPrice(DataSet ds)
        {
            return getPrice(ds);
        }

        private Price getPrice(int indexNumber)
        {
            return getPrice(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber);
        }

        private Price getPrice(int assetId, int timeframeId, int indexNumber)
        {
            DataSet ds = getDataSet(assetId, timeframeId, indexNumber);
            return new Price(ds)
            {
                Id = indexNumber,
                CloseDelta = 1.05,
                Direction2D = 1,
                Direction3D = 0,
                PriceGap = 1.23,
                CloseRatio = 1.23,
                ExtremumRatio = 2.34
            };
        }

        private PriceDto getPriceDto(int indexNumber)
        {
            return getPriceDto(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber);
        }

        private PriceDto getPriceDto(int assetId, int timeframeId, int indexNumber)
        {
            var timeframe = Timeframe.ById(timeframeId);
            DateTime date = timeframe.AddTimeUnits(DEFAULT_BASE_DATE, indexNumber);
            return new PriceDto()
            {
                Id = indexNumber,
                PriceDate = date,
                AssetId = assetId,
                TimeframeId = timeframeId,
                IndexNumber = indexNumber,
                DeltaClosePrice = 1.04,
                PriceDirection2D = 1,
                PriceDirection3D = 1,
                PriceGap = 0.05,
                CloseRatio = 0.23,
                ExtremumRatio = 1
            };
        }



        private Trendline getDefaultTrendline()
        {
            AtsSettings settings = new AtsSettings(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID);

            Price basePrice = getPrice(DEFAULT_START_INDEX);
            Extremum baseMaster = new Extremum(basePrice, ExtremumType.PeakByClose);
            ExtremumGroup baseGroup = new ExtremumGroup(baseMaster, null, DEFAULT_INITIAL_IS_PEAK);
            TrendlinePoint basePoint = new TrendlinePoint(baseGroup, DEFAULT_START_LEVEL);

            Price secondPrice = getPrice(DEFAULT_FOOTHOLD_INDEX);
            Extremum secondMaster = new Extremum(secondPrice, ExtremumType.PeakByClose);
            ExtremumGroup secondGroup = new ExtremumGroup(secondMaster, null, DEFAULT_INITIAL_IS_PEAK);
            TrendlinePoint footholdPoint = new TrendlinePoint(secondGroup, DEFAULT_FOOTHOLD_LEVEL);

            Trendline trendline = new Trendline(settings, basePoint, footholdPoint);
            trendline.Id = DEFAULT_ID;
            trendline.Value = DEFAULT_VALUE;
            trendline.LastUpdateIndex = DEFAULT_LAST_UPDATE_INDEX;
            trendline.FootholdSlaveIndex = DEFAULT_FOOTHOLD_SLAVE_INDEX;
            trendline.CurrentIsPeak = DEFAULT_CURRENT_IS_PEAK;

            return trendline;

        }


        private TrendlineDto getDefaultTrendlineDto()
        {
            return new TrendlineDto()
            {
                Id = 1,
                AssetId = DEFAULT_ASSET_ID,
                TimeframeId = DEFAULT_TIMEFRAME_ID,
                SimulationId = DEFAULT_SIMULATION_ID,
                StartIndex = DEFAULT_START_INDEX,
                StartLevel = DEFAULT_START_LEVEL,
                FootholdIndex = DEFAULT_FOOTHOLD_INDEX,
                FootholdLevel = DEFAULT_FOOTHOLD_LEVEL,
                FootholdSlaveIndex = DEFAULT_FOOTHOLD_SLAVE_INDEX,
                FootholdIsPeak = DEFAULT_FOOTHOLD_TYPE,
                Value = 1.2345,
                LastUpdateIndex = 51
            };
        }

        #endregion INFRASTRUCTURE



        #region CONSTRUCTOR

        [TestMethod]
        public void Constructor_newInstance_hasProperParameters()
        {

            //Act.
            var trendline = getDefaultTrendline();

            //Assert.
            Assert.AreEqual(DEFAULT_ID, trendline.Id);
            Assert.AreEqual(DEFAULT_ASSET_ID, trendline.AssetId);
            Assert.AreEqual(DEFAULT_TIMEFRAME_ID, trendline.TimeframeId);
            Assert.AreEqual(DEFAULT_SIMULATION_ID, trendline.SimulationId);
            Assert.AreEqual(DEFAULT_START_INDEX, trendline.StartIndex);
            Assert.AreEqual(DEFAULT_START_LEVEL, trendline.StartLevel);
            Assert.AreEqual(DEFAULT_FOOTHOLD_INDEX, trendline.FootholdIndex);
            Assert.AreEqual(DEFAULT_FOOTHOLD_LEVEL, trendline.FootholdLevel);
            Assert.AreEqual(DEFAULT_FOOTHOLD_SLAVE_INDEX, trendline.FootholdSlaveIndex);
            Assert.AreEqual(DEFAULT_FOOTHOLD_IS_PEAK, trendline.FootholdIsPeak);
            Assert.AreEqual(DEFAULT_VALUE, trendline.Value);
            Assert.AreEqual(DEFAULT_INITIAL_IS_PEAK, trendline.InitialIsPeak);
            Assert.AreEqual(DEFAULT_LAST_UPDATE_INDEX, trendline.LastUpdateIndex);

        }

        [TestMethod]
        public void Constructor_fromDto_hasCorrectProperties()
        {

            //Act.
            var trendlineDto = new TrendlineDto() { 
                Id = DEFAULT_ID, 
                AssetId = DEFAULT_ASSET_ID, 
                TimeframeId = DEFAULT_TIMEFRAME_ID, 
                SimulationId = DEFAULT_SIMULATION_ID, 
                StartIndex = DEFAULT_START_INDEX, 
                StartLevel = DEFAULT_START_LEVEL, 
                FootholdIndex = DEFAULT_FOOTHOLD_INDEX, 
                FootholdLevel = DEFAULT_FOOTHOLD_LEVEL,
                FootholdSlaveIndex = DEFAULT_FOOTHOLD_SLAVE_INDEX,
                FootholdIsPeak = DEFAULT_FOOTHOLD_IS_PEAK,
                EndIndex = null,
                Value = DEFAULT_VALUE,
                LastUpdateIndex = DEFAULT_LAST_UPDATE_INDEX
            };

            var trendline = new Trendline(trendlineDto);

            //Assert.
            Assert.AreEqual(DEFAULT_ID, trendline.Id);
            Assert.AreEqual(DEFAULT_ASSET_ID, trendline.AssetId);
            Assert.AreEqual(DEFAULT_TIMEFRAME_ID, trendline.TimeframeId);
            Assert.AreEqual(DEFAULT_SIMULATION_ID, trendline.SimulationId);
            Assert.AreEqual(DEFAULT_START_INDEX, trendline.StartIndex);
            Assert.AreEqual(DEFAULT_START_LEVEL, trendline.StartLevel);
            Assert.AreEqual(DEFAULT_FOOTHOLD_INDEX, trendline.FootholdIndex);
            Assert.AreEqual(DEFAULT_FOOTHOLD_LEVEL, trendline.FootholdLevel);
            Assert.AreEqual(DEFAULT_FOOTHOLD_SLAVE_INDEX, trendlineDto.FootholdSlaveIndex);
            Assert.AreEqual(DEFAULT_FOOTHOLD_IS_PEAK, trendlineDto.FootholdIsPeak);
            Assert.AreEqual(DEFAULT_VALUE, trendline.Value);
            Assert.IsNull(trendline.EndIndex);
            Assert.AreEqual(DEFAULT_LAST_UPDATE_INDEX, trendline.LastUpdateIndex);

        }

        #endregion CONSTRUCTOR



        #region TO_DTO


        [TestMethod]
        public void ToDto_returnProperDto()
        {

            //Act

            var trendline = getDefaultTrendline();
            var trendlineDto = trendline.ToDto();

            //Assert.
            Assert.AreEqual(DEFAULT_ID, trendlineDto.Id);
            Assert.AreEqual(DEFAULT_ASSET_ID, trendlineDto.AssetId);
            Assert.AreEqual(DEFAULT_TIMEFRAME_ID, trendlineDto.TimeframeId);
            Assert.AreEqual(DEFAULT_SIMULATION_ID, trendlineDto.SimulationId);
            Assert.AreEqual(DEFAULT_START_INDEX, trendlineDto.StartIndex);
            Assert.AreEqual(DEFAULT_START_LEVEL, trendlineDto.StartLevel);
            Assert.AreEqual(DEFAULT_FOOTHOLD_INDEX, trendlineDto.FootholdIndex);
            Assert.AreEqual(DEFAULT_FOOTHOLD_LEVEL, trendlineDto.FootholdLevel);
            Assert.AreEqual(DEFAULT_FOOTHOLD_SLAVE_INDEX, trendlineDto.FootholdSlaveIndex);
            Assert.AreEqual(DEFAULT_FOOTHOLD_IS_PEAK, trendlineDto.FootholdIsPeak);
            Assert.AreEqual(DEFAULT_VALUE, trendlineDto.Value);
            Assert.IsNull(trendlineDto.EndIndex);
            Assert.AreEqual(DEFAULT_LAST_UPDATE_INDEX, trendlineDto.LastUpdateIndex);

        }


        #endregion TO_DTO



        #region EQUALS


        [TestMethod]
        public void Equals_ReturnsFalse_IfComparedToObjectOfOtherType()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = new { Id = 1 };

            //Act
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfAllPropertiesAreEqual()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfIdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.Id++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfAssetIdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.AssetId += 1;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfTimeframeIdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.TimeframeId += 1;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfSimulationIdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.SimulationId += 1;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfStartIndexIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.StartIndex = comparedItem.StartIndex + 2;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        public void Equals_ReturnsFalse_IfStartLevelIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.StartLevel += 0.1;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        public void Equals_ReturnsFalse_IfFootholdIndexIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.FootholdIndex = comparedItem.FootholdIndex++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        public void Equals_ReturnsFalse_IfFootholdLevelIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.FootholdLevel += 1;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }


        public void Equals_ReturnsFalse_IfValueIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.Value += 1;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        public void Equals_ReturnsFalse_IfLastUpdateIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.LastUpdateIndex = comparedItem.LastUpdateIndex + 5;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        #endregion EQUALS



        [TestMethod]
        public void Equals_ReturnsTrue_IfEndIndexIsEqual()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            baseItem.EndIndex = 100;
            comparedItem.EndIndex = 100;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfEndIndexIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            baseItem.EndIndex = 50;
            comparedItem.EndIndex = 100;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOnlyBaseItemHasEndIndexNull()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            baseItem.EndIndex = null;
            comparedItem.EndIndex = 100;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOnlyComparedItemHasEndIndexNull()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            baseItem.EndIndex = 100;
            comparedItem.EndIndex = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfBothItemHasEndIndexNull()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.EndIndex = null;
            baseItem.EndIndex = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }
    }
}
