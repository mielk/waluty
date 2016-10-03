using System;
using Stock.Domain.Entities;
using System.Collections.Generic;

namespace Stock.Domain.Services
{
    public interface ITrendlineAnalyzer : IAnalyzer
    {
        IEnumerable<Trendline> GetTrendlines();
    }
}
