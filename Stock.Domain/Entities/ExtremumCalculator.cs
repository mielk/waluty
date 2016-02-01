using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;

namespace Stock.Domain.Entities
{
    public class ExtremumCalculator
    {

        public int ExtremumId { get; set; }
        public string Symbol { get; set; }
        public ExtremumType Type { get; set; }
        public DateTime PriceDate { get; set; }
        public int EarlierCounter { get; set; }
        public int LaterCounter { get; set; }
        public double EarlierAmplitude { get; set; }
        public double LaterAmplitude { get; set; }
        public double Volatility { get; set; }
        public double EarlierChange1 { get; set; }
        public double EarlierChange2 { get; set; }
        public double EarlierChange3 { get; set; }
        public double EarlierChange5 { get; set; }
        public double EarlierChange10 { get; set; }
        public double LaterChange1 { get; set; }
        public double LaterChange2 { get; set; }
        public double LaterChange3 { get; set; }
        public double LaterChange5 { get; set; }
        public double LaterChange10 { get; set; }

    }
}
