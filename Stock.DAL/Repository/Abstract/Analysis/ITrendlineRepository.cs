﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;

namespace Stock.DAL.Repositories
{
    public interface ITrendlineRepository
    {
        IEnumerable<TrendlineDto> GetTrendlines(int assetId, int timeframeId, int simulationId);
        TrendlineDto GetTrendlineById(int id);
        void UpdateTrendlines(IEnumerable<TrendlineDto> dtos);
    }
}
