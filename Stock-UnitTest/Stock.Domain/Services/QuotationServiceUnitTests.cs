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
using Stock_UnitTest.tools;

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class QuotationServiceUnitTests
    {

        //getLastDates
        private static IQuotationService service = ProcessServiceFactory.Instance().GetQuotationService();


        private IEnumerable<DataItem> getTestDataItemsCollection(DateTime startDate, DateTime endDate)
        {
            return null;
        }



        [TestMethod]
        public void fetchData_for_firstRequiredDate_equal_to_null_proper_method_of_dataService_is_called()
        {

            AssetTimeframe atf = UnitTestTools.testAssetTimeframe();
            Dictionary<AnalysisType, IAnalyzer> analyzers = new Dictionary<AnalysisType, IAnalyzer>();
            DateTime firstRequired = new DateTime(2016, 8, 1);
            analyzers.Add(AnalysisType.Price, UnitTestTools.generateMockAnalyzer(atf, AnalysisType.Price, firstRequired).Object);
            analyzers.Add(AnalysisType.MACD, UnitTestTools.generateMockAnalyzer(atf, AnalysisType.MACD, null).Object);
            analyzers.Add(AnalysisType.ADX, UnitTestTools.generateMockAnalyzer(atf, AnalysisType.ADX, firstRequired).Object);

            Mock<IDataService2> mockedDataService = new Mock<IDataService2>();
            QuotationService qService = new QuotationService();
            qService.injectDataService(mockedDataService.Object);

            DataItem[] items = qService.fetchData(analyzers);
            mockedDataService.Verify(x => x.GetDataItems(atf, null, null, analyzers.Keys), Times.Exactly(1));

        }

        [TestMethod]
        public void fetchData_for_firstRequiredDate_not_null_proper_method_of_dataService_is_called()
        {

            AssetTimeframe atf = UnitTestTools.testAssetTimeframe();
            Dictionary<AnalysisType, IAnalyzer> analyzers = new Dictionary<AnalysisType, IAnalyzer>();
            DateTime laterDate = new DateTime(2016, 8, 1);
            DateTime earlierDate = new DateTime(2016, 7, 5);
            analyzers.Add(AnalysisType.Price, UnitTestTools.generateMockAnalyzer(atf, AnalysisType.Price, laterDate).Object);
            analyzers.Add(AnalysisType.MACD, UnitTestTools.generateMockAnalyzer(atf, AnalysisType.MACD, earlierDate).Object);
            analyzers.Add(AnalysisType.ADX, UnitTestTools.generateMockAnalyzer(atf, AnalysisType.ADX, laterDate).Object);

            Mock<IDataService2> mockedDataService = new Mock<IDataService2>();
            QuotationService qService = new QuotationService();
            qService.injectDataService(mockedDataService.Object);

            DataItem[] items = qService.fetchData(analyzers);
            mockedDataService.Verify(x => x.GetDataItems(atf, earlierDate, null, analyzers.Keys), Times.Exactly(1));

        }

    }
}
