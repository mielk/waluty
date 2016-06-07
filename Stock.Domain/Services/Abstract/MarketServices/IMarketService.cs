using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;

namespace Stock.Domain.Services
{
    public interface IMarketService
    {

        void injectRepository(IMarketRepository repository);


                                                                        #region markets
        IEnumerable<Market> GetMarkets();
                                                                        #endregion markets


                                                                        #region assets
        IEnumerable<Asset> FilterAssets(string q, int limit);
        Asset GetAsset(int id);
                                                                        #endregion assets


                                                                        #region fx
        IEnumerable<FxPair> FilterPairs(string q, int limit);
        FxPair GetPair(int id);
        FxPair GetPair(string symbol);
                                                                        #endregion fx

    }
}
