using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;
using Stock.Domain.Services.Factories;


namespace Stock.Domain.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public DateTime LastPriceUpdate { get; set; }
        //public DateTime LastCalculation { get; set; }
        //public bool PricesChecked { get; set; }
        //public DateTime LastTrendlinesReview { get; set; }

        public static Asset FromSymbol(string symbol)
        {
            var fxService = FxServiceFactory.Instance().GetService();
            return fxService.GetPair(symbol);
        }

        public static Asset FromId(int id)
        {
            var fxService = FxServiceFactory.Instance().GetService();
            return fxService.GetPair(id);
        }

    }
}
