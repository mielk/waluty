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
        private const int DEFAULT_START_INDEX = 84;
        private const double DEFAULT_START_LEVEL = 1.1654;
        private const int DEFAULT_END_INDEX = 134;
        private const double DEFAULT_END_LEVEL = 1.1754;
        private const double DEFAULT_VALUE = 1.234;
        private const int DEFAULT_LAST_UPDATE_INDEX = 154;



        #region CONSTRUCTOR

        [TestMethod]
        public void Constructor_newInstance_hasProperParameters()
        {

            //Act.
            var trendline = new Trendline(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID, DEFAULT_START_INDEX, DEFAULT_START_LEVEL, DEFAULT_END_INDEX, DEFAULT_END_LEVEL) 
            { 
                Id = DEFAULT_ID, 
                Value = DEFAULT_VALUE,
                LastUpdateIndex = DEFAULT_LAST_UPDATE_INDEX
            };

            //Assert.
            Assert.AreEqual(DEFAULT_ID, trendline.Id);
            Assert.AreEqual(DEFAULT_ASSET_ID, trendline.AssetId);
            Assert.AreEqual(DEFAULT_TIMEFRAME_ID, trendline.TimeframeId);
            Assert.AreEqual(DEFAULT_SIMULATION_ID, trendline.SimulationId);
            Assert.AreEqual(DEFAULT_START_INDEX, trendline.StartIndex);
            Assert.AreEqual(DEFAULT_START_LEVEL, trendline.StartLevel);
            Assert.AreEqual(DEFAULT_END_INDEX, trendline.EndIndex);
            Assert.AreEqual(DEFAULT_END_LEVEL, trendline.EndLevel);
            Assert.AreEqual(DEFAULT_VALUE, trendline.Value);
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
                EndIndex = DEFAULT_END_INDEX, 
                EndLevel = DEFAULT_END_LEVEL,
                Value = DEFAULT_VALUE,
                LastUpdateIndex = DEFAULT_LAST_UPDATE_INDEX
            };

            var trendline = Trendline.FromDto(trendlineDto);

            //Assert.
            Assert.AreEqual(DEFAULT_ID, trendline.Id);
            Assert.AreEqual(DEFAULT_ASSET_ID, trendline.AssetId);
            Assert.AreEqual(DEFAULT_TIMEFRAME_ID, trendline.TimeframeId);
            Assert.AreEqual(DEFAULT_SIMULATION_ID, trendline.SimulationId);
            Assert.AreEqual(DEFAULT_START_INDEX, trendline.StartIndex);
            Assert.AreEqual(DEFAULT_START_LEVEL, trendline.StartLevel);
            Assert.AreEqual(DEFAULT_END_INDEX, trendline.EndIndex);
            Assert.AreEqual(DEFAULT_END_LEVEL, trendline.EndLevel);
            Assert.AreEqual(DEFAULT_VALUE, trendline.Value);
            Assert.AreEqual(DEFAULT_LAST_UPDATE_INDEX, trendline.LastUpdateIndex);

        }

        #endregion CONSTRUCTOR



        #region TO_DTO

        [TestMethod]
        public void ToDto_returnProperDto()
        {

            //Act
            var trendline = new Trendline(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID, DEFAULT_START_INDEX, DEFAULT_START_LEVEL, DEFAULT_END_INDEX, DEFAULT_END_LEVEL) 
            { 
                Id = DEFAULT_ID, 
                Value = DEFAULT_VALUE,
                LastUpdateIndex = DEFAULT_LAST_UPDATE_INDEX
            };
            var trendlineDto = trendline.ToDto();

            //Assert.
            Assert.AreEqual(DEFAULT_ID, trendlineDto.Id);
            Assert.AreEqual(DEFAULT_ASSET_ID, trendlineDto.AssetId);
            Assert.AreEqual(DEFAULT_TIMEFRAME_ID, trendlineDto.TimeframeId);
            Assert.AreEqual(DEFAULT_SIMULATION_ID, trendlineDto.SimulationId);
            Assert.AreEqual(DEFAULT_START_INDEX, trendlineDto.StartIndex);
            Assert.AreEqual(DEFAULT_START_LEVEL, trendlineDto.StartLevel);
            Assert.AreEqual(DEFAULT_END_INDEX, trendlineDto.EndIndex);
            Assert.AreEqual(DEFAULT_END_LEVEL, trendlineDto.EndLevel);
            Assert.AreEqual(DEFAULT_VALUE, trendlineDto.Value);
            Assert.AreEqual(DEFAULT_LAST_UPDATE_INDEX, trendlineDto.LastUpdateIndex);

        }


        #endregion TO_DTO



        #region EQUALS

        private Trendline getDefaultTrendline()
        {
            var trendline = new Trendline(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID, DEFAULT_START_INDEX, DEFAULT_START_LEVEL, DEFAULT_END_INDEX, DEFAULT_END_LEVEL)
            {
                Id = DEFAULT_ID,
                Value = DEFAULT_VALUE,
                LastUpdateIndex = DEFAULT_LAST_UPDATE_INDEX
            };
            return trendline;

        }


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

        public void Equals_ReturnsFalse_IfEndIndexIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.EndIndex = comparedItem.EndIndex++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        public void Equals_ReturnsFalse_IfEndLevelIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultTrendline();
            var comparedItem = getDefaultTrendline();

            //Act
            comparedItem.EndLevel += 1;
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


    }
}
