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
        public void GetNext_ReturnsProperValue_ForTimestampBetweenFullPeriods()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2017, 5, 4, 15, 13, 21);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 15);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 4, 15, 15, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetNext_ReturnsProperValue_ForTimestampOnPeriodEdge()
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
        public void GetNext_ReturnsProperValue_ForLastQuotationBeforeWeekend()
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
        public void GetNext_ReturnsProperValue_ForWeekendValue()
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
        public void GetNext_ReturnsProperValue_ForLastQuotationBeforeHoliday()
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

        [TestMethod]
        public void GetNext_ReturnsProperValue_ForLastQuotationBeforeHolidayInFriday()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            processor.AddHoliday(new DateTime(2017, 5, 5));
            DateTime baseDate = new DateTime(2017, 5, 4, 21, 0, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 30);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 8, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void GetNext_ReturnsProperValue_ForLastWeekQuotationIfMondayIsHoliday()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            processor.AddHoliday(new DateTime(2017, 5, 8, 0, 0, 0));
            DateTime baseDate = new DateTime(2017, 5, 5, 23, 55, 0);

            //Act
            DateTime actualDateTime = processor.GetNext(baseDate, 5);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 9, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        #endregion GET_NEXT



        #region COUNT_TIME_UNITS


        [TestMethod]
        public void CountTimeUnits_ReturnsZero_IfTheSameDateIsGiven()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2017, 5, 4, 15, 0, 0);
            DateTime comparedDate = new DateTime(2017, 5, 4, 15, 0, 0);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 5);

            //Assert
            int expected = 0;
            Assert.AreEqual(expected, unitsBetween);

        }


        [TestMethod]
        public void CountTimeUnits_M5_ReturnsZero_IfDateInTheSamePeriodIsGiven()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2017, 5, 4, 15, 0, 0);
            DateTime comparedDate = new DateTime(2017, 5, 4, 15, 4, 0);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 5);

            //Assert
            int expected = 0;
            Assert.AreEqual(expected, unitsBetween);

        }

        [TestMethod]
        public void CountTimeUnits_M15_ReturnsZero_IfDateInTheSamePeriodIsGiven()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2017, 5, 4, 15, 0, 0);
            DateTime comparedDate = new DateTime(2017, 5, 4, 15, 14, 0);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 15);

            //Assert
            int expected = 0;
            Assert.AreEqual(expected, unitsBetween);

        }

        [TestMethod]
        public void CountTimeUnits_M30_ReturnsZero_IfDateInTheSamePeriodIsGiven()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2017, 5, 4, 15, 0, 0);
            DateTime comparedDate = new DateTime(2017, 5, 4, 15, 27, 12);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 30);

            //Assert
            int expected = 0;
            Assert.AreEqual(expected, unitsBetween);

        }


        [TestMethod]
        public void CountTimeUnits_M5_ReturnsProperValue_IfThereIsNoTimeOffBetweenComparedDates()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 10, 16, 0, 0);
            DateTime comparedDate = new DateTime(2016, 8, 12, 20, 0, 0);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 5);

            //Assert
            int expected = 624;
            Assert.AreEqual(expected, unitsBetween);

        }

        [TestMethod]
        public void CountTimeUnits_M15_ReturnsProperValue_IfThereIsNoTimeOffBetweenComparedDates()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 10, 16, 0, 0);
            DateTime comparedDate = new DateTime(2016, 8, 12, 20, 0, 0);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 15);

            //Assert
            int expected = 208;
            Assert.AreEqual(expected, unitsBetween);

        }

        [TestMethod]
        public void CountTimeUnits_M30_ReturnsProperValue_IfThereIsNoTimeOffBetweenComparedDates()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 10, 16, 0, 0);
            DateTime comparedDate = new DateTime(2016, 8, 12, 20, 0, 0);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 30);

            //Assert
            int expected = 104;
            Assert.AreEqual(expected, unitsBetween);

        }


        [TestMethod]
        public void CountTimeUnits_M5_ReturnsProperValue_IfThereIsWeekendBetweenComparedDates()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime comparedDate = new DateTime(2016, 8, 16, 20, 0, 0);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 5);

            //Assert
            int expected = 672;
            Assert.AreEqual(expected, unitsBetween);

        }

        [TestMethod]
        public void CountTimeUnits_M15_ReturnsProperValue_IfThereIsWeekendBetweenComparedDates()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime comparedDate = new DateTime(2016, 8, 16, 20, 0, 0);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 15);

            //Assert
            int expected = 224;
            Assert.AreEqual(expected, unitsBetween);

        }

        [TestMethod]
        public void CountTimeUnits_M30_ReturnsProperValue_IfThereIsWeekendBetweenComparedDates()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime comparedDate = new DateTime(2016, 8, 16, 20, 0, 0);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 30);

            //Assert
            int expected = 112;
            Assert.AreEqual(expected, unitsBetween);

        }


        [TestMethod]
        public void CountTimeUnits_M5_ReturnsProperValue_IfThereAreFewWeekendsBetweenComparedDates()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 10, 16, 0, 0);
            DateTime comparedDate = new DateTime(2016, 8, 22, 12, 0, 0);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 5);

            //Assert
            int expected = 2256;
            Assert.AreEqual(expected, unitsBetween);

        }

        [TestMethod]
        public void CountTimeUnits_M15_ReturnsProperValue_IfThereAreFewWeekendsBetweenComparedDates()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 10, 16, 0, 0);
            DateTime comparedDate = new DateTime(2016, 8, 22, 12, 0, 0);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 15);

            //Assert
            int expected = 752;
            Assert.AreEqual(expected, unitsBetween);

        }

        [TestMethod]
        public void CountTimeUnits_M30_ReturnsProperValue_IfThereAreFewWeekendsBetweenComparedDates()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 10, 16, 0, 0);
            DateTime comparedDate = new DateTime(2016, 8, 22, 12, 0, 0);

            //Act
            int unitsBetween = processor.CountTimeUnits(baseDate, comparedDate, 30);

            //Assert
            int expected = 376;
            Assert.AreEqual(expected, unitsBetween);

        }

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_earlier_than_base_date()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 23, 8, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(2544, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_later_than_base_date()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 25, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(3216, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_in_the_same_week_after_new_year()
        //{
        //    DateTime d1 = new DateTime(2013, 12, 30, 16, 0, 0);
        //    DateTime d2 = new DateTime(2014, 1, 3, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(781, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_after_weekend_and_after_newYear()
        //{
        //    DateTime d1 = new DateTime(2014, 12, 30, 16, 0, 0);
        //    DateTime d2 = new DateTime(2015, 1, 6, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(1069, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_after_weekend_newYear()
        //{
        //    DateTime d1 = new DateTime(2011, 12, 28, 12, 0, 0);
        //    DateTime d2 = new DateTime(2012, 1, 3, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(1200, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_in_the_same_week_after_christmas()
        //{
        //    DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);
        //    DateTime d2 = new DateTime(2014, 12, 26, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(589, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_after_weekend_and_after_christmas()
        //{
        //    DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);
        //    DateTime d2 = new DateTime(2014, 12, 29, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(829, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_after_weekend_christmas()
        //{
        //    DateTime d1 = new DateTime(2016, 12, 23, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 12, 27, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(624, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_newYears_later()
        //{
        //    DateTime d1 = new DateTime(2013, 10, 15, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 11, 11, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(229374, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_in_the_same_week()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 10, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(-528, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_before_weekend()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 17, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(-1152, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_later_than_base_date()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 16, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(-2544, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_earlier_than_base_date()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 19, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(-3408, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_weekends_earlier()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 7, 21, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(-4272, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_new_year()
        //{
        //    DateTime d1 = new DateTime(2015, 1, 2, 16, 0, 0);
        //    DateTime d2 = new DateTime(2014, 12, 31, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(-301, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_newYear()
        //{
        //    DateTime d1 = new DateTime(2014, 1, 2, 12, 0, 0);
        //    DateTime d2 = new DateTime(2013, 12, 27, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(-781, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_before_weekend_newYear()
        //{
        //    DateTime d1 = new DateTime(2017, 1, 3, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 12, 29, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(-864, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_christmas()
        //{
        //    DateTime d1 = new DateTime(2014, 12, 26, 12, 0, 0);
        //    DateTime d2 = new DateTime(2014, 12, 24, 20, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(-157, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_christmas()
        //{
        //    DateTime d1 = new DateTime(2015, 12, 29, 12, 0, 0);
        //    DateTime d2 = new DateTime(2015, 12, 24, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(-541, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_before_weekend_christmas()
        //{
        //    DateTime d1 = new DateTime(2016, 12, 26, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 12, 23, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(-240, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M5")]
        //public void countTimeUnits_m5_returns_proper_value_for_date_few_newYears_earlier()
        //{
        //    DateTime d1 = new DateTime(2016, 12, 26, 16, 0, 0);
        //    DateTime d2 = new DateTime(2012, 10, 23, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
        //    Assert.AreEqual(-311048, result);
        //}



        //#endregion M5



        //#region M15
        

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_after_weekend()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 16, 20, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(224, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_weekends_later()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 10, 16, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 22, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(752, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_earlier_than_base_date()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 23, 8, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(848, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_later_than_base_date()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 25, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(1072, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_in_the_same_week_after_new_year()
        //{
        //    DateTime d1 = new DateTime(2013, 12, 30, 16, 0, 0);
        //    DateTime d2 = new DateTime(2014, 1, 3, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(261, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_after_weekend_and_after_newYear()
        //{
        //    DateTime d1 = new DateTime(2014, 12, 30, 16, 0, 0);
        //    DateTime d2 = new DateTime(2015, 1, 6, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(357, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_after_weekend_newYear()
        //{
        //    DateTime d1 = new DateTime(2011, 12, 28, 12, 0, 0);
        //    DateTime d2 = new DateTime(2012, 1, 3, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(400, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_in_the_same_week_after_christmas()
        //{
        //    DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);
        //    DateTime d2 = new DateTime(2014, 12, 26, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(197, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_after_weekend_and_after_christmas()
        //{
        //    DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);
        //    DateTime d2 = new DateTime(2014, 12, 29, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(277, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_after_weekend_christmas()
        //{
        //    DateTime d1 = new DateTime(2016, 12, 23, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 12, 27, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(208, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_newYears_later()
        //{
        //    DateTime d1 = new DateTime(2013, 10, 15, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 11, 11, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(76462, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_in_the_same_week()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 10, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(-176, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_before_weekend()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 17, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(-384, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_later_than_base_date()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 16, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(-848, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_earlier_than_base_date()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 19, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(-1136, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_weekends_earlier()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 7, 21, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(-1424, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_new_year()
        //{
        //    DateTime d1 = new DateTime(2015, 1, 2, 16, 0, 0);
        //    DateTime d2 = new DateTime(2014, 12, 31, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(-101, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_newYear()
        //{
        //    DateTime d1 = new DateTime(2014, 1, 2, 12, 0, 0);
        //    DateTime d2 = new DateTime(2013, 12, 27, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(-261, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_before_weekend_newYear()
        //{
        //    DateTime d1 = new DateTime(2017, 1, 3, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 12, 29, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(-288, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_christmas()
        //{
        //    DateTime d1 = new DateTime(2014, 12, 26, 12, 0, 0);
        //    DateTime d2 = new DateTime(2014, 12, 24, 20, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(-53, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_christmas()
        //{
        //    DateTime d1 = new DateTime(2015, 12, 29, 12, 0, 0);
        //    DateTime d2 = new DateTime(2015, 12, 24, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(-181, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_before_weekend_christmas()
        //{
        //    DateTime d1 = new DateTime(2016, 12, 26, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 12, 23, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(-80, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M15")]
        //public void countTimeUnits_m15_returns_proper_value_for_date_few_newYears_earlier()
        //{
        //    DateTime d1 = new DateTime(2016, 12, 26, 16, 0, 0);
        //    DateTime d2 = new DateTime(2012, 10, 23, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
        //    Assert.AreEqual(-103688, result);
        //}

        //#endregion M15



        //#region M30


        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_after_weekend()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 16, 20, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(112, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_weekends_later()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 10, 16, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 22, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(376, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_earlier_than_base_date()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 23, 8, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(424, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_later_than_base_date()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 25, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(536, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_in_the_same_week_after_new_year()
        //{
        //    DateTime d1 = new DateTime(2013, 12, 30, 16, 0, 0);
        //    DateTime d2 = new DateTime(2014, 1, 3, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(131, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_after_weekend_and_after_newYear()
        //{
        //    DateTime d1 = new DateTime(2014, 12, 30, 16, 0, 0);
        //    DateTime d2 = new DateTime(2015, 1, 6, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(179, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_after_weekend_newYear()
        //{
        //    DateTime d1 = new DateTime(2011, 12, 28, 12, 0, 0);
        //    DateTime d2 = new DateTime(2012, 1, 3, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(200, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_in_the_same_week_after_christmas()
        //{
        //    DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);
        //    DateTime d2 = new DateTime(2014, 12, 26, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(99, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_after_weekend_and_after_christmas()
        //{
        //    DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);
        //    DateTime d2 = new DateTime(2014, 12, 29, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(139, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_after_weekend_christmas()
        //{
        //    DateTime d1 = new DateTime(2016, 12, 23, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 12, 27, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(104, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_newYears_later()
        //{
        //    DateTime d1 = new DateTime(2013, 10, 15, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 11, 11, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(38234, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_in_the_same_week()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 10, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(-88, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_before_weekend()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 17, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(-192, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_later_than_base_date()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 16, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(-424, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_earlier_than_base_date()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 19, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(-568, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_weekends_earlier()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 7, 21, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(-712, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_new_year()
        //{
        //    DateTime d1 = new DateTime(2015, 1, 2, 16, 0, 0);
        //    DateTime d2 = new DateTime(2014, 12, 31, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(-51, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_newYear()
        //{
        //    DateTime d1 = new DateTime(2014, 1, 2, 12, 0, 0);
        //    DateTime d2 = new DateTime(2013, 12, 27, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(-131, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_before_weekend_newYear()
        //{
        //    DateTime d1 = new DateTime(2017, 1, 3, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 12, 29, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(-144, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_christmas()
        //{
        //    DateTime d1 = new DateTime(2014, 12, 26, 12, 0, 0);
        //    DateTime d2 = new DateTime(2014, 12, 24, 20, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(-27, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_christmas()
        //{
        //    DateTime d1 = new DateTime(2015, 12, 29, 12, 0, 0);
        //    DateTime d2 = new DateTime(2015, 12, 24, 12, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(-91, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_before_weekend_christmas()
        //{
        //    DateTime d1 = new DateTime(2016, 12, 26, 12, 0, 0);
        //    DateTime d2 = new DateTime(2016, 12, 23, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(-40, result);
        //}

        //[TestMethod]
        //[TestCategory("countTimeUnits.M30")]
        //public void countTimeUnits_m30_returns_proper_value_for_date_few_newYears_earlier()
        //{
        //    DateTime d1 = new DateTime(2016, 12, 26, 16, 0, 0);
        //    DateTime d2 = new DateTime(2012, 10, 23, 16, 0, 0);
        //    int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
        //    Assert.AreEqual(-51848, result);
        //}



        //#endregion M30


        #endregion COUNT_TIME_UNITS


        #region ADD_TIME_UNITS


        [TestMethod]
        public void AddTimeUnits_M5_ReturnsTheSameDate_IfUnitsIsZero()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2017, 5, 4, 15, 5, 0);

            //Act
            DateTime actualDateTime = processor.AddTimeUnits(baseDate, 5, 0);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 4, 15, 5, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void AddTimeUnits_M5_ReturnsNormalizedDate_IfUnitsIsZeroAndDateBetweenPeriods()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2017, 5, 4, 15, 8, 0);

            //Act
            DateTime actualDateTime = processor.AddTimeUnits(baseDate, 5, 0);

            //Assert
            DateTime expectedDateTime = new DateTime(2017, 5, 4, 15, 5, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void AddTimeUnits_M5_ReturnsProperValue_ForRangeInTheMiddleOfWeek()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 9, 8, 0, 0);

            //Act
            DateTime actualDateTime = processor.AddTimeUnits(baseDate, 5, 912);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 12, 12, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void AddTimeUnits_M5_ReturnsProperValue_ForPositiveNumberOfItemsWithWeekBreak()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 2, 8, 0, 0);

            //Act
            DateTime actualDateTime = processor.AddTimeUnits(baseDate, 5, 2352);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 12, 12, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }


        [TestMethod]
        public void AddTimeUnits_M15_ReturnsTheSameDate_IfUnitsIsZero()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 4, 21, 12, 0, 0);

            //Act
            DateTime actualDateTime = processor.AddTimeUnits(baseDate, 15, 0);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 4, 21, 12, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void AddTimeUnits_M15_ReturnsNormalizedDate_IfUnitsIsZeroAndDateBetweenPeriods()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 4, 21, 12, 11, 34);

            //Act
            DateTime actualDateTime = processor.AddTimeUnits(baseDate, 15, 0);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 4, 21, 12, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void AddTimeUnits_M15_ReturnsProperValue_ForRangeInTheMiddleOfWeek()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 9, 8, 0, 0);

            //Act
            DateTime actualDateTime = processor.AddTimeUnits(baseDate, 15, 304);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 12, 12, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void AddTimeUnits_M15_ReturnsProperValue_ForPositiveNumberOfItemsWithWeekBreak()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 2, 8, 0, 0);

            //Act
            DateTime actualDateTime = processor.AddTimeUnits(baseDate, 5, 784);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 12, 12, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }



        [TestMethod]
        public void AddTimeUnits_M30_ReturnsTheSameDate_IfUnitsIsZero()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 4, 21, 12, 0, 0);

            //Act
            DateTime actualDateTime = processor.AddTimeUnits(baseDate, 30, 0);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 4, 21, 12, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void AddTimeUnits_M30_ReturnsNormalizedDate_IfUnitsIsZeroAndDateBetweenPeriods()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 4, 21, 12, 51, 34);

            //Act
            DateTime actualDateTime = processor.AddTimeUnits(baseDate, 30, 0);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 4, 21, 12, 30, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void AddTimeUnits_M30_ReturnsProperValue_ForRangeInTheMiddleOfWeek()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 9, 8, 0, 0);

            //Act
            DateTime actualDateTime = processor.AddTimeUnits(baseDate, 30, 152);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 12, 12, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }

        [TestMethod]
        public void AddTimeUnits_M30_ReturnsProperValue_ForPositiveNumberOfItemsWithWeekBreak()
        {

            //Arrange
            MinutesProcessor processor = new MinutesProcessor();
            DateTime baseDate = new DateTime(2016, 8, 2, 8, 0, 0);

            //Act
            DateTime actualDateTime = processor.AddTimeUnits(baseDate, 5, 392);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 8, 12, 12, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }


        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_positive_with_newYear_at_week()
        //{
        //    DateTime d = new DateTime(2014, 12, 29, 8, 0, 0);
        //    DateTime expected = new DateTime(2015, 1, 7, 16, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 1789);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_positive_with_newYear_at_weekend()
        //{
        //    DateTime d = new DateTime(2016, 12, 28, 8, 0, 0);
        //    DateTime expected = new DateTime(2017, 1, 5, 16, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 1824);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_positive_with_christmas_at_week()
        //{
        //    DateTime d = new DateTime(2014, 12, 23, 16, 0, 0);
        //    DateTime expected = new DateTime(2014, 12, 30, 13, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 1081);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_positive_with_christmas_at_weekend()
        //{
        //    DateTime d = new DateTime(2016, 12, 22, 12, 0, 0);
        //    DateTime expected = new DateTime(2016, 12, 28, 16, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 1200);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_positive_with_christmas_and_newYear()
        //{
        //    DateTime d = new DateTime(2015, 12, 22, 16, 0, 0);
        //    DateTime expected = new DateTime(2016, 1, 5, 12, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 2186);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_positive_omit_weekend_after_skip_christmas()
        //{
        //    DateTime d = new DateTime(2015, 12, 24, 20, 0, 0);
        //    DateTime expected = new DateTime(2015, 12, 28, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 61);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_positive_omit_weekend_after_skip_newYear()
        //{
        //    DateTime d = new DateTime(2015, 12, 31, 20, 0, 0);
        //    DateTime expected = new DateTime(2016, 1, 4, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 61);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_positive_omit_christmas_after_skip_weekend()
        //{
        //    DateTime d = new DateTime(2017, 12, 22, 20, 0, 0);
        //    DateTime expected = new DateTime(2017, 12, 26, 0, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 48);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_positive_omit_newYear_after_skip_weekend()
        //{
        //    DateTime d = new DateTime(2017, 12, 29, 20, 0, 0);
        //    DateTime expected = new DateTime(2018, 1, 2, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 96);

        //    Assert.AreEqual(expected, result);
        //}

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_negative_without_new_year_not_week_break()
        //{
        //    DateTime d = new DateTime(2016, 8, 12, 12, 0, 0);
        //    DateTime expected = new DateTime(2016, 8, 9, 8, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -912);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_negative_with_week_break()
        //{
        //    DateTime d = new DateTime(2016, 7, 26, 4, 0, 0);
        //    DateTime expected = new DateTime(2016, 7, 15, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -2016);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_negative_with_newYear_at_week()
        //{
        //    DateTime d = new DateTime(2015, 1, 6, 12, 0, 0);
        //    DateTime expected = new DateTime(2014, 12, 30, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -1213);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_negative_with_newYear_at_weekend()
        //{
        //    DateTime d = new DateTime(2017, 1, 6, 16, 0, 0);
        //    DateTime expected = new DateTime(2016, 12, 27, 12, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -2352);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_negative_with_christmas_at_week()
        //{
        //    DateTime d = new DateTime(2013, 12, 27, 12, 0, 0);
        //    DateTime expected = new DateTime(2013, 12, 23, 13, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -817);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_negative_with_christmas_at_weekend()
        //{
        //    DateTime d = new DateTime(2011, 12, 28, 16, 0, 0);
        //    DateTime expected = new DateTime(2011, 12, 22, 12, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -1200);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_negative_with_christmas_and_newYear()
        //{
        //    DateTime d = new DateTime(2015, 1, 7, 12, 0, 0);
        //    DateTime expected = new DateTime(2013, 12, 23, 12, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -77044);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_negative_omit_weekend_after_skip_christmas()
        //{
        //    DateTime d = new DateTime(2015, 12, 28, 4, 0, 0);
        //    DateTime expected = new DateTime(2015, 12, 24, 20, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -61);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_negative_omit_weekend_after_skip_newYear()
        //{
        //    DateTime d = new DateTime(2016, 1, 4, 4, 0, 0);
        //    DateTime expected = new DateTime(2015, 12, 31, 20, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -61);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_negative_omit_christmas_after_skip_weekend()
        //{
        //    DateTime d = new DateTime(2017, 12, 26, 0, 0, 0);
        //    DateTime expected = new DateTime(2017, 12, 22, 20, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -48);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M5_if_units_negative_omit_newYear_after_skip_weekend()
        //{
        //    DateTime d = new DateTime(2018, 1, 2, 4, 0, 0);
        //    DateTime expected = new DateTime(2017, 12, 29, 20, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -96);

        //    Assert.AreEqual(expected, result);
        //}

        #endregion ADD_TIME_UNITS



        #region M15


        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_positive_with_newYear_at_week()
        //{
        //    DateTime d = new DateTime(2014, 12, 29, 8, 0, 0);
        //    DateTime expected = new DateTime(2015, 1, 7, 16, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 597);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_positive_with_newYear_at_weekend()
        //{
        //    DateTime d = new DateTime(2016, 12, 28, 8, 0, 0);
        //    DateTime expected = new DateTime(2017, 1, 5, 16, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 608);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_positive_with_christmas_at_week()
        //{
        //    DateTime d = new DateTime(2014, 12, 23, 16, 0, 0);
        //    DateTime expected = new DateTime(2014, 12, 30, 13, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 361);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_positive_with_christmas_at_weekend()
        //{
        //    DateTime d = new DateTime(2016, 12, 22, 12, 0, 0);
        //    DateTime expected = new DateTime(2016, 12, 28, 16, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 400);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_positive_with_christmas_and_newYear()
        //{
        //    DateTime d = new DateTime(2015, 12, 22, 16, 0, 0);
        //    DateTime expected = new DateTime(2016, 1, 5, 12, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 730);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_positive_omit_weekend_after_skip_christmas()
        //{
        //    DateTime d = new DateTime(2015, 12, 24, 20, 0, 0);
        //    DateTime expected = new DateTime(2015, 12, 28, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 21);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_positive_omit_weekend_after_skip_newYear()
        //{
        //    DateTime d = new DateTime(2015, 12, 31, 20, 0, 0);
        //    DateTime expected = new DateTime(2016, 1, 4, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 21);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_positive_omit_christmas_after_skip_weekend()
        //{
        //    DateTime d = new DateTime(2017, 12, 22, 20, 0, 0);
        //    DateTime expected = new DateTime(2017, 12, 26, 0, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 16);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_positive_omit_newYear_after_skip_weekend()
        //{
        //    DateTime d = new DateTime(2017, 12, 29, 20, 0, 0);
        //    DateTime expected = new DateTime(2018, 1, 2, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 32);

        //    Assert.AreEqual(expected, result);
        //}

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_negative_without_new_year_not_week_break()
        //{
        //    DateTime d = new DateTime(2016, 8, 12, 12, 0, 0);
        //    DateTime expected = new DateTime(2016, 8, 9, 8, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -304);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_negative_with_week_break()
        //{
        //    DateTime d = new DateTime(2016, 7, 26, 4, 0, 0);
        //    DateTime expected = new DateTime(2016, 7, 15, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -672);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_negative_with_newYear_at_week()
        //{
        //    DateTime d = new DateTime(2015, 1, 6, 12, 0, 0);
        //    DateTime expected = new DateTime(2014, 12, 30, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -405);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_negative_with_newYear_at_weekend()
        //{
        //    DateTime d = new DateTime(2017, 1, 6, 16, 0, 0);
        //    DateTime expected = new DateTime(2016, 12, 27, 12, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -784);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_negative_with_christmas_at_week()
        //{
        //    DateTime d = new DateTime(2013, 12, 27, 12, 0, 0);
        //    DateTime expected = new DateTime(2013, 12, 23, 13, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -273);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_negative_with_christmas_at_weekend()
        //{
        //    DateTime d = new DateTime(2011, 12, 28, 16, 0, 0);
        //    DateTime expected = new DateTime(2011, 12, 22, 12, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -400);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_negative_with_christmas_and_newYear()
        //{
        //    DateTime d = new DateTime(2015, 1, 7, 12, 0, 0);
        //    DateTime expected = new DateTime(2013, 12, 23, 12, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -25684);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_negative_omit_weekend_after_skip_christmas()
        //{
        //    DateTime d = new DateTime(2015, 12, 28, 4, 0, 0);
        //    DateTime expected = new DateTime(2015, 12, 24, 20, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -21);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_negative_omit_weekend_after_skip_newYear()
        //{
        //    DateTime d = new DateTime(2016, 1, 4, 4, 0, 0);
        //    DateTime expected = new DateTime(2015, 12, 31, 20, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -21);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_negative_omit_christmas_after_skip_weekend()
        //{
        //    DateTime d = new DateTime(2017, 12, 26, 0, 0, 0);
        //    DateTime expected = new DateTime(2017, 12, 22, 20, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -16);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M15_if_units_negative_omit_newYear_after_skip_weekend()
        //{
        //    DateTime d = new DateTime(2018, 1, 2, 4, 0, 0);
        //    DateTime expected = new DateTime(2017, 12, 29, 20, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -32);

        //    Assert.AreEqual(expected, result);
        //}


        #endregion M15



        #region M30

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_positive_with_newYear_at_week()
        //{
        //    DateTime d = new DateTime(2014, 12, 29, 8, 0, 0);
        //    DateTime expected = new DateTime(2015, 1, 7, 16, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 299);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_positive_with_newYear_at_weekend()
        //{
        //    DateTime d = new DateTime(2016, 12, 28, 8, 0, 0);
        //    DateTime expected = new DateTime(2017, 1, 5, 16, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 304);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_positive_with_christmas_at_week()
        //{
        //    DateTime d = new DateTime(2014, 12, 23, 16, 0, 0);
        //    DateTime expected = new DateTime(2014, 12, 30, 13, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 181);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_positive_with_christmas_at_weekend()
        //{
        //    DateTime d = new DateTime(2016, 12, 22, 12, 0, 0);
        //    DateTime expected = new DateTime(2016, 12, 28, 16, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 200);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_positive_with_christmas_and_newYear()
        //{
        //    DateTime d = new DateTime(2015, 12, 22, 16, 0, 0);
        //    DateTime expected = new DateTime(2016, 1, 5, 12, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 366);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_positive_omit_weekend_after_skip_christmas()
        //{
        //    DateTime d = new DateTime(2015, 12, 24, 20, 0, 0);
        //    DateTime expected = new DateTime(2015, 12, 28, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 11);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_positive_omit_weekend_after_skip_newYear()
        //{
        //    DateTime d = new DateTime(2015, 12, 31, 20, 0, 0);
        //    DateTime expected = new DateTime(2016, 1, 4, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 11);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_positive_omit_christmas_after_skip_weekend()
        //{
        //    DateTime d = new DateTime(2017, 12, 22, 20, 0, 0);
        //    DateTime expected = new DateTime(2017, 12, 26, 0, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 8);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_positive_omit_newYear_after_skip_weekend()
        //{
        //    DateTime d = new DateTime(2017, 12, 29, 20, 0, 0);
        //    DateTime expected = new DateTime(2018, 1, 2, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 16);

        //    Assert.AreEqual(expected, result);
        //}

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_negative_without_new_year_not_week_break()
        //{
        //    DateTime d = new DateTime(2016, 8, 12, 12, 0, 0);
        //    DateTime expected = new DateTime(2016, 8, 9, 8, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -152);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_negative_with_week_break()
        //{
        //    DateTime d = new DateTime(2016, 7, 26, 4, 0, 0);
        //    DateTime expected = new DateTime(2016, 7, 15, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -336);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_negative_with_newYear_at_week()
        //{
        //    DateTime d = new DateTime(2015, 1, 6, 12, 0, 0);
        //    DateTime expected = new DateTime(2014, 12, 30, 4, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -203);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_negative_with_newYear_at_weekend()
        //{
        //    DateTime d = new DateTime(2017, 1, 6, 16, 0, 0);
        //    DateTime expected = new DateTime(2016, 12, 27, 12, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -392);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_negative_with_christmas_at_week()
        //{
        //    DateTime d = new DateTime(2013, 12, 27, 12, 0, 0);
        //    DateTime expected = new DateTime(2013, 12, 23, 13, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -137);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_negative_with_christmas_at_weekend()
        //{
        //    DateTime d = new DateTime(2011, 12, 28, 16, 0, 0);
        //    DateTime expected = new DateTime(2011, 12, 22, 12, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -200);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_negative_with_christmas_and_newYear()
        //{
        //    DateTime d = new DateTime(2015, 1, 7, 12, 0, 0);
        //    DateTime expected = new DateTime(2013, 12, 23, 12, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -12844);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_negative_omit_weekend_after_skip_christmas()
        //{
        //    DateTime d = new DateTime(2015, 12, 28, 4, 0, 0);
        //    DateTime expected = new DateTime(2015, 12, 24, 20, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -11);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_negative_omit_weekend_after_skip_newYear()
        //{
        //    DateTime d = new DateTime(2016, 1, 4, 4, 0, 0);
        //    DateTime expected = new DateTime(2015, 12, 31, 20, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -11);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_negative_omit_christmas_after_skip_weekend()
        //{
        //    DateTime d = new DateTime(2017, 12, 26, 0, 0, 0);
        //    DateTime expected = new DateTime(2017, 12, 22, 20, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -8);

        //    Assert.AreEqual(expected, result);
        //}
        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void addTimeUnits_M30_if_units_negative_omit_newYear_after_skip_weekend()
        //{
        //    DateTime d = new DateTime(2018, 1, 2, 4, 0, 0);
        //    DateTime expected = new DateTime(2017, 12, 29, 20, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -16);

        //    Assert.AreEqual(expected, result);
        //}



        #endregion M30




    }

}