using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Utils;

namespace Stock.Domain.Entities
{
    public class AnalysisInfo
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StartIndex { get; set; }
        public int? EndIndex { get; set; }
        public double? MinLevel { get; set; }
        public double? MaxLevel { get; set; }
        public int Counter { get; set; }



        #region SYSTEM.OBJECT

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(AnalysisInfo)) return false;

            AnalysisInfo compared = (AnalysisInfo)obj;
            if (!compared.StartDate.IsEqual(StartDate)) return false;
            if (!compared.EndDate.IsEqual(EndDate)) return false;
            if (!compared.MinLevel.IsEqual(MinLevel)) return false;
            if (!compared.MaxLevel.IsEqual(MaxLevel)) return false;
            if (compared.Counter != Counter) return false;
            if (compared.StartIndex != StartIndex) return false;
            if (compared.EndIndex != EndIndex) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion SYSTEM.OBJECT


    }
}
