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
    public class MarketService : IMarketService
    {

        private readonly IMarketRepository _repository;
        private static readonly MarketService instance = new MarketService(RepositoryFactory.GetMarketRepository());


        public static MarketService Instance()
        {
            return instance;
        }
        private MarketService(IMarketRepository repository)
        {
            _repository = repository;
        }





        public IEnumerable<Market> GetMarkets()
        {
            var dtos = _repository.GetMarkets();
            return dtos.Select(Market.FromDto).ToList();
        }




        public IEnumerable<Company> FilterCompanies(string q, int limit)
        {
            var dtos = _repository.FilterCompanies(q, limit);
            return dtos.Select(Company.FromDto).ToList();
        }

        public Company GetCompany(int id)
        {
            var dto = _repository.GetCompany(id);
            return Company.FromDto(dto);
        }


    }
}
