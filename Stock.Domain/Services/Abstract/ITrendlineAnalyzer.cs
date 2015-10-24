using System;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface ITrendlineAnalyzer
    {
        void LoadItems(DataItem[] items);
        Trendline Analyze(DataItem initialItem, double initialLevel, DataItem boundItem, double boundLevel);
    }
}
