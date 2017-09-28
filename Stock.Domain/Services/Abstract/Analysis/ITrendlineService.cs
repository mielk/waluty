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
        
        //Trendlines
        IEnumerable<Trendline> GetTrendlines(int assetId, int timeframeId, int simulationId);
        Trendline GetTrendlineById(int id);
        void UpdateTrendline(Trendline trendline);

        //Trend hits
        IEnumerable<TrendHit> GetTrendHits(int trendlineId);
        TrendHit GetTrendHitById(int id);
        void UpdateTrendHit(TrendHit trendHit);

    }
}
