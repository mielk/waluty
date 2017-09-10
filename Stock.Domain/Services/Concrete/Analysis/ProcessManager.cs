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
        DataSet[] dataSetsArray;
        Dictionary<AnalysisType, DataSetInfo> dataSetInfos;



        #region CONSTRUCTOR

        public ProcessManager(Simulation simulation)
        {
            this.dataSetService = ServiceFactory.GetDataSetService();
            setSimulation(simulation);
        }

        public ProcessManager(IDataSetService dataSetService, Simulation simulation)
        {
            this.dataSetService = dataSetService;
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

        #endregion CONSTRUCTOR



        public Simulation GetSimulation()
        {
            return simulation;
        }

        public int GetSimulationId()
        {
            return simulation == null ? 0 : simulation.Id;
        }




        public void Run()
        {

            updateData(false);
            IEnumerable<AnalysisType> analysisTypes = simulation.GetAnalysisTypes();

            foreach(AnalysisType type in analysisTypes){
                IAnalysisProcessController processController = ProcessorFactory.GetProperAnalysisProcessController(type);
                processController.Run(this);
            }

        }

        public void updateData(bool force)
        {
            loadDataSets(force);
            loadDataSetInfos(force);
        }

        private void loadDataSets(bool force)
        {
            if (this.dataSetsArray == null)
            {
                this.dataSetsArray = dataSetService.GetDataSets(queryDef).ToArray();
            }
        }

        private void loadDataSetInfos(bool force)
        {
            if (this.dataSetInfos == null)
            {
                dataSetInfos = dataSetService.GetDataSetInfos(queryDef);
            }
        }




        public DataSet GetDataSet(int index)
        {
            return dataSetsArray[index];
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

        public DataSetInfo GetDataSetInfo(AnalysisType type)
        {
            DataSetInfo dsi;
            try
            {
                dataSetInfos.TryGetValue(type, out dsi);
                return dsi;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
