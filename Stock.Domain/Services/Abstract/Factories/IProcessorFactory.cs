using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.Domain.Services
{

    public interface IProcessorFactory
    {
        
        IAnalysisProcessController GetProperAnalysisProcessController(AnalysisType analysisType);
        PriceProcessController GetPriceProcessController();

        IDataProcessor GetProperProcessor(IProcessManager manager, AnalysisType analysisType);
        IPriceProcessor GetPriceProcessor(IProcessManager manager);
        IExtremumProcessor GetExtremumProcessor(IProcessManager manager);

    }

}
