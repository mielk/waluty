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

        private IMarketRepository _repository;
        private static readonly MarketService instance = new MarketService(RepositoryFactory.GetMarketRepository());


        public static MarketService Instance()
        {
            return instance;
        }
        private MarketService(IMarketRepository repository)
        {
            _repository = repository;
        }


        public void injectRepository(IMarketRepository repository)
        {
            _repository = repository;
        }


                                                                        #region markets
        public IEnumerable<Market> GetMarkets()
        {
            var dtos = _repository.GetMarkets();
            return dtos.Select(Market.FromDto).ToList();
        }

        public Market GetMarketById(int id)
        {
            var dto = _repository.GetMarketById(id);
            return Market.FromDto(dto);
        }

        public Market GetMarketByName(string name)
        {
            var dto = _repository.GetMarketByName(name);
            return Market.FromDto(dto);
        }

        public Market GetMarketBySymbol(string symbol)
        {
            var dto = _repository.GetMarketBySymbol(symbol);
            return Market.FromDto(dto);
        }
                                                                        #endregion markets


    }
}
