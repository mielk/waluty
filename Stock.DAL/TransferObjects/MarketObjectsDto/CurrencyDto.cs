using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class CurrencyDto
    {
        [Key]
        public int Id { get; set; }

        [Column("CurrencyName")]
        public string Symbol { get; set; }

        [Column("CurrencyFullName")]
        public string Name { get; set; }

    }
}