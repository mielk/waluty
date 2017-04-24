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


    }
}
