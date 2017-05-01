using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Infrastructure;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public class CurrencyService : ICurrencyService
    {

        private ICurrencyRepository _repository;
        private static readonly ICurrencyService instance = new CurrencyService(RepositoryFactory.GetCurrencyRepository());
        private static IEnumerable<Currency> currencies = new List<Currency>();
        private static IEnumerable<FxPair> fxPairs = new List<FxPair>();


        
        #region INFRASTRUCTURE

        public static ICurrencyService Instance()
        {
            return instance;
        }

        public static ICurrencyService Instance(bool reset)
        {
            if (reset)
            {
                fxPairs = new List<FxPair>();
                currencies = new List<Currency>();
            }
            return instance;
        }

        private CurrencyService(ICurrencyRepository repository)
        {
            _repository = repository;
        }

        public void InjectRepository(ICurrencyRepository repository)
        {
            _repository = repository;
        }

        #endregion INFRASTRUCTURE



        #region CURRENCIES

        public IEnumerable<Currency> GetAllCurrencies()
        {
            List<Currency> result = new List<Currency>();
            var dtos = _repository.GetCurrencies();
            
            foreach (var dto in dtos)
            {
                Currency currency = currencies.SingleOrDefault(c => c.GetId() == dto.Id);
                if (currency == null)
                {
                    currency = Currency.FromDto(dto);
                    appendCurrency(currency);
                }
                result.Add(currency);
            }

            return result;

        }

        private Currency GetCurrency(Func<CurrencyDto> func, Currency currency)
        {
            if (currency == null)
            {
                CurrencyDto result = func();
                if (result != null)
                {
                    currency = Currency.FromDto(result);
                    appendCurrency(currency);
                }
            }
            return currency;
        }

        public Currency GetCurrencyById(int id)
        {
            var currency = currencies.SingleOrDefault(c => c.GetId() == id);
            return GetCurrency(delegate { return _repository.GetCurrencyById(id); }, currency);
        }

        public Currency GetCurrencyBySymbol(string symbol)
        {
            var currency = currencies.SingleOrDefault(c => c.GetSymbol() == symbol);
            return GetCurrency(delegate { return _repository.GetCurrencyBySymbol(symbol); }, currency);
        }

        public Currency GetCurrencyByName(string name)
        {
            var currency = currencies.SingleOrDefault(c => c.GetName() == name);
            return GetCurrency(delegate { return _repository.GetCurrencyByName(name); }, currency);
        }

        private void appendCurrency(Currency currency)
        {
            currencies = currencies.Concat(new[] { currency });
        }

        #endregion CURRENCIES



        #region CURRENCY_PAIRS

        public IEnumerable<FxPair> GetFxPairs(string filter, int limit)
        {
            var dtos = _repository.GetFxPairs(filter, limit);
            return GetFxPairs_processDtos(dtos);
        }

        public IEnumerable<FxPair> GetFxPairs()
        {
            var dtos = _repository.GetFxPairs();
            return GetFxPairs_processDtos(dtos);
        }

        private IEnumerable<FxPair> GetFxPairs_processDtos(IEnumerable<FxPairDto> dtos)
        {
            List<FxPair> result = new List<FxPair>();
            foreach (var dto in dtos)
            {
                FxPair pair = fxPairs.SingleOrDefault(p => p.GetId() == dto.Id);
                if (pair == null)
                {
                    pair = FxPair.FromDto(dto);
                    appendFxPair(pair);
                }
                result.Add(pair);
            }
            return result;
        }

        public FxPair GetFxPairById(int id)
        {
            var pair = fxPairs.SingleOrDefault(p => p.GetId() == id);
            if (pair == null)
            {
                var dto = _repository.GetFxPairById(id);
                if (dto != null)
                {
                    pair = FxPair.FromDto(dto);
                    appendFxPair(pair);
                }
            }
            return pair;
        }

        public FxPair GetFxPairBySymbol(string symbol)
        {
            var pair = fxPairs.SingleOrDefault(p => p.GetName() == symbol);
            if (pair == null)
            {
                var dto = _repository.GetFxPairBySymbol(symbol);
                if (dto != null)
                {
                    pair = FxPair.FromDto(dto);
                    appendFxPair(pair);
                }
            }
            return pair;
        }

        private void appendFxPair(FxPair fxPair)
        {
            fxPairs = fxPairs.Concat(new[] { fxPair });
        }

        #endregion CURRENCY_PAIRS


    }
}