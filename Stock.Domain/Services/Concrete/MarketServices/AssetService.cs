using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Infrastructure;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using Stock.Domain.Entities;
using System.Linq.Expressions;

namespace Stock.Domain.Services
{
    public class AssetService : IAssetService
    {

        private IAssetRepository _repository;
        private IEnumerable<Asset> assets = new List<Asset>();


        #region INFRASTRUCTURE

        public AssetService(IAssetRepository repository)
        {
            _repository = repository;
        }

        public void InjectRepository(IAssetRepository repository)
        {
            _repository = repository;
        }

        #endregion INFRASTRUCTURE


        #region ASSETS

        public IEnumerable<Asset> GetAssets(string filter, int limit)
        {
            var dtos = _repository.GetAssets(filter, limit);
            return GetAssets(dtos);
        }

        public IEnumerable<Asset> GetAllAssets()
        {
            var dtos = _repository.GetAllAssets();
            return GetAssets(dtos);
        }

        public IEnumerable<Asset> GetAssetsForMarket(int marketId)
        {
            var dtos = _repository.GetAssetsForMarket(marketId);
            return GetAssets(dtos);
        }

        private IEnumerable<Asset> GetAssets(IEnumerable<AssetDto> dtos)
        {
            List<Asset> result = new List<Asset>();
            foreach (var dto in dtos)
            {
                Asset asset = assets.SingleOrDefault(a => a.GetId() == dto.Id);
                if (asset == null)
                {
                    asset = Asset.FromDto(dto);
                    appendAsset(asset);
                }
                result.Add(asset);
            }
            return result;
        }

        public Asset GetAssetById(int id)
        {
            var asset = assets.SingleOrDefault(a => a.GetId() == id);
            if (asset == null)
            {
                var dto = _repository.GetAssetById(id);
                if (dto != null)
                {
                    asset = Asset.FromDto(dto);
                    appendAsset(asset);
                }
            }

            return asset;

        }

        public Asset GetAssetBySymbol(string symbol)
        {
            var asset = assets.SingleOrDefault(a => a.GetSymbol().Equals(symbol, StringComparison.CurrentCultureIgnoreCase));
            if (asset == null)
            {
                var dto = _repository.GetAssetBySymbol(symbol);
                if (dto != null)
                {
                    asset = Asset.FromDto(dto);
                    appendAsset(asset);
                }
            }
            return asset;
        }

        private void appendAsset(Asset asset)
        {
            assets = assets.Concat(new[] { asset });
        }

        #endregion ASSETS


    }

}