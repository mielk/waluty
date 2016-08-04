using Stock.Domain.Services.Abstract;
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
        private static IProcessService _processService;
        

        private ProcessServiceFactory()
        {
            _processService = new ProcessService();
            _quotationService = new QuotationService();
        }


        public static ProcessServiceFactory Instance()
        {
            return _instance ?? (_instance = new ProcessServiceFactory());
        }

        public IProcessService GetService()
        {
            return _processService;
        }

        public IProcessService GetService(IProcessService inject)
        {
            if (inject != null){
                _processService = inject;
            }
            return _processService;
        }

        public IQuotationService GetQuotationService()
        {
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