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

    public class DataSetService : IDataSetService
    {
        private IAnalysisRepository analysisRepository;
        private IQuotationRepository quotationRepository;
        private IPriceRepository priceRepository;
        private IEnumerable<DataSetsContainer> containers = new List<DataSetsContainer>();


        #region INFRASTRUCTURE

        public DataSetService()
        {
            this.analysisRepository = RepositoryFactory.GetAnalysisRepository();
            this.quotationRepository = RepositoryFactory.GetQuotationRepository();
            this.priceRepository = RepositoryFactory.GetPriceRepository();
        }

        public void InjectAnalysisRepository(IAnalysisRepository repository)
        {
            this.analysisRepository = repository;
        }

        public void InjectQuotationRepository(IQuotationRepository repository)
        {
            this.quotationRepository = repository;
        }

        public void InjectPriceRepository(IPriceRepository repository)
        {
            this.priceRepository = repository;
        }

        #endregion INFRASTRUCTURE



        #region API

        public IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef)
        {
            
            DataSetsContainer dsc = containers.SingleOrDefault(c => c.AssetId == queryDef.AssetId && c.TimeframeId == queryDef.TimeframeId && c.SimulationId == queryDef.SimulationId);
            if (dsc == null)
            {
                dsc = new DataSetsContainer(queryDef.AssetId, queryDef.TimeframeId, queryDef.SimulationId);
                containers = containers.Concat(new DataSetsContainer[] { dsc } );
            }

            //Quotations.
            IEnumerable<QuotationDto> quotationDtos = quotationRepository.GetQuotations(queryDef);
            dsc.LoadQuotations(quotationDtos);

            //Prices.
            if (queryDef.AnalysisTypes.Contains(AnalysisType.Prices))
            {
                IEnumerable<PriceDto> priceDtos = priceRepository.GetPrices(queryDef);
                dsc.LoadPrices(priceDtos);
            }

            return dsc.GetDataSets(queryDef);

        }

        public IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef, IEnumerable<DataSet> initialSets)
        {
            IEnumerable<DataSet> currentSets = GetDataSets(queryDef);
            int maxIndexOriginal = (currentSets == null ? 0 : (currentSets.Count() > 0 ? currentSets.Max(ds => (ds == null ? 0 : ds.IndexNumber)) : 0));
            int maxIndexAppended = (initialSets == null ? 0 : (initialSets.Count() > 0 ? initialSets.Max(ds => (ds == null ? 0 : ds.IndexNumber)) : 0));
            int maxIndex = Math.Max(maxIndexOriginal, maxIndexAppended);
            DataSet[] array = new DataSet[maxIndex + 1];

            if (initialSets != null)
            {
                foreach (var ds in initialSets)
                {
                    if (ds != null)
                    {
                        var indexNumber = ds.IndexNumber;
                        array[indexNumber] = ds;
                    }
                }
            }

            if (currentSets != null)
            {
                foreach (var ds in currentSets)
                {
                    if (ds != null)
                    {
                        var indexNumber = ds.IndexNumber;
                        array[indexNumber] = ds;
                    }
                }
            }

            return array;

        }

        public AnalysisInfo GetAnalysisInfo(AnalysisDataQueryDefinition queryDef, AnalysisType analysisType)
        {
            AnalysisInfoDto dto = analysisRepository.GetAnalysisInfoDto(queryDef, analysisType);
            return AnalysisInfo.FromDto(dto);
        }

        public void UpdateDataSets(IEnumerable<DataSet> dataSets)
        {

            //Quotations
            if (quotationRepository != null)
            {
                IEnumerable<QuotationDto> quotationDtos = dataSets.Select(ds => ds.GetQuotation()).Where(q => q.IsUpdated() || q.IsNew()).Select(q => q.ToDto());
                quotationRepository.UpdateQuotations(quotationDtos);
            }

            //Prices
            if (priceRepository != null)
            {
                IEnumerable<PriceDto> priceDtos = dataSets.Select(ds => ds.GetPrice()).Where(p => p.IsUpdated() || p.IsNew()).Select(p => p.ToDto());
                priceRepository.UpdatePrices(priceDtos);
            }

        }


        #endregion API


    }

}