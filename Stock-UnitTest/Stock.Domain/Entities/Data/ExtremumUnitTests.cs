
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.DAL.TransferObjects;

namespace Stock_UnitTest.Stock.Domain
{
    [TestClass]
    public class ExtremumUnitTests
    {


        #region FROM_DTO

        [TestMethod]
        public void FromDto_ReturnsProperExtremumObject()
        {

            //Arrange

            ExtremumDto dto = new ExtremumDto() { 
                Id = 1, 
                SimulationId = 1,
                Date = new DateTime(2017, 5, 2, 12, 15, 0), 
                IndexNumber = 1, 
                AssetId = 1, 
                TimeframeId = 1, 
                LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), 
                ExtremumType = 1, 
                Volatility = 1.23, 
                EarlierCounter = 15, 
                EarlierAmplitude = 7.45, 
                EarlierChange1 = 1.12, 
                EarlierChange2 = 2.21, 
                EarlierChange3 = 3.12, 
                EarlierChange5 = 4.56, 
                EarlierChange10 = 5.28, 
                LaterCounter = 16, 
                LaterAmplitude = 1.23, 
                LaterChange1 = 0.72, 
                LaterChange2 = 0.54, 
                LaterChange3 = 1.57, 
                LaterChange5 = 2.41, 
                LaterChange10 = 3.15, 
                IsOpen = true, 
                Timestamp = DateTime.Now,
                Value = 123.42 
            };


            //Act
            Extremum actualExtremum = Extremum.FromDto(dto);

            //Assert
            Extremum expectedExtremum = new Extremum(1, 1, (ExtremumType)1, 1)
            {
                ExtremumId = 1,
                SimulationId = 1,
                Date = new DateTime(2017, 5, 2, 12, 15, 0),
                LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0),
                Volatility = 1.23,
                EarlierCounter = 15,
                EarlierAmplitude = 7.45,
                EarlierChange1 = 1.12,
                EarlierChange2 = 2.21,
                EarlierChange3 = 3.12,
                EarlierChange5 = 4.56,
                EarlierChange10 = 5.28,
                LaterCounter = 16,
                LaterAmplitude = 1.23,
                LaterChange1 = 0.72,
                LaterChange2 = 0.54,
                LaterChange3 = 1.57,
                LaterChange5 = 2.41,
                LaterChange10 = 3.15,
                IsOpen = true,
                Value = 123.42 
            };
            Assert.AreEqual(expectedExtremum, actualExtremum);

        }

        #endregion FROM_DTO


        #region TO_DTO

        [TestMethod]
        public void ToDto_ReturnsProperExtremumDtoObject()
        {

            //Arrange
            Extremum extremum = new Extremum(1, 1, (ExtremumType)1, 1)
            {
                ExtremumId = 1,
                SimulationId = 1,
                Date = new DateTime(2017, 5, 2, 12, 15, 0),
                LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0),
                Volatility = 1.23,
                EarlierCounter = 15,
                EarlierAmplitude = 7.45,
                EarlierChange1 = 1.12,
                EarlierChange2 = 2.21,
                EarlierChange3 = 3.12,
                EarlierChange5 = 4.56,
                EarlierChange10 = 5.28,
                LaterCounter = 16,
                LaterAmplitude = 1.23,
                LaterChange1 = 0.72,
                LaterChange2 = 0.54,
                LaterChange3 = 1.57,
                LaterChange5 = 2.41,
                LaterChange10 = 3.15,
                IsOpen = true,
                Value = 123.42
            };

            //Act
            ExtremumDto actualDto = extremum.ToDto();

            //Assert
            ExtremumDto expectedDto = new ExtremumDto()
            {
                Id = 1,
                SimulationId = 1,
                Date = new DateTime(2017, 5, 2, 12, 15, 0),
                IndexNumber = 1,
                AssetId = 1,
                TimeframeId = 1,
                LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0),
                ExtremumType = 1,
                Volatility = 1.23,
                EarlierCounter = 15,
                EarlierAmplitude = 7.45,
                EarlierChange1 = 1.12,
                EarlierChange2 = 2.21,
                EarlierChange3 = 3.12,
                EarlierChange5 = 4.56,
                EarlierChange10 = 5.28,
                LaterCounter = 16,
                LaterAmplitude = 1.23,
                LaterChange1 = 0.72,
                LaterChange2 = 0.54,
                LaterChange3 = 1.57,
                LaterChange5 = 2.41,
                LaterChange10 = 3.15,
                IsOpen = true,
                Value = 123.42
            };

            Assert.AreEqual(expectedDto, actualDto);

        }

        #endregion TO_DTO


        #region EQUALS

        private Extremum getDefaultExtremum()
        {
            return new Extremum(1, 1, (ExtremumType)1, 1)
            {
                ExtremumId = 1,
                Date = new DateTime(2017, 5, 2, 12, 15, 0),
                LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0),
                Volatility = 1.23,
                EarlierCounter = 15,
                EarlierAmplitude = 7.45,
                EarlierChange1 = 1.12,
                EarlierChange2 = 2.21,
                EarlierChange3 = 3.12,
                EarlierChange5 = 4.56,
                EarlierChange10 = 5.28,
                LaterCounter = 16,
                LaterAmplitude = 1.23,
                LaterChange1 = 0.72,
                LaterChange2 = 0.54,
                LaterChange3 = 1.57,
                LaterChange5 = 2.41,
                LaterChange10 = 3.15,
                IsOpen = true,
                Value = 123.42 
            };
        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfComparedToObjectOfOtherType()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
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
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfISimulationdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.SimulationId++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfExtremumDateIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.Date = comparedItem.Date.AddMinutes(5);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfAssetIdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

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
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.TimeframeId++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfIndexNumberIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.IndexNumber++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfExtremumTypeIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            int comparedType = (int)comparedItem.Type;
            comparedType++;
            comparedItem.Type = (ExtremumType)comparedType;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfVolatilityIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.Volatility += -1;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfEarlierCounterIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.EarlierCounter++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfEarlierAmplitudeIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.EarlierAmplitude += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfEarlierChange1IsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.EarlierChange1 += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfEarlierChange2IsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.EarlierChange2 += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfEarlierChange3IsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.EarlierChange3 += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfEarlierChange5IsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.EarlierChange5 += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }
        
        [TestMethod]
        public void Equals_ReturnsFalse_IfEarlierChange10IsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.EarlierChange10 += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }


        [TestMethod]
        public void Equals_ReturnsFalse_IfLaterCounterIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.LaterCounter++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfLaterAmplitudeIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.LaterAmplitude += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfLaterChange1IsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.LaterChange1 += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfLaterChange2IsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.LaterChange2 += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfLaterChange3IsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.LaterChange3 += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfLaterChange5IsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.LaterChange5 += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfLaterChange10IsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.LaterChange10 += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }


        [TestMethod]
        public void Equals_ReturnsFalse_IfIsOpenIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.IsOpen = !comparedItem.IsOpen;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfValueIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultExtremum();
            var comparedItem = getDefaultExtremum();

            //Act
            comparedItem.Value += 2.15;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }


        #endregion EQUALS



    }
}
