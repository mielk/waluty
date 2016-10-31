using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class TrendDistance
    {
        public int Id { get; set; }
        public AssetTimeframe AssetTimeframe { get; set; }
        public Trendline Trendline { get; set; }
        public TrendHit StartHit { get; set; }
        public TrendHit EndHit { get; set; }
        public int length { get; set; }
        public double amplitude { get; set; }
        public int breaksByExtremum { get; set; }
        public int breaksByClose { get; set; }
        public double pointsForQuotations { get; set; }
        public IEnumerable<TrendBreak> breaks { get; set; }

        public void Calculate()
        {

        }

    }
}
