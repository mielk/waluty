using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities.MarketObjects.TimeframeProcessors;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock_UnitTest.Stock.Domain.Entities.MarketObjects.TimeframeProcessors
{
    [TestClass]
    public class MinutesProcessorUnitTests
    {


        #region GET_PROPER_DATETIME

        [TestMethod]
        public void GetProperDateTime_M5_ReturnsTheSameValue_IfProperValueIsPassed()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 11, 12, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetProperDateTime(baseDate, 5);

            //Assert
            DateTime expectedDateTime = baseDate;
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetProperDateTime_M15_ReturnsTheSameValue_IfProperValueIsPassed()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 11, 12, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetProperDateTime(baseDate, 15);

            //Assert
            DateTime expectedDateTime = baseDate;
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetProperDateTime_M30_ReturnsTheSameValue_IfProperValueIsPassed()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 11, 12, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetProperDateTime(baseDate, 30);

            //Assert
            DateTime expectedDateTime = baseDate;
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }



        [TestMethod]
        public void GetProperDateTime_M5_ReturnLastValueInWeek_ForWeekendDatetime()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 13, 16, 15, 0);

            //Act
            DateTime actualDateTime = processor.GetProperDateTime(baseDate, 5);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 12, 23, 55, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetProperDateTime_M15_ReturnLastValueInWeek_ForWeekendDatetime()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 13, 16, 15, 0);

            //Act
            DateTime actualDateTime = processor.GetProperDateTime(baseDate, 15);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 12, 23, 45, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetProperDateTime_M30_ReturnLastValueInWeek_ForWeekendDatetime()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 13, 16, 15, 0);

            //Act
            DateTime actualDateTime = processor.GetProperDateTime(baseDate, 30);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 12, 23, 30, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }



        [TestMethod]
        public void GetProperDateTime_ReturnsLastValidValueBefore_ForHolidayValue()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            processor.AddHoliday(new DateTime(2016, 1, 1, 0, 0, 0));
            DateTime baseDate = new DateTime(2016, 1, 1, 16, 0, 0);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 5);

            //Assert
            DateTime expectedDateTime = new DateTime(2015, 12, 31, 21, 0, 0);
            Assert.AreEqual(expectedDateTime, result);

        }

        [TestMethod]
        public void GetProperDateTime_ReturnsLastValidValueBefore_ForHolidayValueIfTwoPreviousDaysAreAlsoHoliday()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            processor.AddHoliday(new DateTime(2016, 1, 1, 0, 0, 0));
            processor.AddHoliday(new DateTime(2015, 12, 31, 0, 0, 0));
            processor.AddHoliday(new DateTime(2015, 12, 30, 0, 0, 0));
            DateTime baseDate = new DateTime(2016, 1, 1, 16, 0, 0);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 5);

            //Assert
            DateTime expectedDateTime = new DateTime(2015, 12, 29, 21, 0, 0);
            Assert.AreEqual(expectedDateTime, result);

        }

        [TestMethod]
        public void GetProperDateTime_ReturnsLastValidValueBefore_ForHolidayValueIfPreviousDayIsWeekend()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            processor.AddHoliday(new DateTime(2017, 5, 1, 0, 0, 0));
            DateTime baseDate = new DateTime(2017, 5, 1, 16, 0, 0);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 5);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 4, 28, 23, 55, 0);
            Assert.AreEqual(expectedDateTime, result);

        }


        [TestMethod]
        public void GetProperDateTime_ReturnsLastValidValueBefore_ForDayBeforeHoliday230000()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            processor.AddHoliday(new DateTime(2017, 5, 4, 0, 0, 0));
            DateTime baseDate = new DateTime(2017, 5, 3, 23, 0, 0);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 5);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 3, 21, 0, 0);
            Assert.AreEqual(expectedDateTime, result);

        }

        [TestMethod]
        public void GetProperDateTime_ReturnsLastValidValueBefore_ForWeekendIfFridayWasHoliday()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            processor.AddHoliday(new DateTime(2017, 4, 7, 0, 0, 0));
            DateTime baseDate = new DateTime(2017, 4, 8, 16, 0, 0);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 5);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 4, 6, 21, 0, 0);
            Assert.AreEqual(expectedDateTime, result);

        }

        [TestMethod]
        public void GetProperDateTime_ReturnsDateTimeRoundedDown_ForTimeBetweenFullPeriods()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2017, 5, 8, 16, 14, 27);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 5);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 8, 16, 10, 0);
            Assert.AreEqual(expectedDateTime, result);

        }

        [TestMethod]
        public void GetProperDateTime_ReturnsProperDateTime_ForTimeOnEdgeOfFullPeriod()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2017, 5, 8, 16, 15, 0);

            //Act
            DateTime result = processor.GetProperDateTime(baseDate, 5);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 8, 16, 15, 0);
            Assert.AreEqual(expectedDateTime, result);

        }

        #endregion GET_PROPER_DATETIME



        #region GET_NEXT

        [TestMethod]
        public void GetNext_M30_ReturnsProperValue_ForMiddleHoursValue()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 17, 15, 13, 21);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 30);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 17, 15, 30, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }


        [TestMethod]
        public void GetNext_M30_ReturnsProperValue_ForPeriodEdgeValue()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 17, 15, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 30);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 17, 15, 30, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }


        [TestMethod]
        public void GetNext_M30_ReturnsProperValue_ForLastQuotationBeforeWeekend()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2017, 4, 28, 23, 30, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 30);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 1, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }


        [TestMethod]
        public void GetNext_M30_ReturnsProperValue_ForWeekendValue()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 20, 16, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 30);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 22, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetNext_M30_ReturnsProperValue_ForLastQuotationBeforeHoliday()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            processor.AddHoliday(new DateTime(2017, 5, 3));
            DateTime baseDate = new DateTime(2017, 5, 2, 21, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 30);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 4, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }


        #endregion GET_NEXT


    }
}



        //[TestMethod]
        //[TestCategory("Next.M30")]
        //public void next_M30_returns_proper_value_for_last_quotations_before_newYear()
        //{
        //    DateTime d = new DateTime(2014, 12, 31, 21, 0, 0);
        //    Assert.AreEqual(new DateTime(2015, 1, 2, 0, 0, 0), d.getNext(TimeframeSymbol.M30));
        //}

        //[TestMethod]
        //[TestCategory("Next.M30")]
        //public void next_M30_returns_proper_value_for_last_quotations_before_friday_newYear()
        //{
        //    DateTime d = new DateTime(2015, 12, 31, 21, 0, 0);
        //    Assert.AreEqual(new DateTime(2016, 1, 4, 0, 0, 0), d.getNext(TimeframeSymbol.M30));
        //}

        //[TestMethod]
        //[TestCategory("Next.M30")]
        //public void next_M30_returns_proper_value_for_last_quotations_before_friday_christmas()
        //{
        //    DateTime d = new DateTime(2015, 12, 24, 21, 0, 0);
        //    Assert.AreEqual(new DateTime(2015, 12, 28, 0, 0, 0), d.getNext(TimeframeSymbol.M30));
        //}



        //[TestMethod]
        //[TestCategory("Next.M15")]
        //public void next_M15_returns_proper_value_for_middle_hours_value()
        //{
        //    DateTime d = new DateTime(2016, 8, 17, 15, 13, 21);
        //    Assert.AreEqual(new DateTime(2016, 8, 17, 15, 15, 0), d.getNext(TimeframeSymbol.M15));
        //}

        //[TestMethod]
        //[TestCategory("Next.M15")]
        //public void next_M15_returns_proper_value_for_middle_week_value()
        //{
        //    DateTime d = new DateTime(2016, 8, 17, 16, 0, 0);
        //    Assert.AreEqual(new DateTime(2016, 8, 17, 16, 15, 0), d.getNext(TimeframeSymbol.M15));
        //}

        //[TestMethod]
        //[TestCategory("Next.M15")]
        //public void next_M15_returns_proper_value_for_last_week_quotation()
        //{
        //    DateTime d = new DateTime(2016, 8, 19, 23, 45, 0);
        //    Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.M15));
        //}

        //[TestMethod]
        //[TestCategory("Next.M15")]
        //public void next_M15_returns_proper_value_for_weekend_value()
        //{
        //    DateTime d = new DateTime(2016, 8, 20, 16, 0, 0);
        //    Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.M15));
        //}

        //[TestMethod]
        //[TestCategory("Next.M15")]
        //public void next_M15_returns_proper_value_for_last_quotations_before_christmas()
        //{
        //    DateTime d = new DateTime(2014, 12, 24, 21, 0, 0);
        //    Assert.AreEqual(new DateTime(2014, 12, 26, 0, 0, 0), d.getNext(TimeframeSymbol.M15));
        //}

        //[TestMethod]
        //[TestCategory("Next.M15")]
        //public void next_M15_returns_proper_value_for_last_quotations_before_newYear()
        //{
        //    DateTime d = new DateTime(2014, 12, 31, 21, 0, 0);
        //    Assert.AreEqual(new DateTime(2015, 1, 2, 0, 0, 0), d.getNext(TimeframeSymbol.M15));
        //}

        //[TestMethod]
        //[TestCategory("Next.M15")]
        //public void next_M15_returns_proper_value_for_last_quotations_before_friday_newYear()
        //{
        //    DateTime d = new DateTime(2015, 12, 31, 21, 0, 0);
        //    Assert.AreEqual(new DateTime(2016, 1, 4, 0, 0, 0), d.getNext(TimeframeSymbol.M15));
        //}

        //[TestMethod]
        //[TestCategory("Next.M15")]
        //public void next_M15_returns_proper_value_for_last_quotations_before_friday_christmas()
        //{
        //    DateTime d = new DateTime(2015, 12, 24, 21, 0, 0);
        //    Assert.AreEqual(new DateTime(2015, 12, 28, 0, 0, 0), d.getNext(TimeframeSymbol.M15));
        //}




        //[TestMethod]
        //[TestCategory("Next.M5")]
        //public void next_M5_returns_proper_value_for_middle_hours_value()
        //{
        //    DateTime d = new DateTime(2016, 8, 17, 15, 3, 21);
        //    Assert.AreEqual(new DateTime(2016, 8, 17, 15, 5, 0), d.getNext(TimeframeSymbol.M5));
        //}

        //[TestMethod]
        //[TestCategory("Next.M5")]
        //public void next_M5_returns_proper_value_for_middle_week_value()
        //{
        //    DateTime d = new DateTime(2016, 8, 17, 16, 0, 0);
        //    Assert.AreEqual(new DateTime(2016, 8, 17, 16, 5, 0), d.getNext(TimeframeSymbol.M5));
        //}

        //[TestMethod]
        //[TestCategory("Next.M5")]
        //public void next_M5_returns_proper_value_for_last_week_quotation()
        //{
        //    DateTime d = new DateTime(2016, 8, 19, 23, 55, 0);
        //    Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.M5));
        //}

        //[TestMethod]
        //[TestCategory("Next.M5")]
        //public void next_M5_returns_proper_value_for_weekend_value()
        //{
        //    DateTime d = new DateTime(2016, 8, 20, 16, 0, 0);
        //    Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.M5));
        //}

        //[TestMethod]
        //[TestCategory("Next.M5")]
        //public void next_M5_returns_proper_value_for_last_quotations_before_christmas()
        //{
        //    DateTime d = new DateTime(2014, 12, 24, 21, 0, 0);
        //    Assert.AreEqual(new DateTime(2014, 12, 26, 0, 0, 0), d.getNext(TimeframeSymbol.M5));
        //}

        //[TestMethod]
        //[TestCategory("Next.M5")]
        //public void next_M5_returns_proper_value_for_last_quotations_before_newYear()
        //{
        //    DateTime d = new DateTime(2014, 12, 31, 21, 0, 0);
        //    Assert.AreEqual(new DateTime(2015, 1, 2, 0, 0, 0), d.getNext(TimeframeSymbol.M5));
        //}

        //[TestMethod]
        //[TestCategory("Next.M5")]
        //public void next_M5_returns_proper_value_for_last_quotations_before_friday_newYear()
        //{
        //    DateTime d = new DateTime(2015, 12, 31, 21, 0, 0);
        //    Assert.AreEqual(new DateTime(2016, 1, 4, 0, 0, 0), d.getNext(TimeframeSymbol.M5));
        //}

        //[TestMethod]
        //[TestCategory("Next.M5")]
        //public void next_M5_returns_proper_value_for_last_quotations_before_friday_christmas()
        //{
        //    DateTime d = new DateTime(2015, 12, 24, 21, 0, 0);
        //    Assert.AreEqual(new DateTime(2015, 12, 28, 0, 0, 0), d.getNext(TimeframeSymbol.M5));
        //}

