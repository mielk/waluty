using Stock.DAL.Infrastructure;
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
            IEnumerable<AnalysisTypeDto> results;
            using (var context = new AnalysisContext())
            {
                results = context.AnalysisTypes.ToList();
            }
            return results;
        }

        public AnalysisTypeDto GetAnalysisTypeById(int id)
        {
            AnalysisTypeDto dto;
            using (var context = new AnalysisContext())
            {
                dto = context.AnalysisTypes.SingleOrDefault(c => c.Id == id);
            }
            return dto;
        }

        public AnalysisTypeDto GetAnalysisTypeByName(string name)
        {
            AnalysisTypeDto dto;
            using (var context = new AnalysisContext())
            {
                dto = context.AnalysisTypes.SingleOrDefault(c => c.Name.Equals(name, System.StringComparison.CurrentCultureIgnoreCase));
            }
            return dto;
        }

    }
}
