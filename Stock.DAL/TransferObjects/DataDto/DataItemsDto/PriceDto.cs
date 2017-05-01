﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class PriceDto : IDateItemDto
    {
        [Key]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime PriceDate { get; set; }
        public double DeltaClosePrice { get; set; }
        public int PriceDirection2D { get; set; }
        public int PriceDirection3D { get; set; }
        public double PriceGap { get; set; }
        public double PeakByCloseEvaluation { get; set; }
        public double PeakByHighEvaluation { get; set; }
        public double TroughByCloseEvaluation { get; set; }
        public double TroughByLowEvaluation { get; set; }
        public ExtremumDto PeakByClose { get; set; }
        public ExtremumDto PeakByHigh { get; set; }
        public ExtremumDto TroughByClose { get; set; }
        public ExtremumDto TroughByLow { get; set; }
        public double CloseRatio { get; set; }
        public double ExtremumRatio { get; set; }

        [NotMapped]
        public int TimeframeId { get; set; }

        public DateTime GetDate()
        {
            return PriceDate;
        }

    }
}