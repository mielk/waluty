using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    
    public class MarketDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Short { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
