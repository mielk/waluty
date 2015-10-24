using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Factories
{
    public class MarketServiceFactory{

        private static MarketServiceFactory _instance;

        private readonly IMarketService _service;


        private MarketServiceFactory()
        {
            _service = new MarketService(null);
        }


        public static MarketServiceFactory Instance()
        {
            return _instance ?? (_instance = new MarketServiceFactory());
        }


        public IMarketService GetService()
        {
            return _service;
        }


    }
}