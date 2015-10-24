using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    interface IAssetDto
    {
        int Id { get; set; }
        string PairName { get; set; }
        DateTime LastPriceUpdate { get; set; }
        DateTime LastCalculation { get; set; }
        bool PricesChecked { get; set; }
        DateTime LastTrendlinesReview { get; set; }
    }
}
