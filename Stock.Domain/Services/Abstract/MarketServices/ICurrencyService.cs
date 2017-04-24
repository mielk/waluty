using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Repositories;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface ICurrencyService
    {
        void InjectRepository(ICurrencyRepository repository);
        IEnumerable<Currency> GetAllCurrencies();
        Currency GetCurrencyById(int id);
        Currency GetCurrencyByName(string name);
        Currency GetCurrencyBySymbol(string symbol);
    }
}