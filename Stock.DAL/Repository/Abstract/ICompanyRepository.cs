using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public interface ICompanyRepository
    {
        IEnumerable<CompanyDto> FilterCompanies(string q, int limit);
        CompanyDto GetCompany(int id);
    }
}
