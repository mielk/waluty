using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Stock.DAL.TransferObjects
{
    public class DataItemDto : IDateItem
    {

        public int AssetId { get; set; }

        [NotMapped]
        public string Timeband { get; set; }
        public DateTime PriceDate { get; set; }
        //public QuotationDto Quotation { get; set; }
        public int QuotationId { get; set; }
        public double OpenPrice { get; set; }
        public double LowPrice { get; set; }
        public double HighPrice { get; set; }
        public double ClosePrice { get; set; }
        public int? Volume { get; set; }

        //public PriceDto Price { get; set; }
        public int? PriceId { get; set; }
        public double? DeltaClosePrice { get; set; }
        public int? PriceDirection2D { get; set; }
        public int? PriceDirection3D { get; set; }
        public double? PeakByCloseEvaluation { get; set; }
        public double? PeakByHighEvaluation { get; set; }
        public double? TroughByCloseEvaluation { get; set; }
        public double? TroughByLowEvaluation { get; set; }

    }
}
