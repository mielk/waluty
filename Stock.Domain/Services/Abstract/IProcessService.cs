using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock.Domain.Services
{
    public interface IProcessService
    {
        //void Setup(AnalysisType[] types);
        //void Setup(IEnumerable<AnalysisType> types);
        //void loadAnalyzers(Dictionary<AnalysisType, IAnalyzer> analyzers);
        //void injectQuotationService(IQuotationService instance);

        bool Run(bool fromScratch);

        Asset getAsset();
        Timeframe getTimeframe();
        Dictionary<AnalysisType, IAnalyzer> getAnalyzers();

    }
}