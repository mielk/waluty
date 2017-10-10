using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class TrendRange
    {

        public int Id { get; set; }
        public string Guid { get; set; }
        public int TrendlineId { get; set; }
        public int StartIndex { get; set; }
        public int? EndIndex { get; set; }
        public int QuotationsCounter { get; set; }
        public double TotalDistance { get; set; }
        public double MaxDistance { get; set; }
        public string PreviousHitGuid { get; set; }
        public string PreviousBreakGuid { get; set; }
        public string NextHitGuid { get; set; }
        public string NextBreakGuid { get; set; }
        public double Value { get; set; }


        ////Objects
        //public Trendline Trendline { get; set; }
        //public ITrendEvent StartEvent { get; set; }
        //public ITrendEvent EndEvent { get; set; }
        //public TrendRange PreviousRange { get; set; }
        //public TrendRange NextRange { get; set; }


        

        #region CONSTRUCTOR

        public TrendRange(int trendlineId, int startIndex)
        {
            this.Guid = System.Guid.NewGuid().ToString();
            this.TrendlineId = trendlineId;
            this.StartIndex = startIndex;
        }

        #endregion CONSTRUCTOR



        #region SYSTEM.OBJECT

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(TrendRange)) return false;

            TrendRange compared = (TrendRange)obj;
            if (compared.Id != Id) return false;
            if ((compared.Guid == null && Guid != null) || (compared != null && !compared.Guid.Equals(Guid))) return false;
            if (compared.TrendlineId != TrendlineId) return false;
            if (compared.StartIndex != StartIndex) return false;
            if (compared.EndIndex != EndIndex) return false;
            if (compared.QuotationsCounter != QuotationsCounter) return false;
            if (compared.TotalDistance != TotalDistance) return false;
            if (compared.MaxDistance != MaxDistance) return false;
            if (compared.PreviousHitGuid != PreviousHitGuid) return false;
            if (compared.PreviousBreakGuid != PreviousBreakGuid) return false;
            if (compared.NextHitGuid != NextHitGuid) return false;
            if (compared.NextBreakGuid != NextBreakGuid) return false;            
            if (compared.Value != Value) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "(" + Id + ") | TrendRange: " + this.TrendlineId;
        }

        #endregion SYSTEM.OBJECT



        #region DTO


        public static TrendRange FromDto(TrendRangeDto dto)
        {
            return new TrendRange(dto.TrendlineId, dto.StartIndex)
            {
                Id = dto.Id,
                Guid = dto.Guid,
                EndIndex = dto.EndIndex,
                QuotationsCounter = dto.QuotationsCounter,
                TotalDistance = dto.TotalDistance,
                MaxDistance = 0,
                PreviousBreakGuid = dto.PreviousBreakGuid,
                PreviousHitGuid = dto.PreviousHitGuid,
                NextBreakGuid = dto.NextBreakGuid,
                NextHitGuid = dto.NextHitGuid,
                Value = dto.Value
            };
        }

        public TrendRangeDto ToDto()
        {
            return new TrendRangeDto()
            {
                Id = this.Id,
                Guid = this.Guid,
                TrendlineId = this.TrendlineId,
                StartIndex = this.StartIndex,
                EndIndex = this.EndIndex,
                QuotationsCounter = this.QuotationsCounter,
                TotalDistance = this.TotalDistance,
                PreviousBreakGuid = this.PreviousBreakGuid,
                PreviousHitGuid = this.PreviousHitGuid,
                NextBreakGuid = this.NextBreakGuid,
                NextHitGuid = this.NextHitGuid,
                Value = this.Value
            };
        }


        #endregion DTO















        //public TrendBounce(Trendline TrendRange)
        //{
        //    this.Trendline = TrendRange;
        //    this.breaks = new List<TrendBreak>();
        //}

        //public TrendBounce(Trendline TrendRange, TrendRange startHit)
        //{
        //    this.Trendline = TrendRange;
        //    this.StartHit = startHit;
        //    this.breaks = new List<TrendBreak>();
        //}

        //public void AddBreak(TrendBreak trendBreak)
        //{
        //    this.breaks.Add(trendBreak);
        //}

        //public void AddExtremumBreak(double value)
        //{
        //    if (value > 0)
        //    {
        //        this.breaksByExtremum++;
        //        this.breaksByExtremumPoints += value;
        //    }
        //}

        //public void AddQuotation(Trendline TrendRange, DataItem item)
        //{
        //    this.length++;
        //    this.pointsForQuotations += calculatePointForQuotation(TrendRange, item);
        //}


        //private double calculatePointForQuotation(Trendline TrendRange, DataItem item)
        //{
        //    return 0;
        //}


        //public void Calculate()
        //{

        //}

    }
}
