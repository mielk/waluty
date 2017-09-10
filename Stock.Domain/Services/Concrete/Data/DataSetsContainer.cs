using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;
using Stock.Core;

namespace Stock.Domain.Services
{
    public class DataSetsContainer
    {

        private IQuotationService quotationService;
        private IPriceService priceService;
        private Dictionary<int, AssetItemsContainer> dataSetsByAssets = new Dictionary<int, AssetItemsContainer>();


        #region CONSTRUCTOR

        public DataSetsContainer()
        {
            this.quotationService = ServiceFactory.Instance().GetQuotationService();
            this.priceService = ServiceFactory.Instance().GetPriceService();
        }

        #endregion CONSTRUCTOR


        #region SETTERS

        public void SetQuotationService(IQuotationService service)
        {
            this.quotationService = service;
        }

        public void SetPriceService(IPriceService service)
        {
            this.priceService = service;
        }

        #endregion SETTERS


        #region API

        public IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef)
        {
            AssetItemsContainer assetsContainer = getOrCreateAssetContainer(queryDef.AssetId);
            TimeframeItemsContainer timeframeContainer = assetsContainer.GetOrCreateTimeframeContainer(queryDef.TimeframeId);
            return timeframeContainer.GetDataSets(queryDef, getServices());
        }

        private AssetItemsContainer getOrCreateAssetContainer(int assetId)
        {
            AssetItemsContainer assetContainer = null;
            try
            {
                dataSetsByAssets.TryGetValue(assetId, out assetContainer);
            }
            catch (Exception ex) { }

            if (assetContainer == null)
            {
                assetContainer = new AssetItemsContainer(assetId);
                dataSetsByAssets.Add(assetId, assetContainer);
            }
            return assetContainer;
        }

        #endregion API


        private Dictionary<AnalysisType, IDataAccessService> getServices()
        {
            Dictionary<AnalysisType, IDataAccessService> dict = new Dictionary<AnalysisType, IDataAccessService>();
            dict.Add(AnalysisType.Quotations, quotationService);
            dict.Add(AnalysisType.Prices, priceService);
            return dict;
        }


        private class AssetItemsContainer
        {
            private int assetId;
            private Dictionary<int, TimeframeItemsContainer> dataSetsByTimeframes;

            public AssetItemsContainer(int assetId)
            {
                this.assetId = assetId;
                this.dataSetsByTimeframes = new Dictionary<int, TimeframeItemsContainer>();
            }

            public int GetId()
            {
                return assetId;
            }

            public TimeframeItemsContainer GetOrCreateTimeframeContainer(int timeframeId)
            {
                TimeframeItemsContainer timeframeContainer = null;
                try
                {
                    dataSetsByTimeframes.TryGetValue(timeframeId, out timeframeContainer);
                }
                catch (Exception ex) { }

                if (timeframeContainer == null)
                {
                    timeframeContainer = new TimeframeItemsContainer(timeframeId);
                    dataSetsByTimeframes.Add(timeframeId, timeframeContainer);
                }
                return timeframeContainer;
            }

        }


        private class TimeframeItemsContainer
        {
            private int timeframeId;
            private List<DataSet> items;

            public TimeframeItemsContainer(int timeframeId)
            {
                this.timeframeId = timeframeId;
                this.items = new List<DataSet>();
            }

            public int GetId()
            {
                return timeframeId;
            }

            private IEnumerable<AnalysisType> getDefaultAnalysisTypesCollection()
            {
                return new AnalysisType[] { AnalysisType.Quotations };
            }

            private IEnumerable<AnalysisType> getGivenCollectionIfNotEmptyOrDefaultCollectionOtherwise(IEnumerable<AnalysisType> types)
            {
                if (types == null || types.Count() == 0)
                {
                    types = getDefaultAnalysisTypesCollection();
                }
                return types;
            }

            public IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef, Dictionary<AnalysisType, IDataAccessService> services)
            {
                List<DataSet> result = new List<DataSet>();
                IEnumerable<AnalysisType> analysisTypes = getGivenCollectionIfNotEmptyOrDefaultCollectionOtherwise(queryDef.AnalysisTypes);
                
                //Quotations.
                IDataAccessService quotationService = getDataAccessService(services, AnalysisType.Quotations);
                IEnumerable<Quotation> quotations = quotationService.GetUnits(queryDef).Select(q => (Quotation)q).ToList();

                foreach (var quotation in quotations)
                {
                    DataSet dataSet = items.SingleOrDefault(ds => ds.IndexNumber == quotation.IndexNumber);
                    if (dataSet == null)
                    {
                        dataSet = new DataSet(quotation);
                        items.Add(dataSet);
                    }
                    else
                    {
                        dataSet.SetQuotation(quotation);
                    }

                    result.Add(dataSet);

                }


                foreach (var analysisType in analysisTypes)
                {
                    if (analysisType != AnalysisType.Quotations)
                    {
                        IDataAccessService service = getDataAccessService(services, analysisType);
                        appendDataForSpecificAnalysisType(result, service, analysisType, queryDef);
                    }
                }

                return result;
            
            }

            private void appendDataForSpecificAnalysisType(IEnumerable<DataSet> dataSets, IDataAccessService service, AnalysisType analysisType, AnalysisDataQueryDefinition queryDef)
            {
                if (service == null) throw new Exception(string.Format("Service for analysis type {0} not found", analysisType));

                IEnumerable<IDataUnit> units = service.GetUnits(queryDef).ToList();
                foreach (var unit in units)
                {
                    DataSet dataSet = dataSets.SingleOrDefault(ds => ds.IndexNumber == unit.GetIndexNumber());
                    if (dataSet != null)
                    {
                        dataSet.SetObject(analysisType, unit);
                    }
                }
            }

            private IDataAccessService getDataAccessService(Dictionary<AnalysisType, IDataAccessService> dict, AnalysisType type)
            {
                IDataAccessService service;
                try
                {
                    dict.TryGetValue(type, out service);
                }
                catch (Exception ex)
                {
                    throw new Exception("Missing service for analysis type: " + type.ToString());
                }
                return service;
            }

        }


    }
}
