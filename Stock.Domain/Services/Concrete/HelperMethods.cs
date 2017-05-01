using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.Domain.Services.Factories;
using Stock.Utils;

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
            return new DateTime();

        }


        public static TimeSpan invert(this TimeSpan span)
        {
            return new TimeSpan(span.Hours * -1, span.Minutes * -1, span.Seconds * -1);
        }

        public static TimeSpan multiply(this TimeSpan span, int multiplier)
        {
            return new TimeSpan(span.Hours * multiplier, span.Minutes * multiplier, span.Seconds * multiplier);
        }
        
        public static bool isOpenMarketTime(this DateTime date)
        {
            if (date.IsWeekend() || date.IsChristmas() || date.IsNewYear())
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
                else if (date.IsWeekend())
                {
                    return ifNotOpenMarketGetNext(date.AddDays(date.DayOfWeek == DayOfWeek.Saturday ? 2 : 1).Midnight());
                }

                return date;

            }
        }

        public static DateTime getNext(this DateTime date, TimeframeSymbol timeframe)
        {
            return new DateTime();

        }

    }



}
