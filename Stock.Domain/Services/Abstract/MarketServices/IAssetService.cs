using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Repositories;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface IAssetService
    {

        //Infrastructure.
        void InjectRepository(IAssetRepository repository);

        //Assets.
        IEnumerable<Asset> GetAllAssets();
        IEnumerable<Asset> GetAssets(string filter, int limit);
        IEnumerable<Asset> GetAssetsForMarket(int marketId);
        Asset GetAssetById(int id);
        Asset GetAssetBySymbol(string symbol);

    }
}
