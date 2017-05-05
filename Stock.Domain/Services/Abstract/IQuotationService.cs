using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface IQuotationService
    {
        void injectDataService(IDataService2 dataService);
        DateTime? getLastCalculationDate(string symbol, string analysisSymbol);
        DateTime? getLastCalculationDate(AssetTimeframe atf, AnalysisType analysisType);
        DataItem[] fetchData(Dictionary<AnalysisType, IAnalyzer> analyzers);
    }
}
