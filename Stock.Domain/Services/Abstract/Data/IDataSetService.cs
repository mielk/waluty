using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;
using Stock.DAL.Repositories;

namespace Stock.Domain.Services
{
    
    public interface IDataSetService
    {
        
        //[Injecting services & repositories]
        void InjectQuotationRepository(IQuotationRepository repository);
        void InjectPriceRepository(IPriceRepository repository);

        //[Access to data]
        AnalysisInfo GetAnalysisInfo(AnalysisDataQueryDefinition queryDef, AnalysisType analysisType);
        IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef);
        IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef, IEnumerable<DataSet> initialSets);

        //[Updating data]
        void UpdateDataSets(IEnumerable<DataSet> dataSets);

    }

}