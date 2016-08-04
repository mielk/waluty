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
        void Setup(Asset asset, Timeframe timeframe, AnalysisType[] types);
        DateTime findEarliestRequiredDate(bool fromScratch);
        DataItem[] loadData(DateTime initialTime);
        void count(int x);
    }
}
