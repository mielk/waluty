﻿using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public interface IQuotationRepository
    {
        IEnumerable<QuotationDto> GetQuotations(int assetId, int timeframe);
        IEnumerable<QuotationDto> GetQuotations(AnalysisDataQueryDefinition queryDef);
    }
}