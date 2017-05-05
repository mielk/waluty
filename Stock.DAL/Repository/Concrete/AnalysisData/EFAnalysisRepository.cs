using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public class EFAnalysisRepository : IAnalysisRepository
    {

        public IEnumerable<AnalysisTypeDto> GetAnalysisTypes()
        {
            return null;
        }

    }
}
