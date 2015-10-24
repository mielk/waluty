using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface IFxService
    {

        IEnumerable<Pair> FilterPairs(string q, int limit);
        Pair GetPair(int id);
        Pair GetPair(string symbol);

    }
}
