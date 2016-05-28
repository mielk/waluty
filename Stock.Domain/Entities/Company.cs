using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class Company : Asset
    {
        public int IdMarket { get; set; }
        public string Short { get; set; }


        public static Company FromDto(CompanyDto dto)
        {

            var company = new Company();
            company.Id = dto.Id;
            company.Name = dto.PairName;
            //company.LastPriceUpdate = dto.LastPriceUpdate;
            //company.LastCalculation = dto.LastCalculation;
            //company.PricesChecked = dto.PricesChecked;
            //company.LastTrendlinesReview = dto.LastTrendlinesReview;
            company.IdMarket = dto.IdMarket;
            company.Short = dto.Short;

            return company;

        }

    }
}
