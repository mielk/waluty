using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using Stock.Core;

namespace Stock.Domain.Services
{
    public class TrendlineService : ITrendlineService
    {

        private ITrendlineRepository _repository;
        private IEnumerable<Trendline> trendlines = new List<Trendline>();
        private IEnumerable<TrendHit> trendHits = new List<TrendHit>();
        private IEnumerable<TrendRange> trendRanges = new List<TrendRange>();
        private IEnumerable<TrendBreak> trendBreaks = new List<TrendBreak>();



        #region INFRASTRUCTURE

        public TrendlineService()
        {
            _repository = RepositoryFactory.GetTrendlineRepository();
        }

        public TrendlineService(ITrendlineRepository repository)
        {
            _repository = repository;
        }

        public void InjectRepository(ITrendlineRepository repository)
        {
            _repository = repository;
        }

        #endregion INFRASTRUCTURE



        #region TRENDLINES

        public IEnumerable<Trendline> GetTrendlines(int assetId, int timeframeId, int simulationId)
        {
            var dtos = _repository.GetTrendlines(assetId, timeframeId, simulationId);
            return GetTrendlines(dtos);
        }

        private IEnumerable<Trendline> GetTrendlines(IEnumerable<TrendlineDto> dtos)
        {
            List<Trendline> result = new List<Trendline>();
            foreach (var dto in dtos)
            {
                Trendline trendline = trendlines.SingleOrDefault(s => s.Id == dto.Id);
                if (trendline == null)
                {
                    trendline = Trendline.FromDto(dto);
                    appendTrendline(trendline);
                }
                result.Add(trendline);
            }
            return result;
        }

        public Trendline GetTrendlineById(int id){
            var trendline = trendlines.SingleOrDefault(m => m.Id == id);
            if (trendline == null)
            {
                var dto = _repository.GetTrendlineById(id);
                if (dto != null)
                {
                    trendline = Trendline.FromDto(dto);
                    appendTrendline(trendline);
                }
            }
            return trendline;
        }

        private void appendTrendline(Trendline trendline)
        {
            trendlines = trendlines.Concat(new[] { trendline });
        }

        public void UpdateTrendline(Trendline trendline)
        {
            TrendlineDto dto = trendline.ToDto();
            _repository.UpdateTrendlines(new TrendlineDto[] { dto });
        }

        public void RemoveTrendlines(IEnumerable<Trendline> trendlines)
        {
            _repository.RemoveTrendlines(trendlines.Select(t => t.ToDto()));
            foreach (var trendline in trendlines)
            {
                this.trendlines = this.trendlines.Where(t => t.GetHashCode() != trendline.GetHashCode()).ToList();
            }
        }

        public void RemoveTrendline(Trendline trendline) 
        {
            RemoveTrendlines(new Trendline[] { trendline } );
        }

        #endregion TRENDLINES



        #region TREND_HITS

        public IEnumerable<TrendHit> GetTrendHits(int trendlineId)
        {
            var dtos = _repository.GetTrendHits(trendlineId);
            return GetTrendHits(dtos);
        }

        private IEnumerable<TrendHit> GetTrendHits(IEnumerable<TrendHitDto> dtos)
        {
            List<TrendHit> result = new List<TrendHit>();
            foreach (var dto in dtos)
            {
                TrendHit trendHit = trendHits.SingleOrDefault(t => t.Id == dto.Id);
                if (trendHit == null)
                {
                    trendHit = TrendHit.FromDto(dto);
                    appendTrendHit(trendHit);
                }
                result.Add(trendHit);
            }
            return result;
        }

        public TrendHit GetTrendHitById(int id)
        {
            var trendHit = trendHits.SingleOrDefault(m => m.Id == id);
            if (trendHit == null)
            {
                var dto = _repository.GetTrendHitById(id);
                if (dto != null)
                {
                    trendHit = TrendHit.FromDto(dto);
                    appendTrendHit(trendHit);
                }
            }
            return trendHit;
        }

        private void appendTrendHit(TrendHit trendHit)
        {
            trendHits = trendHits.Concat(new[] { trendHit });
        }

        public void UpdateTrendHit(TrendHit trendHit)
        {
            TrendHitDto dto = trendHit.ToDto();
            _repository.UpdateTrendHits(new TrendHitDto[] { dto });
        }


        #endregion TREND_HITS




        #region TREND_BREAKS


        public IEnumerable<TrendBreak> GetTrendBreaks(int trendlineId)
        {
            var dtos = _repository.GetTrendBreaks(trendlineId);
            return getTrendBreaks(dtos);
        }

        private IEnumerable<TrendBreak> getTrendBreaks(IEnumerable<TrendBreakDto> dtos)
        {
            List<TrendBreak> result = new List<TrendBreak>();
            foreach (var dto in dtos)
            {
                TrendBreak trendBreak = trendBreaks.SingleOrDefault(t => t.Guid.Equals(dto.Guid));
                if (trendBreak == null)
                {
                    trendBreak = TrendBreak.FromDto(dto);
                    appendTrendBreak(trendBreak);
                }
                result.Add(trendBreak);
            }
            return result;
        }

        public TrendBreak GetTrendBreakById(int id)
        {
            var trendBreak = trendBreaks.SingleOrDefault(m => m.Id == id);
            if (trendBreak == null)
            {
                var dto = _repository.GetTrendBreakById(id);
                if (dto != null)
                {
                    trendBreak = TrendBreak.FromDto(dto);
                    appendTrendBreak(trendBreak);
                }
            }
            return trendBreak;
        }

        private void appendTrendBreak(TrendBreak trendBreak)
        {
            trendBreaks = trendBreaks.Concat(new[] { trendBreak });
        }



        public void UpdateTrendBreak(TrendBreak trendBreak)
        {
            TrendBreakDto dto = trendBreak.ToDto();
            _repository.UpdateTrendBreaks(new TrendBreakDto[] { dto });
        }

        #endregion TREND_BREAKS



        #region TREND_RANGES

        public IEnumerable<TrendRange> GetTrendRanges(int trendlineId)
        {
            var dtos = _repository.GetTrendRanges(trendlineId);
            return GetTrendRanges(dtos);
        }

        private IEnumerable<TrendRange> GetTrendRanges(IEnumerable<TrendRangeDto> dtos)
        {
            List<TrendRange> result = new List<TrendRange>();
            foreach (var dto in dtos)
            {
                TrendRange trendRange = trendRanges.SingleOrDefault(t => t.Guid.Equals(dto.Guid));
                if (trendRange == null)
                {
                    trendRange = TrendRange.FromDto(dto);
                    appendTrendRange(trendRange);
                }
                result.Add(trendRange);
            }
            return result;
        }

        public TrendRange GetTrendRangeById(int id)
        {
            var trendRange = trendRanges.SingleOrDefault(m => m.Id == id);
            if (trendRange == null)
            {
                var dto = _repository.GetTrendRangeById(id);
                if (dto != null)
                {
                    trendRange = TrendRange.FromDto(dto);
                    appendTrendRange(trendRange);
                }
            }
            return trendRange;
        }

        private void appendTrendRange(TrendRange trendRange)
        {
            trendRanges = trendRanges.Concat(new[] { trendRange });
        }


        
        public void UpdateTrendRange(TrendRange trendRange)
        {
            TrendRangeDto dto = trendRange.ToDto();
            _repository.UpdateTrendRanges(new TrendRangeDto[] { dto });
        }

        #endregion TREND_RANGES

    }
}
