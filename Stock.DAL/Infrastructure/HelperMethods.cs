using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Infrastructure
{

    public static class HelperMethods
    {

        public static string ToDbString(this double? value)
        {
            if (value == null) return "null";
            return Math.Round((double)value, 5).ToString().Replace(',', '.');
        }

        public static string ToDbString(this double value)
        {
            return Math.Round(value, 5).ToString().Replace(',', '.');
        }

    }
}
