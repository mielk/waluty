using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Core;
using Stock.Utils;

namespace Stock.Domain.Services
{
    public class PriceProcessController : IAnalysisProcessController
    {
        private const AnalysisType ANALYSIS_TYPE = AnalysisType.Prices;
        private const int PAST_QUOTATIONS_REQUIRED_FOR_ANALYSIS = 260;
        private const int DEFAULT_HOW_MANY_ITEMS_BEFORE_INCLUDED = 10;
        public int HowManyItemsBeforeInclude { get; set; }
        private IProcessManager manager;
        private IPriceProcessor processor;


        #region CONSTRUCTOR

        public PriceProcessController()
        {
            HowManyItemsBeforeInclude = DEFAULT_HOW_MANY_ITEMS_BEFORE_INCLUDED;
        }

        #endregion CONSTRUCTOR


        #region SERVICES

        private void setManager(IProcessManager manager)
        {
            this.manager = manager;
            if (this.processor == null)
            {
                this.processor = ProcessorFactory.Instance().GetPriceProcessor(this.manager);
            }
        }

        public void InjectPriceProcessor(IPriceProcessor processor)
        {
            this.processor = processor;
        }

        #endregion SERVICES



        public void Run(IProcessManager manager)
        {
            
            setManager(manager);

            int lastQuotationIndex = manager.GetAnalysisLastUpdatedIndex(AnalysisType.Quotations) ?? 0;
            int lastPriceIndex = manager.GetAnalysisLastUpdatedIndex(ANALYSIS_TYPE) ?? 0;

            if (lastQuotationIndex > lastPriceIndex)
            {
                int startIndex = Math.Max(lastPriceIndex - HowManyItemsBeforeInclude, 1);
                manager.loadDataSets(startIndex - PAST_QUOTATIONS_REQUIRED_FOR_ANALYSIS);
                for (int i = startIndex; i <= lastQuotationIndex; i++)
                {
                    DataSet ds = manager.GetDataSet(i);
                    processor.Process(ds);
                }
            }

        }

    }
}
