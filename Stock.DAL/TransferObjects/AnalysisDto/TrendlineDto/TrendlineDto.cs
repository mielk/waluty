using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class TrendlineDto
    {

        [Key]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        public int SimulationId { get; set; }
        public int StartIndex { get; set; }
        public double StartLevel { get; set; }
        public int EndIndex { get; set; }
        public double EndLevel { get; set; }
        public double Value { get; set; }
        public int LastUpdateIndex { get; set; }



        public void CopyProperties(TrendlineDto dto)
        {
            Id = dto.Id;
            AssetId = dto.AssetId;
            TimeframeId = dto.TimeframeId;
            SimulationId = dto.SimulationId;
            StartIndex = dto.StartIndex;
            StartLevel = dto.StartLevel;
            EndIndex = dto.EndIndex;
            EndLevel = dto.EndLevel;
            Value = dto.Value;
            LastUpdateIndex = dto.LastUpdateIndex;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(TrendlineDto)) return false;

            TrendlineDto compared = (TrendlineDto)obj;
            if (compared.AssetId != AssetId) return false;
            if (compared.TimeframeId != TimeframeId) return false;
            if (compared.SimulationId != SimulationId) return false;
            if (compared.StartIndex != StartIndex) return false;
            if (compared.StartLevel != StartLevel) return false;
            if (compared.EndIndex != EndIndex) return false;
            if (compared.EndLevel != EndLevel) return false;
            return true;
        }



        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "(" + Id + "): " + StartIndex.ToString() + " - " + EndIndex.ToString();
        }


    }
}
