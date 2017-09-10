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
        public int HowManyItemsBeforeInclude { get; set; }
        private IProcessManager manager;
        private IPriceProcessor processor;

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

        public void Run(IProcessManager manager)
        {
            
            setManager(manager);

            int lastQuotationIndex = manager.GetAnalysisLastUpdatedIndex(AnalysisType.Quotations) ?? 0;
            int lastPriceIndex = manager.GetAnalysisLastUpdatedIndex(ANALYSIS_TYPE) ?? 0;

            if (lastQuotationIndex > lastPriceIndex)
            {
                int startIndex = Math.Max(lastPriceIndex - HowManyItemsBeforeInclude, 1);
                for (int i = startIndex; i <= lastQuotationIndex; i++)
                {
                    DataSet ds = manager.GetDataSet(i);
                    processor.Process(ds);
                }
            }

        }

    }
}
