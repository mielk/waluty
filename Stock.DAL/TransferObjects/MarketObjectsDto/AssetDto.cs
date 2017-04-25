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
    public class AssetDto
    {
        [Key]
        public int Id { get; set; }
        public string Symbol { get; set; }
        public int MarketId { get; set; }        
    }
}
