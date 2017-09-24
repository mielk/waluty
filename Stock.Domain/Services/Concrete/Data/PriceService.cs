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
        private AnalysisItemsContainer<Price> container;


        #region INFRASTRUCTURE

        public PriceService(IPriceRepository repository)
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
            appendExtrema(prices, queryDef);
            return prices;
        }

        private void appendExtrema(IEnumerable<Price> prices, AnalysisDataQueryDefinition baseQueryDef)
        {

            if (prices != null && prices.Count() > 0)
            {
                DateTime minDate = prices.Min(p => p.Date);
                DateTime maxDate = prices.Max(p => p.Date);
                AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(baseQueryDef.AssetId, baseQueryDef.TimeframeId) { StartDate = minDate, EndDate = maxDate };
                IEnumerable<ExtremumDto> dtos = _repository.GetExtrema(queryDef);
                foreach (var dto in dtos)
                {
                    Extremum extremum = Extremum.FromDto(dto);
                    Price price = prices.SingleOrDefault(p => p.AssetId == dto.AssetId && p.TimeframeId == dto.TimeframeId && p.Date.CompareTo(dto.Date) == 0);
                    if (price != null)
                    {
                        price.SetExtremum(extremum);
                    }
                }
            }

        }

        public IEnumerable<IDataUnit> GetUnits(AnalysisDataQueryDefinition queryDef)
        {
            return GetPrices(queryDef);
        }

        public void UpdatePrices(IEnumerable<Price> prices) 
        {
            
            IEnumerable<Price> updatedPrices = prices.Where(p => p.IsUpdated || p.IsNew);
            IEnumerable<PriceDto> priceDtos = updatedPrices.Select(p => p.ToDto());
            IEnumerable<Extremum> updatedExtrema = getUpdatedExtrema(prices);
            IEnumerable<ExtremumDto> extremumDtos = updatedExtrema.Select(p => p.ToDto());
            _repository.UpdatePrices(priceDtos);
            _repository.UpdateExtrema(extremumDtos);
        }

        private IEnumerable<Extremum> getUpdatedExtrema(IEnumerable<Price> prices)
        {
            List<Extremum> list = new List<Extremum>();
            foreach (var price in prices)
            {
                IEnumerable<Extremum> priceExtrema = price.GetExtrema();
                foreach (var extremum in priceExtrema)
                {
                    if (extremum.IsUpdated)
                    {
                        list.Add(extremum);
                    }
                }
            }
            return list;
        }


        #endregion API



    }

}
