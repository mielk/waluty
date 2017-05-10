using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class FxPairDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseCurrency { get; set; }
        public int QuoteCurrency { get; set; }
        public bool IsActive { get; set; }
    

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(FxPairDto)) return false;

            FxPairDto compared = (FxPairDto)obj;
            if ((compared.Id) != Id) return false;
            if (!compared.Name.Equals(Name)) return false;
            if (compared.BaseCurrency != BaseCurrency) return false;
            if (compared.QuoteCurrency != QuoteCurrency) return false;
            if (compared.IsActive != IsActive) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

    }

}