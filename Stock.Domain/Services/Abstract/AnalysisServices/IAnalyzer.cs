using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface IAnalyzer
    {
        Asset getAsset();
        Timeframe getTimeframe();
        AssetTimeframe getAssetTimeframe();
        AnalysisType getAnalysisType();
        DateTime? getLastCalculationDate();
        DateTime? getFirstRequiredDate();
        void setLastCalculationDate(DateTime? date);
        void Analyze(DataItem[] items);
    }
}
