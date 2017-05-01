using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Utils
{
    public static class DateTimeHelperMethods
    {

        public static DateTime DayBefore(this DateTime value)
        {
            return value.Add(TimeSpan.FromDays(-1));
        }

        public static DateTime SetTime(this DateTime value, TimeSpan span)
        {
            return value.Midnight().Add(span);
        }

        public static int DayOfWeekTm(this DateTime value)
        {
            return (value.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)value.DayOfWeek);
        }

        public static DateTime Midnight(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static bool IsWeekend(this DateTime value)
        {
            return (value.DayOfWeek == DayOfWeek.Saturday || value.DayOfWeek == DayOfWeek.Sunday);
        }

        public static DateTime GetWeekendStart(this DateTime value)
        {
            int daysToAdd = 6 - value.DayOfWeekTm();
            TimeSpan timeSpan = new TimeSpan(daysToAdd, 0, 0, 0);
            return value.Add(timeSpan).Midnight();
        }

    }
}
