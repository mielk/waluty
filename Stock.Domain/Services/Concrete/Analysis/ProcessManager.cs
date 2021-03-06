﻿using Stock.Core;
using Stock.Domain.Entities;
using Stock.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class ProcessManager : IProcessManager
    {

        public int AssetId { get; set; }
        protected int TimeframeId { get; set; }
        protected AnalysisDataQueryDefinition queryDef;
        protected IDataSetService dataSetService;
        protected IAnalysisTimestampService timestampService;
        protected DataSet[] dataSetsArray;
        protected Dictionary<AnalysisType, int?> lastIndexes;
        protected IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices , AnalysisType.Trendlines };
        private Dictionary<AnalysisType, IAnalysisProcessController> controllers = new Dictionary<AnalysisType, IAnalysisProcessController>();


        #region CONSTRUCTOR

        public ProcessManager(int assetId, int timeframeId)
        {
            this.AssetId = assetId;
            this.TimeframeId = timeframeId;
            this.dataSetService = ServiceFactory.Instance().GetDataSetService();
            this.timestampService = ServiceFactory.Instance().GetAnalysisTimestampService();
        }

        public ProcessManager(int assetId, int timeframeId, IDataSetService dataSetService)
        {
            this.AssetId = assetId;
            this.TimeframeId = timeframeId;
            this.dataSetService = dataSetService;
            this.timestampService = ServiceFactory.Instance().GetAnalysisTimestampService();
        }

        public ProcessManager(int assetId, int timeframeId, IDataSetService dataSetService, IAnalysisTimestampService timestampService)
        {
            this.AssetId = assetId;
            this.TimeframeId = timeframeId;
            this.dataSetService = dataSetService;
            this.timestampService = timestampService;
        }

        public void InjectDataSetService(IDataSetService service)
        {
            this.dataSetService = service;
        }

        public void InjectTimestampService(IAnalysisTimestampService service)
        {
            this.timestampService = service;
        }

        #endregion CONSTRUCTOR


        #region SIMULATION

        public int GetSimulationId()
        {
            return 0;
        }

        #endregion SIMULATION


        #region GETTERS

        public AtsSettings GetSettings()
        {
            return new AtsSettings(AssetId, TimeframeId, GetSimulationId());
        }

        public int GetAssetId()
        {
            return AssetId;
        }

        public int GetTimeframeId()
        {
            return TimeframeId;
        }

        #endregion GETTERS


        #region SETTINGS

        public void changeAsset(int assetId)
        {
        }

        public void changeTimeframe(int timeframeId)
        {
        }

        #endregion SETTINGS


        #region UPDATING DATA SETS

        protected void loadDataSets(AnalysisDataQueryDefinition queryDef)
        {
            queryDef.AnalysisTypes = analysisTypes;
            this.dataSetsArray = dataSetService.GetDataSets(queryDef, this.dataSetsArray).ToArray();
        }

        protected void loadDataSets(int initialIndex)
        {
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(AssetId, TimeframeId) { StartIndex = initialIndex,  AnalysisTypes = analysisTypes };
            loadDataSets(queryDef);
        }

        protected void loadDataSets(int initialIndex, int endIndex)
        {
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(AssetId, TimeframeId) { StartIndex = initialIndex, EndIndex = endIndex };
            loadDataSets(queryDef);
        }

        protected void loadDataSets()
        {
            loadDataSets(0);
        }

        protected void loadLastIndexes()
        {
            this.lastIndexes = timestampService.GetLastAnalyzedIndexes(AssetId, TimeframeId, GetSimulationId());
            int? lastQuotationIndex = GetAnalysisLastUpdatedIndex(AnalysisType.Quotations);
            if (lastQuotationIndex != null)
            {
                this.dataSetsArray = new DataSet[(int)lastQuotationIndex];
            }
        }

        protected void updateLastIndex(AnalysisType type, int index)
        {
            if (lastIndexes.ContainsKey(type))
            {
                lastIndexes[type] = index;
            }
            else
            {
                lastIndexes.Add(type, index);
            }
        }

        #endregion UPDATING DATA SETS



        public void Run()
        {
            loadLastIndexes();
            loadDataSets();
            runAllAnalysisTypes();
        }

        protected void runAllAnalysisTypes()
        {
            foreach (AnalysisType type in analysisTypes)
            {

                IAnalysisProcessController processController = getAnalysisProcessController(type);
                processController.Run(this);
            }
        }

        private IAnalysisProcessController getAnalysisProcessController(AnalysisType type)
        {
            IAnalysisProcessController controller = null;
            try
            {
                controllers.TryGetValue(type, out controller);
            }
            catch (Exception ex)
            {

            }

            if (controller == null)
            {
                controller = ProcessorFactory.Instance().GetProperAnalysisProcessController(type);
                this.controllers.Add(type, controller);
            }

            return controller;

        }




        #region ACCESS TO DATA SETS

        public DataSet GetDataSet(int index)
        {

            if (index <= 0)
            {
                return null;
            } 
            else if (index >= dataSetsArray.Length)
            {
                return null;
            }
            else
            {
                //if (dataSetsArray[index] == null)
                //{
                //    loadDataSets(index, index);
                //}
                return dataSetsArray[index];
            }
        }

        public IEnumerable<DataSet> GetDataSets()
        {
            return dataSetsArray;
        }

        public IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef)
        {
            loadDataSets(queryDef);
            var result = dataSetsArray.ToArray();
            if (queryDef.StartDate != null)
            {
                result = result.Where(q => q != null && q.Date.CompareTo(queryDef.StartDate) >= 0).ToArray();
            }
            if (queryDef.EndDate != null)
            {
                result = result.Where(q => q != null && q.Date.CompareTo(queryDef.EndDate) <= 0).ToArray();
            }
            if (queryDef.StartIndex != null)
            {
                result = result.Where(q => q != null && q.IndexNumber >= (int)queryDef.StartIndex).ToArray();
            }
            if (queryDef.EndIndex != null)
            {
                result = result.Where(q => q != null && q.IndexNumber <= (int)queryDef.EndIndex).ToArray();
            }
            return result;

        }

        public int GetDataSetIndex(DateTime? datetime)
        {
            for (int i = 0; i < dataSetsArray.Length; i++)
            {
                DataSet ds = dataSetsArray[i];
                if (ds != null)
                {
                    if (datetime != null){
                    }
                    if (ds.Date.CompareTo((DateTime)datetime) == 0)
                    {
                        return ds.IndexNumber;
                    }
                }
            }
            return 1;
        }

        public int? GetAnalysisLastUpdatedIndex(AnalysisType type)
        {

            if (type == AnalysisType.Quotations)
            {
                int? result = null;
                if (this.dataSetsArray != null) result = this.dataSetsArray.Length - 1;
                return result;
            } 

            if (lastIndexes == null) loadLastIndexes();

            int? index;
            try
            {
                lastIndexes.TryGetValue(type, out index);
                return index;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public AnalysisInfo GetAnalysisInfo()
        {
            if (dataSetsArray == null || dataSetsArray.Length == 0){
                return null;
            } 
            else 
            {
                IEnumerable<DataSet> notNulls = dataSetsArray.Where(ds => ds != null);
                if (notNulls.Count() > 0)
                {
                    return new AnalysisInfo()
                    {
                        StartDate = notNulls.Min(ds => ds.Date),
                        EndDate = notNulls.Max(ds => ds.Date),
                        StartIndex = 1,
                        EndIndex = dataSetsArray.Length,
                        Counter = dataSetsArray.Length - 1,
                        MinLevel = notNulls.Where(ds => ds.quotation != null).Min(ds => ds.quotation.Low),
                        MaxLevel = notNulls.Where(ds => ds.quotation != null).Max(ds => ds.quotation.High)
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<Trendline> GetTrendlines()
        {
            TrendlineProcessController processController = (TrendlineProcessController)getAnalysisProcessController(AnalysisType.Trendlines);
            if (processController != null)
            {
                return processController.GetTrendlines();
            }
            return new List<Trendline>();
        }

        #endregion ACCESS TO DATA SETS



    }

}