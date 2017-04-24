using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public interface IMarketRepository
    {

                                                #region markets
        IEnumerable<MarketDto> GetMarkets();
        MarketDto GetMarketById(int id);
        MarketDto GetMarketByName(string name);
        MarketDto GetMarketBySymbol(string symbol);
                                                #endregion markets


                                                #region assets
        IEnumerable<AssetDto> FilterAssets(string q, int limit);
        IEnumerable<AssetDto> GetAllAssets();
        IEnumerable<AssetDto> GetAssetsForMarket(int marketId);
        AssetDto GetAsset(int id);
        AssetDto GetAssetByName(string name);
        AssetDto GetAssetBySymbol(string symbol);
                                                #endregion assets


                                                #region fx
        IEnumerable<FxPairDto> FilterPairs(string q, int limit);
        IEnumerable<FxPairDto> GetFxPairs();
        FxPairDto GetFxPair(int id);
        FxPairDto GetFxPair(string symbol);
                                                #endregion fx

    }
}
