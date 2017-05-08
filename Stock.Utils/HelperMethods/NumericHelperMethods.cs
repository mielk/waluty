using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Utils
{

    public static class NumericHelperMethods
    {

        public static bool IsInRange(this double value, double low, double up)
        {
            if (low < up)
            {
                return (value >= low && value <= up);
            }
            else
            {
                return (value >= up && value <= low);
            }
        }

        public static bool IsInRange(this int value, int low, int up)
        {
            if (low < up)
            {
                return (value >= low && value <= up);
            }
            else
            {
                return (value >= up && value <= low);
            }
        }

        public static bool CompareForTest(this double value, double compared, double maxDifference)
        {
            double absDifference = Math.Abs(value - compared);
            return (absDifference <= maxDifference);
        }

    }

}
