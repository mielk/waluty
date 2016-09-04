﻿using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface ITrendlineProcessor : IAnalyzerProcessor
    {
        Trendline Analyze(DataItem initialItem, double initialLevel, DataItem boundItem, double boundLevel);
    }
}