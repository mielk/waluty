using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Stock.Domain.Enums;
using Stock.Domain.Services;
using Moq;
using Stock.Domain.Services.Factories;
using Stock.Domain.Services.Concrete;
using Stock.Domain.Services.Abstract;
using Stock.Domain.Entities;

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class QuotationServiceUnitTests
    {

        //getLastDates
        private static IQuotationService service = ProcessServiceFactory.Instance().GetQuotationService();

        private Dictionary<AnalysisType, IAnalyzer> generateMockAnalyzers()
        {
            AssetTimeframe atf = new AssetTimeframe(new Asset(1, "EURUSD"), Timeframe.GetTimeframe(TimeframeSymbol.H1));
            Dictionary<AnalysisType, IAnalyzer> dict = new Dictionary<AnalysisType, IAnalyzer>();
            Mock<IAnalyzer> mockPriceAnalyzer = new Mock<IAnalyzer>();
            mockPriceAnalyzer.Setup(q => q.getFirstRequiredDate()).Returns(new DateTime(2016, 4, 21, 12, 0, 0));
            mockPriceAnalyzer.Setup(q => q.getAssetTimeframe()).Returns(atf);
            dict.Add(AnalysisType.Price, mockPriceAnalyzer.Object);

            Mock<IAnalyzer> mockMacdAnalyzer = new Mock<IAnalyzer>();
            mockMacdAnalyzer.Setup(q => q.getFirstRequiredDate()).Returns(new DateTime(2016, 4, 23, 16, 0, 0));
            mockMacdAnalyzer.Setup(q => q.getAssetTimeframe()).Returns(atf);
            dict.Add(AnalysisType.MACD, mockMacdAnalyzer.Object);

            return dict;

        }

        [TestMethod]
        public void fetchData_first_item_in_returned_array_has_earliest_required_date_check_for_null()
        {
            var analyzers = generateMockAnalyzers();
            DataItem[] items = service.fetchData(analyzers);
            var result = items[0];
        }


        [TestMethod]
        public void fetchData_first_item_in_returned_array_has_earliest_required_date_check_for_not_null()
        {
        }

        [TestMethod]
        public void fetchData_last_item_in_returned_array_has_last_quotation_date()
        {
        }

    }
}
