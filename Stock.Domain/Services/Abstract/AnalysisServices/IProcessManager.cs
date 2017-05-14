using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface IProcessManager
    {
        DataSet GetDataSet(int index);
        void Run(IDataProcessor processor);
        int GetSimulationId();
    }
}
