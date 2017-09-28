using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class TrendRange
    {

        public int Id { get; set; }
        public Trendline Trendline { get; set; }
        public ITrendEvent StartEvent { get; set; }
        public int StartIndex { get; set; }
        public ITrendEvent EndEvent { get; set; }
        public int EndIndex { get; set; }
        public TrendRange PreviousRange { get; set; }
        public TrendRange NextRange { get; set; }
        public int TrendlineId { get; set; }
        public int QuotationsCounter { get; set; }
        public double TotalDistance { get; set; }
        public double MaxDistance { get; set; }
        public double Value {get;set;}

        

        //public TrendBounce(Trendline trendHit)
        //{
        //    this.Trendline = trendHit;
        //    this.breaks = new List<TrendBreak>();
        //}

        //public TrendBounce(Trendline trendHit, TrendHit startHit)
        //{
        //    this.Trendline = trendHit;
        //    this.StartHit = startHit;
        //    this.breaks = new List<TrendBreak>();
        //}

        //public void AddBreak(TrendBreak trendBreak)
        //{
        //    this.breaks.Add(trendBreak);
        //}

        //public void AddExtremumBreak(double value)
        //{
        //    if (value > 0)
        //    {
        //        this.breaksByExtremum++;
        //        this.breaksByExtremumPoints += value;
        //    }
        //}

        //public void AddQuotation(Trendline trendHit, DataItem item)
        //{
        //    this.length++;
        //    this.pointsForQuotations += calculatePointForQuotation(trendHit, item);
        //}


        //private double calculatePointForQuotation(Trendline trendHit, DataItem item)
        //{
        //    return 0;
        //}


        //public void Calculate()
        //{

        //}

    }
}
