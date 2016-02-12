using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock.Domain.Services
{

    public static class HelperMethods
    {


        public static double GetExtremumEvaluationFactor(this TimebandSymbol value)
        {
            switch (value)
            {
                case TimebandSymbol.M5: return 30d;
                case TimebandSymbol.M15: return 24d;
                case TimebandSymbol.M30: return 12d;
                case TimebandSymbol.H1: return 12d;
                case TimebandSymbol.H4: return 6d;
                case TimebandSymbol.D1: return 2d;
                case TimebandSymbol.W1: return 1d;
                case TimebandSymbol.MN1: return 0.5d;
            }

            return 1d;

        }

        public static TimebandSymbol GetTimebandSymbol(this string value)
        {

            var timebandSymbol = value.Substring(value.IndexOf('_') + 1);

            if (timebandSymbol.Equals("M5"))
            {
                return TimebandSymbol.M5;
            }
            else if (timebandSymbol.Equals("M15"))
            {
                return TimebandSymbol.M15;
            }
            else if (timebandSymbol.Equals("M30"))
            {
                return TimebandSymbol.M30;
            }
            else if (timebandSymbol.Equals("H1"))
            {
                return TimebandSymbol.H1;
            }
            else if (timebandSymbol.Equals("H4"))
            {
                return TimebandSymbol.H4;
            }
            else if (timebandSymbol.Equals("D1"))
            {
                return TimebandSymbol.D1;
            }
            else if (timebandSymbol.Equals("W1"))
            {
                return TimebandSymbol.W1;
            }
            else if (timebandSymbol.Equals("MN1"))
            {
                return TimebandSymbol.MN1;
            } 
            else 
            {
                return TimebandSymbol.MN1;
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

    }
}
