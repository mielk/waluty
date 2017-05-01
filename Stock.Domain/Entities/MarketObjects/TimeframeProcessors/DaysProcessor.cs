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
        private IHolidaysManager holidaysManager = new HolidaysManager();

        public TimeframeUnit GetTimeframeUnit()
        {
            return TIMEFRAME_UNIT;
        }

        private TimeSpan GetTimeSpan(int units)
        {
            return new TimeSpan(units, 0, 0, 0);
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
            DateTime currentProperTimestamp = GetProperDateTime(baseDate, periodLength);
            DateTime nextTimestamp = currentProperTimestamp.AddDays(periodLength);
            if (!holidaysManager.IsWorkingDay(nextTimestamp))
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
