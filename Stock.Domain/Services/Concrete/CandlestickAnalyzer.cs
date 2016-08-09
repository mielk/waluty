using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class CandlestickAnalyzer : Analyzer, ICandlestickAnalyzer
    {
        public const AnalysisType Type = AnalysisType.Candlestick;

        private IAnalysisDataService _dataService;





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





        public override void Analyze()
        {
            Analyze(false);
        }

        public override void Analyze(bool fromScratch)
        {

        }


    }

}
