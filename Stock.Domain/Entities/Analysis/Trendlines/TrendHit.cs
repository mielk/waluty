using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;
using Stock.Utils;
using Stock.DAL.TransferObjects;
using Stock.Domain.Enums;

namespace Stock.Domain.Entities
{
    public class TrendHit : ITrendEvent
    {

        public int Id { get; set; }
        public string Guid { get; set; }
        public int TrendlineId { get; set; }
        public int IndexNumber { get; set; }
        public ExtremumType ExtremumType { get; set; }
        public double Value { get; set; }
        public double DistanceToLine { get; set; }
        public string PreviousRangeGuid { get; set; }
        public string NextRangeGuid { get; set; }



        //        private const double TimeRangeWeight = 0.2d;
        //        private const double SlopeWeight = 1.0d;

        //        public Trendline Trendline { get; set; }
        //        public DataItem Item { get; set; }

        //        //punkt przecięcia linii trendu z ceną.
        //        public double CrossLevel { get; set; }
        //        public TrendlineType Type { get; set; }

        //        //
        //        public double CLHRatio { get; set; }
        //        public double ExtremumPoints { get; set; }

        //        //Scoring
        //        public double Score { get; set; }
        //        public double BounceScore { get; set; }
        //        public double TimeRangeScore { get; set; }
        //        public double SlopeScore { get; set; }
        //        public double EvaluationScore { get; set; }

        //        //Collections.
        //        public TrendHit PreviousHit { get; set; }
        //        public TrendBounce BounceFromPreviousHit { get; set; }
        //        public TrendHit NextHit { get; set; }
        //        public TrendBounce BounceToNextHit { get; set; }









        #region CONSTRUCTOR
        
        //        //public TrendHit(Trendline trendline) : this()
        //        //{
        //        //    this.Trendline = trendline;
        //        //}

        //        public TrendHit(Trendline trendline, DataItem item, double crossPoint, TrendlineType type)
        //        {
        //            Guid = System.Guid.NewGuid();
        //            this.Trendline = trendline;
        //            this.Item = item;
        //            this.CrossLevel = crossPoint;
        //            this.Type = type;
        //        }

        //        public TrendHit(Trendline trendline, DataItem item, double crossPoint, TrendlineType type, TrendHit prevHit, TrendBounce prevBounce)
        //            : this(trendline, item, crossPoint, type)
        //        {
        //            this.PreviousHit = prevHit;
        //            this.BounceFromPreviousHit = prevBounce;
        //        }

        public TrendHit(int trendlineId, int indexNumber, int extremumType) : this(trendlineId, indexNumber, (ExtremumType)extremumType)
        {
        }

        public TrendHit(int trendlineId, int indexNumber, ExtremumType extremumType)
        {
            this.Guid = System.Guid.NewGuid().ToString();
            this.TrendlineId = trendlineId;
            this.IndexNumber = indexNumber;
            this.ExtremumType = extremumType;
        }

        #endregion CONSTRUCTOR



        #region SYSTEM.OBJECT

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(TrendHit)) return false;

            TrendHit compared = (TrendHit)obj;
            if (compared.Id != Id) return false;
            if ((compared.Guid == null && Guid != null) || (compared != null && !compared.Guid.Equals(Guid))) return false;
            if (compared.TrendlineId != TrendlineId) return false;
            if (compared.IndexNumber != IndexNumber) return false;
            if (compared.ExtremumType != ExtremumType) return false;
            if (compared.Value != Value) return false;
            if (compared.DistanceToLine != DistanceToLine) return false;
            if ((compared.PreviousRangeGuid == null && PreviousRangeGuid != null) || (compared != null && !compared.PreviousRangeGuid.Equals(PreviousRangeGuid))) return false;
            if ((compared.NextRangeGuid == null && NextRangeGuid != null) || (compared != null && !compared.NextRangeGuid.Equals(NextRangeGuid))) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "(" + Id + ") | trendline: " + this.TrendlineId + " | index: " + this.IndexNumber;
        }

        #endregion SYSTEM.OBJECT



        #region DTO


        public static TrendHit FromDto(TrendHitDto dto)
        {
            return new TrendHit(dto.TrendlineId, dto.IndexNumber, dto.ExtremumType)
            {
                Id = dto.Id,
                Value = dto.Value,
                DistanceToLine = dto.DistanceToLine,
                Guid = dto.Guid,
                PreviousRangeGuid = dto.PreviousRangeGuid,
                NextRangeGuid = dto.NextRangeGuid
            };
        }

        public TrendHitDto ToDto()
        {
            return new TrendHitDto()
            {
                Id = this.Id,
                TrendlineId = this.TrendlineId,
                IndexNumber = this.IndexNumber,
                ExtremumType = (int)this.ExtremumType,
                Value = this.Value,
                DistanceToLine = this.DistanceToLine,
                Guid = this.Guid,
                PreviousRangeGuid = this.PreviousRangeGuid,
                NextRangeGuid = this.NextRangeGuid
            };
        }


        #endregion DTO




    }

}


//        public void Calculate()
//        {
//            this.Score = (1 + BounceScore) * (1 + SlopeScore * SlopeWeight) * 
//                (1 + TimeRangeScore * TimeRangeWeight) * (1 + EvaluationScore);
//        }


//        public bool IsConfirmed()
//        {
//            return Score > 0;
//        }