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
        public Market market { get; set; }
        public string Short { get; set; }


        public Company(int id, string name, Market market) : base(id, name)
        {
            this.market = market;
        }

        public Company(int id, string name, int marketId) : base(id, name)
        {
            //this.market = Market.
        }


        public static Company FromDto(CompanyDto dto)
        {

            var company = new Company(dto.Id, dto.PairName, dto.IdMarket);
            company.Short = dto.Short;

            return company;

        }

    }
}
