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
        Market GetMarketById(int id);
        Market GetMarketByName(string name);
        Market GetMarketBySymbol(string symbol);
                                                                        #endregion markets


                                                                        #region assets
        IEnumerable<Asset> FilterAssets(string q, int limit);
        Asset GetAsset(int id);
                                                                        #endregion assets


                                                                        #region fx
        IEnumerable<FxPair> FilterPairs(string q, int limit);
        IEnumerable<FxPair> GetFxPairs();
        FxPair GetFxPair(int id);
        FxPair GetFxPair(string symbol);
                                                                        #endregion fx


                                                                        #region currencies
        IEnumerable<Currency> GetCurrencies();
        Currency GetCurrencyById(int id);
        Currency GetCurrencyByName(string name);
        Currency GetCurrencyBySymbol(string symbol);
                                                                        #endregion currencies



    }
}
