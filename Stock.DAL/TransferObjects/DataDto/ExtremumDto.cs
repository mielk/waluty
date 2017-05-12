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
    public class ExtremumDto : IDataUnitDto
    {

        [Key]
        [Column("ExtremumId")]
        public int Id { get; set; }
        public int SimulationId { get; set; }
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        public int IndexNumber { get; set; }
        public double Value { get; set; }
        [Column("ExtremumDate")]
        public DateTime Date { get; set; }
        public DateTime LastCheckedDateTime { get; set; }
        [Column("Type")]
        public int ExtremumType { get; set; }
        public double Volatility { get; set; }
        public int EarlierCounter { get; set; }
        public double EarlierAmplitude { get; set; }
        public double EarlierChange1 { get; set; }
        public double EarlierChange2 { get; set; }
        public double EarlierChange3 { get; set; }
        public double EarlierChange5 { get; set; }
        public double EarlierChange10 { get; set; }
        public int LaterCounter { get; set; }
        public double LaterAmplitude { get; set; }
        public double LaterChange1 { get; set; }
        public double LaterChange2 { get; set; }
        public double LaterChange3 { get; set; }
        public double LaterChange5 { get; set; }
        public double LaterChange10 { get; set; }
        public bool IsOpen { get; set; }
        public DateTime Timestamp { get; set; }


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
            return AnalysisType.Prices;
        }

        #endregion GETTERS


        public void CopyProperties(ExtremumDto dto)
        {
            Id = dto.Id;
            SimulationId = dto.SimulationId;
            AssetId = dto.AssetId;
            TimeframeId = dto.TimeframeId;
            Date = dto.Date;
            IndexNumber = dto.IndexNumber;
            ExtremumType = dto.ExtremumType;
            Volatility = dto.Volatility;
            EarlierCounter = dto.EarlierCounter;
            EarlierAmplitude = dto.EarlierAmplitude;
            EarlierChange1 = dto.EarlierChange1;
            EarlierChange2 = dto.EarlierChange2;
            EarlierChange3 = dto.EarlierChange3;
            EarlierChange5 = dto.EarlierChange5;
            EarlierChange10 = dto.EarlierChange10;
            LaterCounter = dto.LaterCounter;
            LaterAmplitude = dto.LaterAmplitude;
            LaterChange1 = dto.LaterChange1;
            LaterChange2 = dto.LaterChange2;
            LaterChange3 = dto.LaterChange3;
            LaterChange5 = dto.LaterChange5;
            LaterChange10 = dto.LaterChange10;
            IsOpen = dto.IsOpen;
            Timestamp = dto.Timestamp;
            Value = dto.Value;
        }


        #region SYSTEM.OBJECT

        public override bool Equals(object obj)
        {
            const double MAX_VALUE_DIFFERENCE = 0.0001d;
            if (obj == null) return false;
            if (obj.GetType() != typeof(ExtremumDto)) return false;

            ExtremumDto compared = (ExtremumDto)obj;
            if ((compared.SimulationId) != SimulationId) return false;
            if ((compared.IndexNumber) != IndexNumber) return false;
            if (compared.Date.CompareTo(Date) != 0) return false;
            if ((compared.AssetId) != AssetId) return false;
            if ((compared.TimeframeId) != TimeframeId) return false;
            if ((compared.ExtremumType) != ExtremumType) return false;
            if (!compared.Volatility.CompareForTest(Volatility, MAX_VALUE_DIFFERENCE)) return false;
            if ((compared.EarlierCounter) != EarlierCounter) return false;
            if (!compared.EarlierAmplitude.CompareForTest(EarlierAmplitude, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.EarlierChange1.CompareForTest(EarlierChange1, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.EarlierChange2.CompareForTest(EarlierChange2, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.EarlierChange3.CompareForTest(EarlierChange3, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.EarlierChange5.CompareForTest(EarlierChange5, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.EarlierChange10.CompareForTest(EarlierChange10, MAX_VALUE_DIFFERENCE)) return false;
            if ((compared.LaterCounter) != LaterCounter) return false;
            if (!compared.LaterAmplitude.CompareForTest(LaterAmplitude, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.LaterChange1.CompareForTest(LaterChange1, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.LaterChange2.CompareForTest(LaterChange2, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.LaterChange3.CompareForTest(LaterChange3, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.LaterChange5.CompareForTest(LaterChange5, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.LaterChange10.CompareForTest(LaterChange10, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.Value.CompareForTest(Value, MAX_VALUE_DIFFERENCE)) return false;
            if ((compared.IsOpen) != IsOpen) return false;
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

        #endregion SYSTEM.OBJECT

    }
}