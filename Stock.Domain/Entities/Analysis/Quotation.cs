using Stock.DAL.TransferObjects;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class Quotation : IDataUnit
    {
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
        #endregion GETTERS



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

        public double GetProperValue(ExtremumType extremumType)
        {
            switch (extremumType)
            {
                case ExtremumType.PeakByClose: 
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

        public double GetVolatility()
        {
            return (High - Low) / Open;
        }


    }
}
