using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Stock.Domain.Enums;
using System.Linq;

namespace Stock_UnitTest.Stock.Domain.Enums
{
    [TestClass]
    public class AnalysisTypeHelperUnitTests
    {

        private void multiTest_GetType_ReturnsProperValue(string given, AnalysisType expected)
        {
            AnalysisType result = AnalysisTypeHelper.Type(given);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetType_ReturnsProperValue_ForGivenStringValues()
        {
            multiTest_GetType_ReturnsProperValue("prices", AnalysisType.Price);
            multiTest_GetType_ReturnsProperValue("Prices", AnalysisType.Price);
            multiTest_GetType_ReturnsProperValue("MACD", AnalysisType.MACD);
            multiTest_GetType_ReturnsProperValue("macd", AnalysisType.MACD);
            multiTest_GetType_ReturnsProperValue("Macd", AnalysisType.MACD);
            multiTest_GetType_ReturnsProperValue("Adx", AnalysisType.ADX);
            multiTest_GetType_ReturnsProperValue("ADX", AnalysisType.ADX);
            multiTest_GetType_ReturnsProperValue("adx", AnalysisType.ADX);
            multiTest_GetType_ReturnsProperValue("Trendline", AnalysisType.Trendline);
            multiTest_GetType_ReturnsProperValue("trendline", AnalysisType.Trendline);
            multiTest_GetType_ReturnsProperValue("Candlestick", AnalysisType.Candlestick);
            multiTest_GetType_ReturnsProperValue("candlestick", AnalysisType.Candlestick);
        }

        private void multiTest_GetTypeString_ReturnsProperValue(AnalysisType given, string expected)
        {
            string result = AnalysisTypeHelper.getTypeString(given);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetTypeString_ReturnsProperValue_ForGivenValues()
        {
            multiTest_GetTypeString_ReturnsProperValue(AnalysisType.Price, "prices");
            multiTest_GetTypeString_ReturnsProperValue(AnalysisType.MACD, "macd");
            multiTest_GetTypeString_ReturnsProperValue(AnalysisType.ADX, "adx");
            multiTest_GetTypeString_ReturnsProperValue(AnalysisType.Trendline, "trendlines");
            multiTest_GetTypeString_ReturnsProperValue(AnalysisType.Candlestick, "candlestick");
        }

        [TestMethod]
        public void FromStringListToTypesList_ReturnsEmptyArray_ForEmptyString()
        {
            string source = string.Empty;
            AnalysisType[] actual = AnalysisTypeHelper.FromStringListToTypesList(source);
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        public void FromStringListToTypesList_ReturnsArrayOfUnknown_ForOnlyUnknown()
        {
            string source = "a, b, c";
            AnalysisType[] actual = AnalysisTypeHelper.FromStringListToTypesList(source);
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        public void FromStringListToTypesList_ReturnsProperArrayOfTypes()
        {
            string source = "prices, macd, adx";
            AnalysisType[] actual = AnalysisTypeHelper.FromStringListToTypesList(source);
            AnalysisType[] expected = new AnalysisType[] { AnalysisType.Price, AnalysisType.MACD, AnalysisType.ADX };
            var comparison = (actual.Except(expected).Count() == 0);
            Assert.IsTrue(comparison);
        }

        [TestMethod]
        public void FromStringListToTypesList_ReturnsProperArrayOfTypes_IfOtherSeparatorIsGiven()
        {
            char separator = ';';
            string source = "prices;macd;adx";
            AnalysisType[] actual = AnalysisTypeHelper.FromStringListToTypesList(source, separator);
            AnalysisType[] expected = new AnalysisType[] { AnalysisType.Price, AnalysisType.MACD, AnalysisType.ADX };
            var comparison = (actual.Except(expected).Count() == 0);
            Assert.IsTrue(comparison);
        }

    }
}
