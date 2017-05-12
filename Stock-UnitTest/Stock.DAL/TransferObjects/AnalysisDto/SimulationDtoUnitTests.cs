using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using System.Collections.Generic;
using Stock.DAL.TransferObjects;
using Stock.Domain.Services;
using Stock.Utils;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class SimulationDtoUnitTests
    {

        private SimulationDto getDefaultSimulationDto()
        {
            return new SimulationDto()
            {
                Id = 1,
                Name = "Simulation",
                AssetId = 1,
                TimeframeId = 1
            };
        }


        #region COPY_PROPERTIES

        [TestMethod]
        public void CopyProperties_AfterwardAllPropertiesAreEqual()
        {

            //Arrange
            var baseItem = new SimulationDto()
            {
                Id = 1,
                Name = "a",
                AssetId = 1,
                TimeframeId = 1
            };

            var comparedItem = new SimulationDto()
            {
                Id = 1,
                Name = "b",
                AssetId = 2,
                TimeframeId = 2
            };

            //Act
            comparedItem.CopyProperties(baseItem);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }


        #endregion COPY_PROPERTIES


        #region EQUALS

        [TestMethod]
        public void Equals_ReturnsFalse_IfComparedToObjectOfOtherType()
        {

            //Arrange
            var baseItem = getDefaultSimulationDto();
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
            var baseItem = getDefaultSimulationDto();
            var comparedItem = getDefaultSimulationDto();
            
            //Act
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }
        
        [TestMethod]
        public void Equals_ReturnsFalse_IfSimulationNameIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultSimulationDto();
            var comparedItem = getDefaultSimulationDto();

            //Act
            comparedItem.Name += "a";
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfAssetIdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultSimulationDto();
            var comparedItem = getDefaultSimulationDto();

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
            var baseItem = getDefaultSimulationDto();
            var comparedItem = getDefaultSimulationDto();

            //Act
            comparedItem.TimeframeId++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        #endregion EQUALS


    }

}
