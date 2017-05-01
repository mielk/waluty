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
        private Asset asset;
        private Timeframe timeframe;


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
            loadParams(Asset.BySymbol(asset), Timeframe.ByName(timeframe));
        }

        private void loadParams(Asset asset, Timeframe timeframe)
        {
            this.asset = asset;
            this.timeframe = timeframe;
        }

        #endregion CONSTRUCTORS


        #region GETTERS

        public Asset GetAsset()
        {
            return asset;
        }

        public Timeframe GetTimeframe()
        {
            return timeframe;
        }

        #endregion GETTERS


        #region API

        public bool IsValid()
        {
            return asset != null && timeframe != null;
        }

        public string GetSymbol()
        {
            if (!IsValid())
            {
                throw new ArgumentNullException("Asset or timeframe is null");
            }
            return string.Concat(asset.GetSymbol(), "_", timeframe.GetName());
        }

        #endregion API


    }
}
