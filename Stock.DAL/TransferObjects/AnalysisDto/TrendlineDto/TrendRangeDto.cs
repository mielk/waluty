using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Utils;

namespace Stock.DAL.TransferObjects
{
    public class TrendRangeDto
    {

        [Key]
        public int Id { get; set; }
        public string Guid { get; set; }
        public int TrendlineId { get; set; }
        public int StartIndex { get; set; }
        public int? EndIndex { get; set; }
        public int QuotationsCounter { get; set; }
        public double TotalDistance { get; set; }
        public string PreviousHitGuid { get; set; }
        public string PreviousBreakGuid { get; set; }
        public string NextHitGuid { get; set; }
        public string NextBreakGuid { get; set; }
        public double Value { get; set; }



        public void CopyProperties(TrendRangeDto dto)
        {
            Id = dto.Id;
            Guid = dto.Guid;
            TrendlineId = dto.TrendlineId;
            StartIndex = dto.StartIndex;
            EndIndex = dto.EndIndex;
            QuotationsCounter = dto.QuotationsCounter;
            TotalDistance = dto.TotalDistance;
            PreviousHitGuid = dto.PreviousHitGuid;
            PreviousBreakGuid = dto.PreviousBreakGuid;
            NextHitGuid = dto.NextHitGuid;
            NextBreakGuid = dto.NextBreakGuid;
            Value = dto.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(TrendRangeDto)) return false;

            TrendRangeDto compared = (TrendRangeDto)obj;
            if (!compared.Guid.Compare(Guid)) return false;
            if (compared.TrendlineId != TrendlineId) return false;
            if (compared.StartIndex != StartIndex) return false;
            if (compared.EndIndex != EndIndex) return false;
            if (compared.QuotationsCounter != QuotationsCounter) return false;
            if (!compared.TotalDistance.IsEqual(TotalDistance)) return false;
            if (!compared.PreviousHitGuid.Compare(PreviousHitGuid)) return false;
            if (!compared.PreviousBreakGuid.Compare(PreviousBreakGuid)) return false;
            if (!compared.NextHitGuid.Compare(NextHitGuid)) return false;
            if (!compared.NextBreakGuid.Compare(NextBreakGuid)) return false;
            if (!compared.Value.IsEqual(Value)) return false;
            return true;
        }



        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "[TrendRange] (" + Id + ") Trendline: " + TrendlineId;
        }



    }
}
