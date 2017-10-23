using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Repositories;

namespace Stock.Domain.Services
{
    public class ServiceFactory : IServiceFactory
    {
        private static ServiceFactory instance;
        private static ICurrencyService _currencyService;
        private static IAssetService _assetService;
        private static IMarketService _marketService;
        private static ITimeframeService _timeframeService;
        private static IDataSetService _dataSetService;
        private static ISimulationService _simulationService;
        private static IAnalysisTimestampService _analysisTimestampService;
        private static ITrendlineService _trendlineService;



        #region CONSTRUCTOR

        public static ServiceFactory Instance()
        {
            if (instance == null)
            {
                instance = new ServiceFactory();
            }
            return instance;
        }

        #endregion CONSTRUCTOR




        public ICurrencyService GetCurrencyService()
        {
            if (_currencyService == null)
            {
                ICurrencyRepository repository = RepositoryFactory.GetCurrencyRepository();
                _currencyService = new CurrencyService(repository);
            }
            return _currencyService;
        }

        public ICurrencyService GetCurrencyService(ICurrencyService service)
        {
            _currencyService = service;
            return _currencyService;
        }



        public IAssetService GetAssetService()
        {
            if (_assetService == null)
            {
                IAssetRepository repository = RepositoryFactory.GetAssetRepository();
                _assetService = new AssetService(repository);
            }
            return _assetService;
        }

        public IAssetService GetAssetService(IAssetService service)
        {
            _assetService = service;
            return _assetService;
        }



        public IMarketService GetMarketService()
        {
            if (_marketService == null)
            {
                IMarketRepository repository = RepositoryFactory.GetMarketRepository();
                _marketService = new MarketService(repository);
            }
            return _marketService;
        }

        public IMarketService GetMarketService(IMarketService service)
        {
            _marketService = service;
            return _marketService;
        }



        public ITimeframeService GetTimeframeService()
        {
            if (_timeframeService == null)
            {
                ITimeframeRepository repository = RepositoryFactory.GetTimeframeRepository();
                _timeframeService = new TimeframeService(repository);
            }
            return _timeframeService;
        }

        public ITimeframeService GetTimeframeService(ITimeframeService service)
        {
            _timeframeService = service;
            return _timeframeService;
        }



        public IDataSetService GetDataSetService()
        {
            if (_dataSetService == null)
            {
                _dataSetService = new DataSetService();
            }
            return _dataSetService;
        }

        public IDataSetService GetDataSetService(IDataSetService service)
        {
            _dataSetService = service;
            return _dataSetService;
        }


        public ISimulationService GetSimulationService()
        {
            if (_simulationService == null)
            {
                ISimulationRepository repository = RepositoryFactory.GetSimulationRepository();
                _simulationService = new SimulationService(repository);
            }
            return _simulationService;
        }

        public ISimulationService GetSimulationService(ISimulationService service)
        {
            _simulationService = service;
            return _simulationService;
        }

        public IAnalysisTimestampService GetAnalysisTimestampService()
        {
            if (_analysisTimestampService == null)
            {
                ISimulationRepository repository = RepositoryFactory.GetSimulationRepository();
                _analysisTimestampService = new AnalysisTimestampService(repository);
            }
            return _analysisTimestampService;
        }

        public IAnalysisTimestampService GetAnalysisTimestampService(IAnalysisTimestampService service)
        {
            return _analysisTimestampService;
        }

        public ITrendlineService GetTrendlineService()
        {
            if (_trendlineService == null)
            {
                _trendlineService = new TrendlineService();
            }
            return _trendlineService;
        }

        public ITrendlineService GetTrendlineService(ITrendlineService service)
        {
            _trendlineService = service;
            return _trendlineService;
        }

    }

}
