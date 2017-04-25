using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public interface IAssetRepository
    {
        IEnumerable<AssetDto> GetAllAssets();
        IEnumerable<AssetDto> GetAssets(string filter, int limit);
        IEnumerable<AssetDto> GetAssetsForMarket(int marketId);
        AssetDto GetAssetById(int id);
        AssetDto GetAssetBySymbol(string symbol);
    }
}
