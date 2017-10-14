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
        DataSet GetDataSet(int index);
        IEnumerable<DataSet> GetDataSets();
        IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef);
        IEnumerable<Trendline> GetTrendlines();
        int GetDataSetIndex(DateTime? datetime);
        int? GetAnalysisLastUpdatedIndex(AnalysisType type);
        void Run();
        int GetSimulationId();
        AnalysisInfo GetAnalysisInfo();
        AtsSettings GetSettings();
        int GetAssetId();
        int GetTimeframeId();
    }
}
