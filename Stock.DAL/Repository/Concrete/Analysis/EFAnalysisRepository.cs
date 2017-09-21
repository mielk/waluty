using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.DAL.Repositories
{
    public class EFAnalysisRepository : IAnalysisRepository
    {

        public AnalysisInfoDto GetAnalysisInfoDto(AnalysisDataQueryDefinition queryDef, AnalysisType analysisType)
        {
            return null;
        }

    }
}
