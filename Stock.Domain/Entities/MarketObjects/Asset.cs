using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;
using Stock.Domain.Services;
using Stock.Domain.Services.Factories;


namespace Stock.Domain.Entities
{
    public class Asset
    {

        //Static properties.
        private static IAssetService service = ServiceFactory.GetAssetService();

        //Instance properties.
        public int Id { get; set; }
        public string Symbol { get; set; }
        public Market Market { get; set; }
        //public IEnumerable<AssetTimeframe> AssetTimeframes { get; set; }


        #region STATIC_METHODS

        public static void injectService(IAssetService _service)
        {
            service = _service;
        }

        public static void restoreDefaultService()
        {
            service = ServiceFactory.GetAssetService();
        }

        public static IEnumerable<Asset> GetAllAssets()
        {
            return service.GetAllAssets();
        }

        public static IEnumerable<Asset> GetAssets(string filter, int limit)
        {
            return service.GetAssets(filter, limit);
        }

        public static IEnumerable<Asset> GetAssetsForMarket(int marketId)
        {
            var assets = service.GetAssetsForMarket(marketId);
            //return service.GetAssetsForMarket(marketId);
            return assets;
        }

        public static Asset ById(int id)
        {
            return service.GetAssetById(id);
        }

        public static Asset BySymbol(string symbol)
        {
            return service.GetAssetBySymbol(symbol);
        }

        #endregion STATIC_METHODS


        #region CONSTRUCTORS

        public Asset(int id, string symbol, Market market)
        {
            assignProperties(id, symbol, market);
        }

        public Asset(int id, string symbol, int marketId)
        {
            assignProperties(id, symbol, Market.ById(marketId));
        }

        private void assignProperties(int id, string symbol, Market market)
        {
            this.Id = id;
            this.Symbol = symbol;
            this.Market = market;
        }

        public static Asset FromDto(AssetDto dto)
        {
            Asset asset = new Asset(dto.Id, dto.Symbol, dto.MarketId);
            return asset;
        }

        #endregion CONSTRUCTORS


        #region API

        public int MarketId()
        {
            return (Market == null ? 0 : Market.Id);
        }

        #endregion API


        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Asset)) return false;

            Asset compared = (Asset)obj;
            if ((compared.Id) != Id) return false;
            if (!compared.Symbol.Equals(Symbol)) return false;
            if (compared.MarketId() != MarketId()) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }





        //public AssetTimeframe GetAssetTimeframe(Timeframe timeframe)
        //{

        //    var assetTimeframe = AssetTimeframes.SingleOrDefault(a => a.timeframe == timeframe);
        //    if (assetTimeframe == null)
        //    {
        //        assetTimeframe = new AssetTimeframe(this, timeframe);
        //        AssetTimeframes = AssetTimeframes.Concat(new[] { assetTimeframe });
        //    }

        //    return assetTimeframe;
        //}

    }
}
