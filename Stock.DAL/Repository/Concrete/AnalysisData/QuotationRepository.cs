using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;

namespace Stock.DAL.Repositories
{
    public class QuotationRepository : IQuotationRepository
    {
        
        public IEnumerable<QuotationDto> GetQuotations(int assetId, int timeframe)
        {
            return null;
        }

        public IEnumerable<QuotationDto> GetQuotations(AnalysisDataQueryDefinition queryDef)
        {
            return null;
        }

    }
}
