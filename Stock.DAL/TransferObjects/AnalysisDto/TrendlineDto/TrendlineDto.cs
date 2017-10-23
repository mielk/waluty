using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Stock.Utils;
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
        public bool InitialIsPeak { get; set; }
        public int FootholdIndex { get; set; }
        public double FootholdLevel { get; set; }
        public int FootholdSlaveIndex { get; set; }
        public int FootholdIsPeak { get; set; }
        public int? EndIndex { get; set; }
        public double Value { get; set; }
        public bool CurrentIsPeak { get; set; }
        public int LastUpdateIndex { get; set; }



        public void CopyProperties(TrendlineDto dto)
        {
            Id = dto.Id;
            AssetId = dto.AssetId;
            TimeframeId = dto.TimeframeId;
            SimulationId = dto.SimulationId;
            StartIndex = dto.StartIndex;
            StartLevel = dto.StartLevel;
            InitialIsPeak = dto.InitialIsPeak;
            EndIndex = dto.EndIndex;
            FootholdIndex = dto.FootholdIndex;
            FootholdLevel = dto.FootholdLevel;
            FootholdSlaveIndex = dto.FootholdSlaveIndex;
            FootholdIsPeak = dto.FootholdIsPeak;
            Value = dto.Value;
            LastUpdateIndex = dto.LastUpdateIndex;
            CurrentIsPeak = dto.CurrentIsPeak;
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
            if (compared.InitialIsPeak != InitialIsPeak) return false;
            if (!compared.EndIndex.IsEqual(EndIndex)) return false;
            if (compared.FootholdIndex != FootholdIndex) return false;
            if (compared.FootholdLevel != FootholdLevel) return false;
            if (compared.FootholdSlaveIndex != FootholdSlaveIndex) return false;
            if (compared.FootholdIsPeak != FootholdIsPeak) return false;
            if (compared.CurrentIsPeak != CurrentIsPeak) return false;
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
