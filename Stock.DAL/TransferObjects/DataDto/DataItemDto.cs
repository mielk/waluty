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
        public string Timeframe { get; set; }
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
        //Extrema evaluation values.
        //public int? PeakByCloseExtremumId { get; set; }
        //public string? PeakByCloseSymbol { get; set; }
        //public int? PeakByCloseType { get; set; }
        //public DateTime? PeakByClosePriceDate { get; set; }
        //public int? PeakByCloseEarlierCounter { get; set; }
        //public int? PeakByCloseLaterCounter { get; set; }
        //public double? PeakByCloseEarlierAmplitude { get; set; }
        //public double? PeakByCloseLaterAmplitude { get; set; }
        //public double? PeakByCloseVolatility { get; set; }
        //public double? PeakByCloseEarlierChange1 { get; set; }
        //public double? PeakByCloseEarlierChange2 { get; set; }
        //public double? PeakByCloseEarlierChange3 { get; set; }
        //public double? PeakByCloseEarlierChange5 { get; set; }
        //public double? PeakByCloseEarlierChange10 { get; set; }
        //public double? PeakByCloseLaterChange1 { get; set; }
        //public double? PeakByCloseLaterChange2 { get; set; }
        //public double? PeakByCloseLaterChange3 { get; set; }
        //public double? PeakByCloseLaterChange5 { get; set; }
        //public double? PeakByCloseLaterChange10 { get; set; }
        //public bool? PeakByCloseProspective { get; set; }
        //public bool? PeakByCloseCancelled { get; set; }
        //public int? PeakByHighExtremumId { get; set; }
        //public string? PeakByHighSymbol { get; set; }
        //public int? PeakByHighType { get; set; }
        //public DateTime? PeakByHighPriceDate { get; set; }
        //public int? PeakByHighEarlierCounter { get; set; }
        //public int? PeakByHighLaterCounter { get; set; }
        //public double? PeakByHighEarlierAmplitude { get; set; }
        //public double? PeakByHighLaterAmplitude { get; set; }
        //public double? PeakByHighVolatility { get; set; }
        //public double? PeakByHighEarlierChange1 { get; set; }
        //public double? PeakByHighEarlierChange2 { get; set; }
        //public double? PeakByHighEarlierChange3 { get; set; }
        //public double? PeakByHighEarlierChange5 { get; set; }
        //public double? PeakByHighEarlierChange10 { get; set; }
        //public double? PeakByHighLaterChange1 { get; set; }
        //public double? PeakByHighLaterChange2 { get; set; }
        //public double? PeakByHighLaterChange3 { get; set; }
        //public double? PeakByHighLaterChange5 { get; set; }
        //public double? PeakByHighLaterChange10 { get; set; }
        //public bool? PeakByHighProspective { get; set; }
        //public bool? PeakByHighCancelled { get; set; }
        //public int? TroughByCloseExtremumId { get; set; }
        //public string? TroughByCloseSymbol { get; set; }
        //public int? TroughByCloseType { get; set; }
        //public DateTime? TroughByClosePriceDate { get; set; }
        //public int? TroughByCloseEarlierCounter { get; set; }
        //public int? TroughByCloseLaterCounter { get; set; }
        //public double? TroughByCloseEarlierAmplitude { get; set; }
        //public double? TroughByCloseLaterAmplitude { get; set; }
        //public double? TroughByCloseVolatility { get; set; }
        //public double? TroughByCloseEarlierChange1 { get; set; }
        //public double? TroughByCloseEarlierChange2 { get; set; }
        //public double? TroughByCloseEarlierChange3 { get; set; }
        //public double? TroughByCloseEarlierChange5 { get; set; }
        //public double? TroughByCloseEarlierChange10 { get; set; }
        //public double? TroughByCloseLaterChange1 { get; set; }
        //public double? TroughByCloseLaterChange2 { get; set; }
        //public double? TroughByCloseLaterChange3 { get; set; }
        //public double? TroughByCloseLaterChange5 { get; set; }
        //public double? TroughByCloseLaterChange10 { get; set; }
        //public bool? TroughByCloseProspective { get; set; }
        //public bool? TroughByCloseCancelled { get; set; }
        //public int? TroughByLowExtremumId { get; set; }
        //public string? TroughByLowSymbol { get; set; }
        //public int? TroughByLowType { get; set; }
        //public DateTime? TroughByLowPriceDate { get; set; }
        //public int? TroughByLowEarlierCounter { get; set; }
        //public int? TroughByLowLaterCounter { get; set; }
        //public double? TroughByLowEarlierAmplitude { get; set; }
        //public double? TroughByLowLaterAmplitude { get; set; }
        //public double? TroughByLowVolatility { get; set; }
        //public double? TroughByLowEarlierChange1 { get; set; }
        //public double? TroughByLowEarlierChange2 { get; set; }
        //public double? TroughByLowEarlierChange3 { get; set; }
        //public double? TroughByLowEarlierChange5 { get; set; }
        //public double? TroughByLowEarlierChange10 { get; set; }
        //public double? TroughByLowLaterChange1 { get; set; }
        //public double? TroughByLowLaterChange2 { get; set; }
        //public double? TroughByLowLaterChange3 { get; set; }
        //public double? TroughByLowLaterChange5 { get; set; }
        //public double? TroughByLowLaterChange10 { get; set; }
        //public bool? TroughByLowProspective { get; set; }
        //public bool? TroughByLowCancelled { get; set; }


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
        public int? MacdPeak { get; set; }
        public double? LastMacdPeak { get; set; }
        public double? MacdPeakSlope { get; set; }
        public int? MacdTrough { get; set; }
        public double? LastMacdTrough { get; set; }
        public double? MacdTroughSlope { get; set; }

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