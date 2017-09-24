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
        private IQuotationService quotationService;
        private IPriceService priceService;
        private DataSetsContainer container;


        #region INFRASTRUCTURE

        public DataSetService()
        {
            this.analysisRepository = RepositoryFactory.GetAnalysisRepository();
            this.quotationService = ServiceFactory.Instance().GetQuotationService();
            this.priceService = ServiceFactory.Instance().GetPriceService();
            setContainer(new DataSetsContainer());
        }

        public void injectAnalysisRepository(IAnalysisRepository repository)
        {
            this.analysisRepository = repository;
        }

        public void InjectQuotationService(IQuotationService service)
        {
            this.quotationService = service;
            container.SetQuotationService(service);
        }

        public void InjectPriceService(IPriceService service)
        {
            this.priceService = service;
            this.container.SetPriceService(service);
        }

        private void setContainer(DataSetsContainer container)
        {
            this.container = container;
            this.container.SetPriceService(priceService);
            this.container.SetQuotationService(quotationService);
        }

        #endregion INFRASTRUCTURE


        #region API

        public IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef)
        {
            return container.GetDataSets(queryDef);
        }

        public AnalysisInfo GetAnalysisInfo(AnalysisDataQueryDefinition queryDef, AnalysisType analysisType)
        {
            AnalysisInfoDto dto = analysisRepository.GetAnalysisInfoDto(queryDef, analysisType);
            return AnalysisInfo.FromDto(dto);
        }

        public IEnumerable<IDataUnit> GetUnits(AnalysisDataQueryDefinition queryDef)
        {
            return GetDataSets(queryDef);
        }

        public DataSet[] AppendAndReturnAsArray(IEnumerable<DataSet> sets, AnalysisDataQueryDefinition queryDef)
        {

            IEnumerable<DataSet> currentSet = GetDataSets(queryDef);
            int maxIndexOriginal = (currentSet == null ? 0 : (currentSet.Count() > 0 ? currentSet.Max(ds => (ds == null ? 0 : ds.IndexNumber)) : 0));
            int maxIndexAppended = (sets == null ? 0 : (sets.Count() > 0 ? sets.Max(ds => (ds == null ? 0 : ds.IndexNumber)) : 0));
            int maxIndex = Math.Max(maxIndexOriginal, maxIndexAppended);
            DataSet[] array = new DataSet[maxIndex + 1];

            if (sets != null)
            {
                foreach (var ds in sets)
                {
                    if (ds != null)
                    {
                        var indexNumber = ds.IndexNumber;
                        array[indexNumber] = ds;
                    }
                }
            }

            if (currentSet != null)
            {
                foreach (var ds in currentSet)
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
        
        public void UpdateDataSets(IEnumerable<DataSet> dataSets)
        {

            //Quotations
            if (quotationService != null)
            {
                IEnumerable<Quotation> quotations = dataSets.Select(ds => ds.GetQuotation());
                quotationService.UpdateQuotations(quotations);
            }

            //Prices
            if (priceService != null)
            {
                IEnumerable<Price> prices = dataSets.Select(ds => ds.GetPrice());
                priceService.UpdatePrices(prices);
            }

        }


        #endregion API



    }

}
