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
    public class FxPair
    {

        //Static properties.
        private static ICurrencyService service = ServiceFactory.GetCurrencyService();

        //Instance properties.
        private int id { get; set; }
        private Currency baseCurrency { get; set; }
        private Currency quoteCurrency { get; set; }
        private string name { get; set; }


        #region STATIC_METHODS

        public static void injectService(ICurrencyService _service)
        {
            service = _service;
        }

        public static void restoreDefaultService()
        {
            service = ServiceFactory.GetCurrencyService();
        }

        public static IEnumerable<FxPair> GetFxPairs()
        {
            return service.GetFxPairs();
        }

        public static IEnumerable<FxPair> GetFxPairs(string filter, int limit)
        {
            return service.GetFxPairs(filter, limit);
        }

        public static FxPair ById(int id)
        {
            return service.GetFxPairById(id);
        }

        public static FxPair BySymbol(string symbol)
        {
            return service.GetFxPairBySymbol(symbol);
        }

        #endregion STATIC_METHODS


        #region CONSTRUCTORS

        public FxPair(int id, string name, Currency baseCurrency, Currency quoteCurrency)
        {
            this.id = id;
            this.name = name;
            assignCurrencies(baseCurrency, quoteCurrency);
        }

        public FxPair(int id, string name, int baseCurrencyId, int quoteCurrencyId)
        {
            this.id = id;
            this.name = name;
            assignCurrencies(Currency.ById(baseCurrencyId), Currency.ById(quoteCurrencyId));
        }

        private void assignCurrencies(Currency baseCurrency, Currency quoteCurrency)
        {

            if (baseCurrency == null || quoteCurrency == null)
            {
                throw new ArgumentNullException("One of the given currencies is null");
            }
            else if (baseCurrency == quoteCurrency)
            {
                throw new ArgumentException("The given currencies cannot be the same");
            }

            this.baseCurrency = baseCurrency;
            this.quoteCurrency = quoteCurrency;

        }


        public static FxPair FromDto(FxPairDto dto)
        {
            var pair = new FxPair(dto.Id, dto.Name, dto.BaseCurrency, dto.QuoteCurrency);
            return pair;
        }

        #endregion CONSTRUCTORS


        #region ACCESSORS

        public int GetId()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public Currency GetBaseCurrency()
        {
            return baseCurrency;
        }

        public int GetBaseCurrencyId()
        {
            return (baseCurrency == null ? 0 : baseCurrency.GetId());
        }

        public string GetBaseCurrencyName()
        {
            return (baseCurrency == null ? string.Empty : baseCurrency.GetName());
        }

        public string GetBaseCurrencySymbol()
        {
            return (baseCurrency == null ? string.Empty : baseCurrency.GetSymbol());
        }

        public Currency GetQuoteCurrency()
        {
            return quoteCurrency;
        }

        public int GetQuoteCurrencyId()
        {
            return (quoteCurrency == null ? 0 : quoteCurrency.GetId());
        }

        public string GetQuoteCurrencyName()
        {
            return (quoteCurrency == null ? string.Empty : quoteCurrency.GetName());
        }

        public string GetQuoteCurrencySymbol()
        {
            return (quoteCurrency == null ? string.Empty : quoteCurrency.GetSymbol());
        }

        #endregion ACCESSORS


        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(FxPair)) return false;

            FxPair compared = (FxPair)obj;
            if ((compared.id) != id) return false;
            if (compared.baseCurrency == null || baseCurrency == null) return false;
            if (!compared.baseCurrency.Equals(baseCurrency)) return false;
            if (compared.quoteCurrency == null || quoteCurrency == null) return false;
            if (!compared.quoteCurrency.Equals(quoteCurrency)) return false;
            if (!compared.name.Equals(name)) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}