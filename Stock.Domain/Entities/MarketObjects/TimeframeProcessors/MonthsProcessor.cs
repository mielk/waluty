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
