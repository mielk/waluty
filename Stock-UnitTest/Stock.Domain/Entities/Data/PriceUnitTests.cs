using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.DAL.TransferObjects;

namespace Stock_UnitTest.Stock.Domain
{
    [TestClass]
    public class PriceUnitTests
    {


        #region FROM_DTO

        [TestMethod]
        public void FromDto_ReturnsProperPriceObject()
        {

            //Arrange
            PriceDto dto = new PriceDto()
            {
                Id = 1,
                PriceDate = new DateTime(2017, 5, 2, 12, 15, 0),
                IndexNumber = 1,
                AssetId = 1,
                TimeframeId = 1,
                DeltaClosePrice = 1.15,
                PriceDirection2D = 1,
                PriceDirection3D = 0,
                PriceGap = 0,
                CloseRatio = 1.45,
                ExtremumRatio = 2.12
            };

            //Act
            Price actualPrice = Price.FromDto(dto);

            //Assert
            Price expectedPrice = new Price()
            {
                Id = 1,
                Date = new DateTime(2017, 5, 2, 12, 15, 0),
                IndexNumber = 1,
                AssetId = 1,
                TimeframeId = 1,
                CloseDelta = 1.15,
                Direction2D = 1,
                Direction3D = 0,
                PriceGap = 0,
                CloseRatio = 1.45,
                ExtremumRatio = 2.12
            };
            Assert.AreEqual(expectedPrice, actualPrice);

        }

        #endregion FROM_DTO


        #region TO_DTO

        [TestMethod]
        public void ToDto_ReturnsProperPriceDtoObject()
        {

            //Arrange
            Price price = new Price()
            {
                Id = 1,
                Date = new DateTime(2017, 5, 2, 12, 15, 0),
                IndexNumber = 1,
                AssetId = 1,
                TimeframeId = 1,
                CloseDelta = 1.15,
                Direction2D = 1,
                Direction3D = 0,
                PriceGap = 0,
                CloseRatio = 1.45,
                ExtremumRatio = 2.12,
                PeakByClose = new Extremum(1, 1, ExtremumType.PeakByClose, 1)
            };

            //Act
            PriceDto actualDto = price.ToDto();

            //Assert
            PriceDto expectedDto = new PriceDto()
            {
                Id = 1,
                PriceDate = new DateTime(2017, 5, 2, 12, 15, 0),
                IndexNumber = 1,
                AssetId = 1,
                TimeframeId = 1,
                DeltaClosePrice = 1.15,
                PriceDirection2D = 1,
                PriceDirection3D = 0,
                PriceGap = 0,
                CloseRatio = 1.45,
                ExtremumRatio = 2.12
            };

            Assert.AreEqual(expectedDto, actualDto);

        }

        #endregion TO_DTO


        #region EQUALS

        private Price getDefaultPrice()
        {
            return new Price()
            {
                Id = 1,
                Date = new DateTime(2017, 3, 4, 21, 10, 0),
                AssetId = 1,
                TimeframeId = 1,
                CloseDelta = 1.04,
                Direction2D = 1,
                Direction3D = 1,
                PriceGap = 0.05,
                CloseRatio = 0.23,
                ExtremumRatio = 1,
                IndexNumber = 51
            };
        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfComparedToObjectOfOtherType()
        {

            //Arrange
            var baseItem = getDefaultPrice();
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
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfIdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.Id++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfISimulationdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.SimulationId++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfPriceDateIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

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
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

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
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

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
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.IndexNumber++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfDeltaClosePriceIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.CloseDelta += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfDirection2DIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.Direction2D *= -1;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfDirection3DIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.Direction3D *= -1;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfPriceGapIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.PriceGap += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfPeakByCloseIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            baseItem.PeakByClose = new Extremum(1, 1, ExtremumType.PeakByClose, 11) { Date = new DateTime(2017, 5, 2, 12, 5, 0) };
            comparedItem.PeakByClose = new Extremum(1, 1, ExtremumType.PeakByClose, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) };
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfPeakByCloseIsNullOnlyInBasePrice()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.PeakByClose = new Extremum(1, 1, ExtremumType.PeakByClose, 10);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfPeakByCloseIsNullOnlyInComparedPrice()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            baseItem.PeakByClose = new Extremum(1, 1, ExtremumType.PeakByClose, 10);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfPeakByCloseIsTheSameInBothPrices()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            baseItem.PeakByClose = new Extremum(1, 1, ExtremumType.PeakByClose, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) };
            comparedItem.PeakByClose = new Extremum(1, 1, ExtremumType.PeakByClose, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) }; 
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfPeakByHighIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            baseItem.PeakByHigh = new Extremum(1, 1, ExtremumType.PeakByHigh, 11) { Date = new DateTime(2017, 5, 2, 12, 5, 0) };
            comparedItem.PeakByHigh = new Extremum(1, 1, ExtremumType.PeakByHigh, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) };
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfPeakByHighIsNullOnlyInBasePrice()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.PeakByHigh = new Extremum(1, 1, ExtremumType.PeakByHigh, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) };
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfPeakByHighIsNullOnlyInComparedPrice()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            baseItem.PeakByHigh = new Extremum(1, 1, ExtremumType.PeakByHigh, 10);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfPeakByHighIsTheSameInBothPrices()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            baseItem.PeakByHigh = new Extremum(1, 1, ExtremumType.PeakByHigh, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) };
            comparedItem.PeakByHigh = new Extremum(1, 1, ExtremumType.PeakByHigh, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) };
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfTroughByCloseIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            baseItem.TroughByClose = new Extremum(1, 1, ExtremumType.TroughByClose, 11) { Date = new DateTime(2017, 5, 2, 12, 5, 0) };
            comparedItem.TroughByClose = new Extremum(1, 1, ExtremumType.TroughByClose, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) };
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfTroughByCloseIsNullOnlyInBasePrice()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.TroughByClose = new Extremum(1, 1, ExtremumType.TroughByClose, 10);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfTroughByCloseIsNullOnlyInComparedPrice()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            baseItem.TroughByClose = new Extremum(1, 1, ExtremumType.TroughByClose, 10);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfTroughByCloseIsTheSameInBothPrices()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            baseItem.TroughByClose = new Extremum(1, 1, ExtremumType.TroughByClose, 10);
            comparedItem.TroughByClose = new Extremum(1, 1, ExtremumType.TroughByClose, 10);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfTroughByLowIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            baseItem.TroughByLow = new Extremum(1, 1, ExtremumType.TroughByLow, 11) { Date = new DateTime(2017, 5, 2, 12, 5, 0) };
            comparedItem.TroughByLow = new Extremum(1, 1, ExtremumType.TroughByLow, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) };
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfTroughByLowIsNullOnlyInBasePrice()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.TroughByLow = new Extremum(1, 1, ExtremumType.TroughByLow, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) };
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfTroughByLowIsNullOnlyInComparedPrice()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            baseItem.TroughByLow = new Extremum(1, 1, ExtremumType.TroughByLow, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) };
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfTroughByLowIsTheSameInBothPrices()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            baseItem.TroughByLow = new Extremum(1, 1, ExtremumType.TroughByLow, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) };
            comparedItem.TroughByLow = new Extremum(1, 1, ExtremumType.TroughByLow, 10) { Date = new DateTime(2017, 5, 2, 12, 0, 0) };
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfCloseRatioIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.CloseRatio += 0.15;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfExtremumRatioIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultPrice();
            var comparedItem = getDefaultPrice();

            //Act
            comparedItem.ExtremumRatio += 0.15;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        #endregion EQUALS


        #region SETTERS

        [TestMethod]
        public void PriceSetExtremum_AfterSettingPeakByClose_ProperExtremumIsAssigned()
        {

            //Arrange
            Price price = getDefaultPrice();

            //Act
            Extremum peakByClose = new Extremum(price.GetAssetId(), price.GetTimeframeId(), ExtremumType.PeakByClose, price.GetIndexNumber());
            price.SetExtremum(peakByClose);

            //Assert
            Assert.IsTrue(peakByClose == price.PeakByClose);

        }

        [TestMethod]
        public void PriceSetExtremum_AfterSettingPeakByHigh_ProperExtremumIsAssigned()
        {

            //Arrange
            Price price = getDefaultPrice();

            //Act
            Extremum peakByHigh = new Extremum(price.GetAssetId(), price.GetTimeframeId(), ExtremumType.PeakByHigh, price.GetIndexNumber());
            price.SetExtremum(peakByHigh);

            //Assert
            Assert.IsTrue(peakByHigh == price.PeakByHigh);

        }

        [TestMethod]
        public void PriceSetExtremum_AfterSettingTroughByClose_ProperExtremumIsAssigned()
        {

            //Arrange
            Price price = getDefaultPrice();

            //Act
            Extremum troughByClose = new Extremum(price.GetAssetId(), price.GetTimeframeId(), ExtremumType.TroughByClose, price.GetIndexNumber());
            price.SetExtremum(troughByClose);

            //Assert
            Assert.IsTrue(troughByClose == price.TroughByClose);

        }

        [TestMethod]
        public void PriceSetExtremum_AfterSettingTroughByLow_ProperExtremumIsAssigned()
        {

            //Arrange
            Price price = getDefaultPrice();

            //Act
            Extremum troughByLow = new Extremum(price.GetAssetId(), price.GetTimeframeId(), ExtremumType.TroughByLow, price.GetIndexNumber());
            price.SetExtremum(troughByLow);

            //Assert
            Assert.IsTrue(troughByLow == price.TroughByLow);

        }
        #endregion SETTERS



    }
}
