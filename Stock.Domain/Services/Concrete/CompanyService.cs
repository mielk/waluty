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
    public class CompanyService : ICompanyService
    {

        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository ?? RepositoryFactory.GetCompanyRepository();
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
