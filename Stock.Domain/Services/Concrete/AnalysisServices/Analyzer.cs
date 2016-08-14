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
        public DateTime? LastCalculationDate { get; set; }
        protected int DaysForAnalysis { get; set; }


        public Analyzer(AssetTimeframe atf)
        {
            this.AssetTimeframe = atf;
            this.LastCalculationDate = findLastCalculationDate();
            initialize();
        }

        protected abstract void initialize();

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

        public DateTime? getLastCalculationDate()
        {
            return LastCalculationDate;
        }

        protected virtual DateTime? findLastCalculationDate()
        {
            return ProcessServiceFactory.Instance().GetQuotationService().getLastCalculationDate(getSymbol(), Type.toString());
        }

        public DateTime? getFirstRequiredDate()
        {
            if (LastCalculationDate == null) return null;
            var d = (DateTime)LastCalculationDate;
            return d.addTimeUnits(AssetTimeframe.timeframe.Symbol, -DaysForAnalysis);
        }

        public void setLastCalculationDate(DateTime? date)
        {
            this.LastCalculationDate = date;
        }

        public string getSymbol()
        {
            return AssetTimeframe.Symbol();
        }



        public abstract void Analyze(DataItem[] items);



    }
}
