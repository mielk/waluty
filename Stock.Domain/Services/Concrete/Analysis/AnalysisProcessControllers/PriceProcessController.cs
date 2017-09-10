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
                this.processor = ProcessorFactory.GetPriceProcessor(this.manager);
            }
        }

        public void InjectPriceProcessor(IPriceProcessor processor)
        {
            this.processor = processor;
        }

        public void Run(IProcessManager manager)
        {
            setManager(manager);
            DataSetInfo analysisInfo = this.manager.GetDataSetInfo(ANALYSIS_TYPE);
            DataSetInfo quotationsInfo = this.manager.GetDataSetInfo(AnalysisType.Quotations);
            if (quotationsInfo.EndDate.IsLaterThan(analysisInfo.EndDate))
            {
                int lastUpdateIndex = manager.GetDataSetIndex(analysisInfo.EndDate);
                int startIndex = Math.Max(lastUpdateIndex - HowManyItemsBeforeInclude, 1);
                DataSet ds = manager.GetDataSet(startIndex);
                do
                {
                    processor.Process(ds);
                    ds = manager.GetDataSet(++startIndex);
                } while (ds != null);
            }
        }

    }
}
