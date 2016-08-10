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
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public Market Market { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<AssetTimeframe> AssetTimeframes { get; set; }
        //Static.
        private static IMarketService service = MarketServiceFactory.CreateService();
        private static IEnumerable<Asset> assets = new List<Asset>();


        #region static methods

        public static void injectService(IMarketService _service)
        {
            service = _service;
        }

        public static IEnumerable<Asset> GetAllAssets()
        {
            loadAllAssets();
            return assets.ToList();
        }

        private static void loadAllAssets()
        {

            if (assets.Count() > 0) return;

            var dbAssets = service.GetAllAssets();
            foreach (var asset in dbAssets)
            {
                var match = assets.SingleOrDefault(m => m.Id == asset.Id);
                if (match == null)
                {
                    assets = assets.Concat(new[] { asset });
                }

            }
        }

        public static Asset GetAssetById(int id)
        {

            loadAllAssets();

            var asset = assets.SingleOrDefault(m => m.Id == id);
            if (asset == null)
            {
                asset = service.GetAsset(id);
                if (asset != null)
                {
                    assets = assets.Concat(new[] { asset });
                }
            }

            return asset;

        }

        public static Asset GetAssetByName(string name)
        {

            loadAllAssets();

            var asset = assets.SingleOrDefault(m => m.Name.Equals(name));
            if (asset == null)
            {
                asset = service.GetAssetByName(name);
                if (asset != null)
                {
                    assets = assets.Concat(new[] { asset });
                }
            }

            return asset;

        }

        public static Asset GetAssetBySymbol(string symbol)
        {

            loadAllAssets();

            var asset = assets.SingleOrDefault(m => m.ShortName.Equals(symbol));
            if (asset == null)
            {
                asset = service.GetAssetBySymbol(symbol);
                if (asset != null)
                {
                    assets = assets.Concat(new[] { asset });
                }
            }

            return asset;

        }

        public static IEnumerable<Asset> GetAssetsForMarket(int marketId)
        {
            loadAllAssets();
            return assets.Where(a => a.Market.Id == marketId).ToList();
        }

        #endregion static methods




        public Asset(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.ShortName = name;
            AssetTimeframes = new List<AssetTimeframe>();
        }


        public static Asset FromDto(AssetDto dto)
        {
            Asset asset = new Asset(dto.Id, dto.Name);
            asset.ShortName = (dto.Symbol == null || dto.Symbol.Length == 0 ? dto.Name : dto.Symbol);
            asset.setMarket(dto.IdMarket);
            return asset;
        }

        public void setMarket(int id)
        {
            Market market = Market.GetMarketById(id);
            this.Market = market;
        }

        public AssetTimeframe GetAssetTimeframe(Timeframe timeframe)
        {

            var assetTimeframe = AssetTimeframes.SingleOrDefault(a => a.timeframe == timeframe);
            if (assetTimeframe == null)
            {
                assetTimeframe = new AssetTimeframe(this, timeframe);
                AssetTimeframes = AssetTimeframes.Concat(new[] { assetTimeframe });
            }

            return assetTimeframe;
        }

    }
}
