using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.TransferObjects;

namespace Stock_UnitTest.Stock.Domain.Entities.AnalysisObjects
{
    [TestClass]
    public class QuotationUnitTests
    {


        #region FROM_DTO

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
        public void FromDto_ReturnsProperObjecct()
        {

            //Arrange
            QuotationDto dto = getDefaultQuotationDto();

            //Act
            Quotation quotation = Quotation.FromDto(dto);
            Quotation expectedQuotation = getDefaultQuotation();

            //Assert
            var areEqual = expectedQuotation.Equals(quotation);
            Assert.IsTrue(areEqual);

        }

        #endregion FROM_DTO


        #region TO_DTO

        [TestMethod]
        public void ToDto_ReturnsProperObjecct()
        {

            //Arrange
            Quotation quotation = getDefaultQuotation();

            //Act
            QuotationDto dto = quotation.ToDto();
            QuotationDto expectedDto = getDefaultQuotationDto();

            //Assert
            var areEqual = expectedDto.Equals(dto);
            Assert.IsTrue(areEqual);

        }

        #endregion TO_DTO


        #region EQUALS

        private Quotation getDefaultQuotation()
        {
            return new Quotation()
            {
                Id = 1,
                Date = new DateTime(2017, 3, 4, 21, 10, 0),
                AssetId = 1,
                TimeframeId = 1,
                Open = 1.03,
                High = 1.04,
                Low = 1.02,
                Close = 1.04,
                Volume = 100,
                IndexNumber = 51
            };
        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfComparedToObjectOfOtherType()
        {

            //Arrange
            var baseItem = getDefaultQuotation();
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
            var baseItem = getDefaultQuotation();
            var comparedItem = getDefaultQuotation();

            //Act
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfQuotationIdIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotation();
            var comparedItem = getDefaultQuotation();

            //Act
            comparedItem.Id++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfPriceDateIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotation();
            var comparedItem = getDefaultQuotation();

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
            var baseItem = getDefaultQuotation();
            var comparedItem = getDefaultQuotation();

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
            var baseItem = getDefaultQuotation();
            var comparedItem = getDefaultQuotation();

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
            var baseItem = getDefaultQuotation();
            var comparedItem = getDefaultQuotation();

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
            var baseItem = getDefaultQuotation();
            var comparedItem = getDefaultQuotation();

            //Act
            comparedItem.Open += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfHighPriceIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotation();
            var comparedItem = getDefaultQuotation();

            //Act
            comparedItem.High += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfLowPriceIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotation();
            var comparedItem = getDefaultQuotation();

            //Act
            comparedItem.Low += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfClosePriceIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotation();
            var comparedItem = getDefaultQuotation();

            //Act
            comparedItem.Close += 0.015;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfVolumeIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultQuotation();
            var comparedItem = getDefaultQuotation();

            //Act
            comparedItem.Volume++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        #endregion EQUALS


    }
}
