using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.DAL.TransferObjects;

namespace Stock_UnitTest.Stock.Domain
{
    [TestClass]
    public class DataSetUnitTests
    {

        private DateTime DEFAULT_DATETIME = new DateTime(2017, 3, 4, 21, 10, 0);
        private const int DEFAULT_ASSET_ID = 1;
        private const int DEFAULT_TIMEFRAME_ID = 1;
        private const int DEFAULT_INDEX_NUMBER = 1;

        #region EQUALS

        private DataSet getDefaultDataSet()
        {
            return new DataSet(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_DATETIME, DEFAULT_INDEX_NUMBER);
        }

        private Quotation getDefaultQuotation()
        {
            return new Quotation()
            {
                Id = 1,
                Date = DEFAULT_DATETIME,
                AssetId = DEFAULT_ASSET_ID,
                TimeframeId = DEFAULT_TIMEFRAME_ID,
                IndexNumber = DEFAULT_INDEX_NUMBER,
                Open = 1.234,
                High = 1.45,
                Low = 1.11,
                Close = 1.42,
                Volume = 200
            };
        }

        private Price getDefaultPrice()
        {
            return new Price()
            {
                Id = 1,
                Date = DEFAULT_DATETIME,
                AssetId = DEFAULT_ASSET_ID,
                TimeframeId = DEFAULT_TIMEFRAME_ID,
                IndexNumber = DEFAULT_INDEX_NUMBER,
                CloseDelta = 1.04,
                Direction2D = 1,
                Direction3D = 1,
                PriceGap = 0.05,
                CloseRatio = 0.23,
                ExtremumRatio = 1,
            };
        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfComparedToObjectOfOtherType()
        {

            //Arrange
            var baseItem = getDefaultDataSet();
            var comparedItem = new { Id = 1 };

            //Act
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfAllPropertiesAreEqualAndAllObjectsAreNull()
        {

            //Arrange
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

            //Act
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfDateIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

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
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

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
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

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
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

            //Act
            comparedItem.IndexNumber++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfAllPropertiesTheSameAndBothHaveEqualQuotation()
        {

            //Arrange
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

            //Act
            baseItem.SetQuotation(getDefaultQuotation());
            comparedItem.SetQuotation(getDefaultQuotation());
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfAllPropertiesTheSameButBothHaveDifferentQuotation()
        {

            //Arrange
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

            //Act
            Quotation baseQuotation = getDefaultQuotation();
            Quotation comparedQuotation = getDefaultQuotation();
            comparedQuotation.AssetId++;
            baseItem.SetQuotation(baseQuotation);
            comparedItem.SetQuotation(comparedQuotation);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfAllPropertiesTheSameButOnlyBaseItemHaveNullQuotation()
        {

            //Arrange
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

            //Act
            comparedItem.SetQuotation(getDefaultQuotation());
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfAllPropertiesTheSameButOnlyComparedItemHaveNullQuotation()
        {

            //Arrange
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

            //Act
            baseItem.SetQuotation(getDefaultQuotation());
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfAllPropertiesTheSameAndBothHaveEqualPrice()
        {

            //Arrange
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

            //Act
            baseItem.SetPrice(getDefaultPrice());
            comparedItem.SetPrice(getDefaultPrice());
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfAllPropertiesTheSameButBothHaveDifferentPrice()
        {

            //Arrange
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

            //Act
            Price basePrice = getDefaultPrice();
            Price comparedPrice = getDefaultPrice();
            comparedPrice.AssetId++;
            baseItem.SetPrice(basePrice);
            comparedItem.SetPrice(comparedPrice);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfAllPropertiesTheSameButOnlyBaseItemHaveNullPrice()
        {

            //Arrange
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

            //Act
            comparedItem.SetPrice(getDefaultPrice());
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfAllPropertiesTheSameButOnlyComparedItemHaveNullPrice()
        {

            //Arrange
            var baseItem = getDefaultDataSet();
            var comparedItem = getDefaultDataSet();

            //Act
            baseItem.SetPrice(getDefaultPrice());
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        #endregion EQUALS


    }
}
