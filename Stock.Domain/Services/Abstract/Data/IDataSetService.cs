﻿using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.Domain.Services
{
    public interface IDataSetService : IDataAccessService
    {
        void InjectQuotationService(IQuotationService service);
        void InjectPriceService(IPriceService service);
        AnalysisInfo GetAnalysisInfo(AnalysisDataQueryDefinition queryDef, AnalysisType analysisType);
        IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef);
        DataSet[] AppendAndReturnAsArray(IEnumerable<DataSet> sets, AnalysisDataQueryDefinition queryDef);
        void UpdateDataSets(IEnumerable<DataSet> dataSets);
    }
}