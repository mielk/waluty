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
    public class QuotationDto : IDateItemDto
    {
        [Key]
        public int QuotationId { get; set; }
        public DateTime PriceDate { get; set; }
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        public double OpenPrice { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        public double ClosePrice { get; set; }
        public double? RealClosePrice { get; set; }
        public double? Volume { get; set; }
        public int IndexNumber { get; set; }

        public DateTime GetDate()
        {
            return PriceDate;
        }


        public void CopyProperties(QuotationDto dto)
        {
            QuotationId = dto.QuotationId;
            PriceDate = dto.PriceDate;
            AssetId = dto.AssetId;
            TimeframeId = dto.TimeframeId;
            OpenPrice = dto.OpenPrice;
            HighPrice = dto.HighPrice;
            LowPrice = dto.LowPrice;
            ClosePrice = dto.ClosePrice;
            RealClosePrice = dto.RealClosePrice;
            Volume = dto.Volume;
            IndexNumber = dto.IndexNumber;
        }


        public override bool Equals(object obj)
        {
            const double MAX_VALUE_DIFFERENCE = 0.000000001d;
            if (obj.GetType() != typeof(QuotationDto)) return false;

            QuotationDto compared = (QuotationDto)obj;
            if ((compared.QuotationId) != QuotationId) return false;
            if ((compared.IndexNumber) != IndexNumber) return false;
            if (compared.PriceDate.CompareTo(PriceDate) != 0) return false;
            if ((compared.AssetId) != AssetId) return false;
            if ((compared.TimeframeId) != TimeframeId) return false;
            if (compared.OpenPrice.CompareForTest(OpenPrice, MAX_VALUE_DIFFERENCE));
            if (compared.HighPrice.CompareForTest(OpenPrice, MAX_VALUE_DIFFERENCE));
            if (compared.LowPrice.CompareForTest(OpenPrice, MAX_VALUE_DIFFERENCE));
            if (compared.ClosePrice.CompareForTest(OpenPrice, MAX_VALUE_DIFFERENCE));
            if (((double)compared.Volume).CompareForTest((double)Volume, MAX_VALUE_DIFFERENCE));
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return PriceDate.ToString() + " | " + TimeframeId + " | "  + AssetId;
        }


    }
}
