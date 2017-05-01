using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities.MarketObjects.TimeframeProcessors;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock_UnitTest.Stock.Domain.Entities.MarketObjects.TimeframeProcessors
{
    [TestClass]
    public class DaysProcessorUnitTests
    {


        #region GET_PROPER_DATETIME

        [TestMethod]
        public void GetProperDateTime_ReturnsTheSameValue_IfProperValueIsPassed()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            DateTime baseDate = new DateTime(2016, 8, 11, 0, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetProperDateTime(baseDate, 1);

            //Assert
            DateTime expectedDateTime = baseDate;
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }


        [TestMethod]
        public void GetProperDateTime_ReturnsFriday_ForWeekendDatetime()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            DateTime baseDate = new DateTime(2016, 8, 13, 0, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetProperDateTime(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 12, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }


        [TestMethod]
        public void GetProperDateTime_ReturnsLastValidValueBefore_ForHolidayValue()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            processor.AddHoliday(new DateTime(2016, 1, 1, 0, 0, 0));
            DateTime baseDate = new DateTime(2016, 1, 1, 0, 0, 0);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2015, 12, 31, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, result);

        }

        [TestMethod]
        public void GetProperDateTime_ReturnsLastValidValueBefore_ForHolidayValueIfTwoPreviousDaysAreAlsoHoliday()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            processor.AddHoliday(new DateTime(2016, 1, 1, 0, 0, 0));
            processor.AddHoliday(new DateTime(2015, 12, 31, 0, 0, 0));
            processor.AddHoliday(new DateTime(2015, 12, 30, 0, 0, 0));
            DateTime baseDate = new DateTime(2016, 1, 1, 0, 0, 0);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2015, 12, 29, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, result);

        }

        [TestMethod]
        public void GetProperDateTime_ReturnsLastValidValueBefore_ForHolidayValueIfPreviousDayIsWeekend()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            processor.AddHoliday(new DateTime(2017, 5, 1, 0, 0, 0));
            DateTime baseDate = new DateTime(2017, 5, 1, 0, 0, 0);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 4, 28, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, result);

        }

        [TestMethod]
        public void GetProperDateTime_ReturnsLastValidValueBefore_ForWeekendIfFridayWasHoliday()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            processor.AddHoliday(new DateTime(2017, 4, 7, 0, 0, 0));
            DateTime baseDate = new DateTime(2017, 4, 8, 0, 0, 0);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 4, 6, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, result);

        }


        [TestMethod]
        public void GetProperDateTime_ReturnsDateTimeRoundedDown_ForTimeBetweenFullPeriods()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            DateTime baseDate = new DateTime(2017, 5, 8, 16, 14, 27);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 8, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, result);

        }

        [TestMethod]
        public void GetProperDateTime_ReturnsProperDateTime_ForTimeOnEdgeOfFullPeriod()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            DateTime baseDate = new DateTime(2017, 5, 8, 0, 0, 0);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 8, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, result);

        }

        #endregion GET_PROPER_DATETIME



        #region GET_NEXT

        [TestMethod]
        public void GetNext_ReturnsProperValue_ForTimestampBetweenFullPeriods()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            DateTime baseDate = new DateTime(2017, 5, 4, 15, 13, 21);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 5, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetNext_ReturnsProperValue_ForTimestampOnPeriodEdge()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            DateTime baseDate = new DateTime(2017, 5, 3, 0, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 4, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetNext_ReturnsProperValue_ForLastQuotationBeforeWeekend()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            DateTime baseDate = new DateTime(2017, 4, 28, 0, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 1, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetNext_ReturnsProperValue_ForWeekendValue()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            DateTime baseDate = new DateTime(2017, 4, 30, 16, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 1, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetNext_ReturnsProperValue_ForLastQuotationBeforeHoliday()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            processor.AddHoliday(new DateTime(2017, 5, 3));
            DateTime baseDate = new DateTime(2017, 5, 2, 0, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 4, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetNext_ReturnsProperValue_ForLastQuotationBeforeHolidayInFriday()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            processor.AddHoliday(new DateTime(2017, 5, 5));
            DateTime baseDate = new DateTime(2017, 5, 4, 0, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 8, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetNext_ReturnsProperValue_ForLastWeekQuotationIfMondayIsHoliday()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            processor.AddHoliday(new DateTime(2017, 5, 8, 0, 0, 0));
            DateTime baseDate = new DateTime(2017, 5, 5, 0, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 9, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        #endregion GET_NEXT



    }
}
