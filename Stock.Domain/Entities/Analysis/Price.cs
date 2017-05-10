using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.Utils;

namespace Stock.Domain.Entities
{
    public class Price : IDataUnit
    {
        public int Id { get; set; }
        public int SimulationId { get; set; }
        public int TimeframeId { get; set; }
        public int AssetId { get; set; }
        public DateTime Date { get; set; }
        public int IndexNumber { get; set; }
        public double CloseDelta { get; set; }
        public int Direction2D { get; set; }
        public int Direction3D { get; set; }
        public double PriceGap { get; set; }
        public double PeakByClose { get; set; }
        public double PeakByHigh { get; set; }
        public double TroughByClose { get; set; }
        public double TroughByLow { get; set; }
        public double CloseRatio { get; set; }
        public double ExtremumRatio { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsNew { get; set; }
        public bool IsComplete { get; set; }


        #region CONSTRUCTORS

        public static Price FromDto(PriceDto dto)
        {
            var price = new Price();
            price.Id = dto.Id;
            price.Date = dto.PriceDate;
            price.AssetId = dto.AssetId;
            price.TimeframeId = dto.TimeframeId;
            price.IndexNumber = dto.IndexNumber;
            price.CloseDelta = dto.DeltaClosePrice;
            price.Direction2D = dto.PriceDirection2D;
            price.Direction3D = dto.PriceDirection3D;
            price.PriceGap = dto.PriceGap;
            price.PeakByClose = dto.PeakByCloseEvaluation;
            price.PeakByHigh = dto.PeakByHighEvaluation;
            price.TroughByClose = dto.TroughByCloseEvaluation;
            price.TroughByLow = dto.TroughByLowEvaluation;
            price.CloseRatio = dto.CloseRatio;
            price.ExtremumRatio = dto.ExtremumRatio;
            return price;
        }

        public PriceDto ToDto()
        {
            var dto = new PriceDto
            {
                AssetId = this.AssetId, 
                DeltaClosePrice = this.CloseDelta, 
                Id = this.Id, 
                IndexNumber = this.IndexNumber, 
                PriceDate = this.Date, 
                PriceGap = this.PriceGap, 
                PriceDirection2D = this.Direction2D, 
                PriceDirection3D = this.Direction3D, 
                PeakByCloseEvaluation = this.PeakByClose, 
                PeakByHighEvaluation = this.PeakByHigh, 
                TroughByCloseEvaluation = this.TroughByClose, 
                TroughByLowEvaluation = this.TroughByLow, 
                TimeframeId = this.TimeframeId, 
                CloseRatio = this.CloseRatio, 
                ExtremumRatio = this.ExtremumRatio
            };
            return dto;
        }

        #endregion CONSTRUCTORS


        #region GETTERS

        public DateTime GetDate()
        {
            return Date;
        }

        public int GetIndexNumber()
        {
            return IndexNumber;
        }

        #endregion GETTERS



        public void ApplyExtremumValue(ExtremumType type, Extremum extremum)
        {
            //switch (type)
            //{
            //    case ExtremumType.PeakByClose:
            //        PeakByCloseExtremum = extremum;
            //        PeakByClose = extremum == null ? 0 : extremum.Evaluate();
            //        break;
            //    case ExtremumType.PeakByHigh:
            //        PeakByHighExtremum= extremum;
            //        PeakByHigh = extremum == null ? 0 : extremum.Evaluate();
            //        break;
            //    case ExtremumType.TroughByClose:
            //        TroughByCloseExtremum = extremum;
            //        TroughByClose = extremum == null ? 0 : extremum.Evaluate();
            //        break;
            //    case ExtremumType.TroughByLow:
            //        TroughByLowExtremum = extremum;
            //        TroughByLow = extremum == null ? 0 : extremum.Evaluate();
            //        break;
            //}
        }

        public void ApplyExtremumValue(Extremum extremum)
        {
            ExtremumType type = extremum.Type;
            ApplyExtremumValue(type, extremum);
        }

        public bool IsExtremumByClosePrice()
        {
            return (PeakByClose > 0 || TroughByClose > 0);
        }

        public Extremum GetExtremumObject(ExtremumType type)
        {
            //switch (type)
            //{
                //case ExtremumType.PeakByClose:
                //    return PeakByCloseExtremum;
                //case ExtremumType.PeakByHigh:
                //    return PeakByHighExtremum;
                //case ExtremumType.TroughByClose:
                //    return TroughByCloseExtremum;
                //case ExtremumType.TroughByLow:
                //    return TroughByLowExtremum;
                //default:
                //    return null;
            //}
            return null;
        }

        public Extremum GetExtremumObject(bool isPeak, bool byClose)
        {
            return GetExtremumObject(Extrema.GetExtremumType(isPeak, byClose));
        }

        public bool IsExtremumOrPriceGap()
        {
            return (PeakByClose > 0 || PeakByHigh > 0 || TroughByClose > 0 || TroughByLow > 0 || PriceGap > 0);
        }

        public bool IsExtremum()
        {
            return (PeakByClose > 0 || PeakByHigh > 0 || TroughByClose > 0 || TroughByLow > 0);
        }

        public bool IsExtremum(TrendlineType type)
        {
            return type == TrendlineType.Resistance ? IsPeak() : IsTrough();
        }

        

        public bool IsPeak()
        {
            return (PeakByHigh > 0 || PeakByClose > 0);
        }

        public bool IsTrough()
        {
            return (TroughByLow > 0 || TroughByClose > 0);
        }

        public bool IsGap()
        {
            return PriceGap != 0;
        }

        public double ExtremumEvaluation(TrendlineType type)
        {

            var value = 0d;

            if (type == TrendlineType.Support)
            {
                value = Math.Max(TroughByClose, TroughByLow);
            }
            else if (type == TrendlineType.Resistance)
            {
                value = Math.Max(PeakByClose, PeakByHigh);
            }


            return Math.Max(value, PriceGap);

        }

        public TrendlineType toTrendlineType()
        {
            if (PeakByClose > 0 || PeakByHigh > 0)
            {
                return TrendlineType.Resistance;
            }
            else if (TroughByClose > 0 || TroughByLow > 0)
            {
                return TrendlineType.Support;
            }
            else
            {
                return TrendlineType.None;
            }
        }

        public bool IsMatch(TrendlineType type)
        {

            if (type == TrendlineType.Resistance)
            {
                return (PeakByClose > 0 || PeakByHigh > 0);
            }
            else if (type == TrendlineType.Support)
            {
                return (TroughByClose > 0 || TroughByLow > 0);
            }
            else
            {
                return true;
            }

        }


        public double calculateTrendlineQuotationPoints(Trendline trendline)
        {
            return 0;
        }


        public ExtremumGroup GetExtremumGroup()
        {
        //{ get; set; }
        //public Extremum PeakByHighExtremum { get; set; }
        //public Extremum TroughByCloseExtremum { get; set; }
        //public Extremum TroughByLowExtremum { get; set; }

            //if (PeakByCloseExtremum != null){
            //    //return PeakByCloseExtremum.gr
            //} else (IsTrough()){
            //}

            return null;

        }



        public override bool Equals(object obj)
        {
            const double MAX_VALUE_DIFFERENCE = 0.000000001d;
            if (obj == null) return false;
            if (obj.GetType() != typeof(Price)) return false;

            Price compared = (Price)obj;
            if ((compared.Id) != Id) return false;
            if ((compared.SimulationId) != SimulationId) return false;
            if ((compared.IndexNumber) != IndexNumber) return false;
            if (compared.Date.CompareTo(Date) != 0) return false;
            if ((compared.AssetId) != AssetId) return false;
            if ((compared.TimeframeId) != TimeframeId) return false;
            if (!compared.CloseDelta.CompareForTest(CloseDelta, MAX_VALUE_DIFFERENCE)) return false;
            if ((compared.Direction2D) != Direction2D) return false;
            if ((compared.Direction3D) != Direction3D) return false;
            if (!compared.PriceGap.CompareForTest(PriceGap, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.PeakByClose.CompareForTest(PeakByClose, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.PeakByHigh.CompareForTest(PeakByHigh, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.TroughByClose.CompareForTest(TroughByClose, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.TroughByLow.CompareForTest(TroughByLow, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.CloseRatio.CompareForTest(CloseRatio, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.ExtremumRatio.CompareForTest(ExtremumRatio, MAX_VALUE_DIFFERENCE)) return false;
            if (compared.IsUpdated != IsUpdated) return false;
            if (compared.IsNew != IsNew) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Date.ToString() + " | " + TimeframeId + " | " + AssetId;
        }



    }

}
