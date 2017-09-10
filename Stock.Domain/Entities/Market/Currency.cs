using Stock.DAL.TransferObjects;
using Stock.Domain.Services;
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
        private int id { get; set; }
        private string symbol { get; set; }
        private string name { get; set; }


        #region STATIC_METHODS

        public static void InjectService(ICurrencyService _service)
        {
            service = _service;
        }

        public static void RestoreDefaultService()
        {
            service = ServiceFactory.GetCurrencyService();
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
            this.id = id;
            this.symbol = symbol;
            this.name = name;
        }

        public static Currency FromDto(CurrencyDto dto)
        {
            var currency = new Currency(dto.Id, dto.Symbol, dto.Name);
            return currency;
        }

        #endregion CONSTRUCTORS


        #region ACCESSORS

        public int GetId()
        {
            return id;
        }

        public string GetSymbol()
        {
            return symbol;
        }

        public string GetName()
        {
            return name;
        }

        #endregion ACCESSORS


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Currency)) return false;

            Currency compared = (Currency)obj;
            if ((compared.id) != id) return false;
            if (!compared.name.Equals(name)) return false;
            if (!compared.symbol.Equals(symbol)) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
