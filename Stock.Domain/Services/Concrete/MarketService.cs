using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Services
{
    public class MarketService : IMarketService
    {

        private readonly IMarketRepository _repository;

        public MarketService(IMarketRepository repository)
        {
            _repository = repository ?? RepositoryFactory.GetMarketRepository();
        }

        public IEnumerable<Market> GetMarkets()
        {
            var dtos = _repository.GetMarkets();
            return dtos.Select(Market.FromDto).ToList();
        }

    }
}
