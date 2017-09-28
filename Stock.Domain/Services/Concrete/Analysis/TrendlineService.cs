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


    }
}
