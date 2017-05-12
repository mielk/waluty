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
    public class PriceDto : IDataUnitDto
    {

        [Key]
        [Column("PriceId")]
        public int Id { get; set; }
        public int SimulationId { get; set; }
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        public DateTime PriceDate { get; set; }
        public double DeltaClosePrice { get; set; }
        public int PriceDirection2D { get; set; }
        public int PriceDirection3D { get; set; }
        public double PriceGap { get; set; }
        public double CloseRatio { get; set; }
        public double ExtremumRatio { get; set; }
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
            return AnalysisType.Prices;
        }

        #endregion GETTERS


        public void CopyProperties(PriceDto dto)
        {
            Id = dto.Id;
            AssetId = dto.AssetId;
            TimeframeId = dto.TimeframeId;
            PriceDate = dto.PriceDate;
            IndexNumber = dto.IndexNumber;
            DeltaClosePrice = dto.DeltaClosePrice;
            PriceDirection2D = dto.PriceDirection2D;
            PriceDirection3D = dto.PriceDirection3D;
            PriceGap = dto.PriceGap;
            CloseRatio = dto.CloseRatio;
            ExtremumRatio = dto.ExtremumRatio;
        }


        #region SYSTEM.OBJECT

        public override bool Equals(object obj)
        {
            const double MAX_VALUE_DIFFERENCE = 0.000000001d;
            if (obj == null) return false;
            if (obj.GetType() != typeof(PriceDto)) return false;

            PriceDto compared = (PriceDto)obj;
            if ((compared.Id) != Id) return false;
            if ((compared.SimulationId) != SimulationId) return false;
            if ((compared.IndexNumber) != IndexNumber) return false;
            if (compared.PriceDate.CompareTo(PriceDate) != 0) return false;
            if ((compared.AssetId) != AssetId) return false;
            if ((compared.TimeframeId) != TimeframeId) return false;
            if (!compared.DeltaClosePrice.CompareForTest(DeltaClosePrice, MAX_VALUE_DIFFERENCE)) return false;
            if ((compared.PriceDirection2D) != PriceDirection2D) return false;
            if ((compared.PriceDirection3D) != PriceDirection3D) return false;
            if (!compared.PriceGap.CompareForTest(PriceGap, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.CloseRatio.CompareForTest(CloseRatio, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.ExtremumRatio.CompareForTest(ExtremumRatio, MAX_VALUE_DIFFERENCE)) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return PriceDate.ToString() + " | " + TimeframeId + " | " + AssetId;
        }

        #endregion SYSTEM.OBJECT

    }
}