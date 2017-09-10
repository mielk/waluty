using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.Domain.Services
{

    public interface IServiceFactory
    {
        ICurrencyService GetCurrencyService();
        ICurrencyService GetCurrencyService(ICurrencyService service);
        IAssetService GetAssetService();
        IAssetService GetAssetService(IAssetService service);
        IMarketService GetMarketService();
        IMarketService GetMarketService(IMarketService service);
        ITimeframeService GetTimeframeService();
        ITimeframeService GetTimeframeService(ITimeframeService service);
        IQuotationService GetQuotationService();
        IQuotationService GetQuotationService(IQuotationService service);
        IPriceService GetPriceService();
        IPriceService GetPriceService(IPriceService service);
        IDataSetService GetDataSetService();
        IDataSetService GetDataSetService(IDataSetService service);
        ISimulationService GetSimulationService();
        ISimulationService GetSimulationService(ISimulationService service);
        IAnalysisTimestampService GetAnalysisTimestampService();
        IAnalysisTimestampService GetAnalysisTimestampService(IAnalysisTimestampService service);
    }

}
