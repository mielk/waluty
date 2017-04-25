using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public class EFAssetRepository : IAssetRepository
    {

        public IEnumerable<AssetDto> GetAllAssets()
        {
            IEnumerable<AssetDto> results;
            using (var context = new AssetContext())
            {
                results = context.Assets.ToList();
            }
            return results;
        }

        public IEnumerable<AssetDto> GetAssets(string filter, int limit)
        {
            IEnumerable<AssetDto> results;
            using (var context = new AssetContext())
            {
                results = context.Assets.Where(c => c.Symbol.ToLower().Contains(filter.ToLower())).Take(limit).ToList();
            }
            return results;
        }

        public IEnumerable<AssetDto> GetAssetsForMarket(int marketId)
        {
            IEnumerable<AssetDto> results;
            using (var context = new AssetContext())
            {
                results = context.Assets.Where(c => c.MarketId == marketId).ToList();
            }
            return results;
        }

        public AssetDto GetAssetById(int id)
        {
            AssetDto dto;
            using (var context = new AssetContext())
            {
                dto = context.Assets.SingleOrDefault(c => c.Id == id);
            }
            return dto;
        }

        public AssetDto GetAssetBySymbol(string name)
        {
            AssetDto dto;
            using (var context = new AssetContext())
            {
                dto = context.Assets.SingleOrDefault(c => c.Symbol.Equals(name, System.StringComparison.CurrentCultureIgnoreCase));
            }
            return dto;
        }

    }
}
