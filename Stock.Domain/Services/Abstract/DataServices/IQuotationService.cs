using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;
using Stock.Core;

namespace Stock.Domain.Services
{
    public interface IQuotationService : IBasicAnalysisService, IDataAccessService
    {
        void InjectRepository(IQuotationRepository repository);
        IEnumerable<Quotation> GetQuotations(AnalysisDataQueryDefinition queryDef);
        void UpdateQuotations(IEnumerable<Quotation> quotations);
    }
}
