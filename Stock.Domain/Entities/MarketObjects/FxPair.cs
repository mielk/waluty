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
        public int Id { get; set; }
        public Currency BaseCurrency { get; set; }
        public Currency QuoteCurrency { get; set; }
        public string Name { get; set; }


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
            this.Id = id;
            this.Name = name;
            assignCurrencies(baseCurrency, quoteCurrency);
        }

        public FxPair(int id, string name, int baseCurrencyId, int quoteCurrencyId)
        {
            this.Id = id;
            this.Name = name;
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

            this.BaseCurrency = baseCurrency;
            this.QuoteCurrency = quoteCurrency;

        }


        public static FxPair FromDto(FxPairDto dto)
        {
            var pair = new FxPair(dto.Id, dto.Name, dto.BaseCurrency, dto.QuoteCurrency);
            return pair;
        }

        #endregion CONSTRUCTORS


        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(FxPair)) return false;

            FxPair compared = (FxPair)obj;
            if ((compared.Id) != Id) return false;
            if (compared.BaseCurrency == null || BaseCurrency == null) return false;
            if (!compared.BaseCurrency.Equals(BaseCurrency)) return false;
            if (compared.QuoteCurrency == null || QuoteCurrency == null) return false;
            if (!compared.QuoteCurrency.Equals(QuoteCurrency)) return false;
            if (!compared.Name.Equals(Name)) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}