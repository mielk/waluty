using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.Utils;

namespace Stock.Domain.Entities.MarketObjects.TimeframeProcessors
{
    public class MonthsProcessor : ITimeframeProcessor
    {

        private const TimeframeUnit TIMEFRAME_UNIT = TimeframeUnit.Months;

        public TimeframeUnit GetTimeframeUnit()
        {
            return TIMEFRAME_UNIT;
        }

        public DateTime GetProperDateTime(DateTime baseDate, int periodLength)
        {
            return new DateTime(baseDate.Year, baseDate.Month, 1, 0, 0, 0);
        }

        public DateTime GetNext(DateTime baseDate, int periodLength)
        {
            return GetProperDateTime(baseDate, periodLength).AddMonths(1);
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
