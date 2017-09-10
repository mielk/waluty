using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;

namespace Stock.Domain.Services
{
    public interface IMarketService
    {

        //Infrastructure.
        void InjectRepository(IMarketRepository repository);

        //Markets.
        IEnumerable<Market> GetMarkets();
        Market GetMarketById(int id);
        Market GetMarketByName(string name);
        Market GetMarketBySymbol(string symbol);

    }
}
