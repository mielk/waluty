using Stock.DAL.Infrastructure;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{

    public class EFTrendlineRepository : ITrendlineRepository
    {



        #region TRENDLINES

        public IEnumerable<TrendlineDto> GetTrendlines(int assetId, int timeframeId, int simulationId)
        {
            IEnumerable<TrendlineDto> trendlines;
            using (var context = new TrendlineContext())
            {
                trendlines = context.Trendlines.Where(t => t.AssetId == assetId && t.TimeframeId == timeframeId && t.SimulationId == simulationId).ToList();
            }
            return trendlines;
        }

        public TrendlineDto GetTrendlineById(int id)
        {
            using (var context = new TrendlineContext())
            {
                return context.Trendlines.SingleOrDefault(t => t.Id == id);
            }
        }

        public void UpdateTrendlines(IEnumerable<TrendlineDto> trendlines)
        {

            using (var db = new TrendlineContext())
            {

                foreach (TrendlineDto dto in trendlines)
                {
                    var record = db.Trendlines.SingleOrDefault(t => t.Id == dto.Id);
                    if (record != null)
                    {
                        record.CopyProperties(dto);
                    }
                    else
                    {
                        db.Trendlines.Add(dto);
                    }
                }
                db.SaveChanges();

            }
        }

        #endregion TRENDLINES



        #region TREND HITS

        public IEnumerable<TrendHitDto> GetTrendHits(int trendlineId)
        {
            IEnumerable<TrendHitDto> trendlines;
            using (var context = new TrendlineContext())
            {
                trendlines = context.TrendHits.Where(t => t.TrendlineId == trendlineId).ToList();
            }
            return trendlines;
        }

        public TrendHitDto GetTrendHitById(int id)
        {
            using (var context = new TrendlineContext())
            {
                return context.TrendHits.SingleOrDefault(t => t.Id == id);
            }
        }


        public void UpdateTrendHits(IEnumerable<TrendHitDto> trendlines)
        {

            using (var db = new TrendlineContext())
            {

                foreach (TrendHitDto dto in trendlines)
                {
                    var record = db.TrendHits.SingleOrDefault(t => t.Id == dto.Id);
                    if (record != null)
                    {
                        record.CopyProperties(dto);
                    }
                    else
                    {
                        db.TrendHits.Add(dto);
                    }
                }
                db.SaveChanges();

            }
        }

        #endregion TREND HITS


    }

}
