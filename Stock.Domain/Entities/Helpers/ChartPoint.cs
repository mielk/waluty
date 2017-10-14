using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class ChartPoint
    {
        public int IndexNumber { get; set; }
        public double Level { get; set; }

        public ChartPoint(int indexNumber, double level)
        {
            this.IndexNumber = IndexNumber;
            this.Level = level;
        }

    }
}
