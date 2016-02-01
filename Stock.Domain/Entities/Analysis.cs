﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.Domain.Services;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Entities
{
    public class Analysis
    {
        public AnalysisType Type { get; set; }
        public string Symbol { get; set; }
        public DateTime FirstItemDate { get; set; }
        public DateTime LastItemDate { get; set; }
        public int AnalyzedItems { get; set; }
        public DateTime AnalysisStart { get; set; }
        public DateTime AnalysisEnd { get; set; }


        public Analysis(string symbol, AnalysisType type)
        {
            Symbol = symbol;
            Type = type;
            AnalysisStart = DateTime.Now;
        }


        private double TotalTime()
        {
            return AnalysisEnd.Subtract(AnalysisStart).TotalMilliseconds;
        }

        public AnalysisDto ToDto()
        {
            var dto = new AnalysisDto
            {
                Type = Type.TableName(),
                Symbol = Symbol,
                FirstItemDate = FirstItemDate,
                LastItemDate = LastItemDate,
                AnalyzedItems = AnalyzedItems,
                AnalysisStart = AnalysisStart,
                AnalysisEnd = AnalysisEnd,
                AnalysisTotalTime = TotalTime()
            };

            return dto;

        }

    }
}
