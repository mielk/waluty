using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Utils;
using Stock.Core;

namespace Stock.DAL.TransferObjects
{
    public class QuotationDto : IDataUnitDto
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


        #region GETTERS

        public DateTime GetDate()
        {
            return PriceDate;
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
            return AnalysisType.Quotations;
        }

        public bool IsInIndexRange(int? startIndex, int? endIndex)
        {
            bool startIndexStatus, endIndexStatus;
            startIndexStatus = (startIndex == null ? true : IndexNumber >= startIndex);
            endIndexStatus = (endIndex == null ? true : IndexNumber <= endIndex);
            return startIndexStatus && endIndexStatus;
        }

        #endregion GETTERS


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


        #region SYSTEM.OBJECT

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(QuotationDto)) return false;

            QuotationDto compared = (QuotationDto)obj;
            if ((compared.QuotationId) != QuotationId) return false;
            if ((compared.IndexNumber) != IndexNumber) return false;
            if (compared.PriceDate.CompareTo(PriceDate) != 0) return false;
            if ((compared.AssetId) != AssetId) return false;
            if ((compared.TimeframeId) != TimeframeId) return false;
            if (!compared.OpenPrice.IsEqual(OpenPrice)) return false;
            if (!compared.HighPrice.IsEqual(HighPrice)) return false;
            if (!compared.LowPrice.IsEqual(LowPrice)) return false;
            if (!compared.ClosePrice.IsEqual(ClosePrice)) return false;
            if (!((double)compared.Volume).IsEqual((double)Volume)) return false;
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

        #endregion SYSTEM.OBJECT


    }
}
