using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;

namespace Stock.Domain.Entities
{
    public class DataItem
    {
        public int AssetId { get; set; }
        public Timeband Timeband { get; set; }
        public DateTime Date { get; set; }
        public Quotation Quotation { get; set; }
        public Price Price { get; set; }
        public Macd Macd { get; set; }
        public Adx Adx { get; set; }

        public int Index { get; set; }


        /* Covered with unit tests. */
        public double GetValue(bool isPeak, bool byClose)
        {

            if (byClose)
            {
                return Quotation.Close;
            }
            else
            {
                return isPeak ? Quotation.High: Quotation.Low;
            }

        }


        public int Distance(DataItem item)
        {
            return this.Index - item.Index;
        }

        public bool IsExtremum(TrendlineType type)
        {
            if (this.Price != null)
            {
                return this.Price.IsExtremum(type);
            }

            return false;

        }

        public double TrendlineAnalysisStep()
        {
            var sign = (Math.Max(Price.PeakByClose, Price.PeakByHigh) > 
                                                                Math.Max(Price.TroughByClose, Price.TroughByLow)) 
                                                                ? 1 : -1;
            return sign * (Quotation.High - Quotation.Close) / 10d;
        }



        /* Covered with unit tests. */
        public double GetOpenOrClosePrice(TrendlineType type)
        {
            return (type == TrendlineType.Support) ? Math.Min(Quotation.Open, Quotation.Close) : Math.Max(Quotation.Open, Quotation.Close);
        }

        /* Covered with unit tests. */
        public double GetHighOrLowPrice(TrendlineType type)
        {
            return (type == TrendlineType.Support) ? Quotation.Low : Quotation.High;
        }

        /* Covered with unit tests. */
        public double GetClosestDistance(TrendlineType type, double level)
        {

            double closeDistance = Math.Abs(GetOpenOrClosePrice(type) - level);
            double exDistance = Math.Abs(GetHighOrLowPrice(type) - level);
            return Math.Min(closeDistance, exDistance);

        }



        public static DataItem FromDto(DataItemDto dto)
        {
            var item = new DataItem();
            item.AssetId = dto.AssetId;
            item.Date = dto.PriceDate;
            item.Timeband = Timeband.GetTimebandByShortName(dto.Timeband);

            if (dto.QuotationId > 0)
            {
                item.Quotation = new Quotation
                {
                    AssetId = dto.AssetId,
                    Date = dto.PriceDate,
                    Open = dto.OpenPrice,
                    Low = dto.LowPrice,
                    High = dto.HighPrice,
                    Close = dto.ClosePrice,
                    Volume = dto.Volume ?? 0,
                    Id = dto.QuotationId
                };
            }


            //Create Price stats for this data item.
            if (dto.PriceId != null && dto.PriceId > 0)
            {
                item.Price = new Price
                {
                    AssetId = dto.AssetId,
                    CloseDelta = (double)dto.DeltaClosePrice,
                    Date = dto.PriceDate,
                    Direction2D = (int)dto.PriceDirection2D,
                    Direction3D = (int)dto.PriceDirection3D,
                    Id = (int)dto.PriceId,
                    PeakByClose = (double)dto.PeakByCloseEvaluation,
                    PeakByHigh = (double)dto.PeakByHighEvaluation,
                    TroughByClose = (double)dto.TroughByCloseEvaluation,
                    TroughByLow = (double)dto.TroughByLowEvaluation
                };

            }


            //Create Macd stats for this data item.
            if (dto.MacdId != null && dto.MacdId > 0)
            {
                item.Macd = new Macd
                {
                    Id = (int)dto.MacdId,
                    AssetId = dto.AssetId,
                    Date = dto.PriceDate,
                    Ma13 = (double)dto.Ma13,
                    Ema13 = (double)dto.Ema13,
                    Ma26 = (double)dto.Ma26,
                    Ema26 = (double)dto.Ema26,
                    MacdLine = (double)dto.MacdLine,
                    SignalLine = (double)dto.SignalLine,
                    Histogram = (double)dto.Histogram,
                    HistogramAvg = (double)dto.HistogramAvg,
                    HistogramExtremum = (double)dto.HistogramExtremum,
                    DeltaHistogram = (double)dto.DeltaHistogram,
                    DeltaHistogramPositive = (int)dto.DeltaHistogramPositive,
                    DeltaHistogramNegative = (int)dto.DeltaHistogramNegative,
                    DeltaHistogramZero = (int)dto.DeltaHistogramZero,
                    HistogramDirection2D = (int)dto.HistogramDirection2D,
                    HistogramDirection3D = (int)dto.HistogramDirection3D,
                    HistogramDirectionChanged = (int)dto.HistogramDirectionChanged,
                    HistogramToOx = (int)dto.HistogramToOx,
                    HistogramRow = (int)dto.HistogramRow,
                    OxCrossing = (double)dto.OxCrossing,
                    DivergenceByAverage = (int)dto.DivergenceByAverage,
                    MacdPeak = (int)dto.MacdPeak,
                    LastMacdPeak = (double)dto.LastMacdPeak,
                    MacdPeakSlope = (double)dto.MacdPeakSlope,
                    MacdTrough = (int)dto.MacdTrough,
                    LastMacdTrough = (double)dto.LastMacdTrough,
                    MacdTroughSlope = (double)dto.MacdTroughSlope,
                    divergence = (int)dto.divergence
                };
            }


            //Create Adx stats for this data item.
            if (dto.AdxId != null && dto.AdxId > 0)
            {
                item.Adx = new Adx
                {
                    Id = (int)dto.AdxId,
                    AssetId = dto.AssetId,
                    Date = dto.PriceDate,
                    Tr = (double)dto.Tr,
                    Dm1Pos = (double)dto.Dm1Pos,
                    Dm1Neg = (double)dto.Dm1Neg,
                    Tr14 = (double)dto.Tr14,
                    Dm14Pos = (double)dto.Dm14Pos,
                    Dm14Neg = (double)dto.Dm14Neg,
                    Di14Pos = (double)dto.Di14Pos,
                    Di14Neg = (double)dto.Di14Neg,
                    Di14Diff = (double)dto.Di14Diff,
                    Di14Sum = (double)dto.Di14Sum,
                    Dx = (double)dto.Dx,
                    Adx = (double)dto.Adx,
                    DaysUnder20 = (int)dto.DaysUnder20,
                    DaysUnder15 = (int)dto.DaysUnder15,
                    Cross20 = (double)dto.Cross20,
                    DeltaDiPos = (double)dto.DeltaDiPos,
                    DeltaDiNeg = (double)dto.DeltaDiNeg,
                    DeltaAdx = (double)dto.DeltaAdx,
                    DiPosDirection3D = (int)dto.DiPosDirection3D,
                    DiPosDirection2D = (int)dto.DiPosDirection2D,
                    DiNegDirection3D = (int)dto.DiNegDirection3D,
                    DiNegDirection2D = (int)dto.DiNegDirection2D,
                    AdxDirection3D = (int)dto.AdxDirection3D,
                    AdxDirection2D = (int)dto.AdxDirection2D,
                    DiPosDirectionChanged = (int)dto.DiPosDirectionChanged,
                    DiNegDirectionChanged = (int)dto.DiNegDirectionChanged,
                    AdxDirectionChanged = (int)dto.AdxDirectionChanged,
                    DiDifference = (double)dto.DiDifference,
                    DiLinesCrossing = (int)dto.DiLinesCrossing
                };
            }


            return item;

        }


        public bool IsInRange(double value)
        {
            return value <= this.Quotation.High && value >= this.Quotation.Low;
        }

        public double GetMinDifference(double price)
        {
            return Math.Min(Math.Abs(price - Quotation.High), Math.Abs(price - Quotation.Low));
        }

        public double GetMaxDifference(double price)
        {
            return Math.Max(Math.Abs(price - Quotation.High), Math.Abs(price - Quotation.Low));
        }

        public double GetProperOpenOrClose()
        {

            if (Math.Max(Price.PeakByClose, Price.PeakByHigh) > Math.Max(Price.TroughByClose, Price.TroughByLow))
            {
                return Math.Max(Quotation.Open, Quotation.Close);
            }
            else
            {
                return Math.Min(Quotation.Open, Quotation.Close);
            }

        }

        public double GetProperHighOrLow()
        {

            if (Math.Max(Price.PeakByClose, Price.PeakByHigh) > Math.Max(Price.TroughByClose, Price.TroughByLow))
            {
                return Math.Max(Quotation.High, Quotation.Low);
            }
            else
            {
                return Math.Min(Quotation.High, Quotation.Low);
            }
        }

        public bool IsTrendlineBroken(double level, TrendlineType type)
        {

            if (type == TrendlineType.Resistance)
            {
                return Quotation.Close > level;
            }
            else if (type == TrendlineType.Support)
            {
                return Quotation.Close < level;
            }

            return false;

        }

    }
}
