using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class LastDates
    {

        public DateTime LastQuotation { get; set; }
        public DateTime? LastAnalysisItem { get; set; }


        public bool IsUpToDate()
        {
            if (LastAnalysisItem == null) return false;
            return DateTime.Compare((DateTime) LastAnalysisItem, LastQuotation) >= 0;
        }


    }

}