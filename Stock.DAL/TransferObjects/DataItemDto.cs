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

        //Macd
        public int? MacdId { get; set; }
        public double? Ma13 { get; set; }
        public double? Ema13 { get; set; }
        public double? Ma26 { get; set; }
        public double? Ema26 { get; set; }
        public double? MacdLine { get; set; }
        public double? SignalLine { get; set; }
        public double? Histogram { get; set; }
        public double? HistogramAvg { get; set; }
        public double? HistogramExtremum { get; set; }
        public double? DeltaHistogram { get; set; }
        public int? DeltaHistogramPositive { get; set; }
        public int? DeltaHistogramNegative { get; set; }
        public int? DeltaHistogramZero { get; set; }
        public int? HistogramDirection2D { get; set; }
        public int? HistogramDirection3D { get; set; }
        public int? HistogramDirectionChanged { get; set; }
        public int? HistogramToOx { get; set; }
        public int? HistogramRow { get; set; }
        public double? OxCrossing { get; set; }
        public int? DivergenceByAverage { get; set; }
        public int? MacdPeak { get; set; }
        public double? LastMacdPeak { get; set; }
        public double? MacdPeakSlope { get; set; }
        public int? MacdTrough { get; set; }
        public double? LastMacdTrough { get; set; }
        public double? MacdTroughSlope { get; set; }
        public int? divergence { get; set; }

        //Adx
        public int? AdxId { get; set; }
        public double? Tr { get; set; }
        public double? Dm1Pos { get; set; }
        public double? Dm1Neg { get; set; }
        public double? Tr14 { get; set; }
        public double? Dm14Pos { get; set; }
        public double? Dm14Neg { get; set; }
        public double? Di14Pos { get; set; }
        public double? Di14Neg { get; set; }
        public double? Di14Diff { get; set; }
        public double? Di14Sum { get; set; }
        public double? Dx { get; set; }
        public double? Adx { get; set; }
        public int? DaysUnder20 { get; set; }
        public int? DaysUnder15 { get; set; }
        public double? Cross20 { get; set; }
        public double? DeltaDiPos { get; set; }
        public double? DeltaDiNeg { get; set; }
        public double? DeltaAdx { get; set; }
        public int? DiPosDirection3D { get; set; }
        public int? DiPosDirection2D { get; set; }
        public int? DiNegDirection3D { get; set; }
        public int? DiNegDirection2D { get; set; }
        public int? AdxDirection3D { get; set; }
        public int? AdxDirection2D { get; set; }
        public int? DiPosDirectionChanged { get; set; }
        public int? DiNegDirectionChanged { get; set; }
        public int? AdxDirectionChanged { get; set; }
        public double? DiDifference { get; set; }
        public int? DiLinesCrossing { get; set; }

    }

}