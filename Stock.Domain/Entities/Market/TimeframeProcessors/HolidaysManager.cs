using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.Utils;

namespace Stock.Domain.Entities.MarketObjects.TimeframeProcessors
{
    public class HolidaysManager : IHolidaysManager
    {
        private List<DateTime> holidays = new List<DateTime>();
        private TimeSpan holidayEveBreak = new TimeSpan(3, 0, 0);


        public void AddHoliday(DateTime holiday)
        {
            holidays.Add(holiday);
        }

        public void LoadHolidays(List<DateTime> holidays)
        {
            this.holidays = holidays;
        }

        public void SetHolidayEveBreak(TimeSpan span)
        {
            this.holidayEveBreak = span;
        }

        public TimeSpan GetHolidayEveBreak()
        {
            return holidayEveBreak;
        }

        public bool IsHoliday(DateTime datetime)
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

        public bool IsDayBeforeHoliday(DateTime datetime)
        {
            DateTime nextDay = datetime.Add(TimeSpan.FromDays(1));
            return IsHoliday(nextDay);
        }

        public bool IsWorkingDay(DateTime datetime)
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

        public bool IsWorkingTime(DateTime datetime)
        {
            if (IsWorkingDay(datetime))
            {
                return !IsHolidayEveAfterMarketClose(datetime);
            }
            else
            {
                return false;
            }
        }

        public bool IsDayOff(DateTime datetime)
        {
            return !IsWorkingDay(datetime);
        }

        public DateTime GetNextWorkingDay(DateTime datetime)
        {
            DateTime tempDatetime = datetime.AddDays(1);
            while (!IsWorkingDay(tempDatetime))
            {
                tempDatetime = tempDatetime.AddDays(tempDatetime.DayOfWeek == DayOfWeek.Saturday ? 2 : 1);
            }
            return tempDatetime.Midnight();
        }

        public bool IsHolidayEveAfterMarketClose(DateTime datetime)
        {

            if (IsDayBeforeHoliday(datetime))
            {
                DateTime timeOff = datetime.AddDays(1).Midnight().Subtract(holidayEveBreak);
                return (datetime.CompareTo(timeOff) > 0);
            }
            else
            {
                return false;
            }

        }

        public DateTime? GetNextHoliday(DateTime datetime)
        {
            IEnumerable<DateTime> futureHolidays = holidays.Where(d => d.CompareTo(datetime) > 0).OrderBy(d => d.Date);
            if (futureHolidays.Count() > 0)
            {
                return futureHolidays.First();
            }
            else
            {
                return null;
            }
        }
        
        public DateTime? GetNextHoliday(DateTime startDate, DateTime endDate)
        {
            IEnumerable<DateTime> futureHolidays = holidays.Where(d => d.CompareTo(startDate) > 0 && d.CompareTo(endDate) <= 0).OrderBy(d => d.Date);
            if (futureHolidays.Count() > 0)
            {
                return futureHolidays.First();
            }
            else
            {
                return null;
            }
        }

        private DateTime? GetPreviousHoliday(DateTime datetime)
        {
            IEnumerable<DateTime> pastHolidays = holidays.Where(d => d.CompareTo(datetime) < 0).OrderByDescending(d => d.Date);
            if (pastHolidays.Count() > 0)
            {
                return pastHolidays.First();
            }
            else
            {
                return null;
            }
        }
        

        public DateTime GetNextTimeOff(DateTime datetime)
        {

            DateTime? nextHoliday = GetNextHoliday(datetime);
            DateTime nextWeekend = datetime.GetWeekendStart();

            if (nextHoliday != null && ((DateTime)nextHoliday).CompareTo(nextWeekend) < 0)
            {
                return ((DateTime)nextHoliday).Midnight().Subtract(holidayEveBreak);
            }
            else
            {
                return nextWeekend;
            }
        }

        public DateTime GetPreviousTimeOff(DateTime datetime)
        {
            DateTime? previousHolidayStartNullSafe = GetPreviousHoliday(datetime);
            DateTime previousWeekendStart = datetime.AddDays(-7).GetWeekendStart();

            if (previousHolidayStartNullSafe != null)
            {
                DateTime previousHolidayStart = (DateTime)previousHolidayStartNullSafe;
                if (previousWeekendStart.IsEarlierThan(previousHolidayStart))
                {
                    return previousHolidayStart.Subtract(holidayEveBreak);
                }
                else
                {
                    return previousWeekendStart;
                }
            }
            else
            {
                return previousWeekendStart;
            }

        }

    }
}
