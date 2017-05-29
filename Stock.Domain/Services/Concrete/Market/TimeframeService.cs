using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Infrastructure;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using Stock.Domain.Entities;
using System.Linq.Expressions;

namespace Stock.Domain.Services
{
    public class TimeframeService : ITimeframeService
    {

        private ITimeframeRepository _repository;
        private IEnumerable<Timeframe> timeframes = new List<Timeframe>();


        #region INFRASTRUCTURE
        
        public TimeframeService(ITimeframeRepository repository)
        {
            _repository = repository;
        }

        public void InjectRepository(ITimeframeRepository repository)
        {
            _repository = repository;
        }

        #endregion INFRASTRUCTURE


        #region TIMEFRAMES

        private void appendTimeframe(Timeframe timeframe)
        {
            timeframes = timeframes.Concat(new[] { timeframe });
        }

        public IEnumerable<Timeframe> GetAllTimeframes()
        {
            List<Timeframe> result = new List<Timeframe>();
            var dtos = _repository.GetAllTimeframes();
            foreach (var dto in dtos)
            {
                Timeframe timeframe = timeframes.SingleOrDefault(t => t.GetId() == dto.Id);
                if (timeframe == null)
                {
                    timeframe = Timeframe.FromDto(dto);
                    appendTimeframe(timeframe);
                }
                result.Add(timeframe);
            }
            return result;
        }


        private Timeframe GetTimeframe(Func<TimeframeDto> func, Timeframe timeframe)
        {
            if (timeframe == null)
            {
                TimeframeDto result = func();
                if (result != null)
                {
                    timeframe = Timeframe.FromDto(result);
                    appendTimeframe(timeframe);
                }
            }
            return timeframe;
        }

        public Timeframe GetTimeframeById(int id)
        {
            var timeframe = timeframes.SingleOrDefault(t => t.GetId() == id);
            return GetTimeframe(delegate { return _repository.GetTimeframeById(id); }, timeframe);
        }

        public Timeframe GetTimeframeByName(string name)
        {
            var timeframe = timeframes.SingleOrDefault(t => t.GetName().Equals(name, StringComparison.CurrentCultureIgnoreCase));
            return GetTimeframe(delegate { return _repository.GetTimeframeBySymbol(name); }, timeframe);
        }

        #endregion TIMEFRAMES


    }

}