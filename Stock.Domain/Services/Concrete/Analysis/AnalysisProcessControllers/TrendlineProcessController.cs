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
        private const int PAST_QUOTATIONS_REQUIRED_FOR_ANALYSIS = 150;
        private IProcessManager manager;
        private ITrendlineProcessor processor;
        private ITrendlineService service = ServiceFactory.Instance().GetTrendlineService();
        private int assetId;
        private int timeframeId;
        //-----------------------------------------------------------------------------
        private IEnumerable<Trendline> trendlines;
        private IEnumerable<ExtremumGroup> extremumGroups;
        //-----------------------------------------------------------------------------
        private int lastQuotationIndex;
        private int lastTrendlineIndex;
        private int lastExtremumIndex;
        //-----------------------------------------------------------------------------


        #region CONSTRUCTOR

        public TrendlineProcessController()
        {
            trendlines = new List<Trendline>();
        }

        public TrendlineProcessController(IProcessManager manager)
        {
            setManager(manager);
        }

        #endregion CONSTRUCTOR



        #region SERVICES

        private void setManager(IProcessManager manager)
        {
            this.manager = manager;
            this.assetId = manager.GetAssetId();
            this.timeframeId = manager.GetTimeframeId();
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


        
        public IEnumerable<Trendline> GetTrendlines()
        {
            return trendlines.ToArray();
        }




        public void Run(IProcessManager manager)
        {            
            setManager(manager);
            loadTrendlines();
            processTrendlines();
            sortTrendlines();
        }

        private void loadTrendlines()
        {
            this.lastQuotationIndex = manager.GetAnalysisLastUpdatedIndex(AnalysisType.Quotations) ?? 0;
            if (trendlines == null)
            {
                loadExistingTrendlines();
            }
            
            this.lastTrendlineIndex = getLastUpdateIndex();
            if (lastQuotationIndex > lastTrendlineIndex)
            {
                this.extremumGroups = getExtremumGroupsFromIndex(lastTrendlineIndex - PAST_QUOTATIONS_REQUIRED_FOR_ANALYSIS);
                removeTrendlineWithOutOfDateExtremumGroup();
                createNewTrendlines();
            }

        }

        private void loadExistingTrendlines()
        {
            var trendlineService = ServiceFactory.Instance().GetTrendlineService();
            var trendlines = trendlineService.GetTrendlines(assetId, timeframeId, manager.GetSimulationId()).Where(t => t.EndIndex == null).ToList();
        }

        private void removeTrendlineWithOutOfDateExtremumGroup()
        {
            List<Trendline> trendlinesToRemove = new List<Trendline>();
            foreach (var trendline in trendlines)
            {
                var footholdIsPeak = trendline.FootholdIsPeak;
                var footholdIndex = trendline.FootholdIndex;
                var footholdSlaveIndex = trendline.FootholdSlaveIndex;
                var requiredSlaveIndex = getRequiredSlaveIndex(footholdIndex, footholdIsPeak == 1 ? true : false);
                if (footholdSlaveIndex != requiredSlaveIndex)
                {
                    var y = 1;
                    service.RemoveTrendline(trendline);
                    trendlines = trendlines.Where(t => t.Id != trendline.Id).ToList();
                    var x = 1;
                }
                
            }



            
//            var trendlinesToRemove = trendlines.Where(t => t.FootholdIndex >= footholdIndex).ToList();
            //foreach (var trendline in trendlinesToRemove)
            //{
            //}
        }

        private int getRequiredSlaveIndex(int footholdIndex, bool footholdIsPeak)
        {
            var item = extremumGroups.SingleOrDefault(e => e.GetIndex() == footholdIndex && e.IsPeak == footholdIsPeak);
            if (item == null){
                return -1;
            } 
            else 
            {
                return item.GetLateIndexNumber();
            }
        }

        private void createNewTrendlines()
        {
            this.lastExtremumIndex = getLastFootholdIndex();
            
            var newExtremumGroups = this.extremumGroups.Where(e => e.GetLateIndexNumber() > lastExtremumIndex).ToList();
            if (newExtremumGroups.Count() > 0)
            {
                foreach (var group in newExtremumGroups)
                {
                    addTrendlinesForExtremumGroup(group);
                }
            }
        }

        private int getLastFootholdIndex()
        {
            return (trendlines.Count() > 0 ? trendlines.Max(t => t.FootholdIndex) : 0);
        }

        private int getLastUpdateIndex()
        {
            return (trendlines.Count() > 0 ? trendlines.Max(t => t.LastUpdateIndex) : 0);
        }

        private IEnumerable<ExtremumGroup> getExtremumGroupsFromIndex(int index)
        {
            IExtremumProcessor extremumProcessor = new ExtremumProcessor(manager);
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition() { StartIndex = index };
            IEnumerable<DataSet> dataSets = manager.GetDataSets(queryDef);
            return extremumProcessor.ExtractExtremumGroups(dataSets);
        }

        private void addTrendlinesForExtremumGroup(ExtremumGroup group)
        {
            foreach (var extremumGroup in extremumGroups)
            {
                if (processor.CanCreateTrendline(extremumGroup, group))
                {
                    var newTrendlines = processor.GetTrendlines(extremumGroup, group);
                    this.trendlines = trendlines.Concat(newTrendlines);
                }
            }
        }


        private void processTrendlines()
        {
            foreach (var trendline in trendlines)
            {
                trendline.LastUpdateIndex = this.lastQuotationIndex;
            }
        }


        private void sortTrendlines()
        {

        }

    }
}
