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
using Stock.Domain.Services.Factories;

namespace Stock.Domain.Services.Concrete
{
    public class QuotationService : IQuotationService
    {

        private IDataService service = DataServiceFactory.Instance().GetService();


        public void injectDataService(IDataService dataService)
        {
            this.service = dataService;
        }

        public DateTime? getLastCalculationDate(string symbol, string analysisSymbol)
        {
            return service.GetAnalysisLastCalculation(symbol, analysisSymbol);
        }


        private DateTime? findEarliestRequiredDate(IEnumerable<IAnalyzer> analyzers){

            List<DateTime?> dates = new List<DateTime?>();
            foreach (var analyzer in analyzers)
            {
                DateTime? date = analyzer.getFirstRequiredDate();
                dates.Add(date);
            }

            return dates.getEarliestDate();

        }

        public DataItem[] fetchData(Dictionary<AnalysisType, IAnalyzer> analyzers)
        {

            DateTime? firstRequiredQuotationDate = findEarliestRequiredDate(analyzers.Values);
            AssetTimeframe atf = fetchAssetTimeframe(analyzers);
            IEnumerable<AnalysisType> analysisTypes = analyzers.Keys;
            IEnumerable<DataItem> items = service.GetDataItems(atf, firstRequiredQuotationDate, null, analysisTypes);
            DataItem[] itemsArray = items.ToArray();
            itemsArray.AppendIndexNumbers();

            return itemsArray;

        }


        private AssetTimeframe fetchAssetTimeframe(Dictionary<AnalysisType, IAnalyzer> analyzers)
        {
            foreach (var analyzer in analyzers.Values)
            {
                return analyzer.getAssetTimeframe();
            }
            return null;
        }


    }

 
}