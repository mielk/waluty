using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Helpers
{

    public static class HelperMethods
    {
        public static string ToDbString(this int? value)
        {
            if (value == null) return "NULL";
            return value.ToString();
        }

        public static string ToDbString(this int value)
        {
            return value.ToString();
        }

        public static string ToDbString(this double? value)
        {
            if (value == null) return "NULL";
            return Math.Round((double)value, 5).ToString().Replace(',', '.');
        }

        public static string ToDbString(this double value)
        {
            return Math.Round(value, 5).ToString().Replace(',', '.');
        }

        public static string ToDbString(this bool value)
        {
            return (value ? "1" : "0");
        }

        public static string ToDbString(this bool? value)
        {
            if (value == null)
            {
                return "NULL";
            }
            else
            {
                return ((bool)value ? "1" : "0");
            }
        }

        public static string ToDbString(this DateTime value)
        {
            return "'" + value + "'";
        }

        public static string ToDbString(this DateTime? value)
        {
            if (value == null)
            {
                return "NULL";
            }
            else
            {
                return "'" + value.ToString() + "'";
            }
        }

        public static string ToDbString(this string value)
        {
            return "'" + value.Replace("'", "''") + "'";
        }

    }
}
