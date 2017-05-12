using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.DAL.TransferObjects
{
    public class SimulationDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }


        public void CopyProperties(SimulationDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            AssetId = dto.AssetId;
            TimeframeId = dto.TimeframeId;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(SimulationDto)) return false;

            SimulationDto compared = (SimulationDto)obj;
            if (!compared.Name.Equals(Name)) return false;
            if ((compared.AssetId) != AssetId) return false;
            if ((compared.TimeframeId) != TimeframeId) return false;
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
