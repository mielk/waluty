using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using System.Collections.Generic;
using Stock.Domain.Services;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace Stock_UnitTest.Stock.Services
{

    [TestClass]
    public class PriceAnalyzerUnitTests
    {

        private static Asset asset = new Asset(9, "AUDJPY");
        private static Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.D1);
        private static DataItem[] data = UnitTestInitializer.GetData();


        [TestMethod]
        [TestCategory("None")]
        public void GeneralTest()
        {

            Assert.Fail("Dopisać testy do wszystkich metod");

        }



    }


}
