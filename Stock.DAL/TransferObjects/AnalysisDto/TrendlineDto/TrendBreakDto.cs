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
    public class TrendBreakDto
    {
        [Key]
        public int Id { get; set; }
        public string Guid { get; set; }
        public int TrendlineId { get; set; }
        public int IndexNumber { get; set; }
        public string PreviousRangeGuid { get; set; }
        public string NextRangeGuid { get; set; }



        public void CopyProperties(TrendBreakDto dto)
        {
            Id = dto.Id;
            Guid = dto.Guid;
            TrendlineId = dto.TrendlineId;
            IndexNumber = dto.IndexNumber;
            PreviousRangeGuid = dto.PreviousRangeGuid;
            NextRangeGuid = dto.NextRangeGuid;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(TrendBreakDto)) return false;

            TrendBreakDto compared = (TrendBreakDto)obj;
            if (((compared.Guid == null) != (Guid == null)) || (compared.Guid != null && !compared.Guid.Equals(Guid))) return false;
            if (compared.TrendlineId != TrendlineId) return false;
            if (compared.IndexNumber != IndexNumber) return false;
            if (!compared.PreviousRangeGuid.Compare(PreviousRangeGuid)) return false;
            if (!compared.NextRangeGuid.Compare(NextRangeGuid)) return false;
            return true;
        }



        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "[TrendBreak] (" + Id + ") Trendline: " + TrendlineId + " | IndexNumber: " + IndexNumber;
        }


    }
}
