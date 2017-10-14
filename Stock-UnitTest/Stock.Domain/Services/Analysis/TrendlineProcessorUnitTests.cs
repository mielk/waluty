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
        //private double MAX_DOUBLE_COMPARISON_DIFFERENCE = 0.00000000001;


        private AtsSettings getDefaultAtsSettings()
        {
            return new AtsSettings(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID);
        }


        #region CAN_CREATE_TRENDLINES

        [TestMethod]
        public void CanCreateTrendlines_ReturnFalse_IfDistanceBetweenExtremaIsGreaterThanLimit()
        {

            //Arrange
            Mock<IProcessManager> mockManager = new Mock<IProcessManager>();
            Extremum baseMaster = new Extremum(1, 1, ExtremumType.PeakByClose, 100);
            ExtremumGroup baseExtremumGroup = new ExtremumGroup(baseMaster, null, true);
            Extremum footholdMaster = new Extremum(1, 1, ExtremumType.PeakByClose, 500);
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
            Extremum baseMaster = new Extremum(1, 1, ExtremumType.PeakByClose, 100);
            Extremum footholdMaster = new Extremum(1, 1, ExtremumType.PeakByClose, 102);
            ExtremumGroup baseExtremumGroup = new ExtremumGroup(baseMaster, null, true);
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
            Extremum baseMaster = new Extremum(1, 1, ExtremumType.PeakByClose, 100);
            ExtremumGroup baseExtremumGroup = new ExtremumGroup(baseMaster, null, true);
            Extremum footholdMaster = new Extremum(1, 1, ExtremumType.PeakByClose, 200);
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
            Extremum master = new Extremum(getDefaultAtsSettings(), ExtremumType.PeakByClose, 5);
            Quotation quotation5 = new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09307, Low = 1.09165, Close = 1.09207, Volume = 1819, IndexNumber = 5 };
            DataSet ds5 = new DataSet(quotation5);
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
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09207));
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09217));
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09227));
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09237));
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09247));
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09257));
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09267));
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09277));
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09287));
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09297));
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09307));
            var isEqual = expectedResult.HasEqualItems(result);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void GetChartPoints_IfOnlyMasterPeakExtremumWithShortShadow_PointsDifferByOnePipBetweenThemselves()
        {

            //Arrange
            Extremum master = new Extremum(getDefaultAtsSettings(), ExtremumType.PeakByClose, 5);
            Quotation quotation5 = new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09209, Low = 1.09165, Close = 1.09207, Volume = 1819, IndexNumber = 5 };
            DataSet ds5 = new DataSet(quotation5);
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
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09207));
            expectedResult.Add(new ChartPoint(master.IndexNumber, 1.09209));
            var isEqual = expectedResult.HasEqualItems(result);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void GetChartPoints_IfOnlySlavePeakExtremumWithLongShadow_PointsFromShadowTopToHighestAdjacentShadow()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void GetChartPoints_IfOnlySlavePeakExtremumWithShortShadow_PointsFromShadowTopToHighestAdjacentShadow()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void GetChartPoints_IfBothPeakExtremaAndSlaveEarlier_PointsAreProperlyGenerated()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void GetChartPoints_IfBothPeakExtremaAndSlaveLater_PointsAreProperlyGenerated()
        {
            Assert.Fail("Not implemented");
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

    }
}
