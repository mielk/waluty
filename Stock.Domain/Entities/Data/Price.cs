using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.Utils;
using Stock.Core;

namespace Stock.Domain.Entities
{
    public class Price : IDataUnit
    {
        private const AnalysisType analysisType = AnalysisType.Prices;
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
        public Extremum PeakByClose { get; set; }
        public Extremum PeakByHigh { get; set; }
        public Extremum TroughByClose { get; set; }
        public Extremum TroughByLow { get; set; }
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

        public int GetAssetId()
        {
            return AssetId;
        }

        public int GetTimeframeId()
        {
            return TimeframeId;
        }

        public AnalysisType GetAnalysisType()
        {
            return analysisType;
        }

        public Extremum GetExtremum(ExtremumType type)
        {
            switch (type)
            {
                case ExtremumType.PeakByClose: return PeakByClose;
                case ExtremumType.PeakByHigh: return PeakByHigh;
                case ExtremumType.TroughByClose: return TroughByClose;
                case ExtremumType.TroughByLow: return TroughByLow;
                default: return null;
            }
        }

        public IEnumerable<Extremum> GetExtrema()
        {
            List<Extremum> extrema = new List<Extremum>();
            if (PeakByClose != null) extrema.Add(PeakByClose);
            if (PeakByHigh != null) extrema.Add(PeakByHigh);
            if (TroughByClose != null) extrema.Add(TroughByClose);
            if (TroughByLow != null) extrema.Add(TroughByLow);
            return extrema;
        }

        public object GetJson()
        {
            return new
            {
                analysisType = (int)analysisType,
                id = Id,
                simulationId = SimulationId,
                assetId = AssetId,
                timeframeId = TimeframeId,
                date = Date,
                indexNumber = IndexNumber,
                closeDelta = CloseDelta,
                direction2D = Direction2D,
                direction3D = Direction3D,
                priceGap = PriceGap,
                peakByClose = PeakByClose,
                peakByHigh = PeakByHigh,
                troughByClose = TroughByClose,
                troughByLow = TroughByLow,
                closeRatio = CloseRatio,
                extremumRatio = ExtremumRatio,
                isUpdated = IsUpdated,
                isNew = IsNew,
                isComplete = IsComplete
            };
        }


        #endregion GETTERS


        #region SETTERS

        public void SetExtremum(Extremum extremum)
        {
            switch (extremum.Type)
            {
                case ExtremumType.PeakByClose: this.PeakByClose = extremum; break;
                case ExtremumType.PeakByHigh: this.PeakByHigh = extremum; break;
                case ExtremumType.TroughByClose: this.TroughByClose = extremum; break;
                case ExtremumType.TroughByLow: this.TroughByLow = extremum; break;
            }
        }

        #endregion SETTERS


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
            //return (PeakByClose > 0 || TroughByClose > 0);
            return false;
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
            //return (PeakByClose > 0 || PeakByHigh > 0 || TroughByClose > 0 || TroughByLow > 0 || PriceGap > 0);
            return false;
        }

        public bool IsExtremum()
        {
            //return (PeakByClose > 0 || PeakByHigh > 0 || TroughByClose > 0 || TroughByLow > 0);
            return false;
        }

        public bool IsExtremum(TrendlineType type)
        {
            return type == TrendlineType.Resistance ? IsPeak() : IsTrough();
        }

        

        public bool IsPeak()
        {
            //return (PeakByHigh > 0 || PeakByClose > 0);
            return false;
        }

        public bool IsTrough()
        {
            //return (TroughByLow > 0 || TroughByClose > 0);
            return false;
        }

        public bool IsGap()
        {
            return PriceGap != 0;
        }

        public double ExtremumEvaluation(TrendlineType type)
        {

            //var value = 0d;

            //if (type == TrendlineType.Support)
            //{
            //    value = Math.Max(TroughByClose, TroughByLow);
            //}
            //else if (type == TrendlineType.Resistance)
            //{
            //    value = Math.Max(PeakByClose, PeakByHigh);
            //}


            //return Math.Max(value, PriceGap);

            return 0;

        }

        public TrendlineType toTrendlineType()
        {
            //if (PeakByClose > 0 || PeakByHigh > 0)
            //{
            //    return TrendlineType.Resistance;
            //}
            //else if (TroughByClose > 0 || TroughByLow > 0)
            //{
            //    return TrendlineType.Support;
            //}
            //else
            //{
            //    return TrendlineType.None;
            //}
            return TrendlineType.None;
        }

        public bool IsMatch(TrendlineType type)
        {

            //if (type == TrendlineType.Resistance)
            //{
            //    return (PeakByClose > 0 || PeakByHigh > 0);
            //}
            //else if (type == TrendlineType.Support)
            //{
            //    return (TroughByClose > 0 || TroughByLow > 0);
            //}
            //else
            //{
            //    return true;
            //}
            return false;

        }


        //public double calculateTrendlineQuotationPoints(Trendline trendBreak)
        //{
        //    return 0;
        //}


        //public ExtremumGroup GetExtremumGroup()
        //{
        ////{ get; set; }
        ////public Extremum PeakByHighExtremum { get; set; }
        ////public Extremum TroughByCloseExtremum { get; set; }
        ////public Extremum TroughByLowExtremum { get; set; }

        //    //if (PeakByCloseExtremum != null){
        //    //    //return PeakByCloseExtremum.gr
        //    //} else (IsTrough()){
        //    //}

        //    return null;

        //}



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
            if (!compared.CloseDelta.IsEqual(CloseDelta)) return false;
            if ((compared.Direction2D) != Direction2D) return false;
            if ((compared.Direction3D) != Direction3D) return false;
            if (!compared.PriceGap.IsEqual(PriceGap)) return false;
            if (PeakByClose == null && compared.GetExtremum(ExtremumType.PeakByClose) != null) return false;
            if (PeakByClose != null && !PeakByClose.Equals(compared.PeakByClose)) return false;
            if (PeakByHigh == null && compared.GetExtremum(ExtremumType.PeakByHigh) != null) return false;
            if (PeakByHigh != null && !PeakByHigh.Equals(compared.PeakByHigh)) return false;
            if (TroughByClose == null && compared.GetExtremum(ExtremumType.TroughByClose) != null) return false;
            if (TroughByClose != null && !TroughByClose.Equals(compared.TroughByClose)) return false;
            if (TroughByLow == null && compared.GetExtremum(ExtremumType.TroughByLow) != null) return false;
            if (TroughByLow != null && !TroughByLow.Equals(compared.TroughByLow)) return false;
            if (!compared.CloseRatio.IsEqual(CloseRatio)) return false;
            if (!compared.ExtremumRatio.IsEqual(ExtremumRatio)) return false;
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
