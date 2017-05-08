using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class AnalysisDto
    {
        public string Type { get; set; }
        public string Symbol { get; set; }
        public DateTime FirstItemDate { get; set; }
        public DateTime LastItemDate { get; set; }
        public int AnalyzedItems { get; set; }
        public DateTime AnalysisStart { get; set; }
        public DateTime AnalysisEnd { get; set; }
        public double AnalysisTotalTime { get; set; }
    }
}
