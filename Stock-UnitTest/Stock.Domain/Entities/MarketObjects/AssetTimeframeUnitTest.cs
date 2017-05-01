using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Moq;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class AssetTimeframeUnitTest
    {

        private const int DEFAULT_ASSET_ID = 1;
        private const string DEFAULT_ASSET_SYMBOL = "EURUSD";
        private const int DEFAULT_ASSET_MARKET_ID = 1;
        private const int DEFAULT_TIMEFRAME_ID = 1;


        #region INFRASTRUCTURE

        private Asset defaultAsset()
        {
            Market market = new Market(DEFAULT_ASSET_MARKET_ID, "Forex", "FX");
            return new Asset(DEFAULT_ASSET_ID, DEFAULT_ASSET_SYMBOL, market);
        }

        private Timeframe defaultTimeframe()
        {
            return Timeframe.ById(DEFAULT_TIMEFRAME_ID);
        }

        #endregion INFRASTRUCTURE


        #region IS.VALID

        [Ignore]
        [TestMethod]
        public void IsValid_ReturnsTrue_IfBothAssetAndTimeframeAreSet()
        {

            //Arrange
            Asset asset = defaultAsset();
            Timeframe timeframe = defaultTimeframe();
            AssetTimeframe assetTimeframe = new AssetTimeframe(asset, timeframe);

            //Act
            bool isValid = assetTimeframe.IsValid();

            //Assert
            Assert.IsTrue(isValid);

        }

        [TestMethod]
        public void IsValid_ReturnsFalse_IfAssetIsNull()
        {

            //Arrange
            Asset asset = null;
            Timeframe timeframe = defaultTimeframe();
            AssetTimeframe assetTimeframe = new AssetTimeframe(asset, timeframe);

            //Act
            bool isValid = assetTimeframe.IsValid();

            //Assert
            Assert.IsFalse(isValid);

        }

        [TestMethod]
        public void IsValid_ReturnsFalse_IfTimeframeIsNull()
        {

            //Arrange
            Asset asset = defaultAsset();
            Timeframe timeframe = null;
            AssetTimeframe assetTimeframe = new AssetTimeframe(asset, timeframe);

            //Act
            bool isValid = assetTimeframe.IsValid();

            //Assert
            Assert.IsFalse(isValid);

        }

        #endregion IS.VALID


        #region GET.SYMBOL

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Asset or timeframe is null")]
        public void GetSymbol_ThrowsException_IfAssetIsNull()
        {

            //Arrange
            Asset asset = null;
            Timeframe timeframe = defaultTimeframe();
            AssetTimeframe assetTimeframe = new AssetTimeframe(asset, timeframe);

            //Act
            string symbol = assetTimeframe.GetSymbol();

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Asset or timeframe is null")]
        public void GetSymbol_ThrowsException_IfTimeframeIsNull()
        {

            //Arrange
            Asset asset = defaultAsset();
            Timeframe timeframe = null;
            AssetTimeframe assetTimeframe = new AssetTimeframe(asset, timeframe);

            //Act
            string symbol = assetTimeframe.GetSymbol();

        }

        [Ignore]
        [TestMethod]
        public void GetSymbol_ReturnsProperSymbol_IfAssetAndTimeframeAreSet()
        {

            //Arrange
            Asset asset = defaultAsset();
            Timeframe timeframe = defaultTimeframe();
            AssetTimeframe assetTimeframe = new AssetTimeframe(asset, timeframe);

            //Act
            string actualSymbol = assetTimeframe.GetSymbol();

            //Assert
            string expectedSymbol = "EURUSD_M30";
            Assert.AreEqual(expectedSymbol, actualSymbol);

        }

        #endregion GET.SYMBOL


    }
}
