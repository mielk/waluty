using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;

namespace Stock.DAL.Repositories
{
    public interface ITrendlineRepository
    {
        IEnumerable<TrendlineDto> GetTrendlines(int assetId, int timeframeId, int simulationId);
        TrendlineDto GetTrendlineById(int id);
        void UpdateTrendlines(IEnumerable<TrendlineDto> dtos);
        void RemoveTrendlines(IEnumerable<TrendlineDto> dtos);

        IEnumerable<TrendHitDto> GetTrendHits(int trendlineId);
        TrendHitDto GetTrendHitById(int trendHitId);
        void UpdateTrendHits(IEnumerable<TrendHitDto> dtos);

        IEnumerable<TrendRangeDto> GetTrendRanges(int trendlineId);
        TrendRangeDto GetTrendRangeById(int trendRangeId);
        void UpdateTrendRanges(IEnumerable<TrendRangeDto> dtos);

        IEnumerable<TrendBreakDto> GetTrendBreaks(int trendlineId);
        TrendBreakDto GetTrendBreakById(int trendBreakId);
        void UpdateTrendBreaks(IEnumerable<TrendBreakDto> dtos);

    }
}
