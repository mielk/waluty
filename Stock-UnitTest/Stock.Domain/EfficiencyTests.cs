using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Services;

namespace Stock_UnitTest.Stock.Domain
{
    [TestClass]
    public class EfficiencyTests
    {




        [TestMethod]
        [TestCategory("countNewYearBreaks")]
        public void countNewYearBreaks_efficiency_test()
        {

            DateTime baseDate = new DateTime(2015, 12, 30);
            DateTime comparedDate = new DateTime(2016, 1, 4);

            DateTime timeStart = DateTime.Now;
            for (var i = 1; i < 100000; i++)
            {
                int x = baseDate.countNewYearBreaks(comparedDate, true);
            }
            DateTime timeEnd = DateTime.Now;
            double difference = timeEnd.Subtract(timeStart).TotalMilliseconds;

            //Assert.IsTrue(difference < 65);
            Assert.AreEqual(10, difference);

        }

    }
}
