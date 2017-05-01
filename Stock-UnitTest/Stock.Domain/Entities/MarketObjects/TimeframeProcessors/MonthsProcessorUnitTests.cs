using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities.MarketObjects.TimeframeProcessors;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock_UnitTest.Stock.Domain.Entities.MarketObjects.TimeframeProcessors
{
    [TestClass]
    public class MonthsProcessorUnitTests
    {

        #region GET_PROPER_DATETIME

        [TestMethod]
        public void GetProperDateTime_ReturnsTheSameValue_ForFirstDayOfMonth()
        {

            //Arrange
            MonthsProcessor processor = new MonthsProcessor();
            DateTime baseDate = new DateTime(2016, 8, 1, 0, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetProperDateTime(baseDate, 1);

            //Assert
            DateTime expectedDateTime = baseDate;
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }


        [TestMethod]
        public void GetProperDateTime_ReturnsFirstDayOfMonth_ForNotFirstDayOfMonth()
        {

            //Arrange
            MonthsProcessor processor = new MonthsProcessor();
            DateTime baseDate = new DateTime(2016, 8, 13, 0, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetProperDateTime(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 1, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }


        #endregion GET_PROPER_DATETIME


        #region GET_NEXT

        [TestMethod]
        public void GetNext_ReturnsProperValue_ForFirstDayOfMonth()
        {

            //Arrange
            MonthsProcessor processor = new MonthsProcessor();
            DateTime baseDate = new DateTime(2017, 5, 1, 0, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 6, 1, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetNext_ReturnsProperValue_ForOtherThanFirstDay()
        {

            //Arrange
            MonthsProcessor processor = new MonthsProcessor();
            DateTime baseDate = new DateTime(2017, 5, 7, 16, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 6, 1, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        #endregion GET_NEXT



    }
}
