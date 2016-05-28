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

        private AssetTimeframe assetTimeframe;
        private readonly IDataRepository _dataRepository;
        private readonly IPriceAnalyzer _priceAnalyzer;
        private readonly IMacdAnalyzer _macdAnalyzer;



        public ProcessService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? RepositoryFactory.GetDataRepository();
            _priceAnalyzer = new PriceAnalyzer();
            _macdAnalyzer = new MacdAnalyzer();
        }


        public void LoadParams(Asset asset, Timeframe timeframe)
        {
            assetTimeframe = new AssetTimeframe(asset, timeframe);
        }

        public void LoadParams(string asset, string timeframe)
        {
            assetTimeframe = new AssetTimeframe(asset, timeframe);
        }

        public void LoadParams(string symbol)
        {
            assetTimeframe = new AssetTimeframe(symbol);
        }

        
        public bool Run(bool fromScratch)
        {

            if (assetTimeframe == null || !assetTimeframe.isValid()) throw new Exception("Asset is required");

            //_priceAnalyzer.Analyze(asset.Name, true);
            //_macdAnalyzer.Analyze(symbol, false);
            return false;

        }


    }
}
