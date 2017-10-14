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

        //private double MAX_DOUBLE_COMPARISON_DIFFERENCE = 0.00000000001;


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
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void GetChartPoints_IfOnlyMasterPeakExtremumWithShortShadow_PointsDifferByOnePipBetweenThemselves()
        {
            Assert.Fail("Not implemented");
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
