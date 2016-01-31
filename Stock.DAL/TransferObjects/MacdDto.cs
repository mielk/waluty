﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class MacdDto : IDateItem
    {

        [Key]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime PriceDate { get; set; }
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

        [NotMapped]
        public int TimebandId { get; set; }

        public DateTime GetDate()
        {
            return PriceDate;
        }

    }
}