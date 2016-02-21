using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Factories
{
    public class AnalysisDataServiceFactory
    {

        private static AnalysisDataServiceFactory _instance;

        private readonly IAnalysisDataService _service;


        private AnalysisDataServiceFactory()
        {
            _service = new DataService(null);
        }


        public static AnalysisDataServiceFactory Instance()
        {
            return _instance ?? (_instance = new AnalysisDataServiceFactory());
        }


        public IAnalysisDataService GetService()
        {
            return _service;
        }



    }
}