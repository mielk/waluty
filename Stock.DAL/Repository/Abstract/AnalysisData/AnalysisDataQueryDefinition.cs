﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;

namespace Stock.DAL.Repositories
{
    public class AnalysisDataQueryDefinition
    {
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<AnalysisTypeDto> AnalysisTypes { get; set; }
        public int Limit { get; set; }

        public AnalysisDataQueryDefinition(int assetId, int timeframeId)
        {
            this.AssetId = assetId;
            this.TimeframeId = TimeframeId;
            this.StartDate = null;
            this.EndDate = null;
            this.AnalysisTypes = new List<AnalysisTypeDto>();
            this.Limit = 0;
        }

    }
}