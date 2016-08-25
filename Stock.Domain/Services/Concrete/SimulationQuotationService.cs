using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class SimulationQuotationService : IQuotationService
    {

        private ISimulationService simulationService;

        public SimulationQuotationService(ISimulationService simulationService)
        {
            this.simulationService = simulationService;
        }

        public void injectDataService(IDataService dataService)
        {
        }

        public DateTime? getLastCalculationDate(string symbol, string analysisSymbol)
        {
            return simulationService.getLastCalculationDate(AnalysisType.Price);
        }

        public DateTime? getLastCalculationDate(AssetTimeframe atf, AnalysisType analysisType)
        {
            return simulationService.getLastCalculationDate(analysisType);
        }

        private DateTime? findEarliestRequiredDate(IEnumerable<IAnalyzer> analyzers)
        {

            return null;

        }

        public DataItem[] fetchData(Dictionary<AnalysisType, IAnalyzer> analyzers)
        {
            DataItem[] itemsArray = simulationService.fetchData(analyzers);
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
