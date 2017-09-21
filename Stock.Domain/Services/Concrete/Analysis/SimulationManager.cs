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
    public class SimulationManager : ProcessManager, ISimulationManager
    {

        private Simulation simulation;


        #region CONSTRUCTOR

        public SimulationManager(Simulation simulation) : base()
        {
            setSimulation(simulation);
        }

        public SimulationManager(Simulation simulation, IDataSetService dataSetService) : base(dataSetService)
        {
            setSimulation(simulation);
        }

        public SimulationManager(Simulation simulation, IDataSetService dataSetService, IAnalysisTimestampService timestampService) : base(dataSetService, timestampService)
        {
            setSimulation(simulation);
        }

        private void setSimulation(Simulation simulation)
        {
            this.simulation = simulation;
            //this.queryDef = new AnalysisDataQueryDefinition(simulation.AssetId, simulation.TimeframeId) { SimulationId = simulation.Id, AnalysisTypes = simulation.GetAnalysisTypes() };
        }
        
        #endregion CONSTRUCTOR



        #region SIMULATION
        
        public Simulation GetSimulation()
        {
            return simulation;
        }

        public new int GetSimulationId()
        {
            return simulation == null ? 0 : simulation.Id;
        }

        #endregion SIMULATION



        #region UPDATING DATA SETS

        public void loadDataSets(int initialIndex)
        {
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

            //if (lastIndexes == null) loadLastIndexes();

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