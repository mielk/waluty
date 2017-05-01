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
        private IHolidaysManager holidaysManager = new HolidaysManager();

        public TimeframeUnit GetTimeframeUnit()
        {
            return TIMEFRAME_UNIT;
        }

        private TimeSpan getTimeSpan(int units)
        {
            return new TimeSpan(0, units, 0);
        }


        #region MANAGE_HOLIDAYS

        public void SetHolidaysManager(IHolidaysManager manager)
        {
            this.holidaysManager = manager;
        }

        public void AddHoliday(DateTime holiday)
        {
            this.holidaysManager.AddHoliday(holiday);
        }

        public void LoadHolidays(List<DateTime> holidays)
        {
            this.holidaysManager.LoadHolidays(holidays);
        }

        #endregion MANAGE_HOLIDAYS


        public DateTime GetProperDateTime(DateTime baseDate, int periodLength)
        {

            if (holidaysManager.IsHoliday(baseDate))
            {
                DateTime dayBefore = getDateWithLastBeforeHolidayTime(baseDate.DayBefore());
                return GetProperDateTime(dayBefore, periodLength);
            }
            else if (baseDate.IsWeekend())
            {
                DateTime dayBefore = getDateWithLastBeforeWeekendTime(baseDate, periodLength);
                return GetProperDateTime(dayBefore, periodLength);
            }
            else
            {
                if (holidaysManager.IsHolidayEveAfterMarketClose(baseDate))
                {
                    return getDateWithLastBeforeHolidayTime(baseDate);
                }
                else
                {
                    return getRoundedDateTime(baseDate, periodLength);
                }
            }

        }
        
        private DateTime getDateWithLastBeforeHolidayTime(DateTime datetime)
        {
            return datetime.AddDays(1).Midnight().Subtract(holidaysManager.GetHolidayEveBreak());
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
            DateTime currentProperTimestamp = GetProperDateTime(baseDate, periodLength);
            DateTime nextTimestamp = currentProperTimestamp.AddMinutes(periodLength);
            if (!holidaysManager.IsWorkingTime(nextTimestamp)){
                return holidaysManager.GetNextWorkingDay(nextTimestamp);
            }
            else 
            {
                return nextTimestamp;
            }
        }


        public DateTime AddTimeUnits(DateTime baseDate, int periodLength, int units)
        {
            DateTime datetime = GetProperDateTime(baseDate, periodLength);
            DateTime nextTimeOff = holidaysManager.GetNextTimeOff(datetime);
            DateTime dateAfterAdd = datetime.AddMinutes(periodLength * units);

            if (dateAfterAdd.IsEarlierThan(nextTimeOff))
            {
                return dateAfterAdd;
            }
            else
            {
                return dateAfterAdd;
            }

        }


        public int CountTimeUnits(DateTime baseDate, DateTime comparedDate, int periodLength)
        {
            bool isProperOrder = baseDate.IsEarlierThan(comparedDate);
            DateTime startDate = GetProperDateTime(isProperOrder ? baseDate : comparedDate, periodLength);
            DateTime endDate = GetProperDateTime(isProperOrder ? comparedDate : baseDate, periodLength);

            int part = 0;

            while (startDate.IsEarlierThan(endDate)){
                DateTime nextTimeOff = holidaysManager.GetNextTimeOff(startDate);
                if (nextTimeOff.IsEarlierThan(endDate))
                {
                    DateTime nextWorkingTimestamp = holidaysManager.GetNextWorkingDay(nextTimeOff);
                    TimeSpan difference = nextTimeOff - startDate;
                    int units = ((int)difference.TotalMinutes / periodLength);
                    startDate = nextWorkingTimestamp;
                    part += units;
                }
                else
                {
                    TimeSpan difference = endDate - startDate;
                    int units = ((int)difference.TotalMinutes / periodLength);
                    part += units;
                    startDate = endDate;
                }
            }

            return part;

        }

    }
}
