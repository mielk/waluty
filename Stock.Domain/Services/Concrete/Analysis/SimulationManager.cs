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
        private int startIndex = 0;
        private int endIndex = 0;


        #region CONSTRUCTOR

        public SimulationManager(int assetId, int timeframeId, Simulation simulation) : base(assetId, timeframeId)
        {
            setSimulation(simulation);
        }

        public SimulationManager(int assetId, int timeframeId, Simulation simulation, IDataSetService dataSetService) : base(assetId, timeframeId, dataSetService)
        {
            setSimulation(simulation);
        }

        public SimulationManager(int assetId, int timeframeId, Simulation simulation, IDataSetService dataSetService, IAnalysisTimestampService timestampService) : base(assetId, timeframeId, dataSetService, timestampService)
        {
            setSimulation(simulation);
        }

        private void setSimulation(Simulation simulation)
        {
            this.simulation = simulation;
            //this.queryDef = new AnalysisDataQueryDefinition(trendBreak.AssetId, trendBreak.TimeframeId) { SimulationId = trendBreak.Id, AnalysisTypes = trendBreak.GetAnalysisTypes() };
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

        private void loadLimitedDataSets(int startIndex, int endIndex)
        {
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(AssetId, TimeframeId) { SimulationId = simulation.Id, StartIndex = startIndex, EndIndex = endIndex };
            this.dataSetsArray = dataSetService.GetDataSets(queryDef, this.dataSetsArray).ToArray();
        }

        #endregion UPDATING DATA SETS



        public void RunByGivenSteps(int steps)
        {
            endIndex += steps;
            loadLimitedDataSets(startIndex, endIndex);
            runAllAnalysisTypes();

            foreach (AnalysisType type in analysisTypes)
            {
                updateLastIndex(type, endIndex);
            }
        }


    }

}