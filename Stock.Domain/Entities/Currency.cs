using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

        public Currency(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public static Currency FromDto(CurrencyDto dto)
        {

            var currency = new Currency(dto.Id, dto.Name);
            currency.FullName = dto.FullName;

            return currency;

        }

    }
}
