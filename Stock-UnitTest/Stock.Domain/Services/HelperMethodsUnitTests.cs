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




        #region weeksDifference

        [TestMethod]
        [TestCategory("weeksDifference")]
        public void weeksDifference_if_compared_date_few_weeks_earlier_returns_proper_value()
        {
            DateTime d1 = new DateTime(2016, 4, 20);
            DateTime d2 = new DateTime(2016, 3, 7);

            var result = d1.WeeksDifference(d2);
            Assert.AreEqual(-6, result);

        }




        [TestMethod]
        [TestCategory("weeksDifference")]
        public void weeksDifference_if_compared_date_one_week_earlier_and_dayOfWeek_later_returns_proper_value()
        {
            DateTime d1 = new DateTime(2016, 4, 20);
            DateTime d2 = new DateTime(2016, 4, 16);

            var result = d1.WeeksDifference(d2);
            Assert.AreEqual(-1, result);

        }



        [TestMethod]
        [TestCategory("weeksDifference")]
        public void weeksDifference_if_compared_date_one_week_earlier_and_dayOfWeek_earlier_returns_proper_value()
        {
            DateTime d1 = new DateTime(2016, 4, 20);
            DateTime d2 = new DateTime(2016, 4, 11);

            var result = d1.WeeksDifference(d2);
            Assert.AreEqual(-1, result);

        }


        [TestMethod]
        [TestCategory("weeksDifference")]
        public void weeksDifference_if_compared_date_one_week_later_dayOfWeek_earlier_returns_proper_value()
        {
            DateTime d1 = new DateTime(2016, 8, 11);
            DateTime d2 = new DateTime(2016, 8, 16);

            var result = d1.WeeksDifference(d2);
            Assert.AreEqual(1, result);

        }


        [TestMethod]
        [TestCategory("weeksDifference")]
        public void weeksDifference_if_compared_date_one_week_later_dayOfWeek_later_returns_proper_value()
        {
            DateTime d1 = new DateTime(2016, 8, 11);
            DateTime d2 = new DateTime(2016, 8, 19);

            var result = d1.WeeksDifference(d2);
            Assert.AreEqual(1, result);

        }


        [TestMethod]
        [TestCategory("weeksDifference")]
        public void weeksDifference_if_compared_date_few_weeks_later_returns_proper_value()
        {
            DateTime d1 = new DateTime(2016, 4, 20);
            DateTime d2 = new DateTime(2016, 6, 20);

            var result = d1.WeeksDifference(d2);
            Assert.AreEqual(9, result);

        }



        [TestMethod]
        [TestCategory("weeksDifference")]
        public void weeksDifference_if_from_the_same_week_compared_date_is_earlier_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 20);
            DateTime d2 = new DateTime(2016, 4, 18);

            var result = d1.WeeksDifference(d2);
            Assert.AreEqual(0, result);

        }



        [TestMethod]
        [TestCategory("weeksDifference")]
        public void weeksDifference_if_from_the_same_week_compared_date_is_later_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 18);
            DateTime d2 = new DateTime(2016, 4, 21);

            var result = d1.WeeksDifference(d2);
            Assert.AreEqual(0, result);

        }


        #endregion weeksDifference



        #region getEarliestDate
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
        #endregion getEarliestDate



        #region countNewYearBreaks
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
        #endregion countNewYearBreaks



        #region countSpecialDays

        [TestMethod]
        [TestCategory("countSpecialDays")]
        public void countSpecialDays_if_dates_from_the_same_year_but_date_is_not_included_zero_is_returned()
        {

            DateTime baseDate = new DateTime(2015, 2, 10);
            DateTime comparedDate = new DateTime(2015, 4, 15);
            int result = baseDate.countSpecialDays(comparedDate, true, 12, 24);

            Assert.AreEqual(0, result);

        }


        [TestMethod]
        [TestCategory("countSpecialDays")]
        public void countSpecialDays_if_dates_from_the_same_year_and_date_is_within_at_week_one_is_returned()
        {

            DateTime baseDate = new DateTime(2015, 12, 20);
            DateTime comparedDate = new DateTime(2015, 12, 30);
            int result = baseDate.countSpecialDays(comparedDate, false, 12, 24);

            Assert.AreEqual(1, result);

        }




        [TestMethod]
        [TestCategory("countSpecialDays")]
        public void countSpecialDays_if_dates_from_the_same_year_and_date_is_within_at_weekend_but_weekends_included_one_is_returned()
        {

            DateTime baseDate = new DateTime(2015, 12, 10);
            DateTime comparedDate = new DateTime(2015, 12, 17);
            int result = baseDate.countSpecialDays(comparedDate, true, 12, 13);

            Assert.AreEqual(1, result);

        }


        [TestMethod]
        [TestCategory("countSpecialDays")]
        public void countSpecialDays_if_dates_from_the_same_year_and_date_is_within_at_weekend_but_weekend_excluded_zero_is_returned()
        {

            DateTime baseDate = new DateTime(2015, 12, 10);
            DateTime comparedDate = new DateTime(2015, 12, 17);
            int result = baseDate.countSpecialDays(comparedDate, false, 12, 13);

            Assert.AreEqual(0, result);

        }


        [TestMethod]
        [TestCategory("countSpecialDays")]
        public void countSpecialDays_if_dates_from_different_years_and_both_border_years_include_date_weekends_on()
        {

            DateTime baseDate = new DateTime(2011, 12, 10);
            DateTime comparedDate = new DateTime(2015, 12, 30);
            int result = baseDate.countSpecialDays(comparedDate, true, 12, 25);

            Assert.AreEqual(5, result);

        }



        [TestMethod]
        [TestCategory("countSpecialDays")]
        public void countSpecialDays_if_dates_from_different_years_and_both_border_years_include_date_weekends_off()
        {

            DateTime baseDate = new DateTime(2011, 12, 10);
            DateTime comparedDate = new DateTime(2015, 12, 30);
            int result = baseDate.countSpecialDays(comparedDate, false, 12, 25);

            Assert.AreEqual(4, result);

        }



        [TestMethod]
        [TestCategory("countSpecialDays")]
        public void countSpecialDays_if_dates_from_different_years_and_left_border_year_includes_date_weekends_on()
        {

            DateTime baseDate = new DateTime(2011, 12, 10);
            DateTime comparedDate = new DateTime(2015, 12, 10);
            int result = baseDate.countSpecialDays(comparedDate, true, 12, 25);

            Assert.AreEqual(4, result);

        }


        [TestMethod]
        [TestCategory("countSpecialDays")]
        public void countSpecialDays_if_dates_from_different_years_and_left_border_year_includes_date_weekends_off()
        {

            DateTime baseDate = new DateTime(2011, 12, 10);
            DateTime comparedDate = new DateTime(2015, 12, 10);
            int result = baseDate.countSpecialDays(comparedDate, false, 12, 25);

            Assert.AreEqual(3, result);

        }


        [TestMethod]
        [TestCategory("countSpecialDays")]
        public void countSpecialDays_if_dates_from_different_years_and_right_border_year_includes_date_weekends_on()
        {

            DateTime baseDate = new DateTime(2011, 12, 30);
            DateTime comparedDate = new DateTime(2015, 12, 30);
            int result = baseDate.countSpecialDays(comparedDate, true, 12, 25);

            Assert.AreEqual(4, result);

        }



        [TestMethod]
        [TestCategory("countSpecialDays")]
        public void countSpecialDays_if_dates_from_different_years_and_right_border_year_includes_date_weekends_off()
        {

            DateTime baseDate = new DateTime(2011, 12, 30);
            DateTime comparedDate = new DateTime(2016, 12, 30);
            int result = baseDate.countSpecialDays(comparedDate, false, 12, 25);

            Assert.AreEqual(4, result);

        }


        [TestMethod]
        [TestCategory("countSpecialDays")]
        public void countSpecialDays_if_dates_from_different_years_but_none_of_border_include_date_weekends_on()
        {

            DateTime baseDate = new DateTime(2011, 12, 30);
            DateTime comparedDate = new DateTime(2016, 12, 10);
            int result = baseDate.countSpecialDays(comparedDate, true, 12, 25);

            Assert.AreEqual(4, result);

        }



        [TestMethod]
        [TestCategory("countSpecialDays")]
        public void countSpecialDays_if_dates_from_different_years_but_none_of_border_include_date_weekends_off()
        {

            DateTime baseDate = new DateTime(2010, 12, 30);
            DateTime comparedDate = new DateTime(2016, 12, 10);
            int result = baseDate.countSpecialDays(comparedDate, false, 12, 25);

            Assert.AreEqual(4, result);

        }


        #endregion countSpecialDays



        #region proper

        [TestMethod]
        [TestCategory("Proper")]
        public void proper_months_for_first_day_of_months_the_same_day_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 8, 1);
            Assert.AreEqual(baseDate, baseDate.Proper(TimeframeSymbol.MN1));
        }

        [TestMethod]
        [TestCategory("Proper")]
        public void proper_months_for_other_than_first_day_of_months_first_day_of_this_month_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 8, 15);
            Assert.AreEqual(new DateTime(2016, 8, 1), baseDate.Proper(TimeframeSymbol.MN1));
        }


        [TestMethod]
        [TestCategory("Proper")]
        public void proper_weeks_for_sundays_the_same_date_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 4, 17);
            Assert.AreEqual(baseDate, baseDate.Proper(TimeframeSymbol.W1));
        }


        [TestMethod]
        [TestCategory("Proper")]
        public void proper_weeks_for_dayOfWeek_other_than_sunday_last_sunday_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 4, 21);
            Assert.AreEqual(new DateTime(2016, 4, 17), baseDate.Proper(TimeframeSymbol.W1));
        }


        [TestMethod]
        [TestCategory("Proper")]
        public void proper_days_for_non_weekend_day_this_day_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 4, 18);
            Assert.AreEqual(baseDate, baseDate.Proper(TimeframeSymbol.D1));
        }


        [TestMethod]
        [TestCategory("Proper")]
        public void proper_days_for_saturday_friday_before_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 8, 13);
            Assert.AreEqual(new DateTime(2016, 8, 12), baseDate.Proper(TimeframeSymbol.D1));
        }


        [TestMethod]
        [TestCategory("Proper")]
        public void proper_days_for_sunday_friday_before_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 8, 14);
            Assert.AreEqual(new DateTime(2016, 8, 12), baseDate.Proper(TimeframeSymbol.D1));
        }


        [TestMethod]
        [TestCategory("Proper")]
        public void proper_days_for_newYear_the_day_before_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 1, 1);
            Assert.AreEqual(new DateTime(2015, 12, 31), baseDate.Proper(TimeframeSymbol.D1));
        }


        [TestMethod]
        [TestCategory("Proper")]
        public void proper_days_for_monday_new_year_friday_before_is_returned()
        {
            DateTime baseDate = new DateTime(2018, 1, 1);
            Assert.AreEqual(new DateTime(2017, 12, 29), baseDate.Proper(TimeframeSymbol.D1));
        }


        [TestMethod]
        [TestCategory("Proper")]
        public void proper_days_for_weekend_new_year_friday_before_is_returned()
        {
            DateTime baseDate = new DateTime(2017, 1, 1);
            Assert.AreEqual(new DateTime(2016, 12, 30), baseDate.Proper(TimeframeSymbol.D1));
        }


        [TestMethod]
        [TestCategory("Proper")]
        public void proper_h4_if_proper_value_is_passed_the_same_value_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 8, 11, 12, 0, 0);
            Assert.AreEqual(baseDate, baseDate.Proper(TimeframeSymbol.H4));
        }


        [TestMethod]
        [TestCategory("Proper")]
        public void proper_h4_for_weekend_value_function_returns_last_value_from_this_week()
        {
            DateTime baseDate = new DateTime(2016, 8, 13, 16, 15, 0);
            Assert.AreEqual(new DateTime(2016, 8, 12, 20, 0, 0), baseDate.Proper(TimeframeSymbol.H4));
        }



        [TestMethod]
        [TestCategory("Proper")]
        public void proper_h4_for_newYear_value_function_returns_last_valid_value_before()
        {
            DateTime baseDate = new DateTime(2016, 1, 1, 16, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 31, 20, 0, 0), baseDate.Proper(TimeframeSymbol.H4));
        }




        [TestMethod]
        [TestCategory("Proper")]
        public void proper_h4_for_weekendNewYear_value_function_returns_20PM_last_friday_of_previous_year()
        {
            DateTime baseDate = new DateTime(2017, 1, 1, 12, 0, 0);
            Assert.AreEqual(new DateTime(2016, 12, 30, 20, 0, 0), baseDate.Proper(TimeframeSymbol.H4));
        }


        [TestMethod]
        [TestCategory("Proper")]
        public void proper_h4_for_time_between_full_hours_function_returns_earlier_full_hour()
        {
            DateTime baseDate = new DateTime(2016, 8, 11, 15, 14, 31);
            Assert.AreEqual(new DateTime(2016, 8, 11, 12, 0, 0), baseDate.Proper(TimeframeSymbol.H4));
        }



        [TestMethod]
        [TestCategory("Proper.H1")]
        public void proper_h1_if_proper_value_is_passed_the_same_value_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 8, 11, 12, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 11, 12, 0, 0), baseDate.Proper(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Proper.H1")]
        public void proper_h1_for_weekend_value_function_returns_last_value_from_this_week()
        {
            DateTime baseDate = new DateTime(2016, 8, 13, 16, 15, 0);
            Assert.AreEqual(new DateTime(2016, 8, 12, 23, 0, 0), baseDate.Proper(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Proper.H1")]
        public void proper_h1_for_newYear_value_function_returns_last_valid_value_before()
        {
            DateTime baseDate = new DateTime(2016, 1, 1, 16, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 31, 21, 0, 0), baseDate.Proper(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Proper.H1")]
        public void proper_h1_for_weekendNewYear_value_function_returns_20PM_last_friday_of_previous_year()
        {
            DateTime baseDate = new DateTime(2017, 1, 1, 12, 0, 0);
            Assert.AreEqual(new DateTime(2016, 12, 30, 23, 0, 0), baseDate.Proper(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Proper.H1")]
        public void proper_h1_for_time_between_full_hours_function_returns_earlier_full_hour()
        {
            DateTime baseDate = new DateTime(2016, 8, 11, 15, 41, 31);
            Assert.AreEqual(new DateTime(2016, 8, 11, 15, 0, 0), baseDate.Proper(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Proper.H1")]
        public void proper_h1_for_christmasEve_22__21_of_this_day_is_returned()
        {
            DateTime baseDate = new DateTime(2015, 12, 24, 22, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 24, 21, 0, 0), baseDate.Proper(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Proper.H1")]
        public void proper_h1_for_newYearEve_22__21_of_this_day_is_returned()
        {
            DateTime baseDate = new DateTime(2015, 12, 31, 22, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 31, 21, 0, 0), baseDate.Proper(TimeframeSymbol.H1));
        }



        [TestMethod]
        [TestCategory("Proper.M30")]
        public void proper_m30_if_proper_value_is_passed_the_same_value_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 8, 11, 12, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 11, 12, 0, 0), baseDate.Proper(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Proper.M30")]
        public void proper_m30_for_weekend_value_function_returns_last_value_from_this_week()
        {
            DateTime baseDate = new DateTime(2016, 8, 13, 16, 15, 0);
            Assert.AreEqual(new DateTime(2016, 8, 12, 23, 30, 0), baseDate.Proper(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Proper.M30")]
        public void proper_m30_for_newYear_value_function_returns_last_valid_value_before()
        {
            DateTime baseDate = new DateTime(2016, 1, 1, 16, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 31, 21, 0, 0), baseDate.Proper(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Proper.M30")]
        public void proper_m30_for_weekendNewYear_value_function_returns_20PM_last_friday_of_previous_year()
        {
            DateTime baseDate = new DateTime(2017, 1, 1, 12, 0, 0);
            Assert.AreEqual(new DateTime(2016, 12, 30, 23, 30, 0), baseDate.Proper(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Proper.M30")]
        public void proper_m30_for_time_between_full_hours_function_returns_earlier_full_hour()
        {
            DateTime baseDate = new DateTime(2016, 8, 11, 15, 41, 31);
            Assert.AreEqual(new DateTime(2016, 8, 11, 15, 30, 0), baseDate.Proper(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Proper.M30")]
        public void proper_m30_for_christmasEve_2130__21_of_this_day_is_returned()
        {
            DateTime baseDate = new DateTime(2015, 12, 24, 21, 30, 0);
            Assert.AreEqual(new DateTime(2015, 12, 24, 21, 0, 0), baseDate.Proper(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Proper.M30")]
        public void proper_m30_for_newYearEve_2130__21_of_this_day_is_returned()
        {
            DateTime baseDate = new DateTime(2015, 12, 31, 21, 30, 0);
            Assert.AreEqual(new DateTime(2015, 12, 31, 21, 0, 0), baseDate.Proper(TimeframeSymbol.M30));
        }


        [TestMethod]
        [TestCategory("Proper.M15")]
        public void proper_m15_if_proper_value_is_passed_the_same_value_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 8, 11, 12, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 11, 12, 0, 0), baseDate.Proper(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Proper.M15")]
        public void proper_m15_for_weekend_value_function_returns_last_value_from_this_week()
        {
            DateTime baseDate = new DateTime(2016, 8, 13, 16, 15, 0);
            Assert.AreEqual(new DateTime(2016, 8, 12, 23, 45, 0), baseDate.Proper(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Proper.M15")]
        public void proper_m15_for_newYear_value_function_returns_last_valid_value_before()
        {
            DateTime baseDate = new DateTime(2016, 1, 1, 16, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 31, 21, 0, 0), baseDate.Proper(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Proper.M15")]
        public void proper_m15_for_weekendNewYear_value_function_returns_2345PM_last_friday_of_previous_year()
        {
            DateTime baseDate = new DateTime(2017, 1, 1, 12, 0, 0);
            Assert.AreEqual(new DateTime(2016, 12, 30, 23, 45, 0), baseDate.Proper(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Proper.M15")]
        public void proper_m15_for_time_between_full_hours_function_returns_earlier_full_hour()
        {
            DateTime baseDate = new DateTime(2016, 8, 11, 15, 47, 31);
            Assert.AreEqual(new DateTime(2016, 8, 11, 15, 45, 0), baseDate.Proper(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Proper.M15")]
        public void proper_m15_for_christmasEve_22__21_of_this_day_is_returned()
        {
            DateTime baseDate = new DateTime(2015, 12, 24, 22, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 24, 21, 0, 0), baseDate.Proper(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Proper.M15")]
        public void proper_m15_for_newYearEve_22__21_of_this_day_is_returned()
        {
            DateTime baseDate = new DateTime(2015, 12, 31, 22, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 31, 21, 0, 0), baseDate.Proper(TimeframeSymbol.M15));
        }


        [TestMethod]
        [TestCategory("Proper.M5")]
        public void proper_m5_if_proper_value_is_passed_the_same_value_is_returned()
        {
            DateTime baseDate = new DateTime(2016, 8, 11, 12, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 11, 12, 0, 0), baseDate.Proper(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Proper.M5")]
        public void proper_m5_for_weekend_value_function_returns_last_value_from_this_week()
        {
            DateTime baseDate = new DateTime(2016, 8, 13, 16, 15, 0);
            Assert.AreEqual(new DateTime(2016, 8, 12, 23, 55, 0), baseDate.Proper(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Proper.M5")]
        public void proper_m5_for_newYear_value_function_returns_last_valid_value_before()
        {
            DateTime baseDate = new DateTime(2016, 1, 1, 16, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 31, 21, 0, 0), baseDate.Proper(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Proper.M5")]
        public void proper_m5_for_weekendNewYear_value_function_returns_2355PM_last_friday_of_previous_year()
        {
            DateTime baseDate = new DateTime(2017, 1, 1, 12, 0, 0);
            Assert.AreEqual(new DateTime(2016, 12, 30, 23, 55, 0), baseDate.Proper(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Proper.M5")]
        public void proper_m5_for_time_between_full_hours_function_returns_earlier_full_hour()
        {
            DateTime baseDate = new DateTime(2016, 8, 11, 15, 41, 31);
            Assert.AreEqual(new DateTime(2016, 8, 11, 15, 40, 0), baseDate.Proper(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Proper.M5")]
        public void proper_m5_for_christmasEve_22__21_of_this_day_is_returned()
        {
            DateTime baseDate = new DateTime(2015, 12, 24, 22, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 24, 21, 0, 0), baseDate.Proper(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Proper.M5")]
        public void proper_m5_for_newYearEve_22__21_of_this_day_is_returned()
        {
            DateTime baseDate = new DateTime(2015, 12, 31, 22, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 31, 21, 0, 0), baseDate.Proper(TimeframeSymbol.M5));
        }



        #endregion proper



        #region next

        [TestMethod]
        [TestCategory("Next.MN1")]
        public void next_MN1_returns_proper_value_if_base_date_is_first_day_of_month()
        {
            DateTime baseDate = new DateTime(2016, 8, 1);
            Assert.AreEqual(new DateTime(2016, 9, 1), baseDate.getNext(TimeframeSymbol.MN1));
        }

        [TestMethod]
        [TestCategory("Next.MN1")]
        public void next_MN1_returns_proper_value_if_base_date_is_not_first_day_of_month()
        {
            DateTime baseDate = new DateTime(2016, 8, 10);
            Assert.AreEqual(new DateTime(2016, 9, 1), baseDate.getNext(TimeframeSymbol.MN1));
        }

        [TestMethod]
        [TestCategory("Next.W1")]
        public void next_W1_returns_proper_value_for_middle_week_day()
        {
            DateTime d = new DateTime(2016, 8, 16);
            Assert.AreEqual(new DateTime(2016, 8, 21, 0, 0, 0), d.getNext(TimeframeSymbol.W1));
        }
        
        [TestMethod]
        [TestCategory("Next.W1")]
        public void next_W1_returns_proper_value_for_sunday()
        {
            DateTime d = new DateTime(2016, 8, 14);
            Assert.AreEqual(new DateTime(2016, 8, 21, 0, 0, 0), d.getNext(TimeframeSymbol.W1));
        }

        [TestMethod]
        [TestCategory("Next.W1")]
        public void next_W1_returns_proper_value_for_saturday()
        {
            DateTime d = new DateTime(2016, 8, 20);
            Assert.AreEqual(new DateTime(2016, 8, 21, 0, 0, 0), d.getNext(TimeframeSymbol.W1));
        }


        [TestMethod]
        [TestCategory("Next.D1")]
        public void next_D1_returns_proper_value_for_middle_week_day()
        {
            DateTime d = new DateTime(2016, 8, 16);
            Assert.AreEqual(new DateTime(2016, 8, 17, 0, 0, 0), d.getNext(TimeframeSymbol.D1));
        }

        [TestMethod]
        [TestCategory("Next.D1")]
        public void next_D1_returns_proper_value_for_weekend()
        {
            DateTime d = new DateTime(2016, 8, 14);
            Assert.AreEqual(new DateTime(2016, 8, 15, 0, 0, 0), d.getNext(TimeframeSymbol.D1));
        }

        [TestMethod]
        [TestCategory("Next.D1")]
        public void next_D1_returns_proper_value_for_friday_christmas()
        {
            DateTime d = new DateTime(2015, 12, 24);
            Assert.AreEqual(new DateTime(2015, 12, 28), d.getNext(TimeframeSymbol.D1));
        }

        [TestMethod]
        [TestCategory("Next.D1")]
        public void next_D1_returns_proper_value_for_friday_newYear()
        {
            DateTime d = new DateTime(2015, 12, 31);
            Assert.AreEqual(new DateTime(2016, 1, 4), d.getNext(TimeframeSymbol.D1));
        }

        [TestMethod]
        [TestCategory("Next.D1")]
        public void next_D1_returns_proper_value_for_middle_day_value()
        {
            DateTime d = new DateTime(2016, 8, 17, 15, 13, 21);
            Assert.AreEqual(new DateTime(2016, 8, 18, 0, 0, 0), d.getNext(TimeframeSymbol.D1));
        }

        [TestMethod]
        [TestCategory("Next.H4")]
        public void next_H4_returns_proper_value_for_middle_hours_value()
        {
            DateTime d = new DateTime(2016, 8, 17, 15, 13, 21);
            Assert.AreEqual(new DateTime(2016, 8, 17, 16, 0, 0), d.getNext(TimeframeSymbol.H4));
        }

        [TestMethod]
        [TestCategory("Next.H4")]
        public void next_H4_returns_proper_value_for_middle_week_value()
        {
            DateTime d = new DateTime(2016, 8, 17, 16, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 17, 20, 0, 0), d.getNext(TimeframeSymbol.H4));
        }

        [TestMethod]
        [TestCategory("Next.H4")]
        public void next_H4_returns_proper_value_for_last_week_quotation()
        {
            DateTime d = new DateTime(2016, 8, 19, 20, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.H4));
        }

        [TestMethod]
        [TestCategory("Next.H4")]
        public void next_H4_returns_proper_value_for_weekend_value()
        {
            DateTime d = new DateTime(2016, 8, 20, 16, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.H4));
        }

        [TestMethod]
        [TestCategory("Next.H4")]
        public void next_H4_returns_proper_value_for_last_quotations_before_christmas()
        {
            DateTime d = new DateTime(2014, 12, 24, 20, 0, 0);
            Assert.AreEqual(new DateTime(2014, 12, 26, 0, 0, 0), d.getNext(TimeframeSymbol.H4));
        }

        [TestMethod]
        [TestCategory("Next.H4")]
        public void next_H4_returns_proper_value_for_last_quotations_before_newYear()
        {
            DateTime d = new DateTime(2014, 12, 31, 20, 0, 0);
            Assert.AreEqual(new DateTime(2015, 1, 2, 0, 0, 0), d.getNext(TimeframeSymbol.H4));
        }

        [TestMethod]
        [TestCategory("Next.H4")]
        public void next_H4_returns_proper_value_for_last_quotations_before_friday_newYear()
        {
            DateTime d = new DateTime(2015, 12, 31, 20, 0, 0);
            Assert.AreEqual(new DateTime(2016, 1, 4, 0, 0, 0), d.getNext(TimeframeSymbol.H4));
        }

        [TestMethod]
        [TestCategory("Next.H4")]
        public void next_H4_returns_proper_value_for_last_quotations_before_friday_christmas()
        {
            DateTime d = new DateTime(2015, 12, 24, 20, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 28, 0, 0, 0), d.getNext(TimeframeSymbol.H4));
        }






        [TestMethod]
        [TestCategory("Next.H1")]
        public void next_H1_returns_proper_value_for_middle_hours_value()
        {
            DateTime d = new DateTime(2016, 8, 17, 15, 13, 21);
            Assert.AreEqual(new DateTime(2016, 8, 17, 16, 0, 0), d.getNext(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Next.H1")]
        public void next_H1_returns_proper_value_for_middle_week_value()
        {
            DateTime d = new DateTime(2016, 8, 17, 16, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 17, 17, 0, 0), d.getNext(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Next.H1")]
        public void next_H1_returns_proper_value_for_last_week_quotation()
        {
            DateTime d = new DateTime(2016, 8, 19, 23, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Next.H1")]
        public void next_H1_returns_proper_value_for_weekend_value()
        {
            DateTime d = new DateTime(2016, 8, 20, 16, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Next.H1")]
        public void next_H1_returns_proper_value_for_last_quotations_before_christmas()
        {
            DateTime d = new DateTime(2014, 12, 24, 21, 0, 0);
            Assert.AreEqual(new DateTime(2014, 12, 26, 0, 0, 0), d.getNext(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Next.H1")]
        public void next_H1_returns_proper_value_for_last_quotations_before_newYear()
        {
            DateTime d = new DateTime(2014, 12, 31, 21, 0, 0);
            Assert.AreEqual(new DateTime(2015, 1, 2, 0, 0, 0), d.getNext(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Next.H1")]
        public void next_H1_returns_proper_value_for_last_quotations_before_friday_newYear()
        {
            DateTime d = new DateTime(2015, 12, 31, 21, 0, 0);
            Assert.AreEqual(new DateTime(2016, 1, 4, 0, 0, 0), d.getNext(TimeframeSymbol.H1));
        }

        [TestMethod]
        [TestCategory("Next.H1")]
        public void next_H1_returns_proper_value_for_last_quotations_before_friday_christmas()
        {
            DateTime d = new DateTime(2015, 12, 24, 21, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 28, 0, 0, 0), d.getNext(TimeframeSymbol.H1));
        }





        [TestMethod]
        [TestCategory("Next.M30")]
        public void next_M30_returns_proper_value_for_middle_hours_value()
        {
            DateTime d = new DateTime(2016, 8, 17, 15, 13, 21);
            Assert.AreEqual(new DateTime(2016, 8, 17, 15, 30, 0), d.getNext(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Next.M30")]
        public void next_M30_returns_proper_value_for_middle_week_value()
        {
            DateTime d = new DateTime(2016, 8, 17, 16, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 17, 16, 30, 0), d.getNext(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Next.M30")]
        public void next_M30_returns_proper_value_for_last_week_quotation()
        {
            DateTime d = new DateTime(2016, 8, 19, 23, 30, 0);
            Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Next.M30")]
        public void next_M30_returns_proper_value_for_weekend_value()
        {
            DateTime d = new DateTime(2016, 8, 20, 16, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Next.M30")]
        public void next_M30_returns_proper_value_for_last_quotations_before_christmas()
        {
            DateTime d = new DateTime(2014, 12, 24, 21, 0, 0);
            Assert.AreEqual(new DateTime(2014, 12, 26, 0, 0, 0), d.getNext(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Next.M30")]
        public void next_M30_returns_proper_value_for_last_quotations_before_newYear()
        {
            DateTime d = new DateTime(2014, 12, 31, 21, 0, 0);
            Assert.AreEqual(new DateTime(2015, 1, 2, 0, 0, 0), d.getNext(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Next.M30")]
        public void next_M30_returns_proper_value_for_last_quotations_before_friday_newYear()
        {
            DateTime d = new DateTime(2015, 12, 31, 21, 0, 0);
            Assert.AreEqual(new DateTime(2016, 1, 4, 0, 0, 0), d.getNext(TimeframeSymbol.M30));
        }

        [TestMethod]
        [TestCategory("Next.M30")]
        public void next_M30_returns_proper_value_for_last_quotations_before_friday_christmas()
        {
            DateTime d = new DateTime(2015, 12, 24, 21, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 28, 0, 0, 0), d.getNext(TimeframeSymbol.M30));
        }



        [TestMethod]
        [TestCategory("Next.M15")]
        public void next_M15_returns_proper_value_for_middle_hours_value()
        {
            DateTime d = new DateTime(2016, 8, 17, 15, 13, 21);
            Assert.AreEqual(new DateTime(2016, 8, 17, 15, 15, 0), d.getNext(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Next.M15")]
        public void next_M15_returns_proper_value_for_middle_week_value()
        {
            DateTime d = new DateTime(2016, 8, 17, 16, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 17, 16, 15, 0), d.getNext(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Next.M15")]
        public void next_M15_returns_proper_value_for_last_week_quotation()
        {
            DateTime d = new DateTime(2016, 8, 19, 23, 45, 0);
            Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Next.M15")]
        public void next_M15_returns_proper_value_for_weekend_value()
        {
            DateTime d = new DateTime(2016, 8, 20, 16, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Next.M15")]
        public void next_M15_returns_proper_value_for_last_quotations_before_christmas()
        {
            DateTime d = new DateTime(2014, 12, 24, 21, 0, 0);
            Assert.AreEqual(new DateTime(2014, 12, 26, 0, 0, 0), d.getNext(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Next.M15")]
        public void next_M15_returns_proper_value_for_last_quotations_before_newYear()
        {
            DateTime d = new DateTime(2014, 12, 31, 21, 0, 0);
            Assert.AreEqual(new DateTime(2015, 1, 2, 0, 0, 0), d.getNext(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Next.M15")]
        public void next_M15_returns_proper_value_for_last_quotations_before_friday_newYear()
        {
            DateTime d = new DateTime(2015, 12, 31, 21, 0, 0);
            Assert.AreEqual(new DateTime(2016, 1, 4, 0, 0, 0), d.getNext(TimeframeSymbol.M15));
        }

        [TestMethod]
        [TestCategory("Next.M15")]
        public void next_M15_returns_proper_value_for_last_quotations_before_friday_christmas()
        {
            DateTime d = new DateTime(2015, 12, 24, 21, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 28, 0, 0, 0), d.getNext(TimeframeSymbol.M15));
        }




        [TestMethod]
        [TestCategory("Next.M5")]
        public void next_M5_returns_proper_value_for_middle_hours_value()
        {
            DateTime d = new DateTime(2016, 8, 17, 15, 3, 21);
            Assert.AreEqual(new DateTime(2016, 8, 17, 15, 5, 0), d.getNext(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Next.M5")]
        public void next_M5_returns_proper_value_for_middle_week_value()
        {
            DateTime d = new DateTime(2016, 8, 17, 16, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 17, 16, 5, 0), d.getNext(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Next.M5")]
        public void next_M5_returns_proper_value_for_last_week_quotation()
        {
            DateTime d = new DateTime(2016, 8, 19, 23, 55, 0);
            Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Next.M5")]
        public void next_M5_returns_proper_value_for_weekend_value()
        {
            DateTime d = new DateTime(2016, 8, 20, 16, 0, 0);
            Assert.AreEqual(new DateTime(2016, 8, 22, 0, 0, 0), d.getNext(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Next.M5")]
        public void next_M5_returns_proper_value_for_last_quotations_before_christmas()
        {
            DateTime d = new DateTime(2014, 12, 24, 21, 0, 0);
            Assert.AreEqual(new DateTime(2014, 12, 26, 0, 0, 0), d.getNext(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Next.M5")]
        public void next_M5_returns_proper_value_for_last_quotations_before_newYear()
        {
            DateTime d = new DateTime(2014, 12, 31, 21, 0, 0);
            Assert.AreEqual(new DateTime(2015, 1, 2, 0, 0, 0), d.getNext(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Next.M5")]
        public void next_M5_returns_proper_value_for_last_quotations_before_friday_newYear()
        {
            DateTime d = new DateTime(2015, 12, 31, 21, 0, 0);
            Assert.AreEqual(new DateTime(2016, 1, 4, 0, 0, 0), d.getNext(TimeframeSymbol.M5));
        }

        [TestMethod]
        [TestCategory("Next.M5")]
        public void next_M5_returns_proper_value_for_last_quotations_before_friday_christmas()
        {
            DateTime d = new DateTime(2015, 12, 24, 21, 0, 0);
            Assert.AreEqual(new DateTime(2015, 12, 28, 0, 0, 0), d.getNext(TimeframeSymbol.M5));
        }


        #endregion next


        #region M5

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_zero_the_same_date_is_returned()
        {
            DateTime d = new DateTime(2016, 4, 21, 12, 0, 0);
            DateTime expected = new DateTime(2016, 4, 21, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 0);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_positive_without_new_year_not_week_break()
        {
            DateTime d = new DateTime(2016, 8, 9, 8, 0, 0);
            DateTime expected = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 912);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_positive_with_week_break()
        {
            DateTime d = new DateTime(2016, 8, 2, 8, 0, 0);
            DateTime expected = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 2352);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_positive_with_newYear_at_week()
        {
            DateTime d = new DateTime(2014, 12, 29, 8, 0, 0);
            DateTime expected = new DateTime(2015, 1, 7, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 1789);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_positive_with_newYear_at_weekend()
        {
            DateTime d = new DateTime(2016, 12, 28, 8, 0, 0);
            DateTime expected = new DateTime(2017, 1, 5, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 1824);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_positive_with_christmas_at_week()
        {
            DateTime d = new DateTime(2014, 12, 23, 16, 0, 0);
            DateTime expected = new DateTime(2014, 12, 30, 13, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 1081);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_positive_with_christmas_at_weekend()
        {
            DateTime d = new DateTime(2016, 12, 22, 12, 0, 0);
            DateTime expected = new DateTime(2016, 12, 28, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 1200);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_positive_with_christmas_and_newYear()
        {
            DateTime d = new DateTime(2015, 12, 22, 16, 0, 0);
            DateTime expected = new DateTime(2016, 1, 5, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 2186);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_positive_omit_weekend_after_skip_christmas()
        {
            DateTime d = new DateTime(2015, 12, 24, 20, 0, 0);
            DateTime expected = new DateTime(2015, 12, 28, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 61);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_positive_omit_weekend_after_skip_newYear()
        {
            DateTime d = new DateTime(2015, 12, 31, 20, 0, 0);
            DateTime expected = new DateTime(2016, 1, 4, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 61);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_positive_omit_christmas_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 22, 20, 0, 0);
            DateTime expected = new DateTime(2017, 12, 26, 0, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 48);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_positive_omit_newYear_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 29, 20, 0, 0);
            DateTime expected = new DateTime(2018, 1, 2, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, 96);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_negative_without_new_year_not_week_break()
        {
            DateTime d = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime expected = new DateTime(2016, 8, 9, 8, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -912);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_negative_with_week_break()
        {
            DateTime d = new DateTime(2016, 7, 26, 4, 0, 0);
            DateTime expected = new DateTime(2016, 7, 15, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -2016);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_negative_with_newYear_at_week()
        {
            DateTime d = new DateTime(2015, 1, 6, 12, 0, 0);
            DateTime expected = new DateTime(2014, 12, 30, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -1213);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_negative_with_newYear_at_weekend()
        {
            DateTime d = new DateTime(2017, 1, 6, 16, 0, 0);
            DateTime expected = new DateTime(2016, 12, 27, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -2352);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_negative_with_christmas_at_week()
        {
            DateTime d = new DateTime(2013, 12, 27, 12, 0, 0);
            DateTime expected = new DateTime(2013, 12, 23, 13, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -817);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_negative_with_christmas_at_weekend()
        {
            DateTime d = new DateTime(2011, 12, 28, 16, 0, 0);
            DateTime expected = new DateTime(2011, 12, 22, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -1200);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_negative_with_christmas_and_newYear()
        {
            DateTime d = new DateTime(2015, 1, 7, 12, 0, 0);
            DateTime expected = new DateTime(2013, 12, 23, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -77044);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_negative_omit_weekend_after_skip_christmas()
        {
            DateTime d = new DateTime(2015, 12, 28, 4, 0, 0);
            DateTime expected = new DateTime(2015, 12, 24, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -61);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_negative_omit_weekend_after_skip_newYear()
        {
            DateTime d = new DateTime(2016, 1, 4, 4, 0, 0);
            DateTime expected = new DateTime(2015, 12, 31, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -61);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_negative_omit_christmas_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 26, 0, 0, 0);
            DateTime expected = new DateTime(2017, 12, 22, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -48);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M5_if_units_negative_omit_newYear_after_skip_weekend()
        {
            DateTime d = new DateTime(2018, 1, 2, 4, 0, 0);
            DateTime expected = new DateTime(2017, 12, 29, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M5, -96);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_if_the_same_date_is_given_zero_is_returned()
        {           
             DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_if_date_in_the_same_range_is_given_zero_is_returned()
        {           
             DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 11, 12, 1, 53);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_in_the_same_week()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 16, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 12, 20, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(624, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_after_weekend()
        {           
             DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 16, 20, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(672, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_weekends_later()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 16, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 22, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(2256, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_earlier_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 23, 8, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(2544, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_later_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 25, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(3216, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_in_the_same_week_after_new_year()
        {           
             DateTime d1 = new DateTime(2013, 12, 30, 16, 0, 0);            
             DateTime d2 = new DateTime(2014, 1, 3, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(781, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_after_weekend_and_after_newYear()
        {           
             DateTime d1 = new DateTime(2014, 12, 30, 16, 0, 0);            
             DateTime d2 = new DateTime(2015, 1, 6, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(1069, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_after_weekend_newYear()
        {           
             DateTime d1 = new DateTime(2011, 12, 28, 12, 0, 0);            
             DateTime d2 = new DateTime(2012, 1, 3, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(1200, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_in_the_same_week_after_christmas()
        {           
             DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 26, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(589, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_after_weekend_and_after_christmas()
        {           
             DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 29, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(829, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_later_after_weekend_christmas()
        {           
             DateTime d1 = new DateTime(2016, 12, 23, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 12, 27, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(624, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_newYears_later()
        {           
             DateTime d1 = new DateTime(2013, 10, 15, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 11, 11, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(229374, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_in_the_same_week()
        {           
             DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 10, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(-528, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_before_weekend()
        {           
             DateTime d1 = new DateTime(2016, 8, 17, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(-1152, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_later_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 16, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(-2544, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_earlier_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 19, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(-3408, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_weekends_earlier()
        {           
             DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 7, 21, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(-4272, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_new_year()
        {           
             DateTime d1 = new DateTime(2015, 1, 2, 16, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 31, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(-301, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_newYear()
        {           
             DateTime d1 = new DateTime(2014, 1, 2, 12, 0, 0);            
             DateTime d2 = new DateTime(2013, 12, 27, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(-781, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_before_weekend_newYear()
        {           
             DateTime d1 = new DateTime(2017, 1, 3, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 12, 29, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(-864, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_christmas()
        {           
             DateTime d1 = new DateTime(2014, 12, 26, 12, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 24, 20, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(-157, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_christmas()
        {           
             DateTime d1 = new DateTime(2015, 12, 29, 12, 0, 0);            
             DateTime d2 = new DateTime(2015, 12, 24, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(-541, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_days_earlier_before_weekend_christmas()
        {           
             DateTime d1 = new DateTime(2016, 12, 26, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 12, 23, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(-240, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M5")]
        public void countTimeUnits_m5_returns_proper_value_for_date_few_newYears_earlier()
        {           
             DateTime d1 = new DateTime(2016, 12, 26, 16, 0, 0);            
             DateTime d2 = new DateTime(2012, 10, 23, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M5);
             Assert.AreEqual(-311048, result);
        }



        #endregion M5



        #region M15

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_zero_the_same_date_is_returned()
        {
            DateTime d = new DateTime(2016, 4, 21, 12, 0, 0);
            DateTime expected = new DateTime(2016, 4, 21, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 0);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_positive_without_new_year_not_week_break()
        {
            DateTime d = new DateTime(2016, 8, 9, 8, 0, 0);
            DateTime expected = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 304);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_positive_with_week_break()
        {
            DateTime d = new DateTime(2016, 8, 2, 8, 0, 0);
            DateTime expected = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 784);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_positive_with_newYear_at_week()
        {
            DateTime d = new DateTime(2014, 12, 29, 8, 0, 0);
            DateTime expected = new DateTime(2015, 1, 7, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 597);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_positive_with_newYear_at_weekend()
        {
            DateTime d = new DateTime(2016, 12, 28, 8, 0, 0);
            DateTime expected = new DateTime(2017, 1, 5, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 608);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_positive_with_christmas_at_week()
        {
            DateTime d = new DateTime(2014, 12, 23, 16, 0, 0);
            DateTime expected = new DateTime(2014, 12, 30, 13, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 361);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_positive_with_christmas_at_weekend()
        {
            DateTime d = new DateTime(2016, 12, 22, 12, 0, 0);
            DateTime expected = new DateTime(2016, 12, 28, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 400);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_positive_with_christmas_and_newYear()
        {
            DateTime d = new DateTime(2015, 12, 22, 16, 0, 0);
            DateTime expected = new DateTime(2016, 1, 5, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 730);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_positive_omit_weekend_after_skip_christmas()
        {
            DateTime d = new DateTime(2015, 12, 24, 20, 0, 0);
            DateTime expected = new DateTime(2015, 12, 28, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 21);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_positive_omit_weekend_after_skip_newYear()
        {
            DateTime d = new DateTime(2015, 12, 31, 20, 0, 0);
            DateTime expected = new DateTime(2016, 1, 4, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 21);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_positive_omit_christmas_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 22, 20, 0, 0);
            DateTime expected = new DateTime(2017, 12, 26, 0, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 16);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_positive_omit_newYear_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 29, 20, 0, 0);
            DateTime expected = new DateTime(2018, 1, 2, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, 32);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_negative_without_new_year_not_week_break()
        {
            DateTime d = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime expected = new DateTime(2016, 8, 9, 8, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -304);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_negative_with_week_break()
        {
            DateTime d = new DateTime(2016, 7, 26, 4, 0, 0);
            DateTime expected = new DateTime(2016, 7, 15, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -672);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_negative_with_newYear_at_week()
        {
            DateTime d = new DateTime(2015, 1, 6, 12, 0, 0);
            DateTime expected = new DateTime(2014, 12, 30, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -405);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_negative_with_newYear_at_weekend()
        {
            DateTime d = new DateTime(2017, 1, 6, 16, 0, 0);
            DateTime expected = new DateTime(2016, 12, 27, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -784);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_negative_with_christmas_at_week()
        {
            DateTime d = new DateTime(2013, 12, 27, 12, 0, 0);
            DateTime expected = new DateTime(2013, 12, 23, 13, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -273);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_negative_with_christmas_at_weekend()
        {
            DateTime d = new DateTime(2011, 12, 28, 16, 0, 0);
            DateTime expected = new DateTime(2011, 12, 22, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -400);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_negative_with_christmas_and_newYear()
        {
            DateTime d = new DateTime(2015, 1, 7, 12, 0, 0);
            DateTime expected = new DateTime(2013, 12, 23, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -25684);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_negative_omit_weekend_after_skip_christmas()
        {
            DateTime d = new DateTime(2015, 12, 28, 4, 0, 0);
            DateTime expected = new DateTime(2015, 12, 24, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -21);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_negative_omit_weekend_after_skip_newYear()
        {
            DateTime d = new DateTime(2016, 1, 4, 4, 0, 0);
            DateTime expected = new DateTime(2015, 12, 31, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -21);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_negative_omit_christmas_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 26, 0, 0, 0);
            DateTime expected = new DateTime(2017, 12, 22, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -16);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M15_if_units_negative_omit_newYear_after_skip_weekend()
        {
            DateTime d = new DateTime(2018, 1, 2, 4, 0, 0);
            DateTime expected = new DateTime(2017, 12, 29, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M15, -32);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_if_the_same_date_is_given_zero_is_returned()
        {           
             DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_if_date_in_the_same_range_is_given_zero_is_returned()
        {           
             DateTime d1 = new DateTime(2016, 8, 11, 14, 15, 0);            
             DateTime d2 = new DateTime(2016, 8, 11, 14, 21, 53);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_in_the_same_week()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 16, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 12, 20, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(208, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_after_weekend()
        {           
             DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 16, 20, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(224, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_weekends_later()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 16, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 22, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(752, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_earlier_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 23, 8, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(848, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_later_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 25, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(1072, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_in_the_same_week_after_new_year()
        {           
             DateTime d1 = new DateTime(2013, 12, 30, 16, 0, 0);            
             DateTime d2 = new DateTime(2014, 1, 3, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(261, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_after_weekend_and_after_newYear()
        {           
             DateTime d1 = new DateTime(2014, 12, 30, 16, 0, 0);            
             DateTime d2 = new DateTime(2015, 1, 6, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(357, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_after_weekend_newYear()
        {           
             DateTime d1 = new DateTime(2011, 12, 28, 12, 0, 0);            
             DateTime d2 = new DateTime(2012, 1, 3, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(400, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_in_the_same_week_after_christmas()
        {           
             DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 26, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(197, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_after_weekend_and_after_christmas()
        {           
             DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 29, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(277, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_later_after_weekend_christmas()
        {           
             DateTime d1 = new DateTime(2016, 12, 23, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 12, 27, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(208, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_newYears_later()
        {           
             DateTime d1 = new DateTime(2013, 10, 15, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 11, 11, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(76462, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_in_the_same_week()
        {           
             DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 10, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(-176, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_before_weekend()
        {           
             DateTime d1 = new DateTime(2016, 8, 17, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(-384, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_later_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 16, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(-848, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_earlier_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 19, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(-1136, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_weekends_earlier()
        {           
             DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 7, 21, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(-1424, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_new_year()
        {           
             DateTime d1 = new DateTime(2015, 1, 2, 16, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 31, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(-101, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_newYear()
        {           
             DateTime d1 = new DateTime(2014, 1, 2, 12, 0, 0);            
             DateTime d2 = new DateTime(2013, 12, 27, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(-261, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_before_weekend_newYear()
        {           
             DateTime d1 = new DateTime(2017, 1, 3, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 12, 29, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(-288, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_christmas()
        {           
             DateTime d1 = new DateTime(2014, 12, 26, 12, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 24, 20, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(-53, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_christmas()
        {           
             DateTime d1 = new DateTime(2015, 12, 29, 12, 0, 0);            
             DateTime d2 = new DateTime(2015, 12, 24, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(-181, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_days_earlier_before_weekend_christmas()
        {           
             DateTime d1 = new DateTime(2016, 12, 26, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 12, 23, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(-80, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M15")]
        public void countTimeUnits_m15_returns_proper_value_for_date_few_newYears_earlier()
        {           
             DateTime d1 = new DateTime(2016, 12, 26, 16, 0, 0);            
             DateTime d2 = new DateTime(2012, 10, 23, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.M15);
             Assert.AreEqual(-103688, result);
        }

        #endregion M15



        #region M30


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_zero_the_same_date_is_returned()
        {
            DateTime d = new DateTime(2016, 4, 21, 12, 0, 0);
            DateTime expected = new DateTime(2016, 4, 21, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_positive_without_new_year_not_week_break()
        {
            DateTime d = new DateTime(2016, 8, 9, 8, 0, 0);
            DateTime expected = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 152);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_positive_with_week_break()
        {
            DateTime d = new DateTime(2016, 8, 2, 8, 0, 0);
            DateTime expected = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 392);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_positive_with_newYear_at_week()
        {
            DateTime d = new DateTime(2014, 12, 29, 8, 0, 0);
            DateTime expected = new DateTime(2015, 1, 7, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 299);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_positive_with_newYear_at_weekend()
        {
            DateTime d = new DateTime(2016, 12, 28, 8, 0, 0);
            DateTime expected = new DateTime(2017, 1, 5, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 304);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_positive_with_christmas_at_week()
        {
            DateTime d = new DateTime(2014, 12, 23, 16, 0, 0);
            DateTime expected = new DateTime(2014, 12, 30, 13, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 181);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_positive_with_christmas_at_weekend()
        {
            DateTime d = new DateTime(2016, 12, 22, 12, 0, 0);
            DateTime expected = new DateTime(2016, 12, 28, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 200);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_positive_with_christmas_and_newYear()
        {
            DateTime d = new DateTime(2015, 12, 22, 16, 0, 0);
            DateTime expected = new DateTime(2016, 1, 5, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 366);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_positive_omit_weekend_after_skip_christmas()
        {
            DateTime d = new DateTime(2015, 12, 24, 20, 0, 0);
            DateTime expected = new DateTime(2015, 12, 28, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 11);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_positive_omit_weekend_after_skip_newYear()
        {
            DateTime d = new DateTime(2015, 12, 31, 20, 0, 0);
            DateTime expected = new DateTime(2016, 1, 4, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 11);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_positive_omit_christmas_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 22, 20, 0, 0);
            DateTime expected = new DateTime(2017, 12, 26, 0, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 8);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_positive_omit_newYear_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 29, 20, 0, 0);
            DateTime expected = new DateTime(2018, 1, 2, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, 16);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_negative_without_new_year_not_week_break()
        {
            DateTime d = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime expected = new DateTime(2016, 8, 9, 8, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -152);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_negative_with_week_break()
        {
            DateTime d = new DateTime(2016, 7, 26, 4, 0, 0);
            DateTime expected = new DateTime(2016, 7, 15, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -336);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_negative_with_newYear_at_week()
        {
            DateTime d = new DateTime(2015, 1, 6, 12, 0, 0);
            DateTime expected = new DateTime(2014, 12, 30, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -203);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_negative_with_newYear_at_weekend()
        {
            DateTime d = new DateTime(2017, 1, 6, 16, 0, 0);
            DateTime expected = new DateTime(2016, 12, 27, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -392);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_negative_with_christmas_at_week()
        {
            DateTime d = new DateTime(2013, 12, 27, 12, 0, 0);
            DateTime expected = new DateTime(2013, 12, 23, 13, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -137);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_negative_with_christmas_at_weekend()
        {
            DateTime d = new DateTime(2011, 12, 28, 16, 0, 0);
            DateTime expected = new DateTime(2011, 12, 22, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -200);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_negative_with_christmas_and_newYear()
        {
            DateTime d = new DateTime(2015, 1, 7, 12, 0, 0);
            DateTime expected = new DateTime(2013, 12, 23, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -12844);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_negative_omit_weekend_after_skip_christmas()
        {
            DateTime d = new DateTime(2015, 12, 28, 4, 0, 0);
            DateTime expected = new DateTime(2015, 12, 24, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -11);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_negative_omit_weekend_after_skip_newYear()
        {
            DateTime d = new DateTime(2016, 1, 4, 4, 0, 0);
            DateTime expected = new DateTime(2015, 12, 31, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -11);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_negative_omit_christmas_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 26, 0, 0, 0);
            DateTime expected = new DateTime(2017, 12, 22, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -8);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_M30_if_units_negative_omit_newYear_after_skip_weekend()
        {
            DateTime d = new DateTime(2018, 1, 2, 4, 0, 0);
            DateTime expected = new DateTime(2017, 12, 29, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.M30, -16);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_if_the_same_date_is_given_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_if_date_in_the_same_range_is_given_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 8, 11, 14, 21, 53);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_in_the_same_week()
        {
            DateTime d1 = new DateTime(2016, 8, 10, 16, 0, 0);
            DateTime d2 = new DateTime(2016, 8, 12, 20, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(104, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_after_weekend()
        {
            DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 8, 16, 20, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(112, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_weekends_later()
        {
            DateTime d1 = new DateTime(2016, 8, 10, 16, 0, 0);
            DateTime d2 = new DateTime(2016, 8, 22, 12, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(376, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_earlier_than_base_date()
        {
            DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 8, 23, 8, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(424, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_later_than_base_date()
        {
            DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 8, 25, 16, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(536, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_in_the_same_week_after_new_year()
        {
            DateTime d1 = new DateTime(2013, 12, 30, 16, 0, 0);
            DateTime d2 = new DateTime(2014, 1, 3, 12, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(131, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_after_weekend_and_after_newYear()
        {
            DateTime d1 = new DateTime(2014, 12, 30, 16, 0, 0);
            DateTime d2 = new DateTime(2015, 1, 6, 12, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(179, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_after_weekend_newYear()
        {
            DateTime d1 = new DateTime(2011, 12, 28, 12, 0, 0);
            DateTime d2 = new DateTime(2012, 1, 3, 16, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(200, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_in_the_same_week_after_christmas()
        {
            DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);
            DateTime d2 = new DateTime(2014, 12, 26, 16, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(99, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_after_weekend_and_after_christmas()
        {
            DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);
            DateTime d2 = new DateTime(2014, 12, 29, 12, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(139, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_later_after_weekend_christmas()
        {
            DateTime d1 = new DateTime(2016, 12, 23, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 12, 27, 16, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(104, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_newYears_later()
        {
            DateTime d1 = new DateTime(2013, 10, 15, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 11, 11, 16, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(38234, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_in_the_same_week()
        {
            DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 8, 10, 16, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(-88, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_before_weekend()
        {
            DateTime d1 = new DateTime(2016, 8, 17, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(-192, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_later_than_base_date()
        {
            DateTime d1 = new DateTime(2016, 8, 16, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(-424, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_earlier_than_base_date()
        {
            DateTime d1 = new DateTime(2016, 8, 19, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(-568, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_weekends_earlier()
        {
            DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 7, 21, 16, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(-712, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_new_year()
        {
            DateTime d1 = new DateTime(2015, 1, 2, 16, 0, 0);
            DateTime d2 = new DateTime(2014, 12, 31, 12, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(-51, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_newYear()
        {
            DateTime d1 = new DateTime(2014, 1, 2, 12, 0, 0);
            DateTime d2 = new DateTime(2013, 12, 27, 16, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(-131, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_before_weekend_newYear()
        {
            DateTime d1 = new DateTime(2017, 1, 3, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 12, 29, 12, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(-144, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_christmas()
        {
            DateTime d1 = new DateTime(2014, 12, 26, 12, 0, 0);
            DateTime d2 = new DateTime(2014, 12, 24, 20, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(-27, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_christmas()
        {
            DateTime d1 = new DateTime(2015, 12, 29, 12, 0, 0);
            DateTime d2 = new DateTime(2015, 12, 24, 12, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(-91, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_days_earlier_before_weekend_christmas()
        {
            DateTime d1 = new DateTime(2016, 12, 26, 12, 0, 0);
            DateTime d2 = new DateTime(2016, 12, 23, 16, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(-40, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.M30")]
        public void countTimeUnits_m30_returns_proper_value_for_date_few_newYears_earlier()
        {
            DateTime d1 = new DateTime(2016, 12, 26, 16, 0, 0);
            DateTime d2 = new DateTime(2012, 10, 23, 16, 0, 0);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.M30);
            Assert.AreEqual(-51848, result);
        }



        #endregion M30



        #region H1

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_zero_the_same_date_is_returned()
        {
            DateTime d = new DateTime(2016, 4, 21, 12, 0, 0);
            DateTime expected = new DateTime(2016, 4, 21, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 0);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_positive_without_new_year_not_week_break()
        {
            DateTime d = new DateTime(2016, 8, 9, 8, 0, 0);
            DateTime expected = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 76);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_positive_with_week_break()
        {
            DateTime d = new DateTime(2016, 8, 2, 8, 0, 0);
            DateTime expected = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 196);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_positive_with_newYear_at_week()
        {
            DateTime d = new DateTime(2014, 12, 29, 8, 0, 0);
            DateTime expected = new DateTime(2015, 1, 7, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 150);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_positive_with_newYear_at_weekend()
        {
            DateTime d = new DateTime(2016, 12, 28, 8, 0, 0);
            DateTime expected = new DateTime(2017, 1, 5, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 152);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_positive_with_christmas_at_week()
        {
            DateTime d = new DateTime(2014, 12, 23, 16, 0, 0);
            DateTime expected = new DateTime(2014, 12, 30, 13, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 91);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_positive_with_christmas_at_weekend()
        {
            DateTime d = new DateTime(2016, 12, 22, 12, 0, 0);
            DateTime expected = new DateTime(2016, 12, 28, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 100);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_positive_with_christmas_and_newYear()
        {
            DateTime d = new DateTime(2015, 12, 22, 16, 0, 0);
            DateTime expected = new DateTime(2016, 1, 5, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 184);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_positive_omit_weekend_after_skip_christmas()
        {
            DateTime d = new DateTime(2015, 12, 24, 20, 0, 0);
            DateTime expected = new DateTime(2015, 12, 28, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 6);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_positive_omit_weekend_after_skip_newYear()
        {
            DateTime d = new DateTime(2015, 12, 31, 20, 0, 0);
            DateTime expected = new DateTime(2016, 1, 4, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 6);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_positive_omit_christmas_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 22, 20, 0, 0);
            DateTime expected = new DateTime(2017, 12, 26, 0, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 4);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_positive_omit_newYear_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 29, 20, 0, 0);
            DateTime expected = new DateTime(2018, 1, 2, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, 8);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_negative_without_new_year_not_week_break()
        {
            DateTime d = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime expected = new DateTime(2016, 8, 9, 8, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, -76);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_negative_with_week_break()
        {
            DateTime d = new DateTime(2016, 7, 26, 4, 0, 0);
            DateTime expected = new DateTime(2016, 7, 15, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, -168);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_negative_with_newYear_at_week()
        {
            DateTime d = new DateTime(2015, 1, 6, 12, 0, 0);
            DateTime expected = new DateTime(2014, 12, 30, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, -102);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_negative_with_newYear_at_weekend()
        {
            DateTime d = new DateTime(2017, 1, 6, 16, 0, 0);
            DateTime expected = new DateTime(2016, 12, 27, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, -196);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_negative_with_christmas_at_week()
        {
            DateTime d = new DateTime(2013, 12, 27, 12, 0, 0);
            DateTime expected = new DateTime(2013, 12, 23, 13, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, -69);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_negative_with_christmas_at_weekend()
        {
            DateTime d = new DateTime(2011, 12, 28, 16, 0, 0);
            DateTime expected = new DateTime(2011, 12, 22, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, -100);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_negative_with_christmas_and_newYear()
        {
            DateTime d = new DateTime(2015, 1, 7, 12, 0, 0);
            DateTime expected = new DateTime(2013, 12, 23, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, -6424);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_negative_omit_weekend_after_skip_christmas()
        {
            DateTime d = new DateTime(2015, 12, 28, 4, 0, 0);
            DateTime expected = new DateTime(2015, 12, 24, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, -6);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_negative_omit_weekend_after_skip_newYear()
        {
            DateTime d = new DateTime(2016, 1, 4, 4, 0, 0);
            DateTime expected = new DateTime(2015, 12, 31, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, -6);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_negative_omit_christmas_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 26, 0, 0, 0);
            DateTime expected = new DateTime(2017, 12, 22, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, -4);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H1_if_units_negative_omit_newYear_after_skip_weekend()
        {
            DateTime d = new DateTime(2018, 1, 2, 4, 0, 0);
            DateTime expected = new DateTime(2017, 12, 29, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H1, -8);

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_if_the_same_date_is_given_zero_is_returned()
        {           
             DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_if_date_in_the_same_range_is_given_zero_is_returned()
        {           
             DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 11, 14, 21, 53);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(2, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_later_in_the_same_week()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 16, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 12, 20, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(52, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_later_after_weekend()
        {           
             DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 16, 20, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(56, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_weekends_later()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 16, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 22, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(188, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_earlier_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 23, 8, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(212, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_later_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 25, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(268, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_later_in_the_same_week_after_new_year()
        {           
             DateTime d1 = new DateTime(2013, 12, 30, 16, 0, 0);            
             DateTime d2 = new DateTime(2014, 1, 3, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(66, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_later_after_weekend_and_after_newYear()
        {           
             DateTime d1 = new DateTime(2014, 12, 30, 16, 0, 0);            
             DateTime d2 = new DateTime(2015, 1, 6, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(90, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_later_after_weekend_newYear()
        {           
             DateTime d1 = new DateTime(2011, 12, 28, 12, 0, 0);            
             DateTime d2 = new DateTime(2012, 1, 3, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(100, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_later_in_the_same_week_after_christmas()
        {           
             DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 26, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(50, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_later_after_weekend_and_after_christmas()
        {           
             DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 29, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(70, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_later_after_weekend_christmas()
        {           
             DateTime d1 = new DateTime(2016, 12, 23, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 12, 27, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(52, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_newYears_later()
        {           
             DateTime d1 = new DateTime(2013, 10, 15, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 11, 11, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(19120, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_earlier_in_the_same_week()
        {           
             DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 10, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(-44, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_earlier_before_weekend()
        {           
             DateTime d1 = new DateTime(2016, 8, 17, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(-96, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_later_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 16, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(-212, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_earlier_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 19, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(-284, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_weekends_earlier()
        {           
             DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 7, 21, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(-356, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_new_year()
        {           
             DateTime d1 = new DateTime(2015, 1, 2, 16, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 31, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(-26, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_newYear()
        {           
             DateTime d1 = new DateTime(2014, 1, 2, 12, 0, 0);            
             DateTime d2 = new DateTime(2013, 12, 27, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(-66, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_before_weekend_newYear()
        {           
             DateTime d1 = new DateTime(2017, 1, 3, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 12, 29, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(-72, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_christmas()
        {           
             DateTime d1 = new DateTime(2014, 12, 26, 12, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 24, 20, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(-14, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_christmas()
        {           
             DateTime d1 = new DateTime(2015, 12, 29, 12, 0, 0);            
             DateTime d2 = new DateTime(2015, 12, 24, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(-46, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_days_earlier_before_weekend_christmas()
        {           
             DateTime d1 = new DateTime(2016, 12, 26, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 12, 23, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(-20, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H1")]
        public void countTimeUnits_h1_returns_proper_value_for_date_few_newYears_earlier()
        {           
             DateTime d1 = new DateTime(2016, 12, 26, 16, 0, 0);            
             DateTime d2 = new DateTime(2012, 10, 23, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H1);
             Assert.AreEqual(-25928, result);
        }

        #endregion H1



        #region H4

        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_zero_the_same_date_is_returned()
        {
            DateTime d = new DateTime(2016, 4, 21, 12, 0, 0);
            DateTime expected = new DateTime(2016, 4, 21, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 0);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_positive_without_new_year_not_week_break()
        {
            DateTime d = new DateTime(2016, 8, 9, 8, 0, 0);
            DateTime expected = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 19);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_positive_with_week_break()
        {
            DateTime d = new DateTime(2016, 8, 2, 8, 0, 0);
            DateTime expected = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 49);

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_negative_without_new_year_not_week_break()
        {
            DateTime d = new DateTime(2016, 8, 12, 12, 0, 0);
            DateTime expected = new DateTime(2016, 8, 9, 8, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, -19);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_negative_with_week_break()
        {
            DateTime d = new DateTime(2016, 7, 26, 4, 0, 0);
            DateTime expected = new DateTime(2016, 7, 15, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, -42);

            Assert.AreEqual(expected, result);
        }




        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_negative_with_newYear_at_week()
        {
            DateTime d = new DateTime(2015, 1, 6, 12, 0, 0);
            DateTime expected = new DateTime(2014, 12, 30, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, -26);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_positive_with_newYear_at_weekend()
        {
            DateTime d = new DateTime(2016, 12, 28, 8, 0, 0);
            DateTime expected = new DateTime(2017, 1, 5, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 38);

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_positive_with_newYear_at_week()
        {
            DateTime d = new DateTime(2014, 12, 19, 8, 0, 0);
            DateTime expected = new DateTime(2015, 1, 7, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 68);

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_negative_with_newYear_at_weekend()
        {
            DateTime d = new DateTime(2017, 1, 6, 16, 0, 0);
            DateTime expected = new DateTime(2016, 12, 27, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, -49);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_positive_with_christmas_at_week()
        {
            DateTime d = new DateTime(2014, 12, 23, 16, 0, 0);
            DateTime expected = new DateTime(2014, 12, 30, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 23);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_positive_with_christmas_at_weekend()
        {
            DateTime d = new DateTime(2016, 12, 22, 12, 0, 0);
            DateTime expected = new DateTime(2016, 12, 28, 16, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 25);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_positive_with_christmas_and_newYear()
        {
            DateTime d = new DateTime(2015, 12, 22, 16, 0, 0);
            DateTime expected = new DateTime(2016, 1, 5, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 47);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_positive_omit_weekend_after_skip_christmas()
        {
            DateTime d = new DateTime(2015, 12, 24, 20, 0, 0);
            DateTime expected = new DateTime(2015, 12, 28, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 2);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_positive_omit_weekend_after_skip_newYear()
        {
            DateTime d = new DateTime(2015, 12, 31, 20, 0, 0);
            DateTime expected = new DateTime(2016, 1, 4, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 2);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_positive_omit_christmas_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 22, 20, 0, 0);
            DateTime expected = new DateTime(2017, 12, 26, 0, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 1);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_positive_omit_newYear_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 29, 20, 0, 0);
            DateTime expected = new DateTime(2018, 1, 2, 4, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, 2);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_negative_with_christmas_at_week()
        {
            DateTime d = new DateTime(2013, 12, 27, 12, 0, 0);
            DateTime expected = new DateTime(2013, 12, 23, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, -18);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_negative_with_christmas_at_weekend()
        {
            DateTime d = new DateTime(2011, 12, 28, 16, 0, 0);
            DateTime expected = new DateTime(2011, 12, 22, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, -25);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_negative_with_christmas_and_newYear()
        {
            DateTime d = new DateTime(2015, 1, 7, 12, 0, 0);
            DateTime expected = new DateTime(2013, 12, 23, 12, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, -1608);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_negative_omit_weekend_after_skip_christmas()
        {
            DateTime d = new DateTime(2015, 12, 28, 4, 0, 0);
            DateTime expected = new DateTime(2015, 12, 24, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, -2);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_negative_omit_weekend_after_skip_newYear()
        {
            DateTime d = new DateTime(2016, 1, 4, 4, 0, 0);
            DateTime expected = new DateTime(2015, 12, 31, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, -2);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_negative_omit_christmas_after_skip_weekend()
        {
            DateTime d = new DateTime(2017, 12, 26, 0, 0, 0);
            DateTime expected = new DateTime(2017, 12, 22, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, -1);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_H4_if_units_negative_omit_newYear_after_skip_weekend()
        {
            DateTime d = new DateTime(2018, 1, 2, 4, 0, 0);
            DateTime expected = new DateTime(2017, 12, 29, 20, 0, 0);
            DateTime result = d.addTimeUnits(TimeframeSymbol.H4, -2);

            Assert.AreEqual(expected, result);
        }








                [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_if_the_same_date_is_given_zero_is_returned()
        {           
             DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_if_date_in_the_same_range_is_given_zero_is_returned()
        {           
             DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 11, 14, 21, 53);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_later_in_the_same_week()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 16, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 12, 20, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(13, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_later_after_weekend()
        {           
             DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 16, 20, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(14, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_weekends_later()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 16, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 22, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(47, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_earlier_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 23, 8, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(53, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_later_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 10, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 25, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(67, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_later_in_the_same_week_after_new_year()
        {           
             DateTime d1 = new DateTime(2013, 12, 30, 16, 0, 0);            
             DateTime d2 = new DateTime(2014, 1, 3, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(17, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_later_after_weekend_and_after_newYear()
        {           
             DateTime d1 = new DateTime(2014, 12, 30, 16, 0, 0);            
             DateTime d2 = new DateTime(2015, 1, 6, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(23, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_later_after_weekend_newYear()
        {           
             DateTime d1 = new DateTime(2011, 12, 28, 12, 0, 0);            
             DateTime d2 = new DateTime(2012, 1, 3, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(25, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_later_in_the_same_week_after_christmas()
        {           
             DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 26, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(13, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_later_after_weekend_and_after_christmas()
        {           
             DateTime d1 = new DateTime(2014, 12, 23, 12, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 29, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(18, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_later_after_weekend_christmas()
        {           
             DateTime d1 = new DateTime(2016, 12, 23, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 12, 27, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(13, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_newYears_later()
        {           
             DateTime d1 = new DateTime(2013, 10, 15, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 11, 11, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(4783, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_earlier_in_the_same_week()
        {           
             DateTime d1 = new DateTime(2016, 8, 12, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 10, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(-11, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_earlier_before_weekend()
        {           
             DateTime d1 = new DateTime(2016, 8, 17, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 11, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(-24, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_later_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 16, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(-53, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_earlier_than_base_date()
        {           
             DateTime d1 = new DateTime(2016, 8, 19, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 8, 3, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(-71, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_weekends_earlier()
        {           
             DateTime d1 = new DateTime(2016, 8, 11, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 7, 21, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(-89, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_new_year()
        {           
             DateTime d1 = new DateTime(2015, 1, 2, 16, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 31, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(-7, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_newYear()
        {           
             DateTime d1 = new DateTime(2014, 1, 2, 12, 0, 0);            
             DateTime d2 = new DateTime(2013, 12, 27, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(-17, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_earlier_before_weekend_newYear()
        {           
             DateTime d1 = new DateTime(2017, 1, 3, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 12, 29, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(-18, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_christmas()
        {           
             DateTime d1 = new DateTime(2014, 12, 26, 12, 0, 0);            
             DateTime d2 = new DateTime(2014, 12, 24, 20, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(-4, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_christmas()
        {           
             DateTime d1 = new DateTime(2015, 12, 29, 12, 0, 0);            
             DateTime d2 = new DateTime(2015, 12, 24, 12, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(-12, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_days_earlier_before_weekend_christmas()
        {           
             DateTime d1 = new DateTime(2016, 12, 26, 12, 0, 0);            
             DateTime d2 = new DateTime(2016, 12, 23, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(-5, result);
        }

        [TestMethod]
        [TestCategory("countTimeUnits.H4")]
        public void countTimeUnits_h4_returns_proper_value_for_date_few_newYears_earlier()
        {           
             DateTime d1 = new DateTime(2016, 12, 26, 16, 0, 0);            
             DateTime d2 = new DateTime(2012, 10, 23, 16, 0, 0);
             int result = d1.countTimeUnits(d2, TimeframeSymbol.H4);
             Assert.AreEqual(-6486, result);
        }




        #endregion H4



        #region Days


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
        public void addTimeUnits_D1_if_units_positive_without_new_year_nor_week_break()
        {
            DateTime d = new DateTime(2016, 4, 18);
            DateTime expected = new DateTime(2016, 4, 21);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 3);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_positive_with_week_break()
        {
            DateTime d = new DateTime(2016, 1, 21);
            DateTime expected = new DateTime(2016, 8, 29);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 157);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_positive_with_new_year_break()
        {
            DateTime d = new DateTime(2014, 12, 31);
            DateTime expected = new DateTime(2015, 1, 2);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 1);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_positive_with_few_new_year_breaks()
        {
            DateTime d = new DateTime(2012, 1, 25);
            DateTime expected = new DateTime(2017, 8, 29);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 1451);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_baseDate_is_Monday_4January_and_one_day_is_to_be_subtracted()
        {
            DateTime d = new DateTime(2016, 1, 4);
            DateTime expected = new DateTime(2015, 12, 31);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -1);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_baseDate_is_Friday_29December_and_one_day_is_to_be_added()
        {
            DateTime d = new DateTime(2017, 12, 29);
            DateTime expected = new DateTime(2018, 1, 2);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 1);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_baseDate_is_Monday_28December_and_one_day_is_to_be_subtracted()
        {
            DateTime d = new DateTime(2015, 12, 28);
            DateTime expected = new DateTime(2015, 12, 24);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -1);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_baseDate_is_Thursday_21December_and_two_days_are_to_be_added()
        {
            DateTime d = new DateTime(2017, 12, 21);
            DateTime expected = new DateTime(2017, 12, 26);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 2);

            Assert.AreEqual(expected, result);

        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_baseDate_is_Thursday_31December_and_one_day_is_to_be_subtracted()
        {
            DateTime d = new DateTime(2015, 12, 31);
            DateTime expected = new DateTime(2016, 1, 4);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 1);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_baseDate_is_Tuesday_2January_and_one_day_is_to_be_subtracted()
        {
            DateTime d = new DateTime(2018, 1, 2);
            DateTime expected = new DateTime(2017, 12, 29);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -1);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_baseDate_is_Tuesday_26December_and_one_day_is_to_be_subtracted()
        {
            DateTime d = new DateTime(2017, 12, 26);
            DateTime expected = new DateTime(2017, 12, 22);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -1);

            Assert.AreEqual(expected, result);

        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_baseDate_is_Thursday_24December_and_one_day_is_to_be_subtracted()
        {
            DateTime d = new DateTime(2015, 12, 24);
            DateTime expected = new DateTime(2015, 12, 28);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 1);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_positive_with_new_year_break_at_weekend()
        {
            DateTime d = new DateTime(2016, 12, 28);
            DateTime expected = new DateTime(2017, 1, 3);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 4);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_positive_with_new_year_break_at_week()
        {
            DateTime d = new DateTime(2017, 12, 28);
            DateTime expected = new DateTime(2018, 1, 3);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 3);

            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_positive_with_christmas()
        {
            DateTime d = new DateTime(2014, 12, 23);
            DateTime expected = new DateTime(2014, 12, 29);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 3);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_positive_with_weekendChristmas()
        {
            DateTime d = new DateTime(2016, 12, 22);
            DateTime expected = new DateTime(2016, 12, 30);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, 6);

            Assert.AreEqual(expected, result);
        }





        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_negative_with_week_break()
        {
            DateTime d = new DateTime(2016, 8, 17);
            DateTime expected = new DateTime(2016, 8, 4);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -9);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_negative_without_new_year_nor_week_break()
        {
            DateTime d = new DateTime(2016, 8, 17);
            DateTime expected = new DateTime(2016, 8, 15);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -2);

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_negative_with_new_year_break()
        {
            DateTime d = new DateTime(2014, 1, 3);
            DateTime expected = new DateTime(2013, 12, 31);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -2);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_negative_with_christmas()
        {
            DateTime d = new DateTime(2014, 12, 29);
            DateTime expected = new DateTime(2014, 12, 24);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -2);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_negative_with_weekendChristmas()
        {
            DateTime d = new DateTime(2016, 12, 30);
            DateTime expected = new DateTime(2016, 12, 22);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -6);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_negative_with_new_year_and_week_break()
        {
            DateTime d = new DateTime(2014, 1, 3);
            DateTime expected = new DateTime(2013, 12, 31);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -2);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_negative_with_new_year_at_weekend()
        {
            DateTime d = new DateTime(2017, 1, 3);
            DateTime expected = new DateTime(2016, 12, 30);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -2);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_D1_if_units_negative_with_few_new_year_breaks()
        {
            DateTime d = new DateTime(2017, 8, 29);
            DateTime expected = new DateTime(2012, 1, 25);
            DateTime result = d.addTimeUnits(TimeframeSymbol.D1, -1451);

            Assert.AreEqual(expected, result);

        }







        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_if_the_same_date_is_given_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 1);
            DateTime d2 = new DateTime(2016, 4, 1);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(0, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_later_in_the_same_week()
        {
            DateTime d1 = new DateTime(2016, 4, 19);
            DateTime d2 = new DateTime(2016, 4, 21);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(2, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_later_after_weekend()
        {
            DateTime d1 = new DateTime(2016, 4, 19);
            DateTime d2 = new DateTime(2016, 4, 28);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(7, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_weekends_later()
        {
            DateTime d1 = new DateTime(2016, 4, 19);
            DateTime d2 = new DateTime(2016, 6, 8);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(36, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_earlier_than_base_date()
        {
            DateTime d1 = new DateTime(2016, 4, 15);
            DateTime d2 = new DateTime(2016, 4, 28);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(9, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_two_weeks_later_with_dayOfWeek_later_than_base_date()
        {
            DateTime d1 = new DateTime(2016, 4, 12);
            DateTime d2 = new DateTime(2016, 4, 28);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(12, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_later_in_the_same_week_after_new_year()
        {
            DateTime d1 = new DateTime(2014, 12, 30);
            DateTime d2 = new DateTime(2015, 1, 2);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(2, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_later_after_weekend_and_after_newYear()
        {
            DateTime d1 = new DateTime(2014, 12, 30);
            DateTime d2 = new DateTime(2015, 1, 6);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(4, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_later_after_weekend_newYear()
        {
            DateTime d1 = new DateTime(2016, 12, 29);
            DateTime d2 = new DateTime(2017, 1, 4);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(4, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_later_in_the_same_week_after_christmas()
        {
            DateTime d1 = new DateTime(2014, 12, 23);
            DateTime d2 = new DateTime(2014, 12, 26);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(2, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_later_after_weekend_and_after_christmas()
        {
            DateTime d1 = new DateTime(2012, 12, 20);
            DateTime d2 = new DateTime(2012, 12, 28);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(5, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_later_after_weekend_christmas()
        {
            DateTime d1 = new DateTime(2011, 12, 22);
            DateTime d2 = new DateTime(2011, 12, 27);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(3, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_newYears_later()
        {
            DateTime d1 = new DateTime(2010, 5, 21);
            DateTime d2 = new DateTime(2015, 5, 20);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(1297, result);
        }






        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_in_the_same_week()
        {
            DateTime d1 = new DateTime(2016, 4, 21);
            DateTime d2 = new DateTime(2016, 4, 19);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(-2, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_before_weekend()
        {
            DateTime d1 = new DateTime(2016, 4, 28);
            DateTime d2 = new DateTime(2016, 4, 19);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(-7, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_later_than_base_date()
        {
            DateTime d1 = new DateTime(2016, 4, 28);
            DateTime d2 = new DateTime(2016, 4, 15);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(-9, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_two_weeks_earlier_with_dayOfWeek_earlier_than_base_date()
        {
            DateTime d1 = new DateTime(2016, 4, 28);
            DateTime d2 = new DateTime(2016, 4, 12);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(-12, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_weekends_earlier()
        {
            DateTime d1 = new DateTime(2016, 6, 8);
            DateTime d2 = new DateTime(2016, 4, 19);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(-36, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_new_year()
        {
            DateTime d1 = new DateTime(2015, 1, 2);
            DateTime d2 = new DateTime(2014, 12, 30);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(-2, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_newYear()
        {
            DateTime d1 = new DateTime(2015, 1, 6);
            DateTime d2 = new DateTime(2014, 12, 30);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(-4, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_before_weekend_newYear()
        {
            DateTime d1 = new DateTime(2017, 1, 4);
            DateTime d2 = new DateTime(2016, 12, 29);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(-4, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_in_the_same_week_before_christmas()
        {
            DateTime d1 = new DateTime(2014, 12, 26);
            DateTime d2 = new DateTime(2014, 12, 23);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(-2, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_before_weekend_and_before_christmas()
        {
            DateTime d1 = new DateTime(2012, 12, 28);
            DateTime d2 = new DateTime(2012, 12, 20);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(-5, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_days_earlier_before_weekend_christmas()
        {
            DateTime d1 = new DateTime(2011, 12, 27);
            DateTime d2 = new DateTime(2011, 12, 22);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(-3, result);
        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_days_returns_proper_value_for_date_few_newYears_earlier()
        {
            DateTime d1 = new DateTime(2015, 5, 20);
            DateTime d2 = new DateTime(2010, 5, 21);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.D1);

            Assert.AreEqual(-1297, result);
        }



        #endregion days



        #region Weeks




        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_W1_if_units_zero_the_same_date_is_returned()
        {
            DateTime d = new DateTime(2016, 4, 17);
            DateTime result = d.addTimeUnits(TimeframeSymbol.W1, 0);

            Assert.AreEqual(d, result);
        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_W1_for_positive_units()
        {
            DateTime d = new DateTime(2016, 4, 17);
            DateTime expected = new DateTime(2016, 5, 22);
            DateTime result = d.addTimeUnits(TimeframeSymbol.W1, 5);

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_W1_for_negative_units()
        {
            DateTime d = new DateTime(2016, 4, 17);
            DateTime expected = new DateTime(2016, 4, 3);
            DateTime result = d.addTimeUnits(TimeframeSymbol.W1, -2);

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_weeks_if_the_same_date_is_given_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 1);
            DateTime d2 = new DateTime(2016, 4, 1);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.W1);

            Assert.AreEqual(0, result);
        }





        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_weeks_if_is_in_the_same_week_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 8, 9);
            DateTime d2 = new DateTime(2016, 8, 11);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.W1);

            Assert.AreEqual(0, result);
        }





        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_weeks_if_later_date_proper_value_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 1);
            DateTime d2 = new DateTime(2016, 8, 1);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.W1);

            Assert.AreEqual(18, result);
        }




        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_weeks_if_second_date_not_first_date_of_week_rounded_value_is_used()
        {
            DateTime d1 = new DateTime(2016, 8, 1);
            DateTime d2 = new DateTime(2016, 8, 11);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.W1);

            Assert.AreEqual(1, result);

        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_weeks_if_base_value_not_first_day_of_week_rounded_base_value_is_used()
        {
            DateTime d1 = new DateTime(2016, 4, 15);
            DateTime d2 = new DateTime(2016, 4, 20);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.W1);

            Assert.AreEqual(1, result);

        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_weeks_if_both_dates_from_the_same_week_but_base_date_is_earlier_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 19);
            DateTime d2 = new DateTime(2016, 4, 21);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.W1);

            Assert.AreEqual(0, result);

        }




        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_weeks_if_both_dates_from_the_same_week_but_base_date_is_later_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 21);
            DateTime d2 = new DateTime(2016, 4, 19);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.W1);

            Assert.AreEqual(0, result);

        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_weeks_if_baseDate_is_earlier_date_proper_value_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 1);
            DateTime d2 = new DateTime(2016, 4, 21);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.W1);

            Assert.AreEqual(3, result);
        }




        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_weeks_if_comparedDate_from_next_years_proper_value_is_returned()
        {
            DateTime d1 = new DateTime(2015, 12, 5);
            DateTime d2 = new DateTime(2016, 4, 21);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.W1);

            Assert.AreEqual(20, result);
        }




        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_weeks_if_comparedDate_from_previous_years_proper_value_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 21);
            DateTime d2 = new DateTime(2015, 12, 5);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.W1);

            Assert.AreEqual(-20, result);
        }






        #endregion Weeks



        #region Months
        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_months_if_the_same_date_is_given_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 1);
            DateTime d2 = new DateTime(2016, 4, 1);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.MN1);

            Assert.AreEqual(0, result);
        }


        [TestMethod]
        [TestCategory("addTimeUnits")]
        public void addTimeUnits_MN1_if_units_zero_the_same_date_is_returned()
        {
            DateTime d = new DateTime(2016, 4, 1);
            DateTime result = d.addTimeUnits(TimeframeSymbol.MN1, 0);

            Assert.AreEqual(d, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_months_if_is_in_the_same_month_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 1);
            DateTime d2 = new DateTime(2016, 4, 21);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.MN1);

            Assert.AreEqual(0, result);
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
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_months_if_later_date_proper_value_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 1);
            DateTime d2 = new DateTime(2016, 11, 1);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.MN1);

            Assert.AreEqual(7, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_months_if_second_date_not_first_date_of_month_rounded_value_is_used()
        {
            DateTime d1 = new DateTime(2016, 4, 1);
            DateTime d2 = new DateTime(2016, 11, 16);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.MN1);

            Assert.AreEqual(7, result);

        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_months_if_base_value_not_first_day_of_month_rounded_base_value_is_used()
        {
            DateTime d1 = new DateTime(2016, 4, 15);
            DateTime d2 = new DateTime(2016, 11, 16);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.MN1);

            Assert.AreEqual(7, result);

        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_months_if_both_dates_from_the_same_month_but_base_date_is_earlier_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 15);
            DateTime d2 = new DateTime(2016, 4, 30);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.MN1);

            Assert.AreEqual(0, result);

        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_months_if_both_dates_from_the_same_month_but_base_date_is_later_zero_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 15);
            DateTime d2 = new DateTime(2016, 4, 2);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.MN1);

            Assert.AreEqual(0, result);

        }


        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_months_if_earlier_date_proper_value_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 1);
            DateTime d2 = new DateTime(2016, 4, 21);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.MN1);

            Assert.AreEqual(0, result);
        }




        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_months_if_comparedDate_from_next_years_proper_value_is_returned()
        {
            DateTime d1 = new DateTime(2013, 7, 15);
            DateTime d2 = new DateTime(2016, 4, 21);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.MN1);

            Assert.AreEqual(33, result);
        }



        [TestMethod]
        [TestCategory("countTimeUnits")]
        public void countTimeUnits_months_if_comparedDate_from_previous_years_proper_value_is_returned()
        {
            DateTime d1 = new DateTime(2016, 4, 20);
            DateTime d2 = new DateTime(2012, 6, 21);
            int result = d1.countTimeUnits(d2, TimeframeSymbol.MN1);

            Assert.AreEqual(-46, result);
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

        #endregion Months



    }
}
