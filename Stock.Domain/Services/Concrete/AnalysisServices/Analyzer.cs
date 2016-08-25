using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.Domain.Services.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public abstract class Analyzer : IAnalyzer
    {
        public abstract AnalysisType Type { get; }
        public AssetTimeframe AssetTimeframe { get; set; }
        bool lastCalculationDateInitialized { get; set; }
        protected int ItemsForAnalysis { get; set; }
        private DateTime? pLastCalculationDate;
        protected DataItem[] items;
        protected IAnalyzerProcessor processor;
        private IQuotationService quotationService;
        private Analysis analysis;
        public bool IsSimulation { get; set; }

        

        public DateTime? LastCalculationDate { 
            get {
                if (!lastCalculationDateInitialized)
                {
                    pLastCalculationDate = getQuotationService().getLastCalculationDate(AssetTimeframe, Type);
                    lastCalculationDateInitialized = true;
                }
                return pLastCalculationDate;
            }
            set {
                pLastCalculationDate = value;
                this.lastCalculationDateInitialized = true;            
            }
        }

        public Analyzer(AssetTimeframe atf)
        {
            initialize(atf);
        }

        public Analyzer(AssetTimeframe atf, IQuotationService quotationService)
        {
            this.quotationService = quotationService;
            initialize(atf);
        }

        private void initialize(AssetTimeframe atf)
        {
            this.AssetTimeframe = atf;
            initialize_specific();
            analysis = new Analysis(atf, Type);
        }

        protected abstract void initialize_specific();

        public AssetTimeframe getAssetTimeframe(){
            return AssetTimeframe;
        }

        public Asset getAsset()
        {
            return AssetTimeframe.asset;
        }

        public Timeframe getTimeframe()
        {
            return AssetTimeframe.timeframe;
        }

        public AnalysisType getAnalysisType()
        {
            return Type;
        }

        private IQuotationService getQuotationService()
        {
            if (quotationService == null)
            {
                quotationService = ProcessServiceFactory.Instance().GetQuotationService();
            }
            return quotationService;
        }

        public void injectQuotationService(IQuotationService quotationService)
        {
            this.quotationService = quotationService;
        }

        public DateTime? getLastCalculationDate()
        {
            return LastCalculationDate;
        }

        public void setLastCalculationDate(DateTime? date)
        {
            LastCalculationDate = date;
        }

        protected virtual DateTime? findLastCalculationDate()
        {
            return getQuotationService().getLastCalculationDate(getSymbol(), Type.toString());
        }

        public DateTime? getFirstRequiredDate()
        {
            if (LastCalculationDate == null) return null;
            var d = (DateTime) LastCalculationDate;
            return d.addTimeUnits(AssetTimeframe.timeframe.Symbol, -ItemsForAnalysis);
        }


        public string getSymbol()
        {
            return AssetTimeframe.Symbol();
        }

        public void injectDaysForAnalysis(int value)
        {
            this.ItemsForAnalysis = value;
        }

        public DataItem getDataItem(int index)
        {
            return items[index];
        }

        public IEnumerable<DataItem> getDataItems()
        {
            return items;
        }

        public int getDataItemsLength()
        {
            return items.Length;
        }

        protected abstract IAnalyzerProcessor getProcessor();

        public int findItemIndexByDate(IEnumerable<DataItem> items, DateTime date)
        {
            DataItem item = items.SingleOrDefault(i => i.Date.Equals(date));
            return item == null ? -1 : item.Index;
        }

        public virtual void Analyze(DataItem[] items)
        {

            //Create price processor (unless it is already loaded).
            if (processor == null) processor = getProcessor();

            //Save [items] array for future reference.
            this.items = items.ToArray();

            int indexAnalysisStart = 0;
            int indexLastCalculation = -1;

            //Calculate required index numbers.
            if (LastCalculationDate != null){
                DateTime ldc = ((DateTime)LastCalculationDate).Proper(AssetTimeframe.timeframe.Symbol);
                DateTime lastRequiredForRightOnlyAnalysis = ldc.addTimeUnits(AssetTimeframe.timeframe.Symbol, -ItemsForAnalysis);
                indexAnalysisStart = findItemIndexByDate(items, lastRequiredForRightOnlyAnalysis);
                indexLastCalculation = findItemIndexByDate(items, ldc);
            }
            

            //Only right analysis.
            for (var i = Math.Max(indexAnalysisStart, 0) ; i <= indexLastCalculation; i++)
            {
                processor.runRightSide(this, items[i], AssetTimeframe);
            }

            //Complete analysis.
            for (var i = indexLastCalculation + 1; i < items.Length; i++)
            {
                processor.runFull(this, items[i], AssetTimeframe);
            }

            LastCalculationDate = items[items.Length - 1].Date;

            //Save info about analysis to Analysis object.
            //analysis.AnalysisStart = 

        }

    }

}
