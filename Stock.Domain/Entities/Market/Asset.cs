using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;
using Stock.Domain.Services;


namespace Stock.Domain.Entities
{

    public class Asset
    {

        //Static properties.
        private static IAssetService service = ServiceFactory.GetAssetService();

        //Instance properties.
        private int id { get; set; }
        private string symbol { get; set; }
        private Market market { get; set; }
        //public IEnumerable<AssetTimeframe> AssetTimeframes { get; set; }


        #region STATIC_METHODS

        public static void InjectService(IAssetService _service)
        {
            service = _service;
        }

        public static void RestoreDefaultService()
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
            return service.GetAssetsForMarket(marketId);
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
            this.id = id;
            this.symbol = symbol;
            this.market = market;
        }

        public static Asset FromDto(AssetDto dto)
        {
            Asset asset = new Asset(dto.Id, dto.Symbol, dto.MarketId);
            return asset;
        }

        #endregion CONSTRUCTORS


        #region GETTERS

        public int GetId()
        {
            return id;
        }

        public string GetSymbol()
        {
            return symbol;
        }

        public Market GetMarket()
        {
            return market;
        }

        public int GetMarketId()
        {
            return (market == null ? 0 : market.GetId());
        }

        #endregion GETTERS



        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Asset)) return false;

            Asset compared = (Asset)obj;
            if ((compared.GetId()) != id) return false;
            if (!compared.GetSymbol().Equals(symbol)) return false;
            if (compared.GetMarketId() != GetMarketId()) return false;
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
