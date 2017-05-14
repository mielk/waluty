using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class DataSetInfo
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double MinLevel { get; set; }
        public double MaxLevel { get; set; }
        public int Counter { get; set; }
    }
}
