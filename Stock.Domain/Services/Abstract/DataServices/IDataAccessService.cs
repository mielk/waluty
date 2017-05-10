using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Core;

namespace Stock.Domain.Services
{
    public interface IDataAccessService
    {
        IEnumerable<IDataUnit> GetUnits(AnalysisDataQueryDefinition queryDef);
    }
}
