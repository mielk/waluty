using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public class EFCompanyRepository : ICompanyRepository
    {

        //private static readonly EFDbContext Context = EFDbContext.GetInstance();

        public IEnumerable<CompanyDto> FilterCompanies(string q, int limit)
        {
            string lower = q.ToLower();
            IEnumerable<CompanyDto> results = null;

            using (EFDbContext context = new EFDbContext())
            {
                results = context.Companies.Where(c => c.PairName.ToLower().Contains(lower) || 
                                                  c.Short.ToLower().Contains(lower)).Take(limit).ToList();
            }

            return results;

        }

        public CompanyDto GetCompany(int id)
        {

            CompanyDto company = null;

            using (EFDbContext context = new EFDbContext())
            {
                company = context.Companies.SingleOrDefault(c => c.Id == id);
            }

            return company;

        }


    }
}
