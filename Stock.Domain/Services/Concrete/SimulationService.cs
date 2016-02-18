using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using Stock.Domain.Services.Factories;

namespace Stock.Domain.Services
{
    public class SimulationService : ISimulationService
    {

        private readonly IDataService _dataService;
        private readonly IPriceAnalyzer _priceAnalyzer;
        private readonly IMacdAnalyzer _macdAnalyzer;
        public IEnumerable<DataItem> Data { get; set; }

        public SimulationService(IDataService dataService)
        {
            _dataService = dataService ?? DataServiceFactory.Instance().GetService();
            _priceAnalyzer = new PriceAnalyzer();
            _macdAnalyzer = new MacdAnalyzer();
        }

        public bool Start(string pair, string timeband)
        {
            var symbol = pair + '_' + timeband;
            try
            {
                Data = _dataService.GetFxQuotations(symbol, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        
       

    }
}
