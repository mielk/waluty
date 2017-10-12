using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;
using Stock.DAL.Repositories;

namespace Stock.Domain.Services
{
    public class ProcessorFactory : IProcessorFactory
    {

        private static ProcessorFactory instance;
        private static PriceProcessController priceProcessController;
        private static TrendlineProcessController trendlineProcessController;
        private static IPriceProcessor priceProcessor;
        private static ITrendlineProcessor trendlineProcessor;
        private static IExtremumProcessor extremumProcessor;



        #region CONSTRUCTOR

        public static ProcessorFactory Instance()
        {
            if (instance == null)
            {
                instance = new ProcessorFactory();
            }
            return instance;
        }

        public static ProcessorFactory Instance(ProcessorFactory _instance)
        {
            instance = _instance;
            return instance;
        }

        #endregion CONSTRUCTOR



        #region CONTROLLERS
        
        public IAnalysisProcessController GetProperAnalysisProcessController(AnalysisType analysisType)
        {
            switch (analysisType)
            {
                case AnalysisType.Prices:       return GetPriceProcessController();
                case AnalysisType.Trendlines:   return GetTrendlineProcessController();
            }
            return null;
        }

        public PriceProcessController GetPriceProcessController()
        {
            if (priceProcessController == null)
            {
                priceProcessController = new PriceProcessController();
            }
            return priceProcessController;
        }

        public TrendlineProcessController GetTrendlineProcessController()
        {
            if (trendlineProcessController == null)
            {
                trendlineProcessController = new TrendlineProcessController();
            }
            return trendlineProcessController;
        }

        #endregion CONTROLLERS



        #region PROCESSORS

        public IDataProcessor GetProperProcessor(IProcessManager manager, AnalysisType analysisType)
        {
            switch (analysisType)
            {
                case AnalysisType.Prices: return GetPriceProcessor(manager);
            }
            return null;
        }

        public IPriceProcessor GetPriceProcessor(IProcessManager manager)
        {
            if (priceProcessor == null)
            {
                priceProcessor = new PriceProcessor(manager);
            }
            return priceProcessor;
        }

        public IExtremumProcessor GetExtremumProcessor(IProcessManager manager)
        {
            if (extremumProcessor == null)
            {
                extremumProcessor = new ExtremumProcessor(manager);
            }
            return extremumProcessor;
        }

        public ITrendlineProcessor GetTrendlineProcessor(IProcessManager manager)
        {
            if (trendlineProcessor == null)
            {
                trendlineProcessor = new TrendlineProcessor(manager);
            }
            return trendlineProcessor;
        }

        #endregion PROCESSORS


    }

}
