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
        public const AnalysisType Type = AnalysisType.ADX;

        private IAnalysisDataService _dataService;
        


        public AdxAnalyzer(Asset asset, Timeframe timeframe){
            this.Asset = asset;
            this.Timeframe = timeframe;
        }



        public AdxAnalyzer(IAnalysisDataService dataService, Asset asset, Timeframe timeframe)
        {
            this._dataService = dataService;
            this.Asset = asset;
            this.Timeframe = timeframe;
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
