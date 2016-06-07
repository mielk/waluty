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
    public class FxPairDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseCurrency { get; set; }
        public int QuoteCurrency { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public const bool IsFx = true;

    }
}