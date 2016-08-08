//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Stock.Domain.Entities;
//using Stock.Domain.Enums;

//namespace Stock_UnitTest.Stock.Domain
//{
//    [TestClass]
//    public class TimeframeUnitTests
//    {


//        #region GetTimeframeByPeriod

//        [TestMethod]
//        [TestCategory("GetTimeframeByPeriod")]
//        public void GetTimeframeByPeriod_returns_1D_for_1()
//        {

//            var timeframe = Timeframe.GetTimeframeByPeriod(1d);

//            if (timeframe == null)
//            {
//                Assert.Fail("[timeframe] cannot be null");
//            }

//            Assert.AreEqual("D1", timeframe.Name);

//        }


//        [TestMethod]
//        [TestCategory("GetTimeframeByPeriod")]
//        public void GetTimeframeByPeriod_returns_null_for_3_minutes()
//        {

//            var period = 1d / 480d;
//            var timeframe = Timeframe.GetTimeframeByPeriod(period);

//            Assert.IsNull(timeframe);

//        }


//        [TestMethod]
//        [TestCategory("GetTimeframeByPeriod")]
//        public void GetTimeframeByPeriod_returns_MN1_for_more_than_28_days()
//        {

//            var period = 28d;
//            var timeframe = Timeframe.GetTimeframeByPeriod(period);

//            if (timeframe == null)
//            {
//                Assert.Fail("[timeframe] cannot be null");
//            }

//            Assert.AreEqual("MN1", timeframe.Name);

//        }

//        #endregion



//    }
//}
