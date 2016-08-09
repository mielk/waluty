using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public abstract class Analyzer
    {

        public virtual AnalysisType Type { get; set; }
        public Asset Asset { get; set; }
        public Timeframe Timeframe { get; set; }
        public DateTime? LastCalculationDate { get; set; }


        public Asset getAsset()
        {
            return Asset;
        }

        public Timeframe getTimeframe()
        {
            return Timeframe;
        }

        public DateTime? getLastCalculationDate()
        {
            return LastCalculationDate;
        }

        public void setLastCalculationDate(DateTime? date)
        {
            this.LastCalculationDate = date;
        }

        public string getSymbol()
        {
            return Asset.ShortName + "_" + Timeframe.Name;
        }

        public AnalysisType getAnalysisType()
        {
            return Type;
        }


        public abstract void Analyze();
        public abstract void Analyze(bool fromScratch);



    }
}
