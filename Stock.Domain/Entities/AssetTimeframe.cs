using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class AssetTimeframe
    {

        public Asset asset { get; set; }
        public Timeframe timeframe { get; set; }


        public AssetTimeframe(Asset asset, Timeframe timeframe)
        {
            loadParams(asset, timeframe);
        }

        public AssetTimeframe(string asset, string timeframe)
        {
            loadParams(asset, timeframe);
            
        }

        public AssetTimeframe(string symbol)
        {
            var partSymbols = symbol.Split('_');            
            loadParams(partSymbols[0], partSymbols[1]);
        }

        private void loadParams(string asset, string timeframe)
        {
            loadParams(Asset.FromSymbol(asset), Timeframe.GetTimeframeByShortName(timeframe));
        }

        private void loadParams(Asset asset, Timeframe timeframe)
        {
            this.asset = asset;
            this.timeframe = timeframe;
        }



        public bool isValid()
        {
            return asset != null && timeframe != null;
        }

    }
}
