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
        public Asset Asset { get; set; }
        public Timeframe Timeframe { get; set; }
        //public DataItem[] Items { get; set; }


        #region CONSTRUCTORS

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
            this.Asset = asset;
            this.Timeframe = timeframe;
        }

        #endregion CONSTRUCTORS


        #region API

        public bool IsValid()
        {
            return Asset != null && Timeframe != null;
        }

        public string GetSymbol()
        {
            if (!IsValid())
            {
                throw new ArgumentNullException("Asset or timeframe is null");
            }
            return string.Concat(Asset.GetSymbol(), "_", Timeframe.GetName());
        }

        #endregion API


    }
}
