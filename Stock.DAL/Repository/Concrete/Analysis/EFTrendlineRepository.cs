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

        public void RemoveTrendlines(IEnumerable<TrendlineDto> trendlines)
        {

            using (var db = new TrendlineContext())
            {

                foreach (TrendlineDto dto in trendlines)
                {
                    var record = db.Trendlines.SingleOrDefault(t => t.Id == dto.Id);
                    if (record != null)
                    {
                        db.Trendlines.Remove(record);
                    }
                }
                db.SaveChanges();

            }
        }

        #endregion TRENDLINES



        #region TREND HITS

        public IEnumerable<TrendHitDto> GetTrendHits(int trendlineId)
        {
            IEnumerable<TrendHitDto> trendHits;
            using (var context = new TrendlineContext())
            {
                trendHits = context.TrendHits.Where(t => t.TrendlineId == trendlineId).ToList();
            }
            return trendHits;
        }

        public TrendHitDto GetTrendHitById(int id)
        {
            using (var context = new TrendlineContext())
            {
                return context.TrendHits.SingleOrDefault(t => t.Id == id);
            }
        }


        public void UpdateTrendHits(IEnumerable<TrendHitDto> trendHits)
        {

            using (var db = new TrendlineContext())
            {

                foreach (TrendHitDto dto in trendHits)
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



        #region TREND_RANGE


        public IEnumerable<TrendRangeDto> GetTrendRanges(int trendlineId)
        {
            IEnumerable<TrendRangeDto> trendRanges;
            using (var context = new TrendlineContext())
            {
                trendRanges = context.TrendRanges.Where(t => t.TrendlineId == trendlineId).ToList();
            }
            return trendRanges;
        }

        public TrendRangeDto GetTrendRangeById(int id)
        {
            using (var context = new TrendlineContext())
            {
                return context.TrendRanges.SingleOrDefault(t => t.Id == id);
            }
        }

        public void UpdateTrendRanges(IEnumerable<TrendRangeDto> dtos)
        {

            using (var db = new TrendlineContext())
            {

                foreach (TrendRangeDto dto in dtos)
                {
                    var record = db.TrendRanges.SingleOrDefault(t => t.Id == dto.Id);
                    if (record != null)
                    {
                        record.CopyProperties(dto);
                    }
                    else
                    {
                        db.TrendRanges.Add(dto);
                    }
                }
                db.SaveChanges();

            }
        }
        

        #endregion TREND_RANGE



        #region TREND_BREAK

        public void UpdateTrendBreaks(IEnumerable<TrendBreakDto> dtos)
        {

            using (var db = new TrendlineContext())
            {

                foreach (TrendBreakDto dto in dtos)
                {
                    var record = db.TrendBreaks.SingleOrDefault(t => t.Id == dto.Id);
                    if (record != null)
                    {
                        record.CopyProperties(dto);
                    }
                    else
                    {
                        db.TrendBreaks.Add(dto);
                    }
                }
                db.SaveChanges();

            }
        }

        public IEnumerable<TrendBreakDto> GetTrendBreaks(int trendlineId)
        {
            IEnumerable<TrendBreakDto> trendBreaks;
            using (var context = new TrendlineContext())
            {
                trendBreaks = context.TrendBreaks.Where(t => t.TrendlineId == trendlineId).ToList();
            }
            return trendBreaks;
        }

        public TrendBreakDto GetTrendBreakById(int id)
        {
            using (var context = new TrendlineContext())
            {
                return context.TrendBreaks.SingleOrDefault(t => t.Id == id);
            }
        }



        #endregion TREND_BREAK


    }

}
