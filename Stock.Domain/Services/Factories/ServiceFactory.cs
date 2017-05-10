using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



        public static ICurrencyService GetCurrencyService()
        {
            if (_currencyService == null)
            {
                _currencyService = CurrencyService.Instance();
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
                _assetService = AssetService.Instance();
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
                _marketService = MarketService.Instance();
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
                _timeframeService = TimeframeService.Instance();
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
                _quotationService = QuotationService.Instance();
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
                _priceService = PriceService.Instance();
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
                _dataSetService = DataSetService.Instance();
            }
            return _dataSetService;
        }

        public static IDataSetService GetDataSetService(IDataSetService service)
        {
            _dataSetService = service;
            return _dataSetService;
        }


    }

}
