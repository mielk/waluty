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
    public class TrendHitDto
    {

        [Key]
        public int Id { get; set; }
        public string Guid { get; set; }
        public int TrendlineId { get; set; }
        public int IndexNumber { get; set; }
        public int ExtremumType { get; set; }
        public double Value { get; set; }
        public double DistanceToLine { get; set; }
        public string PreviousRangeGuid { get; set; }
        public string NextRangeGuid { get; set; }



        public void CopyProperties(TrendHitDto dto)
        {
            Id = dto.Id;
            Guid = dto.Guid;
            TrendlineId = dto.TrendlineId;
            IndexNumber = dto.IndexNumber;
            ExtremumType = dto.ExtremumType;
            Value = dto.Value;
            DistanceToLine = dto.DistanceToLine;
            PreviousRangeGuid = dto.PreviousRangeGuid;
            NextRangeGuid = dto.NextRangeGuid;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(TrendHitDto)) return false;

            TrendHitDto compared = (TrendHitDto)obj;
            if (((compared.Guid == null) != (Guid == null)) || (compared.Guid != null && !compared.Guid.Equals(Guid))) return false;
            if (compared.TrendlineId != TrendlineId) return false;
            if (compared.IndexNumber != IndexNumber) return false;
            if (compared.ExtremumType != ExtremumType) return false;
            if (!compared.Value.IsEqual(Value)) return false;
            if (!compared.DistanceToLine.IsEqual(DistanceToLine)) return false;
            if (((compared.PreviousRangeGuid == null) != (PreviousRangeGuid == null)) || (compared.PreviousRangeGuid != null && !compared.PreviousRangeGuid.Equals(PreviousRangeGuid))) return false;
            if (((compared.NextRangeGuid == null) != (NextRangeGuid == null)) || (compared.NextRangeGuid != null && !compared.NextRangeGuid.Equals(NextRangeGuid))) return false;
            return true;
        }



        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "(" + Id + ") Trendline: " + TrendlineId + " | IndexNumber: " + IndexNumber;
        }


    }

}