using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class AssetTimeframeUnitTest
    {


        [TestMethod]
        [TestCategory("AssetTimeframe.isValid")]
        public void if_both_asset_and_timeframe_are_set_isValid_returns_true()
        {
            Asset asset = new Asset(1, "x");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M30);
            AssetTimeframe atf = new AssetTimeframe(asset, timeframe);

            Assert.IsTrue(atf.isValid());

        }


        [TestMethod]
        [TestCategory("AssetTimeframe.isValid")]
        public void if_asset_is_null_isValid_returns_false()
        {

            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M30);
            AssetTimeframe atf = new AssetTimeframe(null, timeframe);

            Assert.IsFalse(atf.isValid());

        }


        [TestMethod]
        [TestCategory("AssetTimeframe.isValid")]
        public void if_timeframe_is_null_isValid_returns_false()
        {

            Asset asset = new Asset(1, "x");
            AssetTimeframe atf = new AssetTimeframe(asset, null);

            Assert.IsFalse(atf.isValid());

        }



        [TestMethod]
        [TestCategory("AssetTimeframe.isValid")]
        [ExpectedException(typeof(ArgumentNullException), "Asset or timeframe is null")]
        public void if_atf_is_not_valid_function_throws_exception()
        {

            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M30);
            AssetTimeframe atf = new AssetTimeframe(null, timeframe);
            string symbol = atf.Symbol();
        }



        [TestMethod]
        [TestCategory("AssetTimeframe.isValid")]
        public void function_returns_proper_symbol()
        {

            Asset asset = new Asset(1, "ABC") { ShortName = "ABC" };
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M30);
            AssetTimeframe atf = new AssetTimeframe(asset, timeframe);

            Assert.AreEqual("ABC_M30", atf.Symbol());

        }


    }
}
