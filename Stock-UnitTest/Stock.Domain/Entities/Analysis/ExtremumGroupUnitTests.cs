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
    public class ExtremumGroupUnitTests
    {
        
        private const int DEFAULT_ASSET_ID = 1;
        private const int DEFAULT_TIMEFRAME_ID = 1;
        private const int DEFAULT_SIMULATION_ID = 1;
        private DateTime DEFAULT_BASE_DATE = new DateTime(2017, 3, 1);



        #region INFRASTRUCTURE

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



        private IEnumerable<Quotation> getDefaultQuotationsCollection()
        {
            return new Quotation[] { getQuotation(1), getQuotation(2), getQuotation(3), getQuotation(4) };
        }

        private IEnumerable<QuotationDto> getDefaultQuotationDtosCollection()
        {
            return new QuotationDto[] { getQuotationDto(1), getQuotationDto(2), getQuotationDto(3), getQuotationDto(4) };
        }



        private IEnumerable<Price> getDefaultPricesCollection()
        {
            return new Price[] { getPrice(1), getPrice(2), getPrice(3), getPrice(4) };
        }

        private IEnumerable<PriceDto> getDefaultPriceDtosCollection()
        {
            return new PriceDto[] { getPriceDto(1), getPriceDto(2), getPriceDto(3), getPriceDto(4) };
        }



        #endregion INFRASTRUCTURE





        #region GET_LATE_INDEX_NUMBER

        [TestMethod]
        public void GetLateIndexNumber_IfThereIsOnlyMasterExtremum_MasterExtremumIndexIsReturned()
        {
            
            //Arrange
            DataSet ds = getDataSet(50);
            Price price = getPrice(ds);
            Quotation quotation = new Quotation(ds);
            Extremum master = new Extremum(price, ExtremumType.PeakByClose);
            ExtremumGroup group = new ExtremumGroup(master, null, true);

            //Assert
            int lateIndexNumber = group.GetLateIndexNumber();

            //Act
            int expected = 50;
            Assert.AreEqual(expected, lateIndexNumber);

        }

        [TestMethod]
        public void GetLateIndexNumber_IfThereIsOnlySlaveExtremum_SlaveExtremumIndexIsReturned()
        {

            //Arrange
            DataSet ds = getDataSet(51);
            Price price = getPrice(ds);
            Quotation quotation = new Quotation(ds);
            Extremum slave = new Extremum(price, ExtremumType.PeakByHigh);
            ExtremumGroup group = new ExtremumGroup(null, slave, true);

            //Assert
            int lateIndexNumber = group.GetLateIndexNumber();

            //Act
            int expected = 51;
            Assert.AreEqual(expected, lateIndexNumber);

        }

        [TestMethod]
        public void GetLateIndexNumber_IfSlaveExtremumIsEarlier_MasterExtremumIndexIsReturned()
        {

            //Arrange
            Price price50 = getPrice(50);
            Price price51 = getPrice(51);

            Extremum slave = new Extremum(price50, ExtremumType.PeakByHigh);
            Extremum master = new Extremum(price51, ExtremumType.PeakByClose);
            ExtremumGroup group = new ExtremumGroup(master, slave, true);

            //Assert
            int lateIndexNumber = group.GetLateIndexNumber();

            //Act
            int expected = 51;
            Assert.AreEqual(expected, lateIndexNumber);

        }

        [TestMethod]
        public void GetLateIndexNumber_IfMasterExtremumIsEarlier_SlaveExtremumIndexIsReturned()
        {

            //Arrange
            Price price50 = getPrice(50);
            Price price51 = getPrice(51);

            Extremum master = new Extremum(price50, ExtremumType.PeakByClose);
            Extremum slave = new Extremum(price51, ExtremumType.PeakByHigh);
            ExtremumGroup group = new ExtremumGroup(master, slave, true);
            
            //Assert
            int lateIndexNumber = group.GetLateIndexNumber();
            
            //Act
            int expected = 51;
            Assert.AreEqual(expected, lateIndexNumber);

        }

        #endregion GET_LATE_INDEX_NUMBER




        #region GET_INDEX_NUMBER_FOR_QUOTATION

        [TestMethod]
        public void GetIndexNumberForQuotationForPeak_IfThereIsOnlyMasterQuotation_ReturnsMasterQuotationIndexNumber()
        {
            
            //Arrange


            //Act

            //Assert

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForPeak_IfThereIsOnlySlaveQuotation_ReturnsSlaveQuotationIndexNumber()
        {

            //Arrange

            //Act

            //Assert

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForPeak_IfThereAreBothQuotationsAndLevelLowerThanMasterHighPrice_ReturnsMasterQuotationIndexNumber()
        {

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForPeak_IfThereAreBothQuotationsAndLevelHigherThanMasterHighPrice_ReturnsSlaveQuotationIndexNumber()
        {

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForTrough_IfThereIsOnlyMasterQuotation_ReturnsMasterQuotationIndexNumber()
        {

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForTrough_IfThereIsOnlySlaveQuotation_ReturnsSlaveQuotationIndexNumber()
        {

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForTrough_IfThereAreBothQuotationsAndLevelHigherThanMasterLowPrice_ReturnsMasterQuotationIndexNumber()
        {

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForTrough_IfThereAreBothQuotationsAndLevelLowerThanMasterLowPrice_ReturnsSlaveQuotationIndexNumber()
        {

        }

        #endregion GET_INDEX_NUMBER_FOR_QUOTATION




    }
}
