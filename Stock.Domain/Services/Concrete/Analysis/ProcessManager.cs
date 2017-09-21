using Stock.Core;
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

        private Simulation simulation;
        private AnalysisDataQueryDefinition queryDef;
        private IDataSetService dataSetService;
        private IAnalysisTimestampService timestampService;
        private DataSet[] dataSetsArray;
        private Dictionary<AnalysisType, int?> lastIndexes;



        #region CONSTRUCTOR

        public ProcessManager(Simulation simulation)
        {
            this.dataSetService = ServiceFactory.Instance().GetDataSetService();
            this.timestampService = ServiceFactory.Instance().GetAnalysisTimestampService();
            setSimulation(simulation);
        }

        public ProcessManager(IDataSetService dataSetService, Simulation simulation)
        {
            this.dataSetService = dataSetService;
            this.timestampService = ServiceFactory.Instance().GetAnalysisTimestampService();
            setSimulation(simulation);
        }

        public ProcessManager(IDataSetService dataSetService, IAnalysisTimestampService timestampService, Simulation simulation)
        {
            this.dataSetService = dataSetService;
            this.timestampService = timestampService;
            setSimulation(simulation);
        }

        private void setSimulation(Simulation simulation)
        {
            this.simulation = simulation;
            this.queryDef = new AnalysisDataQueryDefinition(simulation.AssetId, simulation.TimeframeId) { SimulationId = simulation.Id, AnalysisTypes = simulation.GetAnalysisTypes() };
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
        
        public Simulation GetSimulation()
        {
            return simulation;
        }

        public int GetSimulationId()
        {
            return simulation == null ? 0 : simulation.Id;
        }

        #endregion SIMULATION



        #region UPDATING DATA SETS

        private void loadLastIndexes()
        {
            if (this.lastIndexes == null)
            {
                this.lastIndexes = timestampService.GetLastAnalyzedIndexes(simulation.Id);
                int? lastQuotationIndex = GetAnalysisLastUpdatedIndex(AnalysisType.Quotations);
                if (lastQuotationIndex != null)
                {
                    this.dataSetsArray = new DataSet[(int)lastQuotationIndex];
                }
            }
        }

        public void loadDataSets(int initialIndex)
        {
            loadLastIndexes();
            AnalysisDataQueryDefinition _queryDef = queryDef.Clone();
            _queryDef.StartIndex = initialIndex;
            this.dataSetsArray = dataSetService.AppendAndReturnAsArray(this.dataSetsArray, queryDef);
        }

        public void loadDataSets(AnalysisDataQueryDefinition queryDef)
        {
            this.dataSetsArray = dataSetService.AppendAndReturnAsArray(this.dataSetsArray, queryDef);
        }

        #endregion UPDATING DATA SETS



        public void Run()
        {
            IEnumerable<AnalysisType> analysisTypes = simulation.GetAnalysisTypes();
            foreach(AnalysisType type in analysisTypes){
                IAnalysisProcessController processController = ProcessorFactory.Instance().GetProperAnalysisProcessController(type);
                processController.Run(this);
            }
        }



        #region ACCESS TO DATA SETS

        public DataSet GetDataSet(int index)
        {
            return dataSetsArray[index];
        }

        public IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef)
        {
            loadDataSets(queryDef);
            var result = dataSetsArray.ToArray();
            if (queryDef.StartDate != null)
            {
                result = result.Where(q => q.Date.CompareTo(queryDef.StartDate) >= 0).ToArray();
            }
            if (queryDef.EndDate != null)
            {
                result = result.Where(q => q.Date.CompareTo(queryDef.EndDate) <= 0).ToArray();
            }
            if (queryDef.StartIndex != null)
            {
                result = result.Where(q => q.IndexNumber >= (int) queryDef.StartIndex).ToArray();
            }
            if (queryDef.EndIndex != null)
            {
                result = result.Where(q => q.IndexNumber <= (int) queryDef.EndIndex).ToArray();
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

        #endregion ACCESS TO DATA SETS



    }

}