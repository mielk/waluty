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
    public class TrendlineProcessController : IAnalysisProcessController
    {

        private const AnalysisType ANALYSIS_TYPE = AnalysisType.Trendlines;
        private const int PAST_QUOTATIONS_REQUIRED_FOR_ANALYSIS = 260;
        private IProcessManager manager;
        private ITrendlineProcessor processor;



        #region CONSTRUCTOR

        public TrendlineProcessController()
        {
            
        }

        #endregion CONSTRUCTOR



        #region SERVICES

        private void setManager(IProcessManager manager)
        {
            this.manager = manager;
            if (this.processor == null)
            {
                this.processor = ProcessorFactory.Instance().GetTrendlineProcessor(this.manager);
            }
        }

        public void InjectTrendlineProcessor(ITrendlineProcessor processor)
        {
            this.processor = processor;
        }

        #endregion SERVICES



        public void Run(IProcessManager manager)
        {
            
            setManager(manager);

            int lastQuotationIndex = manager.GetAnalysisLastUpdatedIndex(AnalysisType.Quotations) ?? 0;
            int lastTrendlineIndex = manager.GetAnalysisLastUpdatedIndex(ANALYSIS_TYPE) ?? 0;

            if (lastQuotationIndex > lastTrendlineIndex)
            {

                AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition() { StartIndex = Math.Max(lastTrendlineIndex - PAST_QUOTATIONS_REQUIRED_FOR_ANALYSIS, 0) };
                IExtremumProcessor extremumProcessor = new ExtremumProcessor(manager);
                IEnumerable<DataSet> dataSets = manager.GetDataSets(queryDef);
                IEnumerable <ExtremumGroup> extremumGroups = extremumProcessor.ExtractExtremumGroups(null);

                //IExtremumExtractor extremumExtractor = new ExtremumExtractor();
            //    int startIndex = Math.Max(lastQuotationIndex - HowManyItemsBeforeInclude, 1);
            //    for (int i = startIndex; i <= lastQuotationIndex; i++)
            //    {
            //        DataSet ds = manager.GetDataSet(i);
            //        processor.Process(ds);
            //    }
            }

        }


    }
}
