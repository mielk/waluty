using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Factories
{
    public class AnalyzerFactory
    {

        private static AnalyzerFactory _instance;

        public static AnalyzerFactory Instance()
        {
            return _instance ?? (_instance = new AnalyzerFactory());
        }

        public static void inject(AnalyzerFactory instance)
        {
            _instance = instance;
        }

        public IAnalyzer getAnalyzer(AnalysisType type, AssetTimeframe atf)
        {

            switch (type)
            {
                case AnalysisType.Price: return new PriceAnalyzer(atf);
                case AnalysisType.MACD: return new MacdAnalyzer(atf);
                case AnalysisType.ADX: return new AdxAnalyzer(atf);
                case AnalysisType.Candlestick: return new CandlestickAnalyzer(atf);
                case AnalysisType.Trendline: return new TrendlineAnalyzer(atf);
                case AnalysisType.Unknown: return null;
                default: return null;
            }

        }



        public Dictionary<AnalysisType, IAnalyzer> getAnalyzers(AssetTimeframe atf, IEnumerable<AnalysisType> types)
        {

            var dict = new Dictionary<AnalysisType, IAnalyzer>();
            foreach (var type in types)
            {
                IAnalyzer analyzer = getAnalyzer(type, atf);
                dict.Add(type, analyzer);
            }

            return dict;

        }



        public Dictionary<AnalysisType, IAnalyzer> getAnalyzers(Asset asset, Timeframe timeframe, IEnumerable<AnalysisType> types)
        {

            AssetTimeframe atf = new AssetTimeframe(asset, timeframe);
            var dict = new Dictionary<AnalysisType, IAnalyzer>();
            foreach (var type in types)
            {
                IAnalyzer analyzer = getAnalyzer(type, atf);
                dict.Add(type, analyzer);
            }

            return dict;

        }

    }

}
