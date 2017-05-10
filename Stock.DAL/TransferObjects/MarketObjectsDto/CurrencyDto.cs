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

        [Column("CurrencySymbol")]
        public string Symbol { get; set; }

        [Column("CurrencyFullName")]
        public string Name { get; set; }



        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(CurrencyDto)) return false;

            CurrencyDto compared = (CurrencyDto)obj;
            if ((compared.Id) != Id) return false;
            if (!compared.Name.Equals(Name)) return false;
            if (!compared.Symbol.Equals(Symbol)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Symbol);
        }

    }
}