using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public class EFCurrencyRepository : ICurrencyRepository
    {

        public IEnumerable<CurrencyDto> GetCurrencies()
        {
            IEnumerable<CurrencyDto> currencies;
            using (var context = new EFDbContext())
            {
                currencies = context.Currencies.ToList();
            }
            return currencies;
        }

        public CurrencyDto GetCurrencyById(int id)
        {
            CurrencyDto currency;
            using (var context = new EFDbContext())
            {
                currency = context.Currencies.SingleOrDefault(c => c.Id == id);
            }
            return currency;
        }

        public CurrencyDto GetCurrencyByName(string name)
        {
            CurrencyDto currency;
            using (var context = new EFDbContext())
            {
                currency = context.Currencies.SingleOrDefault(c => c.Name.Equals(name));
            }
            return currency;
        }

        public CurrencyDto GetCurrencyBySymbol(string symbol)
        {
            CurrencyDto currency;
            using (var context = new EFDbContext())
            {
                currency = context.Currencies.SingleOrDefault(c => c.Symbol.Equals(symbol));
            }
            return currency;
        }

    }
}
