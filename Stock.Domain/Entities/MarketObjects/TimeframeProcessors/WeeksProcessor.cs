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




            //DateTime startDate = new DateTime(date.Ticks).Proper(timeframe);
            //TimeSpan span = (Math.Sign(units) == 1 ? getTimespan(timeframe) : getTimespan(timeframe).invert());
            //int sign = Math.Sign(units);

            //for(var i = 1; i <= Math.Abs(units); i++){
            //    startDate = startDate.Add(span);

            //    if (!startDate.isOpenMarketTime())
            //    {
            //        DateTime nextOpenMarketTime = startDate.ifNotOpenMarketGetNext();
            //        DateTime proper = startDate.Proper(timeframe);
            //        startDate = (sign > 0 ? startDate.ifNotOpenMarketGetNext() : startDate.Proper(timeframe));   
            //    }
            //}

            //return startDate;















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
