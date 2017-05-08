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
        public string ShortName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsAroundClock { get; set; }



        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(MarketDto)) return false;

            MarketDto compared = (MarketDto)obj;
            if ((compared.Id) != Id) return false;
            if (!compared.Name.Equals(Name)) return false;
            if (!compared.ShortName.Equals(ShortName)) return false;
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

