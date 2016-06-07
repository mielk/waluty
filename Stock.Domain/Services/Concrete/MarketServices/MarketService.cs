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
                                                                        #endregion markets



                                                                        #region assets
        public IEnumerable<Asset> FilterAssets(string q, int limit)
        {
            var dtos = _repository.FilterAssets(q, limit);
            return dtos.Select(Asset.FromDto).ToList();
        }

        public Asset GetAsset(int id)
        {
            var dto = _repository.GetAsset(id);
            return Asset.FromDto(dto);
        }
                                                                        #endregion assets



                                                                        #region fx
        public IEnumerable<FxPair> FilterPairs(string q, int limit)
        {
            var dtos = _repository.FilterPairs(q, limit);
            return dtos.Select(FxPair.FromDto).ToList();
        }


        public FxPair GetPair(int id)
        {
            var dto = _repository.GetPair(id);
            return FxPair.FromDto(dto);
        }


        public FxPair GetPair(string symbol)
        {
            var dto = _repository.GetPair(symbol);
            return FxPair.FromDto(dto);
        }
                                                                        #endregion fx

    }
}
