using Stock.DAL.TransferObjects;
using Stock.Domain.Services;
using Stock.Domain.Services.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        //Static.
        private static IMarketService service = MarketServiceFactory.CreateService();
        private static IEnumerable<Currency> currencies = new List<Currency>();



        #region static methods

        public static void injectService(IMarketService _service)
        {
            service = _service;
        }

        public static IEnumerable<Currency> GetAllCurrencies()
        {
            List<Currency> result = new List<Currency>();
            var dbCurrencies = service.GetCurrencies();
            foreach (var currency in dbCurrencies)
            {
                var match = currencies.SingleOrDefault(c => c.Id == currency.Id);
                if (match == null)
                {
                    currencies = currencies.Concat(new[] { currency });
                    match = currency;
                }

                result.Add(match);

            }

            return result;

        }

        public static Currency GetCurrencyById(int id)
        {

            var currency = currencies.SingleOrDefault(m => m.Id == id);

            if (currency == null)
            {
                currency = service.GetCurrencyById(id);
                if (currency != null)
                {
                    currencies = currencies.Concat(new[] { currency });
                }
            }

            return currency;

        }

        public static Currency GetCurrencyByName(string name)
        {

            var currency = currencies.SingleOrDefault(m => m.Name.Equals(name));

            if (currency == null)
            {
                currency = service.GetCurrencyByName(name);
                if (currency != null)
                {
                    currencies = currencies.Concat(new[] { currency });
                }
            }

            return currency;

        }

        public static Currency GetCurrencyBySymbol(string symbol)
        {

            var currency = currencies.SingleOrDefault(m => m.Symbol.Equals(symbol));

            if (currency == null)
            {
                currency = service.GetCurrencyBySymbol(symbol);
                if (currency != null)
                {
                    currencies = currencies.Concat(new[] { currency });
                }
            }

            return currency;

        }

        #endregion static methods



        public Currency(int id, string symbol, string name)
        {
            this.Id = id;
            this.Symbol = symbol;
            this.Name = name;
        }

        public static Currency FromDto(CurrencyDto dto)
        {

            var currency = new Currency(dto.Id, dto.Symbol, dto.Name);
            return currency;

        }

    }
}
