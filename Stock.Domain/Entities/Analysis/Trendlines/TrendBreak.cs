using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{

    public class TrendBreak : ITrendEvent
    {
        public int Id { get; set; }
        public Trendline Trendline { get; set; }
        public DataSet Item { get; set; }
        public double Level { get; set; }
        public TrendRange PreviousRange { get; set; }
        public TrendRange NextRange { get; set; }
    }

}
