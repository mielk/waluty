using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public interface IFxRepository
    {
        IEnumerable<PairDto> FilterPairs(string q, int limit);
        PairDto GetPair(int id);
        PairDto GetPair(string symbol);
    }
}
