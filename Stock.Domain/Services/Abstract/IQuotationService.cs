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
        DataItem[] fetchData(Dictionary<AnalysisType, Analyzer> analyzers);
    }
}
