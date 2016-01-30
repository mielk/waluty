using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Services
{
    public class ProcessService : IProcessService
    {

        private readonly IDataRepository _dataRepository;
        private readonly IPriceAnalyzer _priceAnalyzer;

        public ProcessService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? RepositoryFactory.GetDataRepository();
            _priceAnalyzer = new PriceAnalyzer();
        }

        public bool Run(bool fromScratch)
        {

            IEnumerable<String> symbols = _dataRepository.GetStats();

            foreach (var symbol in symbols)
            {
                _priceAnalyzer.Analyze(symbol, true);//fromScratch);
            }

            return false;

        }





    }
}
