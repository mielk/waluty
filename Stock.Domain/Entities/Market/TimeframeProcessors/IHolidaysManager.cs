using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities.MarketObjects.TimeframeProcessors
{
    public interface IHolidaysManager
    {
        void AddHoliday(DateTime holiday);
        void LoadHolidays(List<DateTime> holidays);
        void SetHolidayEveBreak(TimeSpan span);
        TimeSpan GetHolidayEveBreak();
        bool IsHoliday(DateTime datetime);
        bool IsDayBeforeHoliday(DateTime datetime);
        bool IsWorkingDay(DateTime datetime);
        bool IsWorkingTime(DateTime datetime);
        bool IsDayOff(DateTime datetime);
        DateTime GetNextWorkingDay(DateTime datetime);
        bool IsHolidayEveAfterMarketClose(DateTime datetime);
        DateTime? GetNextHoliday(DateTime datetime);
        DateTime? GetNextHoliday(DateTime startDate, DateTime endDate);
        DateTime GetNextTimeOff(DateTime datetime);
        DateTime GetPreviousTimeOff(DateTime datetime);
    }
}
