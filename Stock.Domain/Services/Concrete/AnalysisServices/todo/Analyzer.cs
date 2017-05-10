using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.Domain.Services.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.Domain.Services
{
    public abstract class Analyzer : IAnalyzer
    {
        public abstract AnalysisType Type { get; }
        public AssetTimeframe AssetTimeframe { get; set; }
        private IQuotationService3 quotationService;
        protected IAnalyzerProcessor processor;




        bool lastCalculationDateInitialized { get; set; }
        protected int ItemsForAnalysis { get; set; }
        private DateTime? pLastCalculationDate;
        protected DataItem[] items;
        
        
        private Analysis analysis;
        public bool IsSimulation { get; set; }



        public Analyzer(AssetTimeframe atf)
        {
            initialize(atf);
        }

        public Analyzer(AssetTimeframe atf, IQuotationService3 quotationService)
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





        public DateTime? LastCalculationDate
        {
            get
            {
                return pLastCalculationDate;
            }
            set
            {
                pLastCalculationDate = value;
                this.lastCalculationDateInitialized = true;
            }
        }


        public AssetTimeframe getAssetTimeframe(){
            return AssetTimeframe;
        }

        public Asset getAsset()
        {
            return AssetTimeframe.GetAsset();
        }

        public Timeframe getTimeframe()
        {
            return AssetTimeframe.GetTimeframe();
        }

        public AnalysisType getAnalysisType()
        {
            return Type;
        }

        private IQuotationService3 getQuotationService()
        {
            if (quotationService == null)
            {
                quotationService = ProcessServiceFactory.Instance().GetQuotationService();
            }
            return quotationService;
        }

        public void injectQuotationService(IQuotationService3 quotationService)
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
            return getQuotationService().getLastCalculationDate(getSymbol(), Type.ToString());
        }

        public DateTime? getFirstRequiredDate()
        {
            if (LastCalculationDate == null) return null;
            var d = (DateTime) LastCalculationDate;

            //return d.addTimeUnits(AssetTimeframe.Timeframe.GetName(), -ItemsForAnalysis);
            return d;
        }


        public string getSymbol()
        {
            return AssetTimeframe.GetSymbol();
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
                
                DateTime ldc = new DateTime();
                DateTime lastRequiredForRightOnlyAnalysis = new DateTime();
                //DateTime ldc = ((DateTime)LastCalculationDate).Proper(AssetTimeframe.Timeframe.GetName());
                //DateTime lastRequiredForRightOnlyAnalysis = ldc.addTimeUnits(AssetTimeframe.Timeframe.GetName(), -ItemsForAnalysis);

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
