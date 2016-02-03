using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Infrastructure
{

    public static class HelperMethods
    {

        public static string ToDbString(this double value)
        {
            return Math.Round(value, 5).ToString().Replace(',', '.');
        }
    }
}
