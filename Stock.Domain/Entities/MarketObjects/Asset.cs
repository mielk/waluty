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
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public IEnumerable<AssetTimeframe> AssetTimeframes { get; set; }



        public Asset(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            AssetTimeframes = new List<AssetTimeframe>();
        }


        public static Asset FxFromSymbol(string symbol)
        {
            return MarketService.Instance().GetPair(symbol);
        }

        public static Asset FxFromId(int id)
        {
            return MarketService.Instance().GetPair(id);
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
