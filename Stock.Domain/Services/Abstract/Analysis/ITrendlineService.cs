using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface ITrendlineService
    {
        IEnumerable<Trendline> GetTrendlines(int assetId, int timeframeId, int simulationId);
        Trendline GetTrendlineById(int id);
        void Update(Trendline trendline);
    }
}
