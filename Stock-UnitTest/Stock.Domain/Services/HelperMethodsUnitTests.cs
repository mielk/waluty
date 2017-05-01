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




    }
}
