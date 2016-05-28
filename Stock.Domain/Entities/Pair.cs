using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class Pair : Asset
    {
        public bool IsFx { get; set; }
        public int BaseCurrency { get; set; }
        public int QuoteCurrency { get; set; }


        public Pair()
        {
            IsFx = true;
        }


        public static Pair FromDto(PairDto dto)
        {

            var pair = new Pair();
            pair.Id = dto.Id;
            pair.Name = dto.PairName;
            //pair.LastPriceUpdate = dto.LastPriceUpdate;
            //pair.LastCalculation = dto.LastCalculation;
            //pair.PricesChecked = dto.PricesChecked;
            //pair.LastTrendlinesReview = dto.LastTrendlinesReview;
            pair.IsFx = true;
            pair.BaseCurrency = dto.BaseCurrency;
            pair.QuoteCurrency = dto.QuoteCurrency;

            return pair;

        }


    }
}