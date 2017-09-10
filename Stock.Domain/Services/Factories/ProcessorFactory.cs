using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;
using Stock.DAL.Repositories;

namespace Stock.Domain.Services
{
    public class ProcessorFactory
    {

        public static IDataProcessor GetProperProcessor(IProcessManager manager, AnalysisType analysisType)
        {
            switch (analysisType)
            {
                case AnalysisType.Prices: return GetPriceProcessor(manager);
            }
            return null;
        }

        public static IAnalysisProcessController GetProperAnalysisProcessController(AnalysisType analysisType)
        {
            switch (analysisType)
            {
                case AnalysisType.Prices: return GetPriceProcessController();
            }
            return null;
        }

        public static PriceProcessController GetPriceProcessController()
        {
            return new PriceProcessController();
        }

        public static IPriceProcessor GetPriceProcessor(IProcessManager manager)
        {
            return new PriceProcessor(manager);
        }

        public static IExtremumProcessor GetExtremumProcessor(IProcessManager manager)
        {
            return new ExtremumProcessor(manager);
        }

    }

}
