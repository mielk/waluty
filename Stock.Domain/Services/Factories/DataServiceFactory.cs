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

        private readonly IDataService _service;


        private DataServiceFactory()
        {
            _service = new DataService(null);
        }


        public static DataServiceFactory Instance()
        {
            return _instance ?? (_instance = new DataServiceFactory());
        }


        public IDataService GetService()
        {
            return _service;
        }


    }
}
