using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.Domain.Services.Factories;

namespace Stock.Domain.Services
{


    public static class HelperMethods
    {


        public static IEnumerable<ExtremumGroup> GetExtremaGroups(this IEnumerable<DataItem> extrema){
            DataItem[] items = extrema.OrderBy(i => i.Date).ToArray();
            List<ExtremumGroup> list = new List<ExtremumGroup>();

            for (var i = 0; i < items.Length; i++)
            {
                DataItem item = items[i];
                ExtremumGroup group = new ExtremumGroup();
                if (i < items.Length - 1 && items[i + 1].Index - item.Index == 1)
                {
                    if (item.Price.IsExtremumByClosePrice())
                    {
                        group.master = item;
                        group.slave = items[i + 1];
                    }
                    else
                    {
                        group.master = items[i + 1];
                        group.slave = item;
                    }

                    i++;

                }
                else
                {
                    group.master = item;
                }

                group.type = item.Price.IsTrough() ? ExtremumType.TroughByClose : ExtremumType.PeakByClose;
                list.Add(group);

            }

            return list;
        }

        public static bool IsByClosePrice(this ExtremumType type)
        {
            return (type == ExtremumType.PeakByClose || type == ExtremumType.TroughByClose);
        }

        public static bool IsOpposite(this ExtremumType type, ExtremumType tested)
        {
            if (type == ExtremumType.PeakByClose || type == ExtremumType.PeakByHigh)
            {
                return (tested == ExtremumType.TroughByClose || tested == ExtremumType.TroughByLow);
            }
            else
            {
                return (tested == ExtremumType.PeakByClose || tested == ExtremumType.PeakByHigh);
            }
        }

        public static bool IsPeak(this ExtremumType type)
        {
            return (type == ExtremumType.PeakByClose || type == ExtremumType.PeakByHigh);
        }

        public static ExtremumType GetOppositeByPriceLevel(this ExtremumType type)
        {
            if (type == ExtremumType.PeakByClose) return ExtremumType.PeakByHigh;
            if (type == ExtremumType.PeakByHigh) return ExtremumType.PeakByClose;
            if (type == ExtremumType.TroughByClose) return ExtremumType.TroughByLow;
            if (type == ExtremumType.TroughByLow) return ExtremumType.TroughByClose;
            return ExtremumType.PeakByClose;
        }

        public static double GetExtremumEvaluationFactor(this TimeframeSymbol value)
        {
            switch (value)
            {
                case TimeframeSymbol.M5: return 30d;
                case TimeframeSymbol.M15: return 24d;
                case TimeframeSymbol.M30: return 12d;
                case TimeframeSymbol.H1: return 12d;
                case TimeframeSymbol.H4: return 6d;
                case TimeframeSymbol.D1: return 2d;
                case TimeframeSymbol.W1: return 1d;
                case TimeframeSymbol.MN1: return 0.5d;
            }

            return 1d;

        }

        public static TimeframeSymbol GetTimeframeSymbol(this string value)
        {

            var timeframeSymbol = value.Substring(value.IndexOf('_') + 1);

            if (timeframeSymbol.Equals("M5"))
            {
                return TimeframeSymbol.M5;
            }
            else if (timeframeSymbol.Equals("M15"))
            {
                return TimeframeSymbol.M15;
            }
            else if (timeframeSymbol.Equals("M30"))
            {
                return TimeframeSymbol.M30;
            }
            else if (timeframeSymbol.Equals("H1"))
            {
                return TimeframeSymbol.H1;
            }
            else if (timeframeSymbol.Equals("H4"))
            {
                return TimeframeSymbol.H4;
            }
            else if (timeframeSymbol.Equals("D1"))
            {
                return TimeframeSymbol.D1;
            }
            else if (timeframeSymbol.Equals("W1"))
            {
                return TimeframeSymbol.W1;
            }
            else if (timeframeSymbol.Equals("MN1"))
            {
                return TimeframeSymbol.MN1;
            } 
            else 
            {
                return TimeframeSymbol.MN1;
            }

        }



        public static string TableName(this AnalysisType type)
        {
            switch (type)
            {
                case AnalysisType.Price:
                    return "prices";
                case AnalysisType.MACD:
                    return "macd";
                case AnalysisType.ADX:
                    return "adx";
                case AnalysisType.Candlestick:
                    return "candlesticks";
                case AnalysisType.Trendline:
                    return "trendlines";
                default:
                    return "";
            }
        }


        public static bool ByClose(this ExtremumType type)
        {
            if (type == ExtremumType.PeakByClose || type == ExtremumType.TroughByClose)
                return true;

            return false;

        }

        public static void AppendIndexNumbers(this DataItem[] items)
        {

            for (var i = 0; i < items.Length; i++)
            {
                items[i].Index = i;
            }

        }

        public static int GetFactor(this TrendlineType type)
        {
            return (type == TrendlineType.Resistance ? 1 : -1);
        }

        public static bool isInRange(this double value, double low, double up)
        {
            return false;
        }

        public static bool isInRange(this int value, int low, int up)
        {

            int min;
            int max;

            if (low < up)
            {
                min = low;
                max = up;
            }
            else
            {
                min = low;
                max = up;
            }


            return (value >= min && value <= max);

        }





        public static DateTime? getEarliestDate(this IEnumerable<DateTime?> dates){

            DateTime? d = null;
            foreach (var dt in dates)
            {
                if (dt == null)
                {
                    return null;
                }
                else
                {
                    if (d == null || DateTime.Compare((DateTime)dt, (DateTime)d) < 0)
                    {
                        d = dt;
                    }
                }
            }

            return d;

        }

        public static IAnalyzer getAnalyzer(this AnalysisType type, Asset asset, Timeframe timeframe)
        {
            AssetTimeframe atf = new AssetTimeframe(asset, timeframe);
            return AnalyzerFactory.Instance().getAnalyzer(type, atf);
        }

        public static IAnalyzer getAnalyzer(this AnalysisType type, AssetTimeframe atf)
        {
            return AnalyzerFactory.Instance().getAnalyzer(type, atf);
        }

        public static string toString(this AnalysisType type)
        {
            return AnalysisTypeHelper.getTypeString(type);
        }


        public static DateTime addTimeUnits(this DateTime date, TimeframeSymbol timeframe, int units)
        {
            if (units == 0) return date.AddMilliseconds(0);
            return Timeframe.addTimeUnits(date, timeframe, units);
        }

        public static int countTimeUnits(this DateTime date, DateTime compared, TimeframeSymbol timeframe)
        {
            return Timeframe.countTimeUnits(date, compared, timeframe);
        }

        public static int countNewYearBreaks(this DateTime date, DateTime compared, bool includeWeekends)
        {
            return date.countSpecialDays(compared, includeWeekends, 1, 1);
        }

        public static int countChristmas(this DateTime date, DateTime compared, bool includeWeekends)
        {
            return date.countSpecialDays(compared, includeWeekends, 12, 25);
        }

        public static int countSpecialDays(this DateTime date, DateTime compared, bool includeWeekends, int month, int day)
        {

            DateTime earlier, later;
            int counter = 0;


            if (date.CompareTo(compared) > 0)
            {
                earlier = compared;
                later = date;
            }
            else
            {
                earlier = date;
                later = compared;
            }


            int startYear = (earlier.Month < month || earlier.Month == month && earlier.Day < day ? earlier.Year : earlier.Year + 1);
            int endYear = (later.Month > month || later.Month == month && later.Day >= day ? later.Year : later.Year - 1);


            if (includeWeekends)
            {
                return endYear - startYear + 1;
            }
            else
            {
                for (var i = startYear; i <= endYear; i++)
                {
                    DateTime specialDay = new DateTime(i, month, day);
                    if (specialDay.DayOfWeek != DayOfWeek.Sunday && specialDay.DayOfWeek != DayOfWeek.Saturday)
                    {
                        counter++;
                    }
                }

            }


            return counter;

        }



        public static int DaysBetween(this DateTime d1, DateTime d2) {
            return (d2 - d1).Days;
        }

        public static int WeeksDifference(this DateTime d1, DateTime d2)
        {
            DateTime real1 = d1.ProperWeek();
            DateTime real2 = d2.ProperWeek();
            return (real2 - real1).Days / 7;
        }

        public static bool IsChristmas(this DateTime d)
        {
            if (d.Month == 12 && d.Day == 25)
            {
                return true;
            }
            else
            {
                return (d.Month == 12 && d.Day == 24 && d.TimeOfDay.CompareTo(new TimeSpan(21, 0, 0)) > 0);
            }
        }


        public static bool IsNewYear(this DateTime d)
        {
            if (d.Month == 1 && d.Day == 1)
            {
                return true;
            }
            else
            {
                return (d.Month == 12 && d.Day == 31 && d.TimeOfDay.CompareTo(new TimeSpan(21, 0, 0)) > 0);
            }
        }


        public static DateTime Proper(this DateTime d, TimeframeSymbol timeframe)
        {
            switch (timeframe)
            {
                case TimeframeSymbol.MN1: return d.ProperMonth();
                case TimeframeSymbol.W1:  return d.ProperWeek();
                case TimeframeSymbol.D1:  return d.ProperDay();
                default: return d.ProperShortTime(timeframe);
            }

        }

        private static DateTime ProperMonth(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, 1);
        }

        private static DateTime ProperWeek(this DateTime d)
        {
            return d.AddDays(-1 * (int)d.DayOfWeek).midnight();
        }

        private static DateTime ProperDay(this DateTime d)
        {

            if (d.DayOfWeek == DayOfWeek.Saturday)
            {
                return d.AddDays(-1).ProperDay().midnight();
            }
            else if (d.DayOfWeek == DayOfWeek.Sunday)
            {
                return d.AddDays(-2).ProperDay().midnight();
            }
            else if (d.DayOfYear == 1)
            {
                return d.AddDays(-1).ProperDay().midnight();
            }

            return new DateTime(d.Ticks).midnight();

        }

        private static DateTime ProperShortTime(this DateTime d, TimeframeSymbol timeframe)
        {
            //To full time period.
            TimeSpan span = Timeframe.getTimespan(timeframe, 1);

            //include weekends
            if (d.isWeekend())
            {
                var dt = (d.DayOfWeek == DayOfWeek.Sunday ? d.AddDays(-1) : d.AddDays(0));
                var saturdayMidnight = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                return saturdayMidnight.Add(span.invert()).ProperShortTime(timeframe);

            }

            //include christmas & new year
            if (d.IsChristmas())
            {
                return Timeframe.getChristmasProperDate(d, timeframe).ProperShortTime(timeframe);
            }
            
            if (d.IsNewYear())
            {
                return Timeframe.getNewYearProperDate(d, timeframe).ProperShortTime(timeframe);
            }

            //To full time format.
            var hours = -1 * (span.Hours == 0 && span.Minutes == 0 ? d.Hour : (span.Minutes == 0 ? d.Hour % span.Hours : 0));
            var minutes = -1 * (span.Minutes == 0 ? d.Minute : d.Minute % span.Minutes);
            var seconds = -1 * (span.Seconds == 0 ? d.Second : d.Second % span.Seconds);
            TimeSpan toAdd = new TimeSpan(hours, minutes, seconds);

            return d.Add(toAdd);


        }


        public static TimeSpan invert(this TimeSpan span)
        {
            return new TimeSpan(span.Hours * -1, span.Minutes * -1, span.Seconds * -1);
        }

        public static TimeSpan multiply(this TimeSpan span, int multiplier)
        {
            return new TimeSpan(span.Hours * multiplier, span.Minutes * multiplier, span.Seconds * multiplier);
        }


        public static DateTime midnight(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static bool isWeekend(this DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday ? true : false);
        }

        public static bool isOpenMarketTime(this DateTime date)
        {
            if (date.isWeekend() || date.IsChristmas() || date.IsNewYear())
            {
                return false;
            }
            return true;
        }



        public static DateTime getChristmasExactDate(this DateTime date)
        {
            return new DateTime(date.Year, 12, 25, 0, 0, 0);
        }


        public static DateTime getNewYearExactDate(this DateTime date)
        {
            return new DateTime(date.Year + (date.Month == 12 ? 1 : 0), 1, 1, 0, 0, 0);
        }


        public static DateTime ifNotOpenMarketGetNext(this DateTime date)
        {
            if (isOpenMarketTime(date))
            {
                return new DateTime(date.Ticks);
            }
            else
            {

                if (date.IsChristmas()){
                    return ifNotOpenMarketGetNext(date.getChristmasExactDate().AddDays(1));
                }
                else if (date.IsNewYear())
                {
                    return ifNotOpenMarketGetNext(date.getNewYearExactDate().AddDays(1));
                } 
                else if (date.isWeekend())
                {
                    return ifNotOpenMarketGetNext(date.AddDays(date.DayOfWeek == DayOfWeek.Saturday ? 2 : 1).midnight());
                }

                return date;

            }
        }

        public static DateTime getNext(this DateTime date, TimeframeSymbol timeframe)
        {
            DateTime d = date.Proper(timeframe);
            switch (timeframe)
            {
                case TimeframeSymbol.MN1: return new DateTime(d.Year, d.Month + 1, 1, 0, 0, 0);
                case TimeframeSymbol.W1: return d.AddDays(7);
                case TimeframeSymbol.D1:
                    return d.AddDays(1).ifNotOpenMarketGetNext();
                default:
                    return d.Add(Timeframe.getTimespan(timeframe)).ifNotOpenMarketGetNext();
            }

        }

    }



}
