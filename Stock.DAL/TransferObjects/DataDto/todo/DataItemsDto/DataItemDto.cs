using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.DAL.TransferObjects
{
    public class DataItemDto : IDataUnitDto
    {

        public int AssetId { get; set; }

        [NotMapped]
        public string Timeframe { get; set; }
        public DateTime PriceDate { get; set; }
        //public QuotationDto Quotation { get; set; }
        public int QuotationId { get; set; }
        public double OpenPrice { get; set; }
        public double LowPrice { get; set; }
        public double HighPrice { get; set; }
        public double ClosePrice { get; set; }
        public int? Volume { get; set; }
        public int IndexNumber { get; set; }

        public PriceDto Price { get; set; }
        public MacdDto Macd { get; set; }
        public AdxDto Adx { get; set; }
        public CandlestickDto Candlestick { get; set; }



        #region GETTERS

        public DateTime GetDate()
        {
            return PriceDate;
        }

        public int GetIndexNumber()
        {
            //return IndexNumber;
            return 0;
        }

        public int GetAssetId()
        {
            return AssetId;
        }

        public int GetTimeframeId()
        {
            return 0;
        }

        public AnalysisType GetAnalysisType()
        {
            return AnalysisType.Quotations;
        }

        #endregion GETTERS


    }

}