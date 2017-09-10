using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.Domain.Services
{
    public interface IAnalysisTimestampService
    {
        Dictionary<AnalysisType, int?> GetLastAnalyzedIndexes(int simulationId);
    }
}
