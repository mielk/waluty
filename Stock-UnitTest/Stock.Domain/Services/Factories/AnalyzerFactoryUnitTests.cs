using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Services;
using Stock.Domain.Services.Factories;
using Stock.Domain.Enums;
using Stock.Domain.Entities;
using System.Collections.Generic;
using Moq;

namespace Stock_UnitTest.Stock.Domain.Services.Factories
{
    [TestClass]
    public class AnalyzerFactoryUnitTests
    {


        //private AssetTimeframe atf = new AssetTimeframe(new Asset(1, "a"), Timeframe.GetTimeframe(TimeframeSymbol.M5));

        //private Mock<IQuotationService> mockedQuotationService()
        //{
        //    Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
        //    ProcessServiceFactory.Instance().GetQuotationService(quotationService.Object);
        //    return quotationService;
        //}



        //[TestMethod]
        //public void getAnalyzer_returns_proper_analyzer_for_Macd()
        //{
        //    AnalyzerFactory factory = AnalyzerFactory.Instance();
        //    Mock<IQuotationService> mockQuotationService = mockedQuotationService();
        //    mockQuotationService.Setup(q => q.getLastCalculationDate(It.IsAny<string>(), It.IsAny<string>())).Returns(new DateTime());

        //    IAnalyzer analyzer = factory.getAnalyzer(AnalysisType.MACD, atf);

        //    Assert.IsTrue(typeof(IMacdAnalyzer).IsAssignableFrom(analyzer.GetType()));
        //    Assert.AreEqual(analyzer.getAssetTimeframe(), atf);

        //}



        //[TestMethod]
        //public void getAnalyzer_returns_proper_analyzer_for_Prices()
        //{
        //    AnalyzerFactory factory = AnalyzerFactory.Instance();
        //    Mock<IQuotationService> mockQuotationService = mockedQuotationService();
        //    mockQuotationService.Setup(q => q.getLastCalculationDate(It.IsAny<string>(), It.IsAny<string>())).Returns(new DateTime());

        //    IAnalyzer analyzer = factory.getAnalyzer(AnalysisType.Price, atf);

        //    Assert.IsTrue(typeof(IPriceAnalyzer).IsAssignableFrom(analyzer.GetType()));
        //    Assert.AreEqual(analyzer.getAssetTimeframe(), atf);

        //}




        //[TestMethod]
        //public void getAnalyzer_returns_proper_analyzer_for_Adx()
        //{
        //    AnalyzerFactory factory = AnalyzerFactory.Instance();
        //    Mock<IQuotationService> mockQuotationService = mockedQuotationService();
        //    mockQuotationService.Setup(q => q.getLastCalculationDate(It.IsAny<string>(), It.IsAny<string>())).Returns(new DateTime());

        //    IAnalyzer analyzer = factory.getAnalyzer(AnalysisType.ADX, atf);

        //    Assert.IsTrue(typeof(IAdxAnalyzer).IsAssignableFrom(analyzer.GetType()));
        //    Assert.AreEqual(analyzer.getAssetTimeframe(), atf);

        //}




        //[TestMethod]
        //public void getAnalyzer_returns_proper_analyzer_for_Candlesticks()
        //{
        //    AnalyzerFactory factory = AnalyzerFactory.Instance();
        //    Mock<IQuotationService> mockQuotationService = mockedQuotationService();
        //    mockQuotationService.Setup(q => q.getLastCalculationDate(It.IsAny<string>(), It.IsAny<string>())).Returns(new DateTime());

        //    IAnalyzer analyzer = factory.getAnalyzer(AnalysisType.Candlestick, atf);

        //    Assert.IsTrue(typeof(ICandlestickAnalyzer).IsAssignableFrom(analyzer.GetType()));
        //    Assert.AreEqual(analyzer.getAssetTimeframe(), atf);

        //}






        //[TestMethod]
        //public void getAnalyzer_returns_proper_analyzer_for_Trendlines()
        //{
        //    AnalyzerFactory factory = AnalyzerFactory.Instance();
        //    Mock<IQuotationService> mockQuotationService = mockedQuotationService();
        //    mockQuotationService.Setup(q => q.getLastCalculationDate(It.IsAny<string>(), It.IsAny<string>())).Returns(new DateTime());

        //    IAnalyzer analyzer = factory.getAnalyzer(AnalysisType.Trendline, atf);

        //    Assert.IsTrue(typeof(ITrendlineAnalyzer).IsAssignableFrom(analyzer.GetType()));
        //    Assert.AreEqual(analyzer.getAssetTimeframe(), atf);

        //}




        //[TestMethod]
        //[TestCategory("getAnalyzers")]
        //public void getAnalyzers_returns_the_proper_analyzers_for_the_given_analysis_types()
        //{
        //    AnalyzerFactory factory = AnalyzerFactory.Instance();
        //    Asset asset = new Asset(1, "a");
        //    Timeframe timeframe = Timeframe.GetTimeframe(TimeframeSymbol.M5);
        //    AnalysisType[] typesArr = new AnalysisType[] { AnalysisType.Price, AnalysisType.MACD, AnalysisType.Trendline };
        //    IEnumerable<AnalysisType> types = new List<AnalysisType>(typesArr);

        //    var analyzers = factory.getAnalyzers(asset, timeframe, types);


        //    //Check number of items in result dictionary.
        //    Assert.AreEqual(typesArr.Length, analyzers.Count);


        //    //Check if Price analyzers is correctly set.
        //    try
        //    {
        //        IAnalyzer analyzer;
        //        analyzers.TryGetValue(AnalysisType.Price, out analyzer);
        //        Assert.IsTrue(typeof(IPriceAnalyzer).IsAssignableFrom(analyzer.GetType()));
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        Assert.Fail("Price analyzer not found");
        //    }


        //    //Check if MACD analyzers is correctly set.
        //    try
        //    {
        //        IAnalyzer analyzer;
        //        analyzers.TryGetValue(AnalysisType.MACD, out analyzer);
        //        Assert.IsTrue(typeof(IMacdAnalyzer).IsAssignableFrom(analyzer.GetType()));
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        Assert.Fail("MACD analyzer not found");
        //    }


        //    //Check if Trendline analyzers is correctly set.
        //    try
        //    {
        //        IAnalyzer analyzer;
        //        analyzers.TryGetValue(AnalysisType.Trendline, out analyzer);
        //        Assert.IsTrue(typeof(ITrendlineAnalyzer).IsAssignableFrom(analyzer.GetType()));
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        Assert.Fail("Trendline analyzer not found");
        //    }


        //}



    }
}
