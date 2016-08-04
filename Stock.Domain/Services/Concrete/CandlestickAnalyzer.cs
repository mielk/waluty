using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class CandlestickAnalyzer : ICandlestickAnalyzer
    {
        public const AnalysisType Type = AnalysisType.Candlestick;

        private IAnalysisDataService _dataService;

        public Asset Asset { get; set; }
        public Timeframe Timeframe { get; set; }



        public CandlestickAnalyzer(Asset asset, Timeframe timeframe)
        {
            this.Asset = asset;
            this.Timeframe = timeframe;
        }


        public CandlestickAnalyzer(IAnalysisDataService dataService)
        {
            this._dataService = dataService;
        }

        public void injectDataService(IAnalysisDataService dataService)
        {
            _dataService = dataService;
        }




        public void Analyze(string symbol)
        {
            Analyze(symbol, false);
        }



        public void Analyze(string symbol, bool fromScratch)
        {
        }

    }

}
