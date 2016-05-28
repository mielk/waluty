using Stock.DAL.TransferObjects;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class Quotation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AssetId { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsNew { get; set; }


        public bool IsComplete()
        {
            return Open > -1;
        }

        public void CompleteMissing(Quotation quotation)
        {
            var price = quotation.Close;
            Open = price;
            High = price;
            Low = price;
            Close = price;
            Volume = -1;
            IsUpdated = true;
        }

        public double ProperValue(ExtremumType extremumType)
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

        public double Volatility()
        {
            return (High - Low) / Open;
        }

        public static Quotation FromDto(QuotationDto dto)
        {

            var quotation = new Quotation();
            quotation.Id = dto.Id;
            quotation.Date = dto.PriceDate;
            quotation.AssetId = dto.AssetId;
            quotation.Open = dto.OpenPrice;
            quotation.High = dto.HighPrice;
            quotation.Low = dto.LowPrice;
            quotation.Close = dto.ClosePrice;
            quotation.Volume = dto.Volume;


            return quotation;

        }

        public QuotationDto ToDto()
        {
            var dto = new QuotationDto
            {
                
                Id = this.Id,
                AssetId = this.AssetId,
                PriceDate = this.Date,
                OpenPrice = this.Open,
                HighPrice = this.High,
                LowPrice = this.Low,
                ClosePrice = this.Close,
                Volume = this.Volume,
                TimeframeId = 1
            };

            return dto;

        }

    }
}
