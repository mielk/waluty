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
    public class TimeframeDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PeriodCounter { get; set; }
        public string PeriodUnit { get; set; }
    }
}
