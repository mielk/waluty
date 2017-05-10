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
using Stock.Core;

namespace Stock.Domain.Services
{
    public class PriceService : IPriceService
    {

        private IPriceRepository _repository;
        private static readonly IPriceService instance = new PriceService(RepositoryFactory.GetPriceRepository());
        private static AnalysisItemsContainer<Price> container;


        #region INFRASTRUCTURE

        public static IPriceService Instance()
        {
            return instance;
        }

        public static IPriceService Instance(bool reset)
        {
            if (reset)
            {
                container = new AnalysisItemsContainer<Price>(instance);
            }
            return instance;
        }

        private PriceService(IPriceRepository repository)
        {
            _repository = repository;
            container = new AnalysisItemsContainer<Price>(this);
        }

        public void InjectRepository(IPriceRepository repository)
        {
            _repository = repository;
        }

        #endregion INFRASTRUCTURE


        #region API

        public IDataUnit FromDto(IDataUnitDto dto)
        {
            PriceDto priceDto = (PriceDto)dto;
            return Price.FromDto(priceDto);
        }

        public IEnumerable<Price> GetPrices(AnalysisDataQueryDefinition queryDef) 
        {
            IEnumerable<PriceDto> dtos = _repository.GetPrices(queryDef);
            IEnumerable<Price> prices = container.ProcessDtoToItems(dtos, queryDef.AssetId, queryDef.TimeframeId);
            return prices;
        }

        public IEnumerable<IDataUnit> GetUnits(AnalysisDataQueryDefinition queryDef)
        {
            return GetPrices(queryDef);
        }

        public void UpdatePrices(IEnumerable<Price> prices) 
        {
            _repository.UpdatePrices(prices.Select(p => p.ToDto()));
        }


        #endregion API



    }

}
