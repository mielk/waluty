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
        public override AnalysisType Type
        {
            get { return AnalysisType.Candlestick; }
        }

        private IAnalysisDataService _dataService;





        public CandlestickAnalyzer(Asset asset, Timeframe timeframe) : base(asset, timeframe)
        {
        }


        public CandlestickAnalyzer(IAnalysisDataService dataService, Asset asset, Timeframe timeframe)
            : base(asset, timeframe)
        {
            this._dataService = dataService;
        }

        public void injectDataService(IAnalysisDataService dataService)
        {
            _dataService = dataService;
        }

        private void initialize()
        {
            DaysForAnalysis = 240;
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
