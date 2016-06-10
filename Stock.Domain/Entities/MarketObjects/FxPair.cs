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
    public class FxPair : Asset
    {
        public bool IsFx { get; set; }
        public Currency BaseCurrency { get; set; }
        public Currency QuoteCurrency { get; set; }
        //Static.
        private static IMarketService service = MarketServiceFactory.CreateService();
        private static IEnumerable<FxPair> pairs = new List<FxPair>();



        #region static methods

        public static void injectService(IMarketService _service)
        {
            service = _service;
        }

        public static IEnumerable<FxPair> GetAllFxPairs()
        {
            List<FxPair> result = new List<FxPair>();
            var dbPairs = service.GetFxPairs();
            foreach (var pair in dbPairs)
            {
                var match = pairs.SingleOrDefault(c => c.Id == pair.Id);
                if (match == null)
                {
                    pairs = pairs.Concat(new[] { pair });
                    match = pair;
                }

                result.Add(match);

            }

            return result;

        }

        public static FxPair GetFxPairById(int id)
        {

            var pair = pairs.SingleOrDefault(m => m.Id == id);

            if (pair == null)
            {
                pair = service.GetFxPair(id);
                if (pair != null)
                {
                    pairs = pairs.Concat(new[] { pair });
                }
            }

            return pair;

        }

        public static FxPair GetFxPairBySymbol(string symbol)
        {

            var pair = pairs.SingleOrDefault(m => m.Name.Equals(symbol));

            if (pair == null)
            {
                pair = service.GetFxPair(symbol);
                if (pair != null)
                {
                    pairs = pairs.Concat(new[] { pair });
                }
            }

            return pair;

        }

        #endregion static methods


        public FxPair(int id, string name, Currency baseCurrency, Currency quoteCurrency) : base(id, name)
        {
            assignCurrencies(baseCurrency, quoteCurrency);
        }

        public FxPair(int id, string name, int baseCurrencyId, int quoteCurrencyId) : base(id, name)
        {
            assignCurrencies(Currency.GetCurrencyById(baseCurrencyId), Currency.GetCurrencyById(quoteCurrencyId));
        }

        private void assignCurrencies(Currency baseCurrency, Currency quoteCurrency)
        {
            IsFx = true;

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
            pair.IsActive = dto.IsActive;
            pair.IsFx = true;

            return pair;

        }


    }
}