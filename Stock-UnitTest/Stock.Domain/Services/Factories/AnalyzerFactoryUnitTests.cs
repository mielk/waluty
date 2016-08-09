using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Services;
using Stock.Domain.Services.Factories;
using Stock.Domain.Enums;
using Stock.Domain.Entities;
using System.Collections.Generic;

namespace Stock_UnitTest.Stock.Domain.Services.Factories
{
    [TestClass]
    public class AnalyzerFactoryUnitTests
    {


        [TestMethod]
        public void getAnalyzer_returns_proper_analyzer_for_Macd()
        {
            Asset asset = new Asset(1, "a");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
            AnalyzerFactory factory = AnalyzerFactory.Instance();

            Analyzer analyzer = factory.getAnalyzer(AnalysisType.MACD, asset, timeframe);

            Assert.IsTrue(typeof(IMacdAnalyzer).IsAssignableFrom(analyzer.GetType()));
            Assert.AreEqual(analyzer.getAsset(), asset);
            Assert.AreEqual(analyzer.getTimeframe(), timeframe);

        }



        [TestMethod]
        public void getAnalyzer_returns_proper_analyzer_for_Prices()
        {
            Asset asset = new Asset(1, "a");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
            AnalyzerFactory factory = AnalyzerFactory.Instance();

            Analyzer analyzer = factory.getAnalyzer(AnalysisType.Price, asset, timeframe);

            Assert.IsTrue(typeof(IPriceAnalyzer).IsAssignableFrom(analyzer.GetType()));
            Assert.AreEqual(analyzer.getAsset(), asset);
            Assert.AreEqual(analyzer.getTimeframe(), timeframe);

        }




        [TestMethod]
        public void getAnalyzer_returns_proper_analyzer_for_Adx()
        {
            Asset asset = new Asset(1, "a");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
            AnalyzerFactory factory = AnalyzerFactory.Instance();

            Analyzer analyzer = factory.getAnalyzer(AnalysisType.ADX, asset, timeframe);

            Assert.IsTrue(typeof(IAdxAnalyzer).IsAssignableFrom(analyzer.GetType()));
            Assert.AreEqual(analyzer.getAsset(), asset);
            Assert.AreEqual(analyzer.getTimeframe(), timeframe);

        }




        [TestMethod]
        public void getAnalyzer_returns_proper_analyzer_for_Candlesticks()
        {
            Asset asset = new Asset(1, "a");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
            AnalyzerFactory factory = AnalyzerFactory.Instance();

            Analyzer analyzer = factory.getAnalyzer(AnalysisType.Candlestick, asset, timeframe);

            Assert.IsTrue(typeof(ICandlestickAnalyzer).IsAssignableFrom(analyzer.GetType()));
            Assert.AreEqual(analyzer.getAsset(), asset);
            Assert.AreEqual(analyzer.getTimeframe(), timeframe);

        }






        [TestMethod]
        public void getAnalyzer_returns_proper_analyzer_for_Trendlines()
        {
            Asset asset = new Asset(1, "a");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
            AnalyzerFactory factory = AnalyzerFactory.Instance();

            Analyzer analyzer = factory.getAnalyzer(AnalysisType.Trendline, asset, timeframe);

            Assert.IsTrue(typeof(ITrendlineAnalyzer).IsAssignableFrom(analyzer.GetType()));
            Assert.AreEqual(analyzer.getAsset(), asset);
            Assert.AreEqual(analyzer.getTimeframe(), timeframe);

        }




        [TestMethod]
        [TestCategory("getAnalyzers")]
        public void getAnalyzers_returns_the_proper_analyzers_for_the_given_analysis_types()
        {
            AnalyzerFactory factory = AnalyzerFactory.Instance();
            Asset asset = new Asset(1, "a");
            Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
            AnalysisType[] typesArr = new AnalysisType[] { AnalysisType.Price, AnalysisType.MACD, AnalysisType.Trendline };
            IEnumerable<AnalysisType> types = new List<AnalysisType>(typesArr);

            var analyzers = factory.getAnalyzers(asset, timeframe, types);


            //Check number of items in result dictionary.
            Assert.AreEqual(typesArr.Length, analyzers.Count);


            //Check if Price analyzers is correctly set.
            try
            {
                Analyzer analyzer;
                analyzers.TryGetValue(AnalysisType.Price, out analyzer);
                Assert.IsTrue(typeof(IPriceAnalyzer).IsAssignableFrom(analyzer.GetType()));
            }
            catch (ArgumentNullException)
            {
                Assert.Fail("Price analyzer not found");
            }


            //Check if MACD analyzers is correctly set.
            try
            {
                Analyzer analyzer;
                analyzers.TryGetValue(AnalysisType.MACD, out analyzer);
                Assert.IsTrue(typeof(IMacdAnalyzer).IsAssignableFrom(analyzer.GetType()));
            }
            catch (ArgumentNullException)
            {
                Assert.Fail("MACD analyzer not found");
            }


            //Check if Trendline analyzers is correctly set.
            try
            {
                Analyzer analyzer;
                analyzers.TryGetValue(AnalysisType.Trendline, out analyzer);
                Assert.IsTrue(typeof(ITrendlineAnalyzer).IsAssignableFrom(analyzer.GetType()));
            }
            catch (ArgumentNullException)
            {
                Assert.Fail("Trendline analyzer not found");
            }


        }



    }
}
