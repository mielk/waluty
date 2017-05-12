using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public class EFTimeframeRepository : ITimeframeRepository
    {

        public IEnumerable<TimeframeDto> GetAllTimeframes()
        {
            IEnumerable<TimeframeDto> results;
            using (var context = new TimeframeContext())
            {
                results = context.Timeframes.ToList();
            }
            return results;
        }

        public TimeframeDto GetTimeframeById(int id)
        {
            TimeframeDto dto;
            using (var context = new TimeframeContext())
            {
                dto = context.Timeframes.SingleOrDefault(t => t.Id == id);
            }
            return dto;
        }

        public TimeframeDto GetTimeframeBySymbol(string name)
        {
            TimeframeDto dto;
            using (var context = new TimeframeContext())
            {
                dto = context.Timeframes.SingleOrDefault(t => t.Symbol.Equals(name, System.StringComparison.CurrentCultureIgnoreCase));
            }
            return dto;
        }

    }
}
