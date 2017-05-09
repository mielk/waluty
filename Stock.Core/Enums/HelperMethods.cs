using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Core
{
    public static class HelperMethods
    {

        public static AnalysisType ToAnalysisType(this string value)
        {

            foreach (AnalysisType type in AnalysisType.GetValues(typeof(AnalysisType)))
            {
                if (value.Equals(type.ToString(), StringComparison.CurrentCultureIgnoreCase)){
                    return type;
                }
            }

            throw new Exception("Unknown analysis type: " + value);

        }

    }
}
