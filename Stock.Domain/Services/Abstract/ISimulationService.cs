using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock.Domain.Services
{
    public interface ISimulationService
    {
        bool Start(string pair, string timeframe, AnalysisType[] types);
        bool Start(AssetTimeframe atf, AnalysisType[] types);
        int NextStep(int incrementation);
        object GetDataSetProperties();
        IEnumerable<DataItem> GetQuotations(DateTime startDateTime, DateTime endDateTime);
        DateTime? getLastCalculationDate(AnalysisType type);
        DateTime? getLastCalculationDate(string symbol, string analysisSymbol);
        DateTime? getLastCalculationDate(AssetTimeframe atf, AnalysisType analysisType);
        DataItem[] fetchData(Dictionary<AnalysisType, IAnalyzer> analyzers);
    }
}
