using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Stock.Domain.Enums;
using Stock.Domain.Services;
using Moq;
using Stock.Domain.Services.Factories;
using Stock.Domain.Services.Concrete;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class QuotationServiceUnitTests
    {

        //getLastDates
        private static IQuotationService service = ProcessServiceFactory.Instance().GetQuotationService();


        #region testObjects

        private Asset testAsset()
        {
            var asset = new Asset(1, "asset");
            return asset;
        }

        private Timeframe testTimeframe()
        {
            return Timeframe.GetTimeframe(TimeframeSymbol.M5);
        }

        private AssetTimeframe testAssetTimeframe()
        {
            return new AssetTimeframe(testAsset(), testTimeframe());
        }

        #endregion testObjects


        #region testServices

        private Mock<IAnalyzer> generateMockAnalyzer(AssetTimeframe atf, AnalysisType type, DateTime? firstRequiredDate)
        {
            Mock<IAnalyzer> mock = new Mock<IAnalyzer>();
            mock.Setup(q => q.getFirstRequiredDate()).Returns(firstRequiredDate);
            mock.Setup(q => q.getAssetTimeframe()).Returns(atf);
            mock.Setup(q => q.getAnalysisType()).Returns(type);
            return mock;
        }

        #endregion testServices


        private IEnumerable<DataItem> getTestDataItemsCollection(DateTime startDate, DateTime endDate)
        {
            return null;
        }



        [TestMethod]
        public void fetchData_for_firstRequiredDate_equal_to_null_proper_method_of_dataService_is_called()
        {

            AssetTimeframe atf = testAssetTimeframe();
            Dictionary<AnalysisType, IAnalyzer> analyzers = new Dictionary<AnalysisType, IAnalyzer>();
            DateTime firstRequired = new DateTime(2016, 8, 1);
            analyzers.Add(AnalysisType.Price, generateMockAnalyzer(atf, AnalysisType.Price, firstRequired).Object);
            analyzers.Add(AnalysisType.MACD, generateMockAnalyzer(atf, AnalysisType.MACD, null).Object);
            analyzers.Add(AnalysisType.ADX, generateMockAnalyzer(atf, AnalysisType.ADX, firstRequired).Object);

            Mock<IDataService> mockedDataService = new Mock<IDataService>();
            QuotationService qService = new QuotationService();
            qService.injectDataService(mockedDataService.Object);

            DataItem[] items = qService.fetchData(analyzers);
            mockedDataService.Verify(x => x.GetDataItems(atf, null, null, analyzers.Keys), Times.Exactly(1));

        }

        [TestMethod]
        public void fetchData_for_firstRequiredDate_not_null_proper_method_of_dataService_is_called()
        {

            AssetTimeframe atf = testAssetTimeframe();
            Dictionary<AnalysisType, IAnalyzer> analyzers = new Dictionary<AnalysisType, IAnalyzer>();
            DateTime laterDate = new DateTime(2016, 8, 1);
            DateTime earlierDate = new DateTime(2016, 7, 5);
            analyzers.Add(AnalysisType.Price, generateMockAnalyzer(atf, AnalysisType.Price, laterDate).Object);
            analyzers.Add(AnalysisType.MACD, generateMockAnalyzer(atf, AnalysisType.MACD, earlierDate).Object);
            analyzers.Add(AnalysisType.ADX, generateMockAnalyzer(atf, AnalysisType.ADX, laterDate).Object);

            Mock<IDataService> mockedDataService = new Mock<IDataService>();
            QuotationService qService = new QuotationService();
            qService.injectDataService(mockedDataService.Object);

            DataItem[] items = qService.fetchData(analyzers);
            mockedDataService.Verify(x => x.GetDataItems(atf, earlierDate, null, analyzers.Keys), Times.Exactly(1));

        }

    }
}
