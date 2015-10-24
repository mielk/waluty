using Stock.DAL.TransferObjects;
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
        public object Date { get; set; }
        public int AssetId { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        //public double CloseDelta { get; set; }
        //public int Direction2D { get; set; }
        //public int Direction3D { get; set; }
        //public double PeakByClose { get; set; }
        //public double PeakByHigh { get; set; }
        //public double TroughByClose { get; set; }
        //public double TroughByLow { get; set; }


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

    }
}
