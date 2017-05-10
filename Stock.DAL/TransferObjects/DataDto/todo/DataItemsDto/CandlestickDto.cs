using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.DAL.TransferObjects
{
    public class CandlestickDto : IDataUnitDto
    {

        [Key]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime PriceDate { get; set; }
        public int IndexNumber { get; set; }

        [NotMapped]
        public int TimeframeId { get; set; }


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
            return AnalysisType.Candlesticks;
        }

        #endregion GETTERS


    }
}
