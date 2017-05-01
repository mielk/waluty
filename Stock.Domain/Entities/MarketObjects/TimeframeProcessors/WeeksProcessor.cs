using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.Utils;

namespace Stock.Domain.Entities.MarketObjects.TimeframeProcessors
{
    public class WeeksProcessor : ITimeframeProcessor
    {

        private const TimeframeUnit TIMEFRAME_UNIT = TimeframeUnit.Weeks;
        private const DayOfWeek firstDayOfWeek = DayOfWeek.Sunday;

        public TimeframeUnit GetTimeframeUnit()
        {
            return TIMEFRAME_UNIT;
        }

        public DateTime GetProperDateTime(DateTime baseDate, int periodLength)
        {
            int dayOfWeek = (int) baseDate.DayOfWeek;
            return baseDate.Add(TimeSpan.FromDays(-dayOfWeek)).Midnight();
        }

        private TimeSpan getTimeSpan(int periodLength)
        {
            return new TimeSpan(periodLength * 7, 0, 0, 0);
        }


        public DateTime GetNext(DateTime baseDate, int periodLength)
        {
            return GetProperDateTime(baseDate, periodLength).AddDays(periodLength * 7);
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
