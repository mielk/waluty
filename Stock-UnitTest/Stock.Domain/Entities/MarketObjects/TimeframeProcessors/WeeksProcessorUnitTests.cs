using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities.MarketObjects.TimeframeProcessors;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock_UnitTest.Stock.Domain.Entities.MarketObjects.TimeframeProcessors
{
    [TestClass]
    public class WeeksProcessorUnitTests
    {


        #region GET_PROPER_DATETIME
        
        [TestMethod]
        public void GetProperDateTime_ReturnsTheSameDateMidnight_ForSunday()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            DateTime baseDate = new DateTime(2016, 4, 17);

            //Act
            DateTime actualDateTime = processor.GetProperDateTime(baseDate, 1);

            //Assert
            DateTime expectedDateTime = baseDate;
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }



        [TestMethod]
        public void GetProperDateTime_ReturnsPreviousSundayMidnight_ForNonSunday()
        {

            //Arrange
            DaysProcessor processor = new DaysProcessor();
            DateTime baseDate = new DateTime(2016, 4, 21, 15, 14, 52);

            //Act
            DateTime actualDateTime = processor.GetProperDateTime(baseDate, 1);

            //Assert
            DateTime expectedDateTime = new DateTime(2016, 4, 17, 0, 0, 0);
            Assert.AreEqual(expectedDateTime, actualDateTime);

        }
        
        #endregion GET_PROPER_DATETIME




        #region weeksDifference

        //[TestMethod]
        //[TestCategory("weeksDifference")]
        //public void weeksDifference_if_compared_date_few_weeks_earlier_returns_proper_value()
        //{
        //    DateTime d1 = new DateTime(2016, 4, 20);
        //    DateTime d2 = new DateTime(2016, 3, 7);

        //    var result = d1.WeeksDifference(d2);
        //    Assert.AreEqual(-6, result);

        //}




        //[TestMethod]
        //[TestCategory("weeksDifference")]
        //public void weeksDifference_if_compared_date_one_week_earlier_and_dayOfWeek_later_returns_proper_value()
        //{
        //    DateTime d1 = new DateTime(2016, 4, 20);
        //    DateTime d2 = new DateTime(2016, 4, 16);

        //    var result = d1.WeeksDifference(d2);
        //    Assert.AreEqual(-1, result);

        //}



        //[TestMethod]
        //[TestCategory("weeksDifference")]
        //public void weeksDifference_if_compared_date_one_week_earlier_and_dayOfWeek_earlier_returns_proper_value()
        //{
        //    DateTime d1 = new DateTime(2016, 4, 20);
        //    DateTime d2 = new DateTime(2016, 4, 11);

        //    var result = d1.WeeksDifference(d2);
        //    Assert.AreEqual(-1, result);

        //}


        //[TestMethod]
        //[TestCategory("weeksDifference")]
        //public void weeksDifference_if_compared_date_one_week_later_dayOfWeek_earlier_returns_proper_value()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 11);
        //    DateTime d2 = new DateTime(2016, 8, 16);

        //    var result = d1.WeeksDifference(d2);
        //    Assert.AreEqual(1, result);

        //}


        //[TestMethod]
        //[TestCategory("weeksDifference")]
        //public void weeksDifference_if_compared_date_one_week_later_dayOfWeek_later_returns_proper_value()
        //{
        //    DateTime d1 = new DateTime(2016, 8, 11);
        //    DateTime d2 = new DateTime(2016, 8, 19);

        //    var result = d1.WeeksDifference(d2);
        //    Assert.AreEqual(1, result);

        //}


        //[TestMethod]
        //[TestCategory("weeksDifference")]
        //public void weeksDifference_if_compared_date_few_weeks_later_returns_proper_value()
        //{
        //    DateTime d1 = new DateTime(2016, 4, 20);
        //    DateTime d2 = new DateTime(2016, 6, 20);

        //    var result = d1.WeeksDifference(d2);
        //    Assert.AreEqual(9, result);

        //}



        //[TestMethod]
        //[TestCategory("weeksDifference")]
        //public void weeksDifference_if_from_the_same_week_compared_date_is_earlier_zero_is_returned()
        //{
        //    DateTime d1 = new DateTime(2016, 4, 20);
        //    DateTime d2 = new DateTime(2016, 4, 18);

        //    var result = d1.WeeksDifference(d2);
        //    Assert.AreEqual(0, result);

        //}



        //[TestMethod]
        //[TestCategory("weeksDifference")]
        //public void weeksDifference_if_from_the_same_week_compared_date_is_later_zero_is_returned()
        //{
        //    DateTime d1 = new DateTime(2016, 4, 18);
        //    DateTime d2 = new DateTime(2016, 4, 21);

        //    var result = d1.WeeksDifference(d2);
        //    Assert.AreEqual(0, result);

        //}


        #endregion weeksDifference



    }
}
