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
        private static IEnumerable<Market> markets = new List<Market>();


        #region INFRASTRUCTURE

        public static IMarketService Instance()
        {
            return instance;
        }

        public static IMarketService Instance(bool reset)
        {
            if (reset)
            {
                markets = new List<Market>();
            }
            return instance;
        }

        private MarketService(IMarketRepository repository)
        {
            _repository = repository;
        }

        public void InjectRepository(IMarketRepository repository)
        {
            _repository = repository;
        }

        #endregion INFRASTRUCTURE


        #region MARKETS

        public IEnumerable<Market> GetMarkets()
        {
            var dtos = _repository.GetMarkets();
            return GetMarkets(dtos);
        }

        private IEnumerable<Market> GetMarkets(IEnumerable<MarketDto> dtos)
        {
            List<Market> result = new List<Market>();
            foreach (var dto in dtos)
            {
                Market market = markets.SingleOrDefault(m => m.GetId() == dto.Id);
                if (market == null)
                {
                    market = Market.FromDto(dto);
                    appendMarket(market);
                }
                result.Add(market);
            }
            return result;
        }

        public Market GetMarketById(int id)
        {
            var market = markets.SingleOrDefault(m => m.GetId() == id);
            if (market == null)
            {
                var dto = _repository.GetMarketById(id);
                if (dto != null)
                {
                    market = Market.FromDto(dto);
                    appendMarket(market);
                }
            }
            return market;
        }

        public Market GetMarketByName(string name)
        {
            var market = markets.SingleOrDefault(a => a.GetName().Equals(name, StringComparison.CurrentCultureIgnoreCase));
            if (market == null)
            {
                var dto = _repository.GetMarketByName(name);
                if (dto != null)
                {
                    market = Market.FromDto(dto);
                    appendMarket(market);
                }
            }
            return market;
        }

        public Market GetMarketBySymbol(string symbol)
        {
            var market = markets.SingleOrDefault(a => a.GetSymbol().Equals(symbol, StringComparison.CurrentCultureIgnoreCase));
            if (market == null)
            {
                var dto = _repository.GetMarketBySymbol(symbol);
                if (dto != null)
                {
                    market = Market.FromDto(dto);
                    appendMarket(market);
                }
            }
            return market;
        }

        private void appendMarket(Market market)
        {
            markets = markets.Concat(new[] { market });
        }

        #endregion MARKETS


    }

}