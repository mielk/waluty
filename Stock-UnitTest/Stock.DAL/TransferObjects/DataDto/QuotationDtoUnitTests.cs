using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using System.Collections.Generic;
using Stock.DAL.TransferObjects;
using System.Linq;
using Stock.Domain.Services;
using Stock.Utils;
using Moq;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class QuotationDtoUnitTests
    {


        #region COPY_PROPERTIES

        [TestMethod]
        public void CopyProperties_AfterwardAllPropertiesAreEqual()
        {

            //Arrange
            var baseItem = new QuotationDto()
            {
                QuotationId = 1,
                PriceDate = new DateTime(2017, 3, 4, 21, 10, 0),
                AssetId = 1,
                TimeframeId = 1,
                OpenPrice = 1.03,
                HighPrice = 1.04,
                LowPrice = 1.02,
                ClosePrice = 1.04,
                Volume = 100,
                IndexNumber = 51
            };

            var comparedItem = new QuotationDto()
            {
                QuotationId = 2,
                PriceDate = new DateTime(2017, 3, 4, 21, 10, 0),
                AssetId = 2,
                TimeframeId = 1,
                OpenPrice = 1.05,
                HighPrice = 1.02,
                LowPrice = 1.01,
                ClosePrice = 1.03,
                Volume = 103,
                IndexNumber = 52
            };

            //Act
            comparedItem.CopyProperties(baseItem);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }


        #endregion COPY_PROPERTIES


        #region EQUALS

        private QuotationDto getDefaultQuotationDto()
        {
            return new QuotationDto()
            {
                QuotationId = 1,
                PriceDate = new DateTime(2017, 3, 4, 21, 10, 0),
                AssetId = 1,
                TimeframeId = 1,
                OpenPrice = 1.03,
                HighPrice = 1.04,
                LowPrice = 1.02,
                ClosePrice = 1.04,
                Volume = 100,
                IndexNumber = 51
            };
        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfComparedToObjectOfOtherType()
        {

            //Arrange
            var baseItem = getDefaultQuotationDto();
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
            var baseItem = getDefaultQuotationDto();
            var comparedItem = getDefaultQuotationDto();
            
            //Act
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfQuotationIdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotationDto();
            var comparedItem = getDefaultQuotationDto();

            //Act
            comparedItem.QuotationId++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfPriceDateIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotationDto();
            var comparedItem = getDefaultQuotationDto();

            //Act
            comparedItem.PriceDate = comparedItem.PriceDate.AddMinutes(5);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfAssetIdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotationDto();
            var comparedItem = getDefaultQuotationDto();

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
            var baseItem = getDefaultQuotationDto();
            var comparedItem = getDefaultQuotationDto();

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
            var baseItem = getDefaultQuotationDto();
            var comparedItem = getDefaultQuotationDto();

            //Act
            comparedItem.IndexNumber++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOpenPriceIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotationDto();
            var comparedItem = getDefaultQuotationDto();

            //Act
            comparedItem.OpenPrice += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfHighPriceIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotationDto();
            var comparedItem = getDefaultQuotationDto();

            //Act
            comparedItem.HighPrice += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfLowPriceIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotationDto();
            var comparedItem = getDefaultQuotationDto();

            //Act
            comparedItem.LowPrice += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfClosePriceIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotationDto();
            var comparedItem = getDefaultQuotationDto();

            //Act
            comparedItem.ClosePrice += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfVolumeIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotationDto();
            var comparedItem = getDefaultQuotationDto();

            //Act
            comparedItem.Volume++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        #endregion EQUALS


        [TestMethod]
        public void Testy_Dla_IsInIndexRane()
        {
            Assert.Fail("Not implemented yet");
        }


    }

}
