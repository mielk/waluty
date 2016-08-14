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





        public CandlestickAnalyzer(AssetTimeframe atf)
            : base(atf)
        {
        }


        public CandlestickAnalyzer(IAnalysisDataService dataService, AssetTimeframe atf)
            : base(atf)
        {
            this._dataService = dataService;
        }

        public void injectDataService(IAnalysisDataService dataService)
        {
            _dataService = dataService;
        }


        protected override void initialize()
        {
            DaysForAnalysis = 300;
        }






        public override void Analyze(DataItem[] items)
        {
            
        }


    }

}
