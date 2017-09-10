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
        private IQuotationService quotationService;
        private IPriceService priceService;
        private DataSetsContainer container;


        #region INFRASTRUCTURE

        public DataSetService()
        {
            this.quotationService = ServiceFactory.GetQuotationService();
            this.priceService = ServiceFactory.GetPriceService();
            setContainer(new DataSetsContainer());
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

        public IEnumerable<IDataUnit> GetUnits(AnalysisDataQueryDefinition queryDef)
        {
            return GetDataSets(queryDef);
        }

        public DataSet[] AppendAndReturnAsArray(IEnumerable<DataSet> sets, AnalysisDataQueryDefinition queryDef)
        {

            IEnumerable<DataSet> currentSet = GetDataSets(queryDef);
            int maxIndexOriginal = (currentSet.Count() > 0 ? currentSet.Max(ds => (ds == null ? 0 : ds.IndexNumber)) : 0);
            int maxIndexAppended = (sets.Count() > 0 ? sets.Max(ds => (ds == null ? 0 : ds.IndexNumber)) : 0);
            int maxIndex = Math.Max(maxIndexOriginal, maxIndexAppended);
            DataSet[] array = new DataSet[maxIndex + 1];

            foreach (var ds in sets)
            {
                if (ds != null)
                {
                    var indexNumber = ds.IndexNumber;
                    array[indexNumber] = ds;
                }
            }

            foreach (var ds in currentSet)
            {
                if (ds != null)
                {
                    var indexNumber = ds.IndexNumber;
                    array[indexNumber] = ds;
                }
            }

            return array;
        }



        //Aktualnie zawsze zwraca najwyższy i najniższy poziom dla notowań. Zrobić, żeby podawać jako argument dla jakiego rodzaju analizy ma to wyliczać.

        public DataSetInfo GetDataSetInfo(AnalysisDataQueryDefinition queryDef, AnalysisType analysisType)
        {
            DataSetInfo info = new DataSetInfo();
            IEnumerable<Quotation> quotations = quotationService.GetQuotations(queryDef);
            info.StartDate = quotations.Select(q => q.Date).Min();
            info.EndDate = quotations.Select(q => q.Date).Max();
            info.MinLevel = quotations.Select(q => q.Low).Min();
            info.MaxLevel = quotations.Select(q => q.High).Max();
            info.Counter = quotations.Count();
            return info;
        }
        
        public Dictionary<AnalysisType, DataSetInfo> GetDataSetInfos(AnalysisDataQueryDefinition queryDef)
        {
            Dictionary<AnalysisType, DataSetInfo> infos = new Dictionary<AnalysisType, DataSetInfo>();
            foreach(AnalysisType type in queryDef.AnalysisTypes){
                DataSetInfo dsi = GetDataSetInfo(queryDef, type);
                infos.Add(type, dsi);
            }
            return infos;
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
