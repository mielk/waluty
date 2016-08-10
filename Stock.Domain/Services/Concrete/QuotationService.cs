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



        public DateTime? getLastCalculationDate(string symbol, string analysisSymbol)
        {
            return repository.GetAnalysisLastCalculation(symbol, analysisSymbol);
        }


        private DateTime? findEarliestRequiredDate(IEnumerable<Analyzer> analyzers){

            List<DateTime?> dates = new List<DateTime?>();
            foreach (var analyzer in analyzers)
            {
                //dates.Add(analyzer.get
            }

            return null;

        }

        public DataItem[] fetchData(Dictionary<AnalysisType, Analyzer> analyzers)
        {

            DateTime? firstRequiredQuotationDate = findEarliestRequiredDate(analyzers.Values);

            return new DataItem[] { };
        }


    }
}