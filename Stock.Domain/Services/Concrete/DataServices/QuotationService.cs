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
    public class QuotationService : IQuotationService
    {

        private IQuotationRepository _repository;
        private static readonly IQuotationService instance = new QuotationService(RepositoryFactory.GetQuotationRepository());
        private static AnalysisItemsContainer<Quotation> container;


        #region INFRASTRUCTURE

        public static IQuotationService Instance()
        {
            return instance;
        }

        public static IQuotationService Instance(bool reset)
        {
            if (reset)
            {
                container = new AnalysisItemsContainer<Quotation>(instance);
            }
            return instance;
        }

        private QuotationService(IQuotationRepository repository)
        {
            _repository = repository;
            container = new AnalysisItemsContainer<Quotation>(this);
        }

        public void InjectRepository(IQuotationRepository repository)
        {
            _repository = repository;
        }

        #endregion INFRASTRUCTURE


        #region API

        public IDataUnit FromDto(IDataUnitDto dto)
        {
            QuotationDto quotationDto = (QuotationDto) dto;
            return Quotation.FromDto(quotationDto);
        }

        public IEnumerable<Quotation> GetQuotations(AnalysisDataQueryDefinition queryDef) 
        {
            IEnumerable<QuotationDto> dtos = _repository.GetQuotations(queryDef);
            IEnumerable<Quotation> quotations = container.ProcessDtoToItems(dtos, queryDef.AssetId, queryDef.TimeframeId);
            return quotations;
        }

        public IEnumerable<IDataUnit> GetUnits(AnalysisDataQueryDefinition queryDef)
        {
            return GetQuotations(queryDef);
        }

        public void UpdateQuotations(IEnumerable<Quotation> quotations) 
        { 

        }


        #endregion API



    }

}
