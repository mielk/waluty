using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stock.DAL.Repositories;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface ITimeframeService
    {

        //Infrastructure.
        void InjectRepository(ITimeframeRepository repository);

        //Timeframes.
        IEnumerable<Timeframe> GetAllTimeframes();
        Timeframe GetTimeframeById(int id);
        Timeframe GetTimeframeByName(string symbol);

    }
}
