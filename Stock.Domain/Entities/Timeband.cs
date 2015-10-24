using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{

    public enum TimebandSymbol
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


    public class Timeband
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Period { get; set; }
        public int Index { get; set; }
        private static Dictionary<TimebandSymbol, Timeband> timebands;


        public static Timeband GetTimeband(TimebandSymbol symbol)
        {

            if (timebands == null) LoadTimebands();

            Timeband timeband = null;
            timebands.TryGetValue(symbol, out timeband);

            return timeband;

        }

        public static Timeband GetTimebandByPeriod(double period)
        {

            if (timebands == null) LoadTimebands();

            Timeband[] lower = timebands.Values.Where(t => t.Period <= period).ToArray();

            if (lower.Length == 0){
                return null;
            } else {
                return lower.OrderByDescending(t => t.Id).Take(1).ToArray()[0];
            }

        }

        public static Timeband GetTimebandByShortName(string name)
        {

            if (timebands == null) LoadTimebands();

            var filtered = timebands.Values.Where(t => t.Name.Equals(name)).ToArray();
            return (filtered.Length == 0 ? null : filtered[0]);

        }


        private static void LoadTimebands()
        {
            timebands = new Dictionary<TimebandSymbol, Timeband>();
            timebands.Add(TimebandSymbol.M5, new Timeband { Id = 1, Index = 1, Name = "M5", Period = (5d / (60d * 24d)) });
            timebands.Add(TimebandSymbol.M15, new Timeband { Id = 2, Index = 2, Name = "M15", Period = (1d / 96d) });
            timebands.Add(TimebandSymbol.M30, new Timeband { Id = 3, Index = 3, Name = "M30", Period = (1d / 48d) });
            timebands.Add(TimebandSymbol.H1, new Timeband { Id = 4, Index = 4, Name = "H1", Period = (1d / 24d) });
            timebands.Add(TimebandSymbol.H4, new Timeband { Id = 5, Index = 5, Name = "H4", Period = (1d / 6d) });
            timebands.Add(TimebandSymbol.D1, new Timeband { Id = 6, Index = 6, Name = "D1", Period = 1d });
            timebands.Add(TimebandSymbol.W1, new Timeband { Id = 7, Index = 7, Name = "W1", Period = 7d });
            timebands.Add(TimebandSymbol.MN1, new Timeband { Id = 8, Index = 8, Name = "MN1", Period = 28d });

        }

        

    }
}
