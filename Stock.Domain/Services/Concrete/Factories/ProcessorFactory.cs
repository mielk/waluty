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
        private static PriceProcessController priceProcessorController;
        private static IPriceProcessor priceProcessor;
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

        #endregion CONSTRUCTOR



        #region CONTROLLERS
        
        public IAnalysisProcessController GetProperAnalysisProcessController(AnalysisType analysisType)
        {
            switch (analysisType)
            {
                case AnalysisType.Prices: return GetPriceProcessController();
            }
            return null;
        }

        public PriceProcessController GetPriceProcessController()
        {
            if (priceProcessorController == null)
            {
                priceProcessorController = new PriceProcessController();
            }
            return priceProcessorController;
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

        #endregion PROCESSORS


    }

}
