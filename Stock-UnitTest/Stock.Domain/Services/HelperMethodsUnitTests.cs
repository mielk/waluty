using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.Domain.Services;
using System.Collections.Generic;
using Stock.Domain.Enums;

namespace Stock_UnitTest.Stock.Services
{


    [TestClass]
    public class HelperMethodsUnitTests
    {



        [TestMethod]
        [TestCategory("getEarliestDate")]
        public void getEarliestDate_if_at_least_one_date_is_null_function_returns_null()
        {
            DateTime?[] dates = new DateTime?[] {   new DateTime(2016, 1, 1, 0, 0, 0), 
                                                    new DateTime(2015, 1, 1, 0, 0, 0), 
                                                    new DateTime(2016, 2, 2, 0, 0, 0), 
                                                    null,
                                                    new DateTime(2016, 3, 3, 0, 0, 0)};
            List<DateTime?> list = new List<DateTime?>(dates);
            var earliest = list.getEarliestDate();
            Assert.IsNull(earliest);
            
        }


        [TestMethod]
        [TestCategory("getEarliestDate")]
        public void getEarliestDate_if_no_item_is_null_function_returns_earliest_date()
        {
            DateTime?[] dates = new DateTime?[] {   new DateTime(2016, 1, 1, 0, 0, 0), 
                                                    new DateTime(2015, 1, 1, 0, 0, 0), 
                                                    new DateTime(2016, 2, 2, 0, 0, 0), 
                                                    new DateTime(2016, 3, 3, 0, 0, 0)};
            List<DateTime?> list = new List<DateTime?>(dates);
            var earliest = list.getEarliestDate();
            Assert.AreEqual(earliest, new DateTime(2015, 1, 1, 0, 0, 0));
        }



        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_if_dates_in_the_same_year_positive_difference_zero_is_returned()
        {

            DateTime baseDate = new DateTime(2016, 1, 4);
            DateTime comparedDate = new DateTime(2016, 1, 10);

            Assert.AreEqual(0, baseDate.countNewYearBreaks(comparedDate, true));

        }



        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_both_dates_are_newYears_one_of_them_weekend_weekends_excluded()
        {

            DateTime baseDate = new DateTime(2016, 1, 1);
            DateTime comparedDate = new DateTime(2017, 1, 1);

            Assert.AreEqual(0, baseDate.countNewYearBreaks(comparedDate, false));

        }


        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_both_dates_are_newYears_one_of_them_weekend_weekends_included()
        {

            DateTime baseDate = new DateTime(2016, 1, 1);
            DateTime comparedDate = new DateTime(2017, 1, 1);

            Assert.AreEqual(1, baseDate.countNewYearBreaks(comparedDate, true));

        }


        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_both_dates_are_weekend_newYears_weekend_included()
        {

            DateTime baseDate = new DateTime(2012, 1, 1);
            DateTime comparedDate = new DateTime(2017, 1, 1);

            Assert.AreEqual(5, baseDate.countNewYearBreaks(comparedDate, true));

        }


        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_both_dates_are_weekend_newYears_weekend_excluded()
        {

            DateTime baseDate = new DateTime(2012, 1, 1);
            DateTime comparedDate = new DateTime(2017, 1, 1);

            Assert.AreEqual(4, baseDate.countNewYearBreaks(comparedDate, false));

        }




        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_if_dates_in_the_same_year_negative_difference_zero_is_returned()
        {

            DateTime baseDate = new DateTime(2016, 11, 4);
            DateTime comparedDate = new DateTime(2016, 1, 10);

            Assert.AreEqual(0, baseDate.countNewYearBreaks(comparedDate, true));

        }


        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_correct_value_is_returned_for_date_from_previous_year_NY_at_week_including_weekends()
        {

            DateTime baseDate = new DateTime(2016, 1, 4);
            DateTime comparedDate = new DateTime(2015, 12, 30);

            Assert.AreEqual(1, baseDate.countNewYearBreaks(comparedDate, true));

        }


        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_correct_value_is_returned_for_date_from_previous_year_NY_at_week_excluding_weekends()
        {

            DateTime baseDate = new DateTime(2016, 1, 4);
            DateTime comparedDate = new DateTime(2015, 12, 30);

            Assert.AreEqual(1, baseDate.countNewYearBreaks(comparedDate, false));

        }


        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_correct_value_is_returned_for_date_from_previous_year_NY_at_weekend_including_weekends()
        {

            DateTime baseDate = new DateTime(2011, 1, 3);
            DateTime comparedDate = new DateTime(2010, 12, 30);

            Assert.AreEqual(1, baseDate.countNewYearBreaks(comparedDate, true));

        }


        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_correct_value_is_returned_for_date_from_previous_year_NY_at_weekend_excluding_weekends()
        {

            DateTime baseDate = new DateTime(2011, 1, 3);
            DateTime comparedDate = new DateTime(2010, 12, 30);

            Assert.AreEqual(0, baseDate.countNewYearBreaks(comparedDate, false));

        }



        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_correct_value_is_returned_for_negative_difference_including_weekends()
        {

            DateTime baseDate = new DateTime(2016, 1, 4);
            DateTime comparedDate = new DateTime(2010, 12, 30);

            Assert.AreEqual(6, baseDate.countNewYearBreaks(comparedDate, true));

        }



        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_correct_value_is_returned_for_negative_difference_excluding_weekends()
        {
            DateTime baseDate = new DateTime(2016, 1, 4);
            DateTime comparedDate = new DateTime(2010, 12, 30);

            Assert.AreEqual(4, baseDate.countNewYearBreaks(comparedDate, false));

        }



        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_correct_value_is_returned_for_date_from_next_year_NY_at_week_including_weekends()
        {

            DateTime baseDate = new DateTime(2015, 12, 30);
            DateTime comparedDate = new DateTime(2016, 1, 4);
            
            Assert.AreEqual(1, baseDate.countNewYearBreaks(comparedDate, true));

        }



        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_correct_value_is_returned_for_date_from_next_year_NY_at_week_excluding_weekends()
        {

            DateTime baseDate = new DateTime(2015, 12, 30);
            DateTime comparedDate = new DateTime(2016, 1, 4);

            Assert.AreEqual(1, baseDate.countNewYearBreaks(comparedDate, false));

        }


        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_correct_value_is_returned_for_date_from_next_year_NY_at_weekend_including_weekends()
        {

            DateTime baseDate = new DateTime(2010, 12, 30);
            DateTime comparedDate = new DateTime(2011, 1, 3);
            
            Assert.AreEqual(1, baseDate.countNewYearBreaks(comparedDate, true));

        }


        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_correct_value_is_returned_for_date_from_next_year_NY_at_weekend_excluding_weekends()
        {

            DateTime baseDate = new DateTime(2010, 12, 30);
            DateTime comparedDate = new DateTime(2011, 1, 3);

            Assert.AreEqual(0, baseDate.countNewYearBreaks(comparedDate, false));

        }



        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_correct_value_is_returned_for_positive_difference_including_weekends()
        {

            DateTime baseDate = new DateTime(2010, 12, 30);
            DateTime comparedDate = new DateTime(2016, 1, 4);
            
            Assert.AreEqual(6, baseDate.countNewYearBreaks(comparedDate, true));

        }


        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_correct_value_is_returned_for_positive_difference_excluding_weekends()
        {

            DateTime baseDate = new DateTime(2010, 12, 30);
            DateTime comparedDate = new DateTime(2016, 1, 4);

            Assert.AreEqual(4, baseDate.countNewYearBreaks(comparedDate, false));

        }



        //[TestCategory("AppendIndexNumbers")]
        //public void AppendIndexNumbers_appends_numbers_for_all_items()
        //{

        //    //given
        //    DataItem[] items = new DataItem[5];
        //    for (var i = 0; i < items.Length; i++)
        //    {
        //        items[i] = new DataItem { Date = DateTime.Now.AddDays(i), AssetId = 1 };
        //    }


        //    //when
        //    items.AppendIndexNumbers();

        //    //then
        //    for (var i = 0; i < items.Length; i++)
        //    {
        //        var item = items[i];
        //        Assert.AreEqual(i, item.Index);
        //    }


        //}



        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void if_units_zero_the_same_date_is_returned_for_m5()
        //{
        //    DateTime d = new DateTime(2016, 4, 21, 10, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 0);

        //    Assert.AreEqual(d, result); 
        //}




        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_positive_without_new_year_not_week_break_for_m5()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_positive_with_week_break_for_m5()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_negative_without_new_year_not_week_break_for_m5()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_negative_with_week_break_for_m5()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_negative_with_year_break_for_m5()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_positive_with_year_break_for_m5()
        //{ Assert.Fail("Not implemented"); }



        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void if_units_zero_the_same_date_is_returned_for_m15()
        //{
        //    DateTime d = new DateTime(2016, 4, 21, 10, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 0);

        //    Assert.AreEqual(d, result); 
        //}



        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_positive_without_new_year_not_week_break_for_m15()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_positive_with_week_break_for_m15()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_negative_without_new_year_not_week_break_for_m15()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_negative_with_week_break_for_m15()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_negative_with_year_break_for_m15()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_positive_with_year_break_for_m15()
        //{ Assert.Fail("Not implemented"); }



        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void if_units_zero_the_same_date_is_returned_for_m30()
        //{
        //    DateTime d = new DateTime(2016, 4, 21, 10, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 0);

        //    Assert.AreEqual(d, result); 
        //}



        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_positive_without_new_year_not_week_break_for_m30()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_positive_with_week_break_for_m30()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_negative_without_new_year_not_week_break_for_m30()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_negative_with_week_break_for_m30()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_negative_with_year_break_for_m30()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_positive_with_year_break_for_m30()
        //{ Assert.Fail("Not implemented"); }



        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void if_units_zero_the_same_date_is_returned_for_h1()
        //{
        //    DateTime d = new DateTime(2016, 4, 21, 10, 0, 0);
        //    DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 0);

        //    Assert.AreEqual(d, result);
        //}



        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_positive_without_new_year_not_week_break_for_h1()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_positive_with_week_break_for_h1()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_negative_without_new_year_not_week_break_for_h1()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_negative_with_week_break_for_h1()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_negative_with_year_break_for_h1()
        //{ Assert.Fail("Not implemented"); }

        //[TestMethod]
        //[TestCategory("addTimeUnits")]
        //public void check_returned_date_if_units_positive_with_year_break_for_h1()
        //{ Assert.Fail("Not implemented"); }




        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void if_units_zero_the_same_date_is_returned_for_h4()
        {
            DateTime d = new DateTime(2016, 4, 21, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 0);

            Assert.AreEqual(d, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_positive_without_new_year_not_week_break_for_h4()
        {
            DateTime d = new DateTime(2016, 8, 9, 8, 0, 0);
            DateTime expected = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 19);

            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_positive_with_week_break_for_h4()
        { Assert.Fail("Not implemented"); }

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_negative_without_new_year_not_week_break_for_h4()
        { Assert.Fail("Not implemented"); }

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_negative_with_week_break_for_h4()
        { Assert.Fail("Not implemented"); }

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_negative_with_year_break_for_h4()
        { Assert.Fail("Not implemented"); }

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_positive_with_year_break_for_h4()
        { Assert.Fail("Not implemented"); }





        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void if_units_zero_the_same_date_is_returned_for_d1()
        {
            DateTime d = new DateTime(2016, 4, 21);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 0);

            Assert.AreEqual(d, result);
        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_positive_without_new_year_nor_week_break_for_d1()
        {
            DateTime d = new DateTime(2016, 4, 18);
            DateTime expected = new DateTime(2016, 4, 21);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 3);

            Assert.AreEqual(expected, result);

        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_positive_with_week_break_for_d1()
        {
            DateTime d = new DateTime(2016, 1, 21);
            DateTime expected = new DateTime(2016, 8, 29);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 157);

            Assert.AreEqual(expected, result);

        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_positive_with_new_year_break_for_d1()
        {
            DateTime d = new DateTime(2014, 12, 31);
            DateTime expected = new DateTime(2015, 1, 2);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 1);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_positive_with_few_new_year_breaks_for_d1()
        {
            DateTime d = new DateTime(2012, 1, 25);
            DateTime expected = new DateTime(2017, 8, 29);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 1455);

            Assert.AreEqual(expected, result);

        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_positive_with_new_year_break_at_weekend_d1()
        {
            DateTime d = new DateTime(2016, 12, 28);
            DateTime expected = new DateTime(2017, 1, 3);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 4);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_positive_with_new_year_break_at_week_d1()
        {
            DateTime d = new DateTime(2017, 12, 28);
            DateTime expected = new DateTime(2018, 1, 3);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 3);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_negative_with_week_break_for_d1()
        {
            DateTime d = new DateTime(2016, 8, 17);
            DateTime expected = new DateTime(2016, 8, 4);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -9);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_negative_without_new_year_nor_week_break_for_d1()
        {
            DateTime d = new DateTime(2016, 8, 17);
            DateTime expected = new DateTime(2016, 8, 15);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -2);

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_negative_with_new_year_break_for_d1()
        {
            DateTime d = new DateTime(2014, 1, 3);
            DateTime expected = new DateTime(2013, 12, 31);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -2);

            Assert.AreEqual(expected, result);
        }




        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_negative_with_new_year_and_week_break_for_d1()
        {
            DateTime d = new DateTime(2014, 1, 3);
            DateTime expected = new DateTime(2013, 12, 31);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -2);

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_if_units_negative_with_new_year_at_weekend_for_d1()
        {
            DateTime d = new DateTime(2017, 1, 3);
            DateTime expected = new DateTime(2016, 12, 30);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -2);

            Assert.AreEqual(expected, result);
        }




        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void if_units_zero_the_same_date_is_returned_for_w1()
        {
            DateTime d = new DateTime(2016, 4, 18);
            DateTime result = d.addTimeUnits(TimeframeSymbol.W1, 0);

            Assert.AreEqual(d, result);
        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_for_positive_units_w1()
        {
            DateTime d = new DateTime(2016, 4, 18);
            DateTime expected = new DateTime(2016, 5, 23);
            DateTime result = d.addTimeUnits(TimeframeSymbol.W1, 5);

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_for_negative_units_w1()
        {
            DateTime d = new DateTime(2016, 4, 18);
            DateTime expected = new DateTime(2016, 4, 4);
            DateTime result = d.addTimeUnits(TimeframeSymbol.W1, -2);

            Assert.AreEqual(expected, result);
        }




        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void if_units_zero_the_same_date_is_returned_for_m1()
        {
            DateTime d = new DateTime(2016, 4, 1);
            DateTime result = d.addTimeUnits(TimeframeSymbol.MN1, 0);

            Assert.AreEqual(d, result);
        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_for_positive_units_mn1()
        {
            DateTime d = new DateTime(2016, 4, 1);
            DateTime expected = new DateTime(2016, 9, 1);
            DateTime result = d.addTimeUnits(TimeframeSymbol.MN1, 5);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void check_returned_date_for_negative_units_mn1()
        {
            DateTime d = new DateTime(2016, 4, 1);
            DateTime expected = new DateTime(2015, 11, 1);
            DateTime result = d.addTimeUnits(TimeframeSymbol.MN1, -5);

            Assert.AreEqual(expected, result);
        }



    }
}
