using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Services;

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



        #region countTimeUnits

        public static int countTimeUnits(DateTime baseDate, DateTime comparedDate, TimeframeSymbol timeframe)
        {
            DateTime properBaseDate = baseDate.Proper(timeframe);
            DateTime properComparedDate = comparedDate.Proper(timeframe);
            switch (timeframe)
            {
                case TimeframeSymbol.MN1:
                    return countTimeUnits_month(properBaseDate, properComparedDate);
                case TimeframeSymbol.W1:
                    return countTimeUnits_weeks(properBaseDate, properComparedDate);
                case TimeframeSymbol.D1:
                    return countTimeUnits_days(properBaseDate, properComparedDate);
                case TimeframeSymbol.H4:
                case TimeframeSymbol.H1:
                case TimeframeSymbol.M30:
                case TimeframeSymbol.M15:
                case TimeframeSymbol.M5:
                    return countTimeUnits_shortPeriod(properBaseDate, properComparedDate, timeframe);
            }

            return 0;
        }

        private static int countTimeUnits_month(DateTime baseDate, DateTime comparedDate)
        {
            return comparedDate.Month - baseDate.Month + (comparedDate.Year - baseDate.Year) * 12;
        }

        private static int countTimeUnits_weeks(DateTime baseDate, DateTime comparedDate)
        {
            return baseDate.WeeksDifference(comparedDate);
        }

        private static int countTimeUnits_days(DateTime baseDate, DateTime comparedDate)
        {

            DateTime realBaseDate = baseDate.Proper(TimeframeSymbol.D1);
            DateTime realComparedDate = comparedDate.Proper(TimeframeSymbol.D1);

            int weeks = countTimeUnits_weeks(realBaseDate, realComparedDate);

            //Include year breaks.
            int yearBreaks = realBaseDate.countNewYearBreaks(realComparedDate, false);
            int christmas = realBaseDate.countChristmas(realComparedDate, false);
            return (realComparedDate - realBaseDate).Days - (weeks * 2) - (yearBreaks + christmas) * (realBaseDate.CompareTo(realComparedDate) < 0 ? 1 : -1);
        }

        private static int countTimeUnits_shortPeriod(DateTime baseDate, DateTime comparedDate, TimeframeSymbol timeframe)
        {
            DateTime properBaseDate = baseDate.Proper(timeframe);
            DateTime properComparedDate = comparedDate.Proper(timeframe);
            TimeSpan span = getTimespan(timeframe);
            int spanMinutes = span.Hours * 60 + span.Minutes;

            long datesMinutesDifference = (properComparedDate - properBaseDate).Ticks / 600000000;
            int result = (int) datesMinutesDifference / spanMinutes;
            int excluded = countExcludedItems(baseDate, comparedDate, timeframe);
            return result - countExcludedItems(baseDate, comparedDate, timeframe);

        }

        #endregion countTimeUnits


        #region addTimeUnits

        public static DateTime addTimeUnits(DateTime date, TimeframeSymbol timeframe, int units)
        {

            switch (timeframe)
            {
                case TimeframeSymbol.MN1:
                    return addTimeUnits_month(date, units);
                case TimeframeSymbol.W1:
                    return addTimeUnits_weeks(date.Proper(timeframe), units);
                case TimeframeSymbol.D1:
                    return addTimeUnits_days(date.Proper(timeframe), units);
                case TimeframeSymbol.H4:
                case TimeframeSymbol.H1:
                case TimeframeSymbol.M30:
                case TimeframeSymbol.M15:
                case TimeframeSymbol.M5:
                    return addTimeUnits_shortPeriods(date.Proper(timeframe), timeframe, units);
            }

            return date;

        }


        private static DateTime addTimeUnits_month(DateTime date, int units)
        {
            return date.AddMonths(units);
        }

        private static DateTime addTimeUnits_weeks(DateTime date, int units)
        {
            return date.AddDays(units * 7);
        }

        private static int countWeekendItems(DateTime startDate, DateTime endDate, TimeframeSymbol timeframe)
        {
            DateTime realStartDate = startDate.Proper(TimeframeSymbol.W1);
            DateTime realEndDate = endDate.Proper(TimeframeSymbol.W1);
            int weeks = countTimeUnits_weeks(realStartDate, realEndDate);
            return weeks * dayUnitsForTimeframe(timeframe) * 2;
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

        private static DateTime addTimeUnits_days(DateTime date, int units)
        {
            int remainder = units % 5;
            int newWeekDay = (int)date.DayOfWeek + remainder;
            bool breakWeek = newWeekDay > 5 || newWeekDay <= 0;
            int daysToAdd = (units / 5) * 7 + remainder + Math.Sign(units) * (breakWeek ? 2 : 0);
            DateTime newDate = date.AddDays(daysToAdd);

            //Include year breaks.
            int yearBreaks = date.countNewYearBreaks(newDate, false);
            int christmas = date.countChristmas(newDate, false);
            if (yearBreaks + christmas > 0)
            {
                newDate = addTimeUnits_days(newDate, Math.Sign(units) * (yearBreaks + christmas));
            }

            //Check if the result date is not New Year nor Christmas.
            while (newDate.DayOfWeek == DayOfWeek.Sunday || newDate.DayOfWeek == DayOfWeek.Saturday || newDate.DayOfYear == 1 || newDate.IsChristmas())
            {
                int sign = Math.Sign(units);
                newDate = newDate.AddDays(sign);
            }

            return newDate;

        }

        private static DateTime addTimeUnits_shortPeriods(DateTime date, TimeframeSymbol timeframe, int units)
        {

            DateTime startDate = new DateTime(date.Ticks).Proper(timeframe);
            TimeSpan span = (Math.Sign(units) == 1 ? getTimespan(timeframe) : getTimespan(timeframe).invert());
            int sign = Math.Sign(units);

            for(var i = 1; i <= Math.Abs(units); i++){
                startDate = startDate.Add(span);

                if (!startDate.isOpenMarketTime())
                {
                    DateTime nextOpenMarketTime = startDate.nextOpenMarketTime();
                    DateTime proper = startDate.Proper(timeframe);
                    startDate = (sign > 0 ? startDate.nextOpenMarketTime() : startDate.Proper(timeframe));   
                }
            }

            return startDate;

        }



        public static DateTime getChristmasProperDate(DateTime date, TimeframeSymbol timeframe)
        {
            DateTime d = date.getChristmasExactDate();
            int units = getTimeframeHolidayInactiveUnits(timeframe);
            TimeSpan span = getTimespan(timeframe, (-1) * units - 1);
            return d.Add(span);
        }

        public static DateTime getNewYearProperDate(DateTime date, TimeframeSymbol timeframe)
        {
            DateTime d = date.getNewYearExactDate();
            int units = getTimeframeHolidayInactiveUnits(timeframe);
            TimeSpan span = getTimespan(timeframe, (-1) * units - 1);
            return d.Add(span);
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


        private static int countExcludedItems(DateTime startDate, DateTime endDate, TimeframeSymbol timeframe)
        {
            int weekends = 0;
            int christmas = 0;
            int newYears = 0;
            int sign = (startDate.CompareTo(endDate) < 0 ? 1 : -1);

            weekends = countWeekendItems(startDate, endDate, timeframe);
            newYears = sign * startDate.countNewYearBreaks(endDate, false) * (dayUnitsForTimeframe(timeframe) + getTimeframeHolidayInactiveUnits(timeframe));
            christmas = sign * startDate.countChristmas(endDate, false) * (dayUnitsForTimeframe(timeframe) + getTimeframeHolidayInactiveUnits(timeframe));

            return (weekends + christmas + newYears);

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

        #endregion addTimeUnits




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
