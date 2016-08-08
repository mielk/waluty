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




        /* Getter methods (for IAnalyzer interface) */
        public Asset getAsset() { return Asset; }
        public Timeframe getTimeframe() { return Timeframe; }




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





        public void Analyze()
        {
            Analyze(false);
        }

        public void Analyze(bool fromScratch)
        {

        }


    }

}
