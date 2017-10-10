using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Utils;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Entities
{

    public class TrendBreak : ITrendEvent
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public int TrendlineId { get; set; }
        public int IndexNumber { get; set; }
        public DataSet Item { get; set; }
        public string PreviousRangeGuid { get; set; }
        public string NextRangeGuid { get; set; }
        //public TrendRange PreviousRange { get; set; }
        //public TrendRange NextRange { get; set; }






        #region CONSTRUCTOR

        public TrendBreak(int trendlineId, int IndexNumber)
        {
            this.Guid = System.Guid.NewGuid().ToString();
            this.TrendlineId = trendlineId;
            this.IndexNumber = IndexNumber;
        }

        #endregion CONSTRUCTOR



        #region SYSTEM.OBJECT

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(TrendBreak)) return false;

            TrendBreak compared = (TrendBreak)obj;
            if (compared.Id != Id) return false;
            if ((compared.Guid == null && Guid != null) || (compared != null && !compared.Guid.Equals(Guid))) return false;
            if (compared.TrendlineId != TrendlineId) return false;
            if (compared.IndexNumber != IndexNumber) return false;
            if (compared.PreviousRangeGuid.Compare(PreviousRangeGuid) == false) return false;
            if (compared.NextRangeGuid.Compare(NextRangeGuid) == false) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "[TrendBreak] (" + Id + ") | trendlineId: " + this.TrendlineId + " | index: " + this.IndexNumber;
        }

        #endregion SYSTEM.OBJECT



        #region DTO


        public static TrendBreak FromDto(TrendBreakDto dto)
        {
            return new TrendBreak(dto.TrendlineId, dto.IndexNumber)
            {
                Id = dto.Id,
                Guid = dto.Guid,
                PreviousRangeGuid = dto.PreviousRangeGuid,
                NextRangeGuid = dto.NextRangeGuid
            };
        }

        public TrendBreakDto ToDto()
        {
            return new TrendBreakDto()
            {
                Id = this.Id,
                TrendlineId = this.TrendlineId,
                IndexNumber = this.IndexNumber,
                Guid = this.Guid,
                PreviousRangeGuid = this.PreviousRangeGuid,
                NextRangeGuid = this.NextRangeGuid
            };
        }


        #endregion DTO





    }

}
