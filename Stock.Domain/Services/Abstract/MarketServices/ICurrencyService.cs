using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Repositories;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface ICurrencyService
    {

        //Infrastructure.
        void InjectRepository(ICurrencyRepository repository);

        //Currencies.
        IEnumerable<Currency> GetAllCurrencies();
        Currency GetCurrencyById(int id);
        Currency GetCurrencyByName(string name);
        Currency GetCurrencyBySymbol(string symbol);

        //Currency pairs.
        IEnumerable<FxPair> GetFxPairs();
        IEnumerable<FxPair> GetFxPairs(string filter, int limit);
        FxPair GetFxPairById(int id);
        FxPair GetFxPairBySymbol(string symbol);

    }
}