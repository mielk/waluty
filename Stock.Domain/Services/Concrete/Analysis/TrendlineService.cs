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


        #region API

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

        public void Update(Trendline trendline)
        {
            TrendlineDto dto = trendline.ToDto();
            _repository.UpdateTrendlines(new TrendlineDto[] { dto });
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

        
        #endregion API




    }
}
