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
                default:
                    return "";
            }
        }

        public static bool IsPeak(this ExtremumType type)
        {
            if (type == ExtremumType.PeakByClose || type == ExtremumType.PeakByHigh)
                return true;

            return false;

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

        public static Analyzer getAnalyzer(this AnalysisType type, Asset asset, Timeframe timeframe)
        {
            return AnalyzerFactory.Instance().getAnalyzer(type, asset, timeframe);
        }

        public static Analyzer getAnalyzer(this AnalysisType type, AssetTimeframe atf)
        {
            return AnalyzerFactory.Instance().getAnalyzer(type, atf.asset, atf.timeframe);
        }

        public static string toString(this AnalysisType type)
        {
            return AnalysisTypeHelper.getTypeString(type);
        }


        public static DateTime addTimeUnits(this DateTime date, TimeframeSymbol timeframe, int units)
        {

            DateTime newDate;

            if (units == 0) return date.AddMilliseconds(0);

            switch (timeframe)
            {
                case TimeframeSymbol.MN1:
                    return date.AddMonths(units);

                case TimeframeSymbol.W1:
                    return date.AddDays(units * 7);

                case TimeframeSymbol.D1:
                    {
                        int remainder = units % 5;
                        int newWeekDay = (int)date.DayOfWeek + remainder;
                        bool breakWeek = newWeekDay > 5 || newWeekDay <= 0;
                        int daysToAdd = (units / 5) * 7 + remainder + Math.Sign(units) * (breakWeek ? 2 : 0);
                        newDate = date.AddDays(daysToAdd);

                        //Include year breaks.
                        int yearBreaks = date.countNewYearBreaks(newDate, false);
                        if (yearBreaks > 0)
                        {
                            newDate = newDate.addTimeUnits(timeframe, Math.Sign(units) * yearBreaks);
                        }

                        //Check if the result date is not New Year.
                        if (newDate.DayOfYear == 1)
                        {
                            newDate = newDate.AddDays(Math.Sign(units));
                        }

                    }

                    return newDate;

                case TimeframeSymbol.H4:
                    {
                        //int remainder = units % 30;
                        //int newWeekDay = (int)date.DayOfWeek + units % 5;

                        newDate = date.AddHours(4 * units);

                    }

                    return newDate;

            }

            return date;
            
        }


        public static int countNewYearBreaks(this DateTime date, DateTime compared, bool includeWeekendBreaks)
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



            if (includeWeekendBreaks)
            {
                return later.Year - earlier.Year;
            }
            else
            {

                for (var i = earlier.Year + 1; i <= later.Year; i++)
                {
                    DateTime newYear = new DateTime(i, 1, 1);
                    if (newYear.DayOfWeek != DayOfWeek.Sunday && newYear.DayOfWeek != DayOfWeek.Saturday)
                    {
                        counter++;
                    }
                }

            }


            return counter;

        }


    }
}
