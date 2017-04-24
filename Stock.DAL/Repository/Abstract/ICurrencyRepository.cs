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
        IEnumerable<CurrencyDto> GetCurrencies();
        CurrencyDto GetCurrencyById(int id);
        CurrencyDto GetCurrencyByName(string name);
        CurrencyDto GetCurrencyBySymbol(string symbol);
    }
}
