using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Repositories;
using Stock.Domain.Entities;
using Stock.Core;

namespace Stock.Domain.Services
{
    public interface IPriceService : IBasicAnalysisService, IDataAccessService
    {
        void InjectRepository(IPriceRepository repository);
        IEnumerable<Price> GetPrices(AnalysisDataQueryDefinition queryDef);
        void UpdatePrices(IEnumerable<Price> prices);
    }
}
