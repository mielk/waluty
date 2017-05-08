using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class CandlestickDto : IDateItemDto
    {

        [Key]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime PriceDate { get; set; }

        [NotMapped]
        public int TimeframeId { get; set; }

        public DateTime GetDate()
        {
            return PriceDate;
        }

    }
}
