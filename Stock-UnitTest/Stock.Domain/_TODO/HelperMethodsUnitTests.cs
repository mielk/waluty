//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Stock.Domain.Entities;
//using Stock.Domain.Services;

//namespace Stock_UnitTest.Stock.Services
//{
//    [TestClass]
//    public class HelperMethodsUnitTests
//    {

//        [TestMethod]
//        [TestCategory("AppendIndexNumbers")]
//        public void AppendIndexNumbers_appends_numbers_for_all_items()
//        {

//            //given
//            DataItem[] items = new DataItem[5];
//            for (var i = 0; i < items.Length; i++)
//            {
//                items[i] = new DataItem { Date = DateTime.Now.AddDays(i), AssetId = 1 };
//            }


//            //when
//            items.AppendIndexNumbers();

//            //then
//            for (var i = 0; i < items.Length; i++)
//            {
//                var item = items[i];
//                Assert.AreEqual(i, item.Index);
//            }


//        }

//    }
//}
