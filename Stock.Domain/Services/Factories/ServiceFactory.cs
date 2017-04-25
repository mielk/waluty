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


    }

}
