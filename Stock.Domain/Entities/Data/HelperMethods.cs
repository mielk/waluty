using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.Domain.Entities
{
    public static class HelperMethods
    {

        public static AnalysisInfo GetAnalysisInfo(this IEnumerable<DataSet> dataSets)
        {
            IEnumerable<DataSet> notNullDataSets = dataSets.Where(ds => ds != null);
            if (notNullDataSets != null && notNullDataSets.Count() > 0)
            {
                AnalysisInfo info = new AnalysisInfo();
                info.StartDate = notNullDataSets.Select(d => d.Date).Min();
                info.StartIndex = notNullDataSets.Select(d => d.IndexNumber).Min();
                info.EndDate = notNullDataSets.Select(d => d.Date).Max();
                info.EndIndex = notNullDataSets.Select(d => d.IndexNumber).Max();
                info.MinLevel = notNullDataSets.Select(d => d.GetQuotation().Low).Min();
                info.MaxLevel = notNullDataSets.Select(d => d.GetQuotation().High).Max();
                info.Counter = notNullDataSets.Count();
                return info;
            }
            else
            {
                return null;
            }
            
        }

        public static AnalysisInfo GetAnalysisInfo(this IEnumerable<DataSet> dataSets, AnalysisType typeForValuesRange)
        {
            AnalysisInfo info = new AnalysisInfo();
            info.StartDate = dataSets.Select(d => d.Date).Min();
            info.StartIndex = dataSets.Select(d => d.IndexNumber).Min();
            info.EndDate = dataSets.Select(d => d.Date).Max();
            info.EndIndex = dataSets.Select(d => d.IndexNumber).Max();

            //zastosować GetProperDataUnit zamiast GetQuotation
            info.MinLevel = dataSets.Select(d => d.GetQuotation().Low).Min();
            info.MaxLevel = dataSets.Select(d => d.GetQuotation().High).Max();

            info.Counter = dataSets.Count();
            return info;
        }

    }
}
