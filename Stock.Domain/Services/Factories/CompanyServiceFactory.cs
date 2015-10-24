using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Factories
{
    public class CompanyServiceFactory{

        private static CompanyServiceFactory _instance;

        private readonly ICompanyService _service;


        private CompanyServiceFactory()
        {
            _service = new CompanyService(null);
        }


        public static CompanyServiceFactory Instance()
        {
            return _instance ?? (_instance = new CompanyServiceFactory());
        }


        public ICompanyService GetService()
        {
            return _service;
        }


    }
}