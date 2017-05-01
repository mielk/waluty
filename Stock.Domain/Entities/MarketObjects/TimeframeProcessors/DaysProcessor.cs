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
            DateTime datetime = GetProperDateTime(baseDate, periodLength);
            if (units >= 0)
            {
                return addTimeUnits_properItemPositiveUnits(baseDate, periodLength, units);
            }
            else
            {
                return addTimeUnits_properItemNegativeUnits(baseDate, periodLength, units);
            }

        }

        private DateTime addTimeUnits_properItemPositiveUnits(DateTime baseDate, int periodLength, int units)
        {
            DateTime nextTimeOff = holidaysManager.GetNextTimeOff(baseDate);
            DateTime firstWorkingAfterTimeOff = holidaysManager.GetNextWorkingDay(nextTimeOff);
            DateTime dateAfterPlainAdd = baseDate.AddDays(periodLength * units);

            if (dateAfterPlainAdd.IsEarlierThan(nextTimeOff))
            {
                return dateAfterPlainAdd;
            }
            else
            {
                int itemsBeforeFirstDayOff = CountTimeUnits(baseDate, firstWorkingAfterTimeOff, periodLength);
                int remainingItems = units - itemsBeforeFirstDayOff;
                return AddTimeUnits(firstWorkingAfterTimeOff, periodLength, remainingItems);
            }

        }

        private DateTime addTimeUnits_properItemNegativeUnits(DateTime baseDate, int periodLength, int units)
        {
            DateTime previousTimeOff = holidaysManager.GetPreviousTimeOff(baseDate);
            DateTime previousTimeOffEnd = holidaysManager.GetNextWorkingDay(previousTimeOff);
            DateTime lastWorkingBeforeTimeOff = GetProperDateTime(previousTimeOff, periodLength);
            DateTime dateAfterPlainSubtract = baseDate.AddDays(periodLength * units);

            if (dateAfterPlainSubtract.IsNotEarlierThan(previousTimeOffEnd))
            {
                return dateAfterPlainSubtract;
            }
            else
            {
                int itemsAfterLastDayOff = CountTimeUnits(baseDate, lastWorkingBeforeTimeOff, periodLength);
                int remainingItems = units - itemsAfterLastDayOff;
                return AddTimeUnits(lastWorkingBeforeTimeOff, periodLength, remainingItems);
            }
        }



        public int CountTimeUnits(DateTime baseDate, DateTime comparedDate, int periodLength)
        {
            bool isProperOrder = baseDate.IsEarlierThan(comparedDate);
            DateTime startDate = GetProperDateTime(isProperOrder ? baseDate : comparedDate, periodLength);
            DateTime endDate = GetProperDateTime(isProperOrder ? comparedDate : baseDate, periodLength);
            int counter = countTimeUnits_inProperOrder(startDate, endDate, periodLength);
            return counter * (isProperOrder ? 1 : -1);
        }

        private int countTimeUnits_inProperOrder(DateTime startDate, DateTime endDate, int periodLength)
        {

            DateTime? nextHolidayNullSafe = holidaysManager.GetNextHoliday(startDate, endDate);
            if (nextHolidayNullSafe != null)
            {
                DateTime nextHoliday = (DateTime)nextHolidayNullSafe;
                DateTime lastBeforeHoliday = GetProperDateTime(nextHoliday, periodLength);
                DateTime firstAfterHoliday = holidaysManager.GetNextWorkingDay(nextHoliday);
                int itemsToNextHoliday = countTimeUnits_inProperOrder(startDate, lastBeforeHoliday, periodLength);
                int itemsFromNextHoliday = countTimeUnits_inProperOrder(firstAfterHoliday, endDate, periodLength);
                return itemsToNextHoliday + 1 + itemsFromNextHoliday;
            }
            else
            {
                int weeksDifference = startDate.GetWeeksDifferenceTo(endDate);
                int periodPerWeekend = (int)TimeSpan.FromDays(2).TotalDays / periodLength;
                int totalWeekendPeriods = weeksDifference * periodPerWeekend;
                int totalUnitsDifference = (int)(endDate - startDate).TotalDays / periodLength;
                return totalUnitsDifference - totalWeekendPeriods;
            }

        }



    }
}
