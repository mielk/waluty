using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Factories
{
    public class ProcessServiceFactory{

        private static ProcessServiceFactory _instance;

        private readonly IProcessService _service;


        private ProcessServiceFactory()
        {
            _service = new ProcessService(null);
        }


        public static ProcessServiceFactory Instance()
        {
            return _instance ?? (_instance = new ProcessServiceFactory());
        }


        public IProcessService GetService()
        {
            return _service;
        }


    }
}