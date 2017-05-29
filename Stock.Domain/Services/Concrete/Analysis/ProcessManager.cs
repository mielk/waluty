using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class ProcessManager : IProcessManager
    {

        private int simulationId = 1;
        private IDataSetService dataSetService;
        DataSet[] dataSetsArray;

        public void InjectDataSetService(IDataSetService service)
        {
            this.dataSetService = service;
        }

        public int GetSimulationId()
        {
            return simulationId;
        }

        public DataSet GetDataSet(int index)
        {
            return dataSetsArray[index];
        }

        public void Run(IDataProcessor processor)
        {

        }

    }
}
