using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Abstract
{
    public interface IQuotationService
    {
        DateTime? getLastCalculationDate(string symbol, string analysisSymbol);
        DataItem[] fetchData(Dictionary<AnalysisType, IAnalyzer> analyzers);
    }
}
