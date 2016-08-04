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

        public Asset Asset { get; set; }
        public Timeframe Timeframe { get; set; }
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




        public void Analyze(string symbol)
        {
            Analyze(symbol, false);
        }



        public void Analyze(string symbol, bool fromScratch)
        {
        }


    }


}
