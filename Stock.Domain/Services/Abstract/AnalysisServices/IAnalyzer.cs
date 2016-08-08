using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface IAnalyzer
    {
        void Analyze();
        void Analyze(bool fromScratch);
        Asset getAsset();
        Timeframe getTimeframe();
    }
}
