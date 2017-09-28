//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Stock.Domain.Entities;
//using System.Collections.Generic;
//using Stock.Domain.Services;
//using System.Linq;
//using System.IO;
//using System.Text.RegularExpressions;

//namespace Stock_UnitTest.Stock.Services
//{

//    [TestClass]
//    public class TrendServiceUnitTests
//    {

//        private static Asset asset = new Asset (9, "AUDJPY");
//        private static Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.D1);
//        private static DataItem[] data = UnitTestInitializer.GetData();
//        private TrendService service = new TrendService(asset, timeframe, data);


//        [TestMethod]
//        [TestCategory("None")]
//        public void GeneralTest()
//        {

//            var extrema = service.GetExtrema();
//            var item = extrema[0];
//            var subitem = extrema[2];
//            var trendHits = service.ProcessSinglePair(item, subitem);

//            foreach (var trendHit in trendHits)
//            {
//                System.Diagnostics.Debug.WriteLine(trendHit.ToString());
//            }


//        }



//    }


//}
