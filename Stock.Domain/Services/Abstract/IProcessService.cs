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
        void LoadAssetTimeframe(Asset asset, Timeframe timeframe);
        void LoadAssetTimeframe(string asset, string timeframe);
        void LoadAssetTimeframe(string symbol);
        void LoadAnalysisTypes(AnalysisType[] type);
        bool Run(bool fromScratch);
    }
}
