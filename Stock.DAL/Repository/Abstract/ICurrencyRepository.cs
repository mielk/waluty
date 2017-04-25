using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public interface ICurrencyRepository
    {
        
        //Currencies.
        IEnumerable<CurrencyDto> GetCurrencies();
        CurrencyDto GetCurrencyById(int id);
        CurrencyDto GetCurrencyByName(string name);
        CurrencyDto GetCurrencyBySymbol(string symbol);

        //Currency pairs.
        IEnumerable<FxPairDto> GetFxPairs();
        IEnumerable<FxPairDto> GetFxPairs(string filter, int limit);
        FxPairDto GetFxPairById(int id);
        FxPairDto GetFxPairBySymbol(string symbol);

    }
}
