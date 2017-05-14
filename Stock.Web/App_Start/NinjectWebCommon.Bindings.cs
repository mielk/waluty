using System.Diagnostics.CodeAnalysis;
using Ninject.Syntax;
using Stock.DAL.Repositories;
//using Stock.DAL.Repository.Fake;
using Stock.Domain.Services;
using Stock.Domain.Services.Factories;

// ReSharper disable once CheckNamespace
namespace Stock.Web
{
    // registration code moved here for better separation of concerns
    public static partial class NinjectWebCommon
    {
        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "IOC registration method")]
        private static void RegisterServices(IBindingRoot kernel)
        {
            kernel.Bind<ISimulationRepository>().To<EFSimulationRepository>();
            kernel.Bind<IPriceRepository>().To<EFPriceRepository>();
            kernel.Bind<IQuotationRepository>().To<EFQuotationRepository>();
            kernel.Bind<IAssetRepository>().To<EFAssetRepository>();
            kernel.Bind<ICurrencyRepository>().To<EFCurrencyRepository>();
            kernel.Bind<IMarketRepository>().To<EFMarketRepository>();
            kernel.Bind<ITimeframeRepository>().To<EFTimeframeRepository>();

            kernel.Bind<ISimulationService>().To<SimulationService>();
            kernel.Bind<IQuotationService>().To<QuotationService>();
            kernel.Bind<IPriceService>().To<PriceService>();
            kernel.Bind<IDataSetService>().To<DataSetService>();

            kernel.Bind<IMarketService>().To<MarketService>();
            kernel.Bind<IAssetService>().To<AssetService>();
            kernel.Bind<ITimeframeService>().To<TimeframeService>();
            kernel.Bind<ICurrencyService>().To<CurrencyService>();

            kernel.Bind<IDataService2>().To<DataService2>();
            kernel.Bind<ISimulationService2>().To<SimulationService2>();
            kernel.Bind<ISimulationServiceFactory>().To<SimulationServiceFactory>();
            kernel.Bind<IProcessService>().To<ProcessService>();
            kernel.Bind<ITrendlineAnalyzer>().To<TrendlineAnalyzer>();
            kernel.Bind<IDataRepository2>().To<EFDataRepository2>();

        }


    }


}