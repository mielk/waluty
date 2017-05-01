using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.Utils;

namespace Stock.Domain.Entities.MarketObjects.TimeframeProcessors
{
    public class MinutesProcessor : ITimeframeProcessor
    {

        private const TimeframeUnit TIMEFRAME_UNIT = TimeframeUnit.Minutes;
        private List<DateTime> holidays = new List<DateTime>();
        private TimeSpan beforeHolidayLastValue = new TimeSpan(21, 0, 0);

        public TimeframeUnit GetTimeframeUnit()
        {
            return TIMEFRAME_UNIT;
        }

        private TimeSpan getTimeSpan(int units)
        {
            return new TimeSpan(0, units, 0);
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

        private bool IsDayBeforeHoliday(DateTime datetime)
        {
            return IsHoliday(datetime.Add(TimeSpan.FromDays(1)));
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

        private bool IsWorkingTime(DateTime datetime)
        {
            if (IsWorkingDay(datetime))
            {
                if (IsDayBeforeHoliday(datetime))
                {
                    return !isTimeAfterHolidayEveMarketClose(datetime);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private bool IsDayOff(DateTime datetime)
        {
            return !IsWorkingDay(datetime);
        }

        private DateTime getNextWorkingDay(DateTime datetime)
        {
            DateTime tempDatetime = datetime.AddDays(1);
            while (!IsWorkingDay(tempDatetime))
            {
                tempDatetime.AddDays(tempDatetime.DayOfWeek == DayOfWeek.Saturday ? 2 : 1);
            }
            return tempDatetime.Midnight();
        }

        #endregion MANAGE_HOLIDAYS


        public DateTime GetProperDateTime(DateTime baseDate, int periodLength)
        {

            if (IsHoliday(baseDate))
            {
                DateTime dayBefore = baseDate.DayBefore().SetTime(beforeHolidayLastValue);
                return GetProperDateTime(dayBefore, periodLength);
            }
            else if (baseDate.IsWeekend())
            {
                DateTime dayBefore = getDateWithLastBeforeWeekendTime(baseDate, periodLength);
                return GetProperDateTime(dayBefore, periodLength);
            }
            else
            {
                if (isTimeAfterHolidayEveMarketClose(baseDate) && IsDayBeforeHoliday(baseDate))
                {
                    return getDateWithLastBeforeHolidayTime(baseDate);
                }
                else
                {
                    return getRoundedDateTime(baseDate, periodLength);
                }
            }

        }
        
        private bool isTimeAfterHolidayEveMarketClose(DateTime datetime)
        {
            if (datetime.Hour > beforeHolidayLastValue.Hours)
            {
                return true;
            }
            else if (datetime.Hour == beforeHolidayLastValue.Hours)
            {
                return (datetime.Minute > beforeHolidayLastValue.Minutes);
            }
            else
            {
                return false;
            }
        }

        private DateTime getDateWithLastBeforeHolidayTime(DateTime datetime)
        {
            return datetime.Midnight().Add(beforeHolidayLastValue);
        }

        private DateTime getDateWithLastBeforeWeekendTime(DateTime datetime, int periodLength)
        {
            TimeSpan timeSpan = getTimeSpan(-periodLength);
            return datetime.GetWeekendStart().Add(timeSpan);
        }

        private DateTime getRoundedDateTime(DateTime datetime, int periodLength)
        {
            int minutes = (datetime.Minute / periodLength) * periodLength;
            TimeSpan timeSpan = new TimeSpan(datetime.Hour, minutes, 0);
            return datetime.Date.Add(timeSpan);
        }



        public DateTime GetNext(DateTime baseDate, int periodLength)
        {
            DateTime properDateTime = GetProperDateTime(baseDate, periodLength);
            DateTime nextDateTime = properDateTime.AddMinutes(periodLength);
            if (!IsWorkingTime(nextDateTime)){
                return getNextWorkingDay(nextDateTime);
            }
            else 
            {
                return nextDateTime;
            }
        }









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
