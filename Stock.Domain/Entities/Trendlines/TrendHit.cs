using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class TrendHit
    {
        private const double TimeRangeWeight = 0.2d;
        private const double SlopeWeight = 1.0d;
        public int Id { get; set; }
        public Trendline Trendline { get; set; }
        public DataItem Item { get; set; }
        public double CrossLevel { get; set; }
        public double CLHRatio { get; set; }
        public double ExtremumPoints { get; set; }
        public double Score { get; set; }
        public Guid Guid { get; set; }
        public double BounceScore { get; set; }
        public double TimeRangeScore { get; set; }
        public double SlopeScore { get; set; }
        public double EvaluationScore { get; set; }
        public TrendHit PreviousHit { get; set; }
        public TrendBounce BounceFromPreviousHit { get; set; }
        public TrendHit NextHit { get; set; }
        public TrendBounce BounceToNextHit { get; set; }
        

        public TrendHit()
        {
            Guid = System.Guid.NewGuid();
        }

        public TrendHit(Trendline trendline)
        {
            Guid = System.Guid.NewGuid();
            this.Trendline = trendline;
        }

        public TrendHit(Trendline trendline, DataItem item)
        {
            Guid = System.Guid.NewGuid();
            this.Trendline = trendline;
            this.Item = item;
        }

        public TrendHit(Trendline trendline, DataItem item, TrendHit prevHit, TrendBounce prevBounce)
        {
            Guid = System.Guid.NewGuid();
            this.Trendline = trendline;
            this.Item = item;
            this.PreviousHit = prevHit;
            this.BounceFromPreviousHit = prevBounce;
        }


        public void Calculate()
        {
            this.Score = (1 + BounceScore) * (1 + SlopeScore * SlopeWeight) * 
                (1 + TimeRangeScore * TimeRangeWeight) * (1 + EvaluationScore);
        }


        public bool IsConfirmed()
        {
            return Score > 0;
        }


    }
}
