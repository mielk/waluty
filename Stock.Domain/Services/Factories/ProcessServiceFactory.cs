using Stock.Domain.Entities;
using Stock.Domain.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Factories
{
    public class ProcessServiceFactory{

        private static ProcessServiceFactory _instance;
        private static IQuotationService _quotationService;
        private static Dictionary<string, IProcessService> services = new Dictionary<string, IProcessService>();


        private ProcessServiceFactory()
        {}


        public static ProcessServiceFactory Instance()
        {
            return _instance ?? (_instance = new ProcessServiceFactory());
        }

        public IProcessService GetProcessService(Asset asset, Timeframe timeframe)
        {
            return GetProcessService(new AssetTimeframe(asset, timeframe));
        }

        public IProcessService GetProcessService(AssetTimeframe atf)
        {
            string symbol = atf.Symbol();
            IProcessService service = null;
            try
            {
                services.TryGetValue(symbol, out service);
                return service;
            }
            catch (Exception) {
                service = new ProcessService(atf);
                return service;
            }
        }

        public IQuotationService GetQuotationService()
        {
            if (_quotationService == null) _quotationService = new QuotationService();
            return _quotationService;
        }

        public IQuotationService GetQuotationService(IQuotationService inject){
            if (inject != null){
                _quotationService = inject;
            }
            return _quotationService;
        }




    }
}