using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.DAL.TransferObjects;

namespace Stock_UnitTest.Stock.Domain
{
    [TestClass]
    public class DataSetInfoUnitTests
    {

        private DateTime DEFAULT_START_DATETIME = new DateTime(2017, 3, 4, 21, 10, 0);
        private DateTime DEFAULT_END_DATETIME = new DateTime(2017, 3, 7, 21, 10, 0);
        private const double DEFAULT_MIN_LEVEL = 1.03245;
        private const double DEFAULT_MAX_LEVEL = 1.03845;
        private const int DEFAULT_COUNTER = 10;

        #region EQUALS

        private DataSetInfo getDefaultDataSetInfo()
        {
            return new DataSetInfo()
            {
                StartDate = DEFAULT_START_DATETIME,
                EndDate = DEFAULT_END_DATETIME,
                MinLevel = DEFAULT_MIN_LEVEL,
                MaxLevel = DEFAULT_MAX_LEVEL,
                Counter = DEFAULT_COUNTER
            };
        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfComparedToObjectOfOtherType()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
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
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }


        [TestMethod]
        public void Equals_ReturnsFalse_IfStartDateIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            comparedItem.StartDate = ((DateTime)comparedItem.StartDate).AddMinutes(5);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfBothStartDateAreNull()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            baseItem.StartDate = null;
            comparedItem.StartDate = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOnlyBaseStartDateIsNull()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            baseItem.StartDate = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOnlyComparedStartDateIsNull()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            comparedItem.StartDate = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }


        [TestMethod]
        public void Equals_ReturnsFalse_IfEndDateIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            comparedItem.EndDate = ((DateTime)comparedItem.EndDate).AddMinutes(5);
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfBothEndDateAreNull()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            baseItem.EndDate = null;
            comparedItem.EndDate = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOnlyBaseEndDateIsNull()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            baseItem.EndDate = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOnlyComparedEndDateIsNull()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            comparedItem.EndDate = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }


        [TestMethod]
        public void Equals_ReturnsFalse_IfMinLevelIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            comparedItem.MinLevel = ((double)comparedItem.MinLevel) + 0.01;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfBothMinLevelAreNull()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            baseItem.MinLevel = null;
            comparedItem.MinLevel = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOnlyBaseMinLevelIsNull()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            baseItem.MinLevel = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOnlyComparedMinLevelIsNull()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            comparedItem.MinLevel = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }



        [TestMethod]
        public void Equals_ReturnsFalse_IfMaxLevelIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            comparedItem.MaxLevel = ((double)comparedItem.MaxLevel) + 0.01;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfBothMaxLevelAreNull()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            baseItem.MaxLevel = null;
            comparedItem.MaxLevel = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOnlyBaseMaxLevelIsNull()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            baseItem.MaxLevel = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOnlyComparedMaxLevelIsNull()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            comparedItem.MaxLevel = null;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }


        [TestMethod]
        public void Equals_ReturnsFalse_IfCounterIsDifferent()
        {

            //Arrange
            var baseItem = getDefaultDataSetInfo();
            var comparedItem = getDefaultDataSetInfo();

            //Act
            comparedItem.Counter++;
            var areEqual = baseItem.Equals(comparedItem);

            //Assert
            Assert.IsFalse(areEqual);

        }

        #endregion EQUALS


    }
}
