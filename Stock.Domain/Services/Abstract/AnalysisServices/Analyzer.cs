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
    public abstract class Analyzer
    {
        public abstract AnalysisType Type { get; }
        public Asset Asset { get; set; }
        public Timeframe Timeframe { get; set; }
        public DateTime? LastCalculationDate { get; set; }
        protected int DaysForAnalysis { get; set; }


        public Analyzer(Asset asset, Timeframe timeframe)
        {
            this.Asset = asset;
            this.Timeframe = timeframe;
            this.LastCalculationDate = findLastCalculationDate();
        }


        public Asset getAsset()
        {
            return Asset;
        }

        public Timeframe getTimeframe()
        {
            return Timeframe;
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
            return d.addTimeUnits(Timeframe.Symbol, -DaysForAnalysis);
        }

        public void setLastCalculationDate(DateTime? date)
        {
            this.LastCalculationDate = date;
        }

        public string getSymbol()
        {
            return Asset.ShortName + "_" + Timeframe.Name;
        }



        public abstract void Analyze();
        public abstract void Analyze(bool fromScratch);



    }
}
