using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock_UnitTest.Stock.Domain
{
    [TestClass]
    public class TimebandUnitTests
    {


        #region GetTimebandByPeriod

        [TestMethod]
        [TestCategory("GetTimebandByPeriod")]
        public void GetTimebandByPeriod_returns_1D_for_1()
        {

            var timeband = Timeband.GetTimebandByPeriod(1d);

            if (timeband == null)
            {
                Assert.Fail("[timeband] cannot be null");
            }

            Assert.AreEqual("D1", timeband.Name);

        }


        [TestMethod]
        [TestCategory("GetTimebandByPeriod")]
        public void GetTimebandByPeriod_returns_null_for_3_minutes()
        {

            var period = 1d / 480d;
            var timeband = Timeband.GetTimebandByPeriod(period);

            Assert.IsNull(timeband);

        }


        [TestMethod]
        [TestCategory("GetTimebandByPeriod")]
        public void GetTimebandByPeriod_returns_MN1_for_more_than_28_days()
        {

            var period = 28d;
            var timeband = Timeband.GetTimebandByPeriod(period);

            if (timeband == null)
            {
                Assert.Fail("[timeband] cannot be null");
            }

            Assert.AreEqual("MN1", timeband.Name);

        }

        #endregion



    }
}
