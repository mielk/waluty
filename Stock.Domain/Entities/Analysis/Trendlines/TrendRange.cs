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

        

        //public TrendBounce(Trendline trendline)
        //{
        //    this.Trendline = trendline;
        //    this.breaks = new List<TrendBreak>();
        //}

        //public TrendBounce(Trendline trendline, TrendHit startHit)
        //{
        //    this.Trendline = trendline;
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

        //public void AddQuotation(Trendline trendline, DataItem item)
        //{
        //    this.length++;
        //    this.pointsForQuotations += calculatePointForQuotation(trendline, item);
        //}


        //private double calculatePointForQuotation(Trendline trendline, DataItem item)
        //{
        //    return 0;
        //}


        //public void Calculate()
        //{

        //}

    }
}
