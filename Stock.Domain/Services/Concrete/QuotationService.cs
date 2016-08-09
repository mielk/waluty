using Stock.DAL.Repositories;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Services.Concrete
{
    public class QuotationService : IQuotationService
    {

        private IDataRepository repository = RepositoryFactory.GetDataRepository();

        


        public void loadLastCalculationDates(Dictionary<AnalysisType, Analyzer> analyzers)
        {
            foreach (var analyzer in analyzers.Values)
            {
                LastDates lastDates = repository.GetSymbolLastItems(analyzer.getSymbol(), analyzer.getAnalysisType().toString());
                //analyzer.setLastCalculationDate(repository
                

                //GetSymbolLastItems

                //analyzer.Analyze
            }


        }


        public DataItem[] fetchData(Dictionary<AnalysisType, Analyzer> analyzers)
        {
            
            return new DataItem[] { };

        }

    }
}