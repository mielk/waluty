using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class TrendBounce
    {
        public int Id { get; set; }
        public Trendline Trendline { get; set; }
        public TrendHit StartHit { get; set; }
        public TrendHit EndHit { get; set; }
        public int length { get; set; }
        public double amplitude { get; set; }
        public int breaksByExtremum { get; set; }
        public double breaksByExtremumPoints { get; set; }
        public int breaksByClose { get; set; }
        public double breaksByClosePoints { get; set; }
        public double pointsForQuotations { get; set; }
        public List<TrendBreak> breaks { get; set; }

        public TrendBounce(Trendline trendline)
        {
            this.Trendline = trendline;
            this.breaks = new List<TrendBreak>();
        }

        public TrendBounce(Trendline trendline, TrendHit startHit)
        {
            this.Trendline = trendline;
            this.StartHit = startHit;
            this.breaks = new List<TrendBreak>();
        }

        public void AddBreak(TrendBreak trendBreak){
            this.breaks.Add(trendBreak);
        }

        public void AddExtremumBreak(double value)
        {
            if (value > 0)
            {
                this.breaksByExtremum++;
                this.breaksByExtremumPoints += value;
            }
        }

        public void AddQuotationPoints(double value)
        {
            this.length++;
            this.pointsForQuotations += value;
        }

        public void Calculate()
        {

        }

    }
}
