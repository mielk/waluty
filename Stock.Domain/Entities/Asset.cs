using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;
using Stock.Domain.Services.Factories;


namespace Stock.Domain.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<AssetTimeframe> AssetTimeframes { get; set; }
        //public DateTime LastPriceUpdate { get; set; }
        //public DateTime LastCalculation { get; set; }
        //public bool PricesChecked { get; set; }
        //public DateTime LastTrendlinesReview { get; set; }

        public Asset(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            AssetTimeframes = new List<AssetTimeframe>();
        }

        public static Asset FromSymbol(string symbol)
        {
            var fxService = FxServiceFactory.Instance().GetService();
            return fxService.GetPair(symbol);
        }

        public static Asset FromId(int id)
        {
            var fxService = FxServiceFactory.Instance().GetService();
            return fxService.GetPair(id);
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
