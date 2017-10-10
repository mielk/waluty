using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.Utils;
using Stock.Core;

namespace Stock.Domain.Services
{


    public static class HelperMethods
    {

        public static DateTime? getEarliestDate(this IEnumerable<DateTime?> dates)
        {

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







        //public static IEnumerable<ExtremumGroup> GetExtremaGroups(this IEnumerable<DataItem> extrema){
        //    DataItem[] items = extrema.OrderBy(i => i.Date).ToArray();
        //    List<ExtremumGroup> list = new List<ExtremumGroup>();

        //    for (var i = 0; i < items.Length; i++)
        //    {
        //        DataItem item = items[i];
        //        ExtremumGroup group = new ExtremumGroup();
        //        if (i < items.Length - 1 && items[i + 1].Index - item.Index == 1)
        //        {
        //            if (item.Price.IsExtremumByClosePrice())
        //            {
        //                group.master = item;
        //                group.slave = items[i + 1];
        //            }
        //            else
        //            {
        //                group.master = items[i + 1];
        //                group.slave = item;
        //            }

        //            i++;

        //        }
        //        else
        //        {
        //            group.master = item;
        //        }

        //        group.type = item.Price.IsTrough() ? ExtremumType.TroughByClose : ExtremumType.PeakByClose;
        //        list.Add(group);

        //    }

        //    return list;
        //}

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
                case AnalysisType.Prices:
                    return "prices";
                case AnalysisType.Macd:
                    return "macd";
                case AnalysisType.Adx:
                    return "adx";
                case AnalysisType.Candlesticks:
                    return "candlesticks";
                case AnalysisType.Trendlines:
                    return "trendRanges";
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

        public static int GetFactor(this TrendlineType type)
        {
            return (type == TrendlineType.Resistance ? 1 : -1);
        }





        //public static IAnalyzer getAnalyzer(this AnalysisType type, Asset asset, Timeframe timeframe)
        //{
        //    AssetTimeframe atf = new AssetTimeframe(asset, timeframe);
        //    return AnalyzerFactory.Instance().getAnalyzer(type, atf);
        //}

        //public static IAnalyzer getAnalyzer(this AnalysisType type, AssetTimeframe atf)
        //{
        //    return AnalyzerFactory.Instance().getAnalyzer(type, atf);
        //}

    }



}
