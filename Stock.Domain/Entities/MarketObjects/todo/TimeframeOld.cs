using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Services;
using Stock.Domain.Enums;
using Stock.DAL.TransferObjects;

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


    public class TimeframeOld_noWorking
    {

        //Static properties.
//        private static IAssetService service = ServiceFactory.GetAssetService();

        //Instance properties.
        private int id;
        private string name;
        private TimeframeUnit unitType;
        private int unitsCounter;
        public TimeframeSymbol Symbol { get; set; }


        #region CONSTRUCTORS

        public TimeframeOld_noWorking(int id, string name, TimeframeUnit unitType, int unitsCounter)
        {
            this.id = id;
            this.name = name;
            this.unitType = unitType;
            this.unitsCounter = unitsCounter;
        }

        public static TimeframeOld_noWorking FromDto(TimeframeDto dto)
        {
            //TimeframeUnit unitType = dto.PeriodUnit;
            //return new Timeframe(dto.Id, dto.Name
            return null;
        }

        #endregion CONSTRUCTORS

        #region GETTERS

        public int GetId()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public TimeframeUnit GetUnitType()
        {
            return unitType;
        }

        public int GetUnitsCounter()
        {
            return unitsCounter;
        }

        #endregion GETTERS

        public static TimeframeOld_noWorking GetTimeframe(TimeframeSymbol symbol)
        {

            //if (timeframes == null) LoadTimeframes();

            //Timeframe timeframe = null;
            //timeframes.TryGetValue(symbol, out timeframe);

            //return timeframe;
            return null;

        }

        public static TimeframeOld_noWorking GetTimeframeByPeriod(double period)
        {

            //if (timeframes == null) LoadTimeframes();

            //Timeframe[] lower = timeframes.Values.Where(t => t.Period <= period).ToArray();

            //if (lower.Length == 0){
            //    return null;
            //} else {
            //    return lower.OrderByDescending(t => t.Id).Take(1).ToArray()[0];
            //}

            return null;

        }

        public static TimeframeOld_noWorking GetTimeframeByShortName(string name)
        {

            //if (timeframes == null) LoadTimeframes();

            //var filtered = timeframes.Values.Where(t => t.name.Equals(name)).ToArray();
            //return (filtered.Length == 0 ? null : filtered[0]);
            return null;
        }


        public static int countTimeUnits(DateTime baseDate, DateTime comparedDate, TimeframeSymbol timeframe)
        {
            return 0;
        }

        public static DateTime addTimeUnits(DateTime date, TimeframeSymbol timeframe, int units)
        {

            return new DateTime();

        }

        private static int countWeekendItems(DateTime startDate, DateTime endDate, TimeframeSymbol timeframe)
        {
            return 0;
        }

        private static int dayUnitsForTimeframe(TimeframeSymbol timeframe)
        {
            switch (timeframe)
            {
                case TimeframeSymbol.D1: return 1;
                case TimeframeSymbol.H4: return 6;
                case TimeframeSymbol.H1: return 24;
                case TimeframeSymbol.M30: return 48;
                case TimeframeSymbol.M15: return 96;
                case TimeframeSymbol.M5: return 288;
                default: return 0;
            }
        }


        private static int getTimeframeHolidayInactiveUnits(TimeframeSymbol timeframe)
        {
            switch (timeframe)
            {
                case TimeframeSymbol.H4: return 0;
                case TimeframeSymbol.H1: return 2;
                case TimeframeSymbol.M30: return 5;
                case TimeframeSymbol.M15: return 11;
                case TimeframeSymbol.M5: return 35;
                default: return 0;
            }
        }


        public static TimeSpan getTimespan(TimeframeSymbol timeframe)
        {
            return getTimespan(timeframe, 1);
        }

        public static TimeSpan getTimespan(TimeframeSymbol timeframe, int units)
        {
            switch (timeframe)
            {
                case TimeframeSymbol.H4: return new TimeSpan(units * 4, 0, 0);
                case TimeframeSymbol.H1: return new TimeSpan(units * 1, 0, 0);
                case TimeframeSymbol.M30: return new TimeSpan(0, units * 30, 0);
                case TimeframeSymbol.M15: return new TimeSpan(0, units * 15, 0);
                case TimeframeSymbol.M5: return new TimeSpan(0, units * 5, 0);
            }

            return new TimeSpan(0);

        }



        private static void LoadTimeframes()
        {
            //timeframes = new Dictionary<TimeframeSymbol, Timeframe>();
            //timeframes.Add(TimeframeSymbol.M5, new Timeframe { Id = 1, Index = 1, name = "M5", Symbol = TimeframeSymbol.M5, Period = (5d / (60d * 24d)) });
            //timeframes.Add(TimeframeSymbol.M15, new Timeframe { Id = 2, Index = 2, name = "M15", Symbol = TimeframeSymbol.M15, Period = (1d / 96d) });
            //timeframes.Add(TimeframeSymbol.M30, new Timeframe { Id = 3, Index = 3, name = "M30", Symbol = TimeframeSymbol.M30, Period = (1d / 48d) });
            //timeframes.Add(TimeframeSymbol.H1, new Timeframe { Id = 4, Index = 4, name = "H1", Symbol = TimeframeSymbol.H1, Period = (1d / 24d) });
            //timeframes.Add(TimeframeSymbol.H4, new Timeframe { Id = 5, Index = 5, name = "H4", Symbol = TimeframeSymbol.H4, Period = (1d / 6d) });
            //timeframes.Add(TimeframeSymbol.D1, new Timeframe { Id = 6, Index = 6, name = "D1", Symbol = TimeframeSymbol.D1, Period = 1d });
            //timeframes.Add(TimeframeSymbol.W1, new Timeframe { Id = 7, Index = 7, name = "W1", Symbol = TimeframeSymbol.W1, Period = 7d });
            //timeframes.Add(TimeframeSymbol.MN1, new Timeframe { Id = 8, Index = 8, name = "MN1", Symbol = TimeframeSymbol.MN1, Period = 28d });

        }

        

    }

}
