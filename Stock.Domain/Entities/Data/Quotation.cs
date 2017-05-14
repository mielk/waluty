using Stock.DAL.TransferObjects;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Utils;
using Stock.Core;

namespace Stock.Domain.Entities
{
    public class Quotation : IDataUnit
    {
        private const AnalysisType analysisType = AnalysisType.Quotations;
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public int IndexNumber { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsNew { get; set; }


        #region DTO
        public static Quotation FromDto(QuotationDto dto)
        {

            var quotation = new Quotation();
            quotation.Id = dto.QuotationId;
            quotation.Date = dto.PriceDate;
            quotation.AssetId = dto.AssetId;
            quotation.TimeframeId = dto.TimeframeId;
            quotation.Open = dto.OpenPrice;
            quotation.High = dto.HighPrice;
            quotation.Low = dto.LowPrice;
            quotation.Close = dto.ClosePrice;
            quotation.Volume = dto.Volume ?? 0;
            quotation.IndexNumber = dto.IndexNumber;
            return quotation;
        }

        public QuotationDto ToDto()
        {
            var dto = new QuotationDto
            {
                QuotationId = this.Id,
                AssetId = this.AssetId,
                IndexNumber = this.IndexNumber,
                PriceDate = this.Date,
                OpenPrice = this.Open,
                HighPrice = this.High,
                LowPrice = this.Low,
                ClosePrice = this.Close,
                Volume = this.Volume,
                TimeframeId = this.TimeframeId
            };

            return dto;

        }
        #endregion DTO


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

        #endregion GETTERS


        #region EXTREMA

        public double GetProperValue(ExtremumType type)
        {
            switch (type)
            {
                case ExtremumType.PeakByClose:
                    return Close;
                case ExtremumType.TroughByClose:
                    return Close;
                case ExtremumType.PeakByHigh:
                    return High;
                case ExtremumType.TroughByLow:
                    return Low;
                default:
                    return Close;
            }
        }

        public double GetOppositeValue(ExtremumType type)
        {
            switch (type)
            {
                case ExtremumType.PeakByClose:
                    return Low;
                case ExtremumType.TroughByClose:
                    return High;
                case ExtremumType.PeakByHigh:
                    return Low;
                case ExtremumType.TroughByLow:
                    return High;
                default:
                    return Close;
            }
        }

        #endregion EXTREMA


        public double GetProperValue(TrendlineType type)
        {
            if (type == TrendlineType.Resistance)
            {
                return High;
            }
            else
            {
                return Low;
            }
        }

        public double GetVolatility()
        {
            return (High - Low) / Open;
        }


        #region OBJECT METHODS

        public override bool Equals(object obj)
        {
            const double MAX_VALUE_DIFFERENCE = 0.000000001d;
            if (obj == null) return false;
            if (obj.GetType() != typeof(Quotation)) return false;

            Quotation compared = (Quotation)obj;
            if ((compared.Id) != Id) return false;
            if ((compared.IndexNumber) != IndexNumber) return false;
            if (compared.Date.CompareTo(Date) != 0) return false;
            if ((compared.AssetId) != AssetId) return false;
            if ((compared.TimeframeId) != TimeframeId) return false;
            if (!compared.Open.CompareForTest(Open, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.High.CompareForTest(High, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.Low.CompareForTest(Low, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.Close.CompareForTest(Close, MAX_VALUE_DIFFERENCE)) return false;
            if (!((double)compared.Volume).CompareForTest((double)Volume, MAX_VALUE_DIFFERENCE)) return false;
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

        #endregion OBJECT METHODS


    }
}
