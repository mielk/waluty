using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.DAL.Repositories
{
    public interface IPriceRepository
    {
        void UpdatePrices(IEnumerable<PriceDto> prices);
        IEnumerable<PriceDto> GetPrices(AnalysisDataQueryDefinition queryDef);
    }
}
