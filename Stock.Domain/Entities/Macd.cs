using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;

namespace Stock.Domain.Entities
{
    public class Macd
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime Date { get; set; }
        public double Ma13 { get; set; }
        public double Ema13 { get; set; }
        public double Ma26 { get; set; }
        public double Ema26 { get; set; }
        public double MacdLine { get; set; }
        public double SignalLine { get; set; }
        public double Histogram { get; set; }
        public double HistogramAvg { get; set; }
        public double HistogramExtremum { get; set; }
        public double DeltaHistogram { get; set; }
        public int DeltaHistogramPositive { get; set; }
        public int DeltaHistogramNegative { get; set; }
        public int DeltaHistogramZero { get; set; }
        public int HistogramDirection2D { get; set; }
        public int HistogramDirection3D { get; set; }
        public int HistogramDirectionChanged { get; set; }
        public int HistogramToOx { get; set; }
        public int HistogramRow { get; set; }
        public double OxCrossing { get; set; }
        public int MacdPeak { get; set; }
        public double LastMacdPeak { get; set; }
        public double MacdPeakSlope { get; set; }
        public int MacdTrough { get; set; }
        public double LastMacdTrough { get; set; }
        public double MacdTroughSlope { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsNew { get; set; }


        public static Macd FromDto(MacdDto dto)
        {

            var macd = new Macd();
            macd.Id = dto.Id;
            macd.AssetId = dto.AssetId;
            macd.Date = dto.PriceDate;
            macd.Ma13 = dto.Ma13;
            macd.Ema13 = dto.Ema13;
            macd.Ma26 = dto.Ma26;
            macd.Ema26 = dto.Ema26;
            macd.MacdLine = dto.MacdLine;
            macd.SignalLine = dto.SignalLine;
            macd.Histogram = dto.Histogram;
            macd.HistogramAvg = dto.HistogramAvg;
            macd.HistogramExtremum = dto.HistogramExtremum;
            macd.DeltaHistogram = dto.DeltaHistogram;
            macd.DeltaHistogramPositive = dto.DeltaHistogramPositive;
            macd.DeltaHistogramNegative = dto.DeltaHistogramNegative;
            macd.DeltaHistogramZero = dto.DeltaHistogramZero;
            macd.HistogramDirection2D = dto.HistogramDirection2D;
            macd.HistogramDirection3D = dto.HistogramDirection3D;
            macd.HistogramDirectionChanged = dto.HistogramDirectionChanged;
            macd.HistogramToOx = dto.HistogramToOx;
            macd.HistogramRow = dto.HistogramRow;
            macd.OxCrossing = dto.OxCrossing;
            macd.MacdPeak = dto.MacdPeak;
            macd.LastMacdPeak = dto.LastMacdPeak;
            macd.MacdPeakSlope = dto.MacdPeakSlope;
            macd.MacdTrough = dto.MacdTrough;
            macd.LastMacdTrough = dto.LastMacdTrough;
            macd.MacdTroughSlope = dto.MacdTroughSlope;

            return macd;

        }

        public MacdDto ToDto()
        {
            var dto = new MacdDto
            {
                Id = this.Id,
                AssetId = this.AssetId,
                PriceDate = this.Date,
                Ma13 = this.Ma13,
                Ema13 = this.Ema13,
                Ma26 = this.Ma26,
                Ema26 = this.Ema26,
                MacdLine = this.MacdLine,
                SignalLine = this.SignalLine,
                Histogram = this.Histogram,
                HistogramAvg = this.HistogramAvg,
                HistogramExtremum = this.HistogramExtremum,
                DeltaHistogram = this.DeltaHistogram,
                DeltaHistogramPositive = this.DeltaHistogramPositive,
                DeltaHistogramNegative = this.DeltaHistogramNegative,
                DeltaHistogramZero = this.DeltaHistogramZero,
                HistogramDirection2D = this.HistogramDirection2D,
                HistogramDirection3D = this.HistogramDirection3D,
                HistogramDirectionChanged = this.HistogramDirectionChanged,
                HistogramToOx = this.HistogramToOx,
                HistogramRow = this.HistogramRow,
                OxCrossing = this.OxCrossing,
                MacdPeak = this.MacdPeak,
                LastMacdPeak = this.LastMacdPeak,
                MacdPeakSlope = this.MacdPeakSlope,
                MacdTrough = this.MacdTrough,
                LastMacdTrough = this.LastMacdTrough,
                MacdTroughSlope = this.MacdTroughSlope,
                TimebandId = 1
            };

            return dto;

        }

    }
}
