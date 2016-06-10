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



                                                                        #region assets
        public IEnumerable<Asset> FilterAssets(string q, int limit)
        {
            var dtos = _repository.FilterAssets(q, limit);
            return dtos.Select(Asset.FromDto).ToList();
        }

        public IEnumerable<Asset> GetAllAssets()
        {
            var dtos = _repository.GetAllAssets();
            return dtos.Select(Asset.FromDto).ToList();
        }

        public IEnumerable<Asset> GetAssetsForMarket(int marketId)
        {
            var dtos = _repository.GetAssetsForMarket(marketId);
            return dtos.Select(Asset.FromDto).ToList();
        }

        public Asset GetAsset(int id)
        {
            var dto = _repository.GetAsset(id);
            return Asset.FromDto(dto);
        }

        public Asset GetAssetByName(string name)
        {
            var dto = _repository.GetAssetByName(name);
            return Asset.FromDto(dto);
        }

        public Asset GetAssetBySymbol(string symbol)
        {
            var dto = _repository.GetAssetBySymbol(symbol);
            return Asset.FromDto(dto);
        }

                                                                        #endregion assets



                                                                        #region fx
        public IEnumerable<FxPair> FilterPairs(string q, int limit)
        {
            var dtos = _repository.FilterPairs(q, limit);
            return dtos.Select(FxPair.FromDto).ToList();
        }

        public IEnumerable<FxPair> GetFxPairs()
        {
            var dtos = _repository.GetFxPairs();
            return dtos.Select(FxPair.FromDto).ToList();
        }

        public FxPair GetFxPair(int id)
        {
            var dto = _repository.GetFxPair(id);
            return FxPair.FromDto(dto);
        }


        public FxPair GetFxPair(string symbol)
        {
            var dto = _repository.GetFxPair(symbol);
            return FxPair.FromDto(dto);
        }
                                                                        #endregion fx



                                                                        #region currencies
        public IEnumerable<Currency> GetCurrencies()
        {
            var dtos = _repository.GetCurrencies();
            return dtos.Select(Currency.FromDto).ToList();
        }

        public Currency GetCurrencyById(int id)
        {
            var dto = _repository.GetCurrencyById(id);
            return Currency.FromDto(dto);
        }

        public Currency GetCurrencyByName(string name)
        {
            var dto = _repository.GetCurrencyByName(name);
            return Currency.FromDto(dto);
        }

        public Currency GetCurrencyBySymbol(string symbol)
        {
            var dto = _repository.GetCurrencyBySymbol(symbol);
            return Currency.FromDto(dto);
        }
                                                                        #endregion currencies


    }
}
