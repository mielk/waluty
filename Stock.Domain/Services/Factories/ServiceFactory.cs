using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Repositories;

namespace Stock.Domain.Services
{
    public class ServiceFactory
    {

        private static ICurrencyService _currencyService;
        private static IAssetService _assetService;
        private static IMarketService _marketService;
        private static ITimeframeService _timeframeService;
        private static IQuotationService _quotationService;
        private static IPriceService _priceService;
        private static IDataSetService _dataSetService;
        private static ISimulationService _simulationService;



        public static ICurrencyService GetCurrencyService()
        {
            if (_currencyService == null)
            {
                ICurrencyRepository repository = RepositoryFactory.GetCurrencyRepository();
                _currencyService = new CurrencyService(repository);
            }
            return _currencyService;
        }

        public static ICurrencyService GetCurrencyService(ICurrencyService service)
        {
            _currencyService = service;
            return _currencyService;
        }



        public static IAssetService GetAssetService()
        {
            if (_assetService == null)
            {
                IAssetRepository repository = RepositoryFactory.GetAssetRepository();
                _assetService = new AssetService(repository);
            }
            return _assetService;
        }

        public static IAssetService GetAssetService(IAssetService service)
        {
            _assetService = service;
            return _assetService;
        }



        public static IMarketService GetMarketService()
        {
            if (_marketService == null)
            {
                IMarketRepository repository = RepositoryFactory.GetMarketRepository();
                _marketService = new MarketService(repository);
            }
            return _marketService;
        }

        public static IMarketService GetMarketService(IMarketService service)
        {
            _marketService = service;
            return _marketService;
        }



        public static ITimeframeService GetTimeframeService()
        {
            if (_timeframeService == null)
            {
                ITimeframeRepository repository = RepositoryFactory.GetTimeframeRepository();
                _timeframeService = new TimeframeService(repository);
            }
            return _timeframeService;
        }

        public static ITimeframeService GetTimeframeService(ITimeframeService service)
        {
            _timeframeService = service;
            return _timeframeService;
        }



        public static IQuotationService GetQuotationService()
        {
            if (_quotationService == null)
            {
                IQuotationRepository repository = RepositoryFactory.GetQuotationRepository();
                _quotationService = new QuotationService(repository);
            }
            return _quotationService;
        }

        public static IQuotationService GetQuotationService(IQuotationService service)
        {
            _quotationService = service;
            return _quotationService;
        }



        public static IPriceService GetPriceService()
        {
            if (_priceService == null)
            {
                IPriceRepository repository = RepositoryFactory.GetPriceRepository();
                _priceService = new PriceService(repository);
            }
            return _priceService;
        }

        public static IPriceService GetPriceService(IPriceService service)
        {
            _priceService = service;
            return _priceService;
        }



        public static IDataSetService GetDataSetService()
        {
            if (_dataSetService == null)
            {
                _dataSetService = new DataSetService();
            }
            return _dataSetService;
        }

        public static IDataSetService GetDataSetService(IDataSetService service)
        {
            _dataSetService = service;
            return _dataSetService;
        }


        public static ISimulationService GetSimulationService()
        {
            if (_simulationService == null)
            {
                ISimulationRepository repository = RepositoryFactory.GetSimulationRepository();
                _simulationService = new SimulationService(repository);
            }
            return _simulationService;
        }

        public static ISimulationService GetSimulationService(ISimulationService service)
        {
            _simulationService = service;
            return _simulationService;
        }


    }

}
