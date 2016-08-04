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
        void Setup(Asset asset, Timeframe timeframe, AnalysisType[] type);
        bool Run(bool fromScratch);
    }
}