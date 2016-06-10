//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Stock.Domain.Entities;
//using Stock.Domain.Enums;

//namespace Stock_UnitTest.Stock.Domain
//{
//    [TestClass]
//    public class PriceUnitTests
//    {


//        #region PriceExtremumEvaluation
//        [TestMethod]
//        [TestCategory("PriceExtremumEvaluation")]
//        public void PriceExtremumEvaluation_returns_proper_evaluation_for_peak()
//        {
//            Price price = new Price
//            {
//                PeakByClose = 1,
//                PeakByHigh = 2,
//                TroughByLow = 3,
//                TroughByClose = 4
//            };

//            Assert.AreEqual(2, price.ExtremumEvaluation(TrendlineType.Resistance));

//        }


//        [TestMethod]
//        [TestCategory("PriceExtremumEvaluation")]
//        public void PriceExtremumEvaluation_returns_proper_evaluation_for_trough()
//        {
//            Price price = new Price
//            {
//                PeakByClose = 1,
//                PeakByHigh = 2,
//                TroughByLow = 3,
//                TroughByClose = 4
//            };

//            Assert.AreEqual(4, price.ExtremumEvaluation(TrendlineType.Support));

//        }

//        #endregion
        

//    }
//}
