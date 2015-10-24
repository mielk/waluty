using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    
    //[NotMapped]
    public class CompanyDto : IAssetDto
    {
        [Key]
        public int Id { get; set; }
        public string PairName { get; set; }
        public int IdMarket { get; set; }
        public string Short { get; set; }
        public DateTime LastPriceUpdate { get; set; }
        public DateTime LastCalculation { get; set; }
        public bool PricesChecked { get; set; }
        public DateTime LastTrendlinesReview { get; set; }
    }
}
