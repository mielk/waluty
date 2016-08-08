using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class AdxAnalyzer : IAdxAnalyzer
    {

        private IAnalysisDataService _dataService;


        public Asset Asset { get; set; }
        public Timeframe Timeframe { get; set; }
        

        /* Getter methods (for IAnalyzer interface) */
        public Asset getAsset() { return Asset; }
        public Timeframe getTimeframe() { return Timeframe; }




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






        public void Analyze()
        {
            Analyze(false);
        }

        public void Analyze(bool fromScratch)
        {

        }


    }


}
