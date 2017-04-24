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

        //Static properties.
        private static ICurrencyService service = ServiceFactory.GetCurrencyService();

        //Instance properties.
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }



        #region STATIC_METHODS

        public static void injectService(ICurrencyService _service)
        {
            service = _service;
        }

        public static IEnumerable<Currency> GetAllCurrencies()
        {
            return service.GetAllCurrencies();
        }

        public static Currency ById(int id)
        {
            return service.GetCurrencyById(id);
        }

        public static Currency ByName(string name)
        {
            return service.GetCurrencyByName(name);
        }

        public static Currency BySymbol(string symbol)
        {
            return service.GetCurrencyBySymbol(symbol);
        }

        #endregion STATIC_METHODS



        #region CONSTRUCTORS

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

        #endregion CONSTRUCTORS



        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Currency)) return false;

            Currency compared = (Currency)obj;
            if ((compared.Id) != Id) return false;
            if (!compared.Name.Equals(Name)) return false;
            if (!compared.Symbol.Equals(Symbol)) return false;
            return true;

        }

    }
}
