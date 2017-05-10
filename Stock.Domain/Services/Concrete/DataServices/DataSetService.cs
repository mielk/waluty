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
        private static readonly DataSetService instance = new DataSetService();
        private IQuotationService quotationService;
        private IPriceService priceService;
        private DataSetsContainer container;



        #region INFRASTRUCTURE


        public static DataSetService Instance()
        {
            return instance;
        }

        public static DataSetService Instance(bool reset)
        {
            if (reset)
            {
                instance.ResetContainer();
            }
            return instance;
        }

        public void ResetContainer()
        {
            container = new DataSetsContainer();
        }

        private DataSetService()
        {
            container = new DataSetsContainer();
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

        public void UpdateDataSets(IEnumerable<DataSet> prices)
        {
            
        }


        #endregion API



    }

}
