using Stock.DAL.Infrastructure;
using Stock.DAL.Repositories;
using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class CurrencyService : ICurrencyService
    {
        private ICurrencyRepository _repository;
        private static readonly ICurrencyService instance = new CurrencyService(RepositoryFactory.GetCurrencyRepository());
        private static IEnumerable<Currency> currencies = new List<Currency>();


        #region INFRASTRUCTURE

        public static ICurrencyService Instance()
        {
            return instance;
        }

        public static ICurrencyService Instance(bool reset)
        {
            currencies = new List<Currency>();
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


        public IEnumerable<Currency> GetAllCurrencies()
        {
            List<Currency> result = new List<Currency>();
            var dtos = _repository.GetCurrencies();
            
            foreach (var dto in dtos)
            {
                Currency currency = currencies.SingleOrDefault(c => c.Id == dto.Id);
                if (currency == null)
                {
                    currency = Currency.FromDto(dto);
                }
                result.Add(currency);
            }

            return result;

        }

        public Currency GetCurrencyById(int id)
        {

            var currency = currencies.SingleOrDefault(c => c.Id == id);
            if (currency == null)
            {
                var dto = _repository.GetCurrencyById(id);
                if (dto != null)
                {
                    currency = Currency.FromDto(dto);
                    currencies = currencies.Concat(new[] { currency });
                }
            }

            return currency;

        }

        public Currency GetCurrencyByName(string name)
        {

            var currency = currencies.SingleOrDefault(c => c.Name == name);
            if (currency == null)
            {
                var dto = _repository.GetCurrencyByName(name);
                if (dto != null)
                {
                    currency = Currency.FromDto(dto);
                    currencies = currencies.Concat(new[] { currency });
                }
            }

            return currency;

        }

        public Currency GetCurrencyBySymbol(string symbol)
        {

            var currency = currencies.SingleOrDefault(c => c.Symbol == symbol);
            if (currency == null)
            {
                var dto = _repository.GetCurrencyBySymbol(symbol);
                if (dto != null)
                {
                    currency = Currency.FromDto(dto);
                    currencies = currencies.Concat(new[] { currency });
                }
            }

            return currency;

        }

    }
}