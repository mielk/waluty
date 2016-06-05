using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Services
{
    public class FxService : IFxService
    {


        private readonly IFxRepository _repository;

        public FxService(IFxRepository repository)
        {
            _repository = repository ?? RepositoryFactory.GetFxRepository();
        }


        public IEnumerable<FxPair> FilterPairs(string q, int limit)
        {
            var dtos = _repository.FilterPairs(q, limit);
            return dtos.Select(FxPair.FromDto).ToList();
        }


        public FxPair GetPair(int id)
        {
            var dto = _repository.GetPair(id);
            return FxPair.FromDto(dto);
        }


        public FxPair GetPair(string symbol)
        {
            var dto = _repository.GetPair(symbol);
            return FxPair.FromDto(dto);
        }


    }
}
