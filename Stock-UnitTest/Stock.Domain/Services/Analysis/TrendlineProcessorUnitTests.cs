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
using Stock.Domain.Enums;
using Stock.Domain.Services;
using Stock.Utils;
using Stock.Core;

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class TrendlineProcessorUnitTests
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



        #region CAN_CREATE_TRENDLINES

        [TestMethod]
        public void CanCreateTrendlines_ReturnFalse_IfDistanceBetweenExtremaIsGreaterThanLimit()
        {

            //Arrange
            Mock<IProcessManager> mockManager = new Mock<IProcessManager>();

            Price basePrice = getPrice(100);
            Extremum baseMaster = new Extremum(basePrice, ExtremumType.PeakByClose);
            ExtremumGroup baseExtremumGroup = new ExtremumGroup(baseMaster, null, true);

            Price footholdPrice = getPrice(500);
            Extremum footholdMaster = new Extremum(footholdPrice, ExtremumType.PeakByClose);
            ExtremumGroup footholdExtremumGroup = new ExtremumGroup(footholdMaster, null, true);

            //Act
            TrendlineProcessor processor = new TrendlineProcessor(mockManager.Object);
            processor.MaxDistanceBetweenExtrema = 100;

            //Assert
            var result = processor.CanCreateTrendline(baseExtremumGroup, footholdExtremumGroup);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void CanCreateTrendlines_ReturnFalse_IfDistanceBetweenExtremaIsLessThanMinLimit()
        {

            //Arrange
            Mock<IProcessManager> mockManager = new Mock<IProcessManager>();
            Price basePrice = getPrice(100);
            Extremum baseMaster = new Extremum(basePrice, ExtremumType.PeakByClose);
            ExtremumGroup baseExtremumGroup = new ExtremumGroup(baseMaster, null, true);

            Price footholdPrice = getPrice(102);
            Extremum footholdMaster = new Extremum(footholdPrice, ExtremumType.PeakByClose);
            ExtremumGroup footholdExtremumGroup = new ExtremumGroup(footholdMaster, null, true);

            //Act
            TrendlineProcessor processor = new TrendlineProcessor(mockManager.Object);
            processor.MaxDistanceBetweenExtrema = 3;

            //Assert
            var result = processor.CanCreateTrendline(baseExtremumGroup, footholdExtremumGroup);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanCreateTrendlines_ReturnTrue_IfDistanceIsSuitable()
        {

            //Arrange
            Mock<IProcessManager> mockManager = new Mock<IProcessManager>();
            Price basePrice = getPrice(100);
            Extremum baseMaster = new Extremum(basePrice, ExtremumType.PeakByClose);
            ExtremumGroup baseExtremumGroup = new ExtremumGroup(baseMaster, null, true);

            Price footholdPrice = getPrice(200);
            Extremum footholdMaster = new Extremum(footholdPrice, ExtremumType.PeakByClose);
            ExtremumGroup footholdExtremumGroup = new ExtremumGroup(footholdMaster, null, true);

            //Act
            TrendlineProcessor processor = new TrendlineProcessor(mockManager.Object);
            processor.MaxDistanceBetweenExtrema = 150;

            //Assert
            var result = processor.CanCreateTrendline(baseExtremumGroup, footholdExtremumGroup);
            Assert.IsTrue(result);
        }

        #endregion CAN_CREATE_TRENDLINES



        #region GET_CHART_POINTS

        [TestMethod]
        public void GetChartPoints_IfOnlyMasterPeakExtremumWithLongShadow_OnlyLimitedNumberOfPointsIsCreated()
        {

            //Arrange
            DataSet ds5 = getDataSet(5);
            Price basePrice = getPrice(ds5);
            Extremum master = new Extremum(basePrice, ExtremumType.PeakByClose);
            Quotation quotation5 = new Quotation(ds5) { Id = 5, Open = 1.09193, High = 1.09307, Low = 1.09165, Close = 1.09207, Volume = 1819 };
            ExtremumGroup extremumGroup = new ExtremumGroup(master, null, true);
            Mock<IProcessManager> mockManager = new Mock<IProcessManager>();
            mockManager.Setup(m => m.GetDataSet(5)).Returns(ds5);

            //Act
            TrendlineProcessor processor = new TrendlineProcessor(mockManager.Object);

            //Assert
            processor.MaxChartPointsForExtremumGroup = 6;
            processor.MinDistanceBetweenChartPoints = 0.0001;
            var result = processor.GetChartPoints(extremumGroup);
            var expectedResult = new List<ChartPoint>();
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09207));
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09227));
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09247));
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09267));
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09287));
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09307));
            var isEqual = expectedResult.HasEqualItems(result);
            Assert.IsTrue(isEqual);

        }

        [TestMethod]
        public void GetChartPoints_IfOnlyMasterPeakExtremumWithShortShadow_PointsDifferByOnePipBetweenThemselves()
        {

            //Arrange
            DataSet ds5 = getDataSet(5);
            Price price5 = getPrice(ds5);
            Quotation quotation5 = new Quotation(ds5) { Id = 5, Open = 1.09193, High = 1.09209, Low = 1.09165, Close = 1.09207, Volume = 1819 };
            Extremum master = new Extremum(price5, ExtremumType.PeakByClose);
            ExtremumGroup extremumGroup = new ExtremumGroup(master, null, true);
            Mock<IProcessManager> mockManager = new Mock<IProcessManager>();
            mockManager.Setup(m => m.GetDataSet(5)).Returns(ds5);

            //Act
            TrendlineProcessor processor = new TrendlineProcessor(mockManager.Object);
            processor.MinDistanceBetweenChartPoints = 1;

            //Assert
            processor.MaxChartPointsForExtremumGroup = 6;
            processor.MinDistanceBetweenChartPoints = 0.0001;
            var result = processor.GetChartPoints(extremumGroup);
            var expectedResult = new List<ChartPoint>();
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09207));
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09209));
            var isEqual = expectedResult.HasEqualItems(result);
            Assert.IsTrue(isEqual);

        }

        [TestMethod]
        public void GetChartPoints_IfOnlySlavePeakExtremumWithLongShadow_PointsFromShadowTopToHighestAdjacentShadow()
        {

            //Arrange
            DataSet ds5 = getDataSet(5);
            Price price5 = getPrice(ds5);
            Quotation quotation5 = new Quotation(ds5) { Id = 5, Open = 1.09193, High = 1.0927, Low = 1.09165, Close = 1.09207, Volume = 1819 };
            Extremum slave = new Extremum(price5, ExtremumType.PeakByHigh);
            ExtremumGroup extremumGroup = new ExtremumGroup(null, slave, true);
            Mock<IProcessManager> mockManager = new Mock<IProcessManager>();
            mockManager.Setup(m => m.GetDataSet(5)).Returns(ds5);

            //Act
            TrendlineProcessor processor = new TrendlineProcessor(mockManager.Object);
            processor.MinDistanceBetweenChartPoints = 1;

            //Assert
            processor.MaxChartPointsForExtremumGroup = 6;
            processor.MinDistanceBetweenChartPoints = 0.0001;
            var result = processor.GetChartPoints(extremumGroup);
            var expectedResult = new List<ChartPoint>();
            expectedResult.Add(new ChartPoint(slave.GetIndexNumber(), 1.0927));
            expectedResult.Add(new ChartPoint(slave.GetIndexNumber(), 1.09207));
            expectedResult.Add(new ChartPoint(slave.GetIndexNumber(), 1.092574));
            expectedResult.Add(new ChartPoint(slave.GetIndexNumber(), 1.092196));
            expectedResult.Add(new ChartPoint(slave.GetIndexNumber(), 1.092448));
            expectedResult.Add(new ChartPoint(slave.GetIndexNumber(), 1.092322));
            var isEqual = expectedResult.HasEqualItems(result);
            Assert.IsTrue(isEqual);

        }

        [TestMethod]
        public void GetChartPoints_IfOnlySlavePeakExtremumWithShortShadow_PointsFromShadowTopToHighestAdjacentShadow()
        {

            //Arrange
            DataSet ds5 = getDataSet(5);
            Price price5 = getPrice(ds5);
            Quotation quotation5 = new Quotation(ds5) { Id = 5, Open = 1.09193, High = 1.09209, Low = 1.09165, Close = 1.09207, Volume = 1819 };
            Extremum slave = new Extremum(price5, ExtremumType.PeakByHigh);
            ExtremumGroup extremumGroup = new ExtremumGroup(null, slave, true);
            Mock<IProcessManager> mockManager = new Mock<IProcessManager>();
            mockManager.Setup(m => m.GetDataSet(5)).Returns(ds5);

            //Act
            TrendlineProcessor processor = new TrendlineProcessor(mockManager.Object);
            processor.MinDistanceBetweenChartPoints = 1;

            //Assert
            processor.MaxChartPointsForExtremumGroup = 6;
            processor.MinDistanceBetweenChartPoints = 0.0001;
            var result = processor.GetChartPoints(extremumGroup);
            var expectedResult = new List<ChartPoint>();
            expectedResult.Add(new ChartPoint(slave.GetIndexNumber(), 1.09207));
            expectedResult.Add(new ChartPoint(slave.GetIndexNumber(), 1.09209));
            var isEqual = expectedResult.HasEqualItems(result);
            Assert.IsTrue(isEqual);

        }

        [TestMethod]
        public void GetChartPoints_IfBothPeakExtremaAndSlaveEarlier_PointsAreProperlyGenerated()
        {

            //Arrange
            DataSet ds5 = getDataSet(5);
            Price price5 = getPrice(ds5);
            Quotation quotation5 = new Quotation(ds5) { Id = 5, Open = 1.09193, High = 1.09307, Low = 1.09165, Close = 1.09207, Volume = 1819 };
            Extremum master = new Extremum(price5, ExtremumType.PeakByClose);
            ExtremumGroup extremumGroup = new ExtremumGroup(master, null, true);
            Mock<IProcessManager> mockManager = new Mock<IProcessManager>();
            mockManager.Setup(m => m.GetDataSet(5)).Returns(ds5);

            //Act
            TrendlineProcessor processor = new TrendlineProcessor(mockManager.Object);

            //Assert
            processor.MaxChartPointsForExtremumGroup = 6;
            processor.MinDistanceBetweenChartPoints = 0.0001;
            var result = processor.GetChartPoints(extremumGroup);
            var expectedResult = new List<ChartPoint>();
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09207));
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09227));
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09247));
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09267));
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09287));
            expectedResult.Add(new ChartPoint(master.GetIndexNumber(), 1.09307));
            var isEqual = expectedResult.HasEqualItems(result);
            Assert.IsTrue(isEqual);

        }

        [TestMethod]
        public void GetChartPoints_IfBothPeakExtremaAndSlaveLater_PointsAreProperlyGenerated()
        {

        }





        [TestMethod]
        public void GetChartPoints_IfOnlyMasterTroughExtremumWithLongShadow_OnlyLimitedNumberOfPointsIsCreated()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void GetChartPoints_IfOnlyMasterTroughExtremumWithShortShadow_PointsDifferByOnePipBetweenThemselves()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void GetChartPoints_IfOnlySlaveTroughExtremumWithLongShadow_PointsFromShadowTopToHighestAdjacentShadow()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void GetChartPoints_IfOnlySlaveTroughExtremumWithShortShadow_PointsFromShadowTopToHighestAdjacentShadow()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void GetChartPoints_IfBothTroughExtremaAndSlaveEarlier_PointsAreProperlyGenerated()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void GetChartPoints_IfBothTroughExtremaAndSlaveLater_PointsAreProperlyGenerated()
        {
            Assert.Fail("Not implemented");
        }





        #endregion GET_CHART_POINTS



        #region PROCESS

        [TestMethod]
        public void Process_IfNewTrendlineAndStartIndexLaterThanGivenItem_NothingHappens()
        {

            //Arrange
            Mock<IProcessManager> mockManager = null; // getDefaultMockManager(4);
            //Extremum baseMaster = new Extremum(getDefaultAtsSettings(), ExtremumType.PeakByClose, 4);
            //Extremum secondMaster = new Extremum(getDefaultAtsSettings(), ExtremumType.PeakByClose, 10);

            //Act
            Trendline trendline = new Trendline(null);
            TrendlineProcessor processor = new TrendlineProcessor(mockManager.Object);

            //processor.Process(trendline, 

            //Assert
            Assert.Fail("Not implemented yet");

        }

        [TestMethod]
        public void Process_IfNewTrendlineAndGivenItemIsStartIndex_PeakObjectIsCreated()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_IfSlaveIndexOFExtremumIsProcessed_ItIsCalculatedLikeRegularQuotation()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_AfterNormalQuotationProcessed_CounterIsLargerByOne()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_AfterNormalQuotationProcessed_TotalDistanceFromTrendlineIsProper()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_AfterBreakIsFound_TrendlineTypeIsChanged()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_AfterBreakIsFound_CurrentRangeIsClosedWithNextBreakObjectSet()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_AfterBreakIsFound_IfNextQuotationIsBackToTrend_SetThisTrendBreakAsSingleSession()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_AfterBreakIsFoundAsSingleSession_IfSecondQuotationIsProcessedItIsSkipped()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_AfterBreakIsFound_FromNextQuoationNextRangeIsStarted()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_IfExtremumTooFarFromTrendline_ItIsCalculatedAsRegularQuotation()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_IfExtremumCloseEnoughToTrendline_NewTrendHitIsCreated()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_IfExtremumCloseEnoughToTrendline_CurrentRangeIsClosed()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_IfExtremumCloseEnoughToTrendline_IfSlaveIndexProcessedAfterward_ItIsSkipped()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_IfTrendHitIsSet_IfNextQuotationAfterSlaveIndexIsProcessed_NewRangeIsCreated()
        {
            Assert.Fail("Not implemented yet");
        }

        [TestMethod]
        public void Process_IfTrendHitIsSet_()
        {
            Assert.Fail("Not implemented yet");
        }

        #endregion PROCESS

    }
}
