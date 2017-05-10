using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.Domain.Services
{
    public interface IQuotationService3
    {
        DateTime? getLastCalculationDate(string symbol, string analysisSymbol);
        DateTime? getLastCalculationDate(AssetTimeframe atf, AnalysisType analysisType);
        DataItem[] fetchData(Dictionary<AnalysisType, IAnalyzer> analyzers);
    }
}