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

        public static IEnumerable<AnalysisType> ToAnalysisTypes(this IEnumerable<string> values)
        {

            List<AnalysisType> analysisTypes = new List<AnalysisType>();

            foreach (var value in values)
            {
                bool isFound = false;
                foreach (AnalysisType type in AnalysisType.GetValues(typeof(AnalysisType)))
                {
                    if (value.Equals(type.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        isFound = true;
                        analysisTypes.Add(type);
                        break;
                    }
                }

                if (!isFound)
                {
                    throw new Exception("Unknown analysis type: " + value);
                }

            }

            return analysisTypes;

        }

    }
}
