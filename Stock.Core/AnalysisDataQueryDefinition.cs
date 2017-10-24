using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Core
{
    public class AnalysisDataQueryDefinition
    {
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StartIndex { get; set; }
        public int? EndIndex { get; set; }
        public IEnumerable<AnalysisType> AnalysisTypes { get; set; }
        public int Limit { get; set; }
        public int SimulationId { get; set; }

        public AnalysisDataQueryDefinition()
        {
            this.StartDate = null;
            this.EndDate = null;
            this.AnalysisTypes = new List<AnalysisType>();
            this.Limit = 0;
            this.SimulationId = 0;
        }

        public AnalysisDataQueryDefinition(int assetId, int timeframeId) : base()
        {
            this.AssetId = assetId;
            this.TimeframeId = timeframeId;
        }

        public AnalysisDataQueryDefinition Clone()
        {
            return new AnalysisDataQueryDefinition(AssetId, TimeframeId)
            {
                Limit = this.Limit,
                StartDate = this.StartDate,
                SimulationId = this.SimulationId,
                EndDate = this.EndDate,
                AnalysisTypes = (this.AnalysisTypes != null ? this.AnalysisTypes.ToArray() : new AnalysisType[] { }),
                StartIndex = this.StartIndex,
                EndIndex = this.EndIndex
            };
        }

        public bool ContainsAnalysisType(AnalysisType type)
        {
            if (AnalysisTypes != null)
            {
                return AnalysisTypes.Contains(type);
            }
            return false;
        }

    }

}
