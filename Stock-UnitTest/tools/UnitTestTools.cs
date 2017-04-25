using Moq;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.Domain.Services;
using Stock.Domain.Services.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Services;

namespace Stock_UnitTest.tools
{
    public class UnitTestTools
    {


        #region testObjects

        public static Asset testAsset()
        {
            var asset = new Asset(1, "asset", 1);
            return asset;
        }

        public static Timeframe testTimeframe()
        {
            return Timeframe.GetTimeframe(TimeframeSymbol.M5);
        }

        public static AssetTimeframe testAssetTimeframe()
        {
            return new AssetTimeframe(testAsset(), testTimeframe());
        }

        public static List<AnalysisType> createAnalysisTypeList(AnalysisType[] types)
        {
            List<AnalysisType> list = new List<AnalysisType>();
            foreach (var type in types)
            {
                list.Add(type);
            }
            return list;
        }

        #endregion testObjects


        #region testServices

        public static Mock<IAnalyzer> generateMockAnalyzer(AssetTimeframe atf, AnalysisType type, DateTime? firstRequiredDate)
        {
            Mock<IAnalyzer> mock = new Mock<IAnalyzer>();
            mock.Setup(q => q.getFirstRequiredDate()).Returns(firstRequiredDate);
            mock.Setup(q => q.getAssetTimeframe()).Returns(atf);
            mock.Setup(q => q.getAnalysisType()).Returns(type);
            return mock;
        }

        public static Mock<IQuotationService> mockedQuotationService()
        {
            Mock<IQuotationService> quotationService = new Mock<IQuotationService>();
            ProcessServiceFactory.Instance().GetQuotationService(quotationService.Object);
            return quotationService;
        }

        public static DataItem[] getDataItemsArray(int counter)
        {
            return new DataItem[] { };
        }

        public static DataItem[] getDataItemsArray(TimeframeSymbol timeframe, DateTime startDate, DateTime endDate, List<AnalysisType> types)
        {
            DateTime minDate = startDate.CompareTo(endDate) < 0 ? startDate : endDate;
            DateTime d = new DateTime(minDate.Ticks);
            List<DataItem> items = new List<DataItem>();

            while (d.CompareTo(endDate) <= 0)
            {
                var item = generateDataItem(d, types);
                d = d.getNext(timeframe);
                items.Add(item);
            }

            var array = items.ToArray();
            array.AppendIndexNumbers();
            return array;

        }

        public static DataItem generateDataItem(DateTime d, List<AnalysisType> types)
        {
            var item = new DataItem();
            item.Asset = testAsset();
            item.Timeframe = testTimeframe();
            item.Date = d;

            if (types.Contains(AnalysisType.Price)) item.Price = new Price() { Date = d };
            if (types.Contains(AnalysisType.MACD)) item.Macd = new Macd() { Date = d };
            if (types.Contains(AnalysisType.ADX)) item.Adx = new Adx() { Date = d };

            return item;

        }

        #endregion testServices



    }
}
