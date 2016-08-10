using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{

    public enum TimeframeSymbol
    {
        M5,
        M15,
        M30,
        H1,
        H4,
        D1,
        W1,
        MN1
    }


    public class Timeframe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Period { get; set; }
        public int Index { get; set; }
        public TimeframeSymbol Symbol { get; set; }
        private static Dictionary<TimeframeSymbol, Timeframe> timeframes;


        public static Timeframe GetTimeframe(TimeframeSymbol symbol)
        {

            if (timeframes == null) LoadTimeframes();

            Timeframe timeframe = null;
            timeframes.TryGetValue(symbol, out timeframe);

            return timeframe;

        }

        public static Timeframe GetTimeframeByPeriod(double period)
        {

            if (timeframes == null) LoadTimeframes();

            Timeframe[] lower = timeframes.Values.Where(t => t.Period <= period).ToArray();

            if (lower.Length == 0){
                return null;
            } else {
                return lower.OrderByDescending(t => t.Id).Take(1).ToArray()[0];
            }

        }

        public static Timeframe GetTimeframeByShortName(string name)
        {

            if (timeframes == null) LoadTimeframes();

            var filtered = timeframes.Values.Where(t => t.Name.Equals(name)).ToArray();
            return (filtered.Length == 0 ? null : filtered[0]);

        }


        private static void LoadTimeframes()
        {
            timeframes = new Dictionary<TimeframeSymbol, Timeframe>();
            timeframes.Add(TimeframeSymbol.M5, new Timeframe { Id = 1, Index = 1, Name = "M5", Period = (5d / (60d * 24d)) });
            timeframes.Add(TimeframeSymbol.M15, new Timeframe { Id = 2, Index = 2, Name = "M15", Period = (1d / 96d) });
            timeframes.Add(TimeframeSymbol.M30, new Timeframe { Id = 3, Index = 3, Name = "M30", Period = (1d / 48d) });
            timeframes.Add(TimeframeSymbol.H1, new Timeframe { Id = 4, Index = 4, Name = "H1", Period = (1d / 24d) });
            timeframes.Add(TimeframeSymbol.H4, new Timeframe { Id = 5, Index = 5, Name = "H4", Period = (1d / 6d) });
            timeframes.Add(TimeframeSymbol.D1, new Timeframe { Id = 6, Index = 6, Name = "D1", Period = 1d });
            timeframes.Add(TimeframeSymbol.W1, new Timeframe { Id = 7, Index = 7, Name = "W1", Period = 7d });
            timeframes.Add(TimeframeSymbol.MN1, new Timeframe { Id = 8, Index = 8, Name = "MN1", Period = 28d });

        }

        

    }
}
