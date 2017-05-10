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


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(AssetDto)) return false;

            AssetDto compared = (AssetDto)obj;
            if ((compared.Id) != Id) return false;
            if (!compared.Symbol.Equals(Symbol)) return false;
            if ((compared.MarketId) != MarketId) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Symbol;
        }


    }
}
