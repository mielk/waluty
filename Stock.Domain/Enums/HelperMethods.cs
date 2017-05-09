using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Enums
{
    public static class HelperMethods
    {

        public static TimeframeUnit ToTimeframeUnit(this string value)
        {

            foreach(TimeframeUnit unit in TimeframeUnit.GetValues(typeof(TimeframeUnit))){
                if (value.Equals(unit.ToString(), StringComparison.CurrentCultureIgnoreCase)){
                    return unit;
                }
            }

            throw new Exception("Unknown timeframe unit: " + value);

        }

    }
}
