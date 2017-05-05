using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Factories
{
    public class DataServiceFactory
    {

        private static DataServiceFactory _instance;

        private readonly IDataService2 _service;


        private DataServiceFactory()
        {
            _service = new DataService2(null);
        }


        public static DataServiceFactory Instance()
        {
            return _instance ?? (_instance = new DataServiceFactory());
        }


        public IDataService2 GetService()
        {
            return _service;
        }


    }
}
