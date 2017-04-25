using Stock.Domain.Enums;
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
        public DataItem[] Items { get; set; }


        public string Symbol()
        {
            if (!isValid())
            {
                throw new ArgumentNullException("Asset or timeframe is null");
            }

            return asset.Symbol + "_" + timeframe.Name;

        }


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
            //loadParams(FxPair.BySymbol(asset), Timeframe.GetTimeframeByShortName(timeframe));
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
