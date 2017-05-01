using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Services;
using Stock.Domain.Entities;
using Moq;
using Stock.Domain.Services.Factories;
using System.Collections.Generic;
using Stock.Domain.Enums;
using Stock_UnitTest.tools;
using System.Linq;

namespace Stock_UnitTest.Stock.Domain.Services.Analyzers
{
    [TestClass]
    public class PriceAnalyzerUnitTests
    {


        private Mock<IQuotationService> mockedQuotationService(AssetTimeframe atf, DateTime lastAnalysisDate, AnalysisType type)
        {
            Mock<IQuotationService> obj = new Mock<IQuotationService>();
            obj.Setup(mqs => mqs.getLastCalculationDate(atf, type)).Returns(lastAnalysisDate);
            return obj;
        }


        [Ignore]
        [TestMethod]
        public void run_proper_items_are_only_right_calculated()
        {
            int itemsForAnalysis = 240;
            TimeframeSymbol timeframe = TimeframeSymbol.M5;
            AnalysisType analysisType = AnalysisType.Price;
            AssetTimeframe atf = UnitTestTools.testAssetTimeframe();
            PriceAnalyzer analyzer = new PriceAnalyzer(atf);
            DateTime startDate = (new DateTime(2016, 4, 21, 12, 30, 0)).Proper(timeframe);
            DateTime lastAnalysisDate = (new DateTime(2016, 4, 23, 15, 0, 0)).Proper(timeframe);
            DateTime endDate = (new DateTime(2016, 4, 27, 15, 30, 0)).Proper(timeframe);
            DataItem[] items = UnitTestTools.getDataItemsArray(timeframe, startDate, endDate, UnitTestTools.createAnalysisTypeList(new AnalysisType[]{ AnalysisType.Price }));
            Mock<IQuotationService> mockQuotationService = mockedQuotationService(atf, lastAnalysisDate, AnalysisType.Price);
            mockQuotationService.Setup(q => q.getLastCalculationDate(atf, analysisType)).Returns(lastAnalysisDate);
            Mock<IPriceProcessor> mockedProcessor = new Mock<IPriceProcessor>();
            analyzer.injectProcessor(mockedProcessor.Object);
            analyzer.injectQuotationService(mockQuotationService.Object);

            DateTime expectedFirstRightOnlyItem = lastAnalysisDate.addTimeUnits(timeframe, -itemsForAnalysis);
            DateTime expectedLastRightOnlyItem = lastAnalysisDate;

            analyzer.Analyze(items);

            //Test 1.
            DateTime d = startDate;
            while (d.CompareTo(endDate) < 0){
                d = d.getNext(timeframe);
                DataItem dataItem = items.SingleOrDefault(i => i.Date.Equals(d));
                if (dataItem ==  null){
                    throw new ArgumentNullException(string.Format("Data item for [0] has not been found", d.ToString()));
                }

                if (d.CompareTo(expectedFirstRightOnlyItem) >= 0 && d.CompareTo(expectedLastRightOnlyItem) <= 0){
                    mockedProcessor.Verify(p => p.runRightSide(It.IsAny<IAnalyzer>(), dataItem, atf), Times.Exactly(1));
                } else {
                    mockedProcessor.Verify(p => p.runRightSide(It.IsAny<IAnalyzer>(), dataItem, atf), Times.Exactly(0));
                }

            }
             
        }


        [Ignore]
        [TestMethod]
        public void run_proper_items_are_full_calculated()
        {
            int itemsForAnalysis = 240;
            TimeframeSymbol timeframe = TimeframeSymbol.M5;
            AnalysisType analysisType = AnalysisType.Price;
            AssetTimeframe atf = UnitTestTools.testAssetTimeframe();
            PriceAnalyzer analyzer = new PriceAnalyzer(atf);
            DateTime startDate = (new DateTime(2016, 4, 21, 12, 30, 0)).Proper(timeframe);
            DateTime lastAnalysisDate = (new DateTime(2016, 4, 23, 15, 0, 0)).Proper(timeframe);
            DateTime endDate = (new DateTime(2016, 4, 27, 15, 30, 0)).Proper(timeframe);
            DataItem[] items = UnitTestTools.getDataItemsArray(timeframe, startDate, endDate, UnitTestTools.createAnalysisTypeList(new AnalysisType[] { AnalysisType.Price }));
            Mock<IQuotationService> mockQuotationService = mockedQuotationService(atf, lastAnalysisDate, AnalysisType.Price);
            mockQuotationService.Setup(q => q.getLastCalculationDate(atf, analysisType)).Returns(lastAnalysisDate);
            Mock<IPriceProcessor> mockedProcessor = new Mock<IPriceProcessor>();
            analyzer.injectProcessor(mockedProcessor.Object);
            analyzer.injectQuotationService(mockQuotationService.Object);

            DateTime expectedFirstRightOnlyItem = lastAnalysisDate.addTimeUnits(timeframe, -itemsForAnalysis);
            DateTime expectedLastRightOnlyItem = lastAnalysisDate;

            analyzer.Analyze(items);

            //Test 1.
            DateTime d = startDate;
            while (d.CompareTo(endDate) < 0)
            {
                d = d.getNext(timeframe);
                DataItem dataItem = items.SingleOrDefault(i => i.Date.Equals(d));
                if (dataItem == null)
                {
                    throw new ArgumentNullException(string.Format("Data item for [0] has not been found", d.ToString()));
                }

                if (d.CompareTo(expectedLastRightOnlyItem) > 0)
                {
                    mockedProcessor.Verify(p => p.runFull(It.IsAny<IAnalyzer>(), dataItem, atf), Times.Exactly(1));
                }
                else
                {
                    mockedProcessor.Verify(p => p.runFull(It.IsAny<IAnalyzer>(), dataItem, atf), Times.Exactly(0));
                }

            }
             
        }




    }
}
