using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Factories
{

    public class MarketServiceFactory
    {
        public static IMarketService CreateService()
        {
            return MarketService.Instance();
        }
    }

}