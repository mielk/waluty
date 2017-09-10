using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface IProcessManager
    {
        DataSetInfo GetDataSetInfo(AnalysisType type);
        DataSet GetDataSet(int index);
        int GetDataSetIndex(DateTime? datetime);
        void Run();
        int GetSimulationId();
    }
}
