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
        void Setup(Asset asset, Timeframe timeframe, Dictionary<AnalysisType, IAnalyzer> analyzers);
        void Setup(AssetTimeframe atf, Dictionary<AnalysisType, IAnalyzer> analyzers);
        DataItem[] fetchData();
    }
}
