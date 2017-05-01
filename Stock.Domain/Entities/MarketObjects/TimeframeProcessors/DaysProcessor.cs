using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.Utils;

namespace Stock.Domain.Entities.MarketObjects.TimeframeProcessors
{
    public class DaysProcessor : ITimeframeProcessor
    {

        private const TimeframeUnit TIMEFRAME_UNIT = TimeframeUnit.Days;
        private List<DateTime> holidays = new List<DateTime>();
        private TimeSpan beforeHolidayLastValue = new TimeSpan(0, 0, 0);

        public TimeframeUnit GetTimeframeUnit()
        {
            return TIMEFRAME_UNIT;
        }

        private TimeSpan GetTimeSpan(int units)
        {
            return new TimeSpan(units, 0, 0, 0);
        }

        #region MANAGE_HOLIDAYS

        public void AddHoliday(DateTime holiday)
        {
            holidays.Add(holiday);
        }

        public void LoadHolidays(List<DateTime> holidays)
        {
            this.holidays = holidays;
        }

        private bool IsHoliday(DateTime datetime)
        {
            foreach (var holiday in holidays)
            {
                if (holiday.Date == datetime.Date)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsWorkingDay(DateTime datetime)
        {
            if (datetime.IsWeekend())
            {
                return false;
            }
            else
            {
                return !IsHoliday(datetime);
            }
        }

        private bool IsDayOff(DateTime datetime)
        {
            return !IsWorkingDay(datetime);
        }

        #endregion MANAGE_HOLIDAYS

        public DateTime GetProperDateTime(DateTime baseDate, int periodLength)
        {

            if (IsHoliday(baseDate))
            {
                DateTime previousDay = baseDate.Add(TimeSpan.FromDays(-periodLength));
                return GetProperDateTime(previousDay, periodLength);
            }
            else if (baseDate.IsWeekend())
            {
                DateTime dayBefore = baseDate.GetWeekendStart().Add(TimeSpan.FromDays(-periodLength));
                return GetProperDateTime(dayBefore, periodLength);
            }
            else
            {
                return baseDate.Midnight();
            }

        }

        public DateTime GetNext(DateTime baseDate, int periodLength)
        {
            return new DateTime();
        }




            //DateTime startDate = new DateTime(date.Ticks).Proper(timeframe);
            //TimeSpan span = (Math.Sign(units) == 1 ? getTimespan(timeframe) : getTimespan(timeframe).invert());
            //int sign = Math.Sign(units);

            //for(var i = 1; i <= Math.Abs(units); i++){
            //    startDate = startDate.Add(span);

            //    if (!startDate.isOpenMarketTime())
            //    {
            //        DateTime nextOpenMarketTime = startDate.ifNotOpenMarketGetNext();
            //        DateTime proper = startDate.Proper(timeframe);
            //        startDate = (sign > 0 ? startDate.ifNotOpenMarketGetNext() : startDate.Proper(timeframe));   
            //    }
            //}

            //return startDate;















        public int GetDifferenceBetweenDates(DateTime baseDate, DateTime comparedDate)
        {
            return 0;


        //private static int countTimeUnits_shortPeriod(DateTime baseDate, DateTime comparedDate, TimeframeSymbol timeframe)
        //{
        //    DateTime properBaseDate = baseDate.Proper(timeframe);
        //    DateTime properComparedDate = comparedDate.Proper(timeframe);
        //    TimeSpan span = getTimespan(timeframe);
        //    int spanMinutes = span.Hours * 60 + span.Minutes;

        //    long datesMinutesDifference = (properComparedDate - properBaseDate).Ticks / 600000000;
        //    int result = (int) datesMinutesDifference / spanMinutes;
        //    int excluded = countExcludedItems(baseDate, comparedDate, timeframe);
        //    return result - countExcludedItems(baseDate, comparedDate, timeframe);

        //}


        }

        public DateTime AddTimeUnits(DateTime baseDate, int units)
        {
            return new DateTime();
        }

    }
}
