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

        public IAnalyzer getAnalyzer(AnalysisType type, Asset asset, Timeframe timeframe)
        {

            switch (type)
            {
                case AnalysisType.Price: return new PriceAnalyzer(asset, timeframe);
                case AnalysisType.MACD: return new MacdAnalyzer(asset, timeframe);
                case AnalysisType.ADX: return new AdxAnalyzer(asset, timeframe);
                case AnalysisType.Candlestick: return new CandlestickAnalyzer(asset, timeframe);
                case AnalysisType.Trendline: return new TrendlineAnalyzer(asset, timeframe);
                case AnalysisType.Unknown: return null;
                default: return null;
            }

        }

    }

}
