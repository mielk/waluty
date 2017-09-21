using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;
using Stock.Core;

namespace Stock.DAL.Repositories
{
    public interface IAnalysisRepository
    {
        AnalysisInfoDto GetAnalysisInfoDto(AnalysisDataQueryDefinition queryDef, AnalysisType analysisType);
    }
}