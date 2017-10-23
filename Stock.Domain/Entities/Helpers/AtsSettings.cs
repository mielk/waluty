using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class AtsSettings
    {
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        public int SimulationId { get; set; }

        public AtsSettings(int assetId, int timeframeId)
        {
            this.AssetId = assetId;
            this.TimeframeId = timeframeId;
        }

        public AtsSettings(int assetId, int timeframeId, int simulationId)
        {
            this.AssetId = assetId;
            this.TimeframeId = timeframeId;
            this.SimulationId = simulationId;
        }

    }
}