using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Short { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }


        public static Market FromDto(MarketDto dto)
        {

            var market = new Market();

            market.Id = dto.Id;
            market.Name = dto.Name;
            market.Short = dto.Short;
            market.StartTime = dto.StartTime;
            market.EndTime = dto.EndTime;

            return market;

        }

    }
}
