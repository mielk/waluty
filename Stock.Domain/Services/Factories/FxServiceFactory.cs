using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Factories
{
    public class FxServiceFactory
    {

        private static FxServiceFactory _instance;

        private readonly IFxService _service;


        private FxServiceFactory()
        {
            _service = new FxService(null);
        }


        public static FxServiceFactory Instance()
        {
            return _instance ?? (_instance = new FxServiceFactory());
        }


        public IFxService GetService()
        {
            return _service;
        }



    }
}
