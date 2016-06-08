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
        AssetDto GetAsset(int id);
                                                #endregion assets


                                                #region fx
        IEnumerable<FxPairDto> FilterPairs(string q, int limit);
        FxPairDto GetPair(int id);
        FxPairDto GetPair(string symbol);
                                                #endregion fx

    }
}
