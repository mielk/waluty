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
    public class SimulationUnitTests
    {
        private const int DEFAULT_ID = 1;
        private const int DEFAULT_ASSET_ID = 1;
        private const int DEFAULT_TIMEFRAME_ID = 1;
        private const string DEFAULT_NAME = "Simulation";



        #region CONSTRUCTOR

        [TestMethod]
        public void Constructor_newInstance_hasProperIdNameAndCurrencies()
        {

            //Act.
            var simulation = new Simulation() { Id = DEFAULT_ID, Name = DEFAULT_NAME, AssetId = DEFAULT_ASSET_ID, TimeframeId = DEFAULT_TIMEFRAME_ID };

            //Assert.
            Assert.AreEqual(DEFAULT_ID, simulation.Id);
            Assert.AreEqual(DEFAULT_NAME, simulation.Name);
            Assert.AreEqual(DEFAULT_ASSET_ID, simulation.AssetId);
            Assert.AreEqual(DEFAULT_TIMEFRAME_ID, simulation.TimeframeId);

        }

        [TestMethod]
        public void Constructor_fromDto_hasCorrectProperties()
        {

            //Act.
            SimulationDto dto = new SimulationDto
            {
                Id = DEFAULT_ID,
                Name = DEFAULT_NAME,
                AssetId = DEFAULT_ASSET_ID,
                TimeframeId = DEFAULT_TIMEFRAME_ID
            };
            var simulation = Simulation.FromDto(dto);

            //Assert.
            Assert.AreEqual(DEFAULT_ID, simulation.Id);
            Assert.AreEqual(DEFAULT_NAME, simulation.Name);
            Assert.AreEqual(DEFAULT_ASSET_ID, simulation.AssetId);
            Assert.AreEqual(DEFAULT_TIMEFRAME_ID, simulation.TimeframeId);

        }

        #endregion CONSTRUCTOR


        #region TO_DTO

        [TestMethod]
        public void ToDto_returnProperDto()
        {

            //Act.
            Simulation simulation = new Simulation
            {
                Id = DEFAULT_ID,
                Name = DEFAULT_NAME,
                AssetId = DEFAULT_ASSET_ID,
                TimeframeId = DEFAULT_TIMEFRAME_ID
            };
            var simulationDto = simulation.ToDto();

            //Assert.
            Assert.AreEqual(DEFAULT_ID, simulationDto.Id);
            Assert.AreEqual(DEFAULT_NAME, simulationDto.Name);
            Assert.AreEqual(DEFAULT_ASSET_ID, simulationDto.AssetId);
            Assert.AreEqual(DEFAULT_TIMEFRAME_ID, simulationDto.TimeframeId);

        }


        #endregion TO_DTO


        #region ADDING_LAST_UPDATES

        [TestMethod]
        public void AfterAddLastUpdate_ThisTimestampIsReturned()
        {

            //Arrange
            DateTime datetime = new DateTime(2017, 5, 2, 12, 0, 0);

            //Act
            Simulation simulation = new Simulation() { Id = 1, Name = "a", AssetId = 1, TimeframeId = 1 };
            simulation.AddLastUpdate(AnalysisType.Prices, datetime);

            //Assert
            DateTime? actualLastUpdate = simulation.GetLastUpdate(AnalysisType.Prices);
            Assert.IsTrue(actualLastUpdate.IsEqual(datetime));

        }

        #endregion ADDING_LAST_UPDATES


        #region EQUALS

        private Simulation getDefaultSimulation()
        {
            return new Simulation(){
                AssetId = DEFAULT_ASSET_ID, 
                TimeframeId = DEFAULT_TIMEFRAME_ID, 
                Id = DEFAULT_ID,
                Name = DEFAULT_NAME
            };
        }


        [TestMethod]
        public void Equals_ReturnsFalse_IfComparedToObjectOfOtherType()
        {

            //Arrange
            var baseItem = getDefaultSimulation();
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
            var baseItem = getDefaultSimulation();
            var comparedItem = getDefaultSimulation();

            //Act
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfIdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultSimulation();
            var comparedItem = getDefaultSimulation();

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
            var baseItem = getDefaultSimulation();
            var comparedItem = getDefaultSimulation();

            //Act
            comparedItem.AssetId++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfTimeframeIdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultSimulation();
            var comparedItem = getDefaultSimulation();

            //Act
            comparedItem.TimeframeId++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfNameIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultSimulation();
            var comparedItem = getDefaultSimulation();

            //Act
            comparedItem.Name += "a";
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfLastUpdatesDictionariesAreDifferent()
        {

            //Arrange
            var baseItem = getDefaultSimulation();
            var comparedItem = getDefaultSimulation();

            //Act
            baseItem.AddLastUpdate(AnalysisType.Prices, new DateTime(2017, 5, 2, 12, 0, 0));
            baseItem.AddLastUpdate(AnalysisType.Macd, new DateTime(2017, 5, 2, 12, 0, 0));
            comparedItem.AddLastUpdate(AnalysisType.Prices, new DateTime(2017, 5, 2, 12, 0, 0));
            comparedItem.AddLastUpdate(AnalysisType.Macd, null);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfLastUpdatesDictionariesAreEqual()
        {

            //Arrange
            var baseItem = getDefaultSimulation();
            var comparedItem = getDefaultSimulation();

            //Act
            baseItem.AddLastUpdate(AnalysisType.Prices, new DateTime(2017, 5, 2, 12, 0, 0));
            baseItem.AddLastUpdate(AnalysisType.Macd, new DateTime(2017, 5, 2, 12, 0, 0));
            comparedItem.AddLastUpdate(AnalysisType.Prices, new DateTime(2017, 5, 2, 12, 0, 0));
            comparedItem.AddLastUpdate(AnalysisType.Macd, new DateTime(2017, 5, 2, 12, 0, 0));
            
            //Assert
            var areEqual = baseItem.Equals(comparedItem);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfLastUpdatesDictionariesHaveDifferentNumberOfItems()
        {

            //Arrange
            var baseItem = getDefaultSimulation();
            var comparedItem = getDefaultSimulation();

            //Act
            baseItem.AddLastUpdate(AnalysisType.Prices, new DateTime(2017, 5, 2, 12, 0, 0));
            baseItem.AddLastUpdate(AnalysisType.Adx, new DateTime(2017, 5, 2, 12, 0, 0));
            baseItem.AddLastUpdate(AnalysisType.Macd, new DateTime(2017, 5, 2, 12, 0, 0));
            comparedItem.AddLastUpdate(AnalysisType.Prices, new DateTime(2017, 5, 2, 12, 0, 0));
            comparedItem.AddLastUpdate(AnalysisType.Adx, new DateTime(2017, 5, 2, 12, 0, 0));
            
            //Assert
            var areEqual = baseItem.Equals(comparedItem);
            Assert.IsFalse(areEqual);

        }
        #endregion EQUALS


        #region GET.ANALYSIS.TIMESTAMP.DTOS


        [TestMethod]
        public void GetAnalysisTimestampDtos_ReturnsEmptyContainer_IfThereIsNoEntries()
        {

            //Act.
            Simulation simulation = new Simulation
            {
                Id = DEFAULT_ID,
                Name = DEFAULT_NAME,
                AssetId = DEFAULT_ASSET_ID,
                TimeframeId = DEFAULT_TIMEFRAME_ID
            };

            //Assert.
            var result = simulation.GetAnalysisTimestampDtos();
            var isEmpty = (result.Count() == 0);
            Assert.IsTrue(isEmpty);

        }


        [TestMethod]
        public void GetAnalysisTimestampDtos_ReturnsProperContainer_IfThereAreEntries()
        {

            //Act.
            Simulation simulation = new Simulation
            {
                Id = DEFAULT_ID,
                Name = DEFAULT_NAME,
                AssetId = DEFAULT_ASSET_ID,
                TimeframeId = DEFAULT_TIMEFRAME_ID
            };
            simulation.AddLastUpdate((AnalysisType)2, new DateTime(2016, 5, 2, 12, 0, 0));
            simulation.AddLastUpdate((AnalysisType)3, new DateTime(2016, 5, 2, 13, 0, 0));
            simulation.AddLastUpdate((AnalysisType)4, new DateTime(2016, 5, 2, 14, 0, 0));

            AnalysisTimestampDto timestamp1 = new AnalysisTimestampDto() { SimulationId = 1, AnalysisTypeId = 2, LastAnalysedItem = new DateTime(2016, 5, 2, 12, 0, 0) };
            AnalysisTimestampDto timestamp2 = new AnalysisTimestampDto() { SimulationId = 1, AnalysisTypeId = 3, LastAnalysedItem = new DateTime(2016, 5, 2, 13, 0, 0) };
            AnalysisTimestampDto timestamp3 = new AnalysisTimestampDto() { SimulationId = 1, AnalysisTypeId = 4, LastAnalysedItem = new DateTime(2016, 5, 2, 14, 0, 0) };
            var expectedDtos = new AnalysisTimestampDto[] { timestamp1, timestamp2, timestamp3 };

            //Assert.
            var result = simulation.GetAnalysisTimestampDtos();
            var areEqual = expectedDtos.HasEqualItems(result);
            Assert.IsTrue(areEqual);

        }

        #endregion GET.ANALYSIS.TIMESTAMP.DTOS


    }
}
