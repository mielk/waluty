using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public interface ITimeframeRepository
    {
        IEnumerable<TimeframeDto> GetAllTimeframes();
        TimeframeDto GetTimeframeById(int id);
        TimeframeDto GetTimeframeBySymbol(string symbol);
    }
}
