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


        public Company(int id, string name) : base(id, name)
        {

        }


        public static Company FromDto(CompanyDto dto)
        {

            var company = new Company(dto.Id, dto.PairName);
            company.IdMarket = dto.IdMarket;
            company.Short = dto.Short;

            return company;

        }

    }
}
