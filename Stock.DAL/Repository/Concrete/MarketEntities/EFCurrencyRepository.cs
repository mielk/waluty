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
            using (var context = new CurrencyContext())
            {
                currencies = context.Currencies.ToList();
            }
            return currencies;
        }

        public CurrencyDto GetCurrencyById(int id)
        {
            CurrencyDto currency;
            using (var context = new CurrencyContext())
            {
                currency = context.Currencies.SingleOrDefault(c => c.Id == id);
            }
            return currency;
        }

        public CurrencyDto GetCurrencyByName(string name)
        {
            CurrencyDto currency;
            using (var context = new CurrencyContext())
            {
                currency = context.Currencies.SingleOrDefault(c => c.Name.Equals(name));
            }
            return currency;
        }

        public CurrencyDto GetCurrencyBySymbol(string symbol)
        {
            CurrencyDto currency;
            using (var context = new CurrencyContext())
            {
                currency = context.Currencies.SingleOrDefault(c => c.Symbol.Equals(symbol));
            }
            return currency;
        }


        
        public IEnumerable<FxPairDto> GetFxPairs()
        {
            IEnumerable<FxPairDto> results;
            using (var context = new CurrencyContext())
            {
                results = context.Pairs.ToList();
            }
            return results;
        }

        public IEnumerable<FxPairDto> GetFxPairs(string filter, int limit)
        {
            IEnumerable<FxPairDto> results;
            using (var context = new CurrencyContext())
            {
                results = context.Pairs.Where(p => p.IsActive && p.Name.ToLower().Contains(filter.ToLower())).Take(limit).ToList();
            }
            return results;
        }

        public FxPairDto GetFxPairById(int id)
        {
            FxPairDto pair;
            using (var context = new CurrencyContext())
            {
                pair = context.Pairs.SingleOrDefault(p => p.Id == id);
            }
            return pair;
        }

        public FxPairDto GetFxPairBySymbol(string symbol)
        {
            FxPairDto pair;
            using (var context = new CurrencyContext())
            {
                pair = context.Pairs.SingleOrDefault(p => p.Name == symbol);
            }
            return pair;
        }


    }

}
