using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.Utils;

namespace Stock.Domain.Entities.MarketObjects.TimeframeProcessors
{
    public class HoursProcessor : ITimeframeProcessor
    {

        private const TimeframeUnit TIMEFRAME_UNIT = TimeframeUnit.Hours;
        private IHolidaysManager holidaysManager = new HolidaysManager();

        public TimeframeUnit GetTimeframeUnit()
        {
            return TIMEFRAME_UNIT;
        }

        private TimeSpan getTimeSpan(int units)
        {
            return new TimeSpan(units, 0, 0);
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
                int hours = (int) (baseDate.Hour / periodLength) * periodLength;
                TimeSpan timeSpan = new TimeSpan(hours, 0, 0);
                return baseDate.SetTime(timeSpan);
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

        public DateTime GetNext(DateTime baseDate, int periodLength)
        {
            DateTime currentProperTimestamp = GetProperDateTime(baseDate, periodLength);
            DateTime nextTimestamp = currentProperTimestamp.AddHours(periodLength);
            if (!holidaysManager.IsWorkingTime(nextTimestamp))
            {
                return holidaysManager.GetNextWorkingDay(nextTimestamp);
            }
            else
            {
                return nextTimestamp;
            }
        }


        public DateTime AddTimeUnits(DateTime baseDate, int periodLength, int units)
        {
            return new DateTime();
        }



        public int CountTimeUnits(DateTime baseDate, DateTime comparedDate, int periodLength)
        {
            return 0;
        }


    }
}
