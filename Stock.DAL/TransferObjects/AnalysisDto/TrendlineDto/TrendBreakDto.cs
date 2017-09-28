using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class TrendBreakDto
    {
        [Key]
        public int Id { get; set; }
        public int TrendlineId { get; set; }
        public int IndexNumber { get; set; }
    }
}
