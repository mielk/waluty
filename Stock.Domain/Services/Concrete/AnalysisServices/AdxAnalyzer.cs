using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class AdxAnalyzer : Analyzer, IAdxAnalyzer
    {

        public override AnalysisType Type
        {
            get { return AnalysisType.ADX; }
        }


        private IAnalysisDataService _dataService;
        


        public AdxAnalyzer(Asset asset, Timeframe timeframe) : base(asset, timeframe)
        {
            initialize();
        }

        public AdxAnalyzer(IAnalysisDataService dataService, Asset asset, Timeframe timeframe) : base(asset, timeframe)
        {
            initialize();
            this._dataService = dataService;
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
