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
            kernel.Bind<ICompanyService>().To<CompanyService>();
            kernel.Bind<IMarketService>().To<MarketService>();
            kernel.Bind<IDataService>().To<DataService>();
            kernel.Bind<IFxService>().To<FxService>();
            kernel.Bind<ISimulationService>().To<SimulationService>();
            kernel.Bind<ISimulationServiceFactory>().To<SimulationServiceFactory>();
            kernel.Bind<IProcessService>().To<ProcessService>();
            kernel.Bind<ITrendlineAnalyzer>().To<TrendlineAnalyzer>();
            kernel.Bind<ITrendService>().To<TrendService>();

            kernel.Bind<ICompanyRepository>().To<EFCompanyRepository>();
            kernel.Bind<IMarketRepository>().To<EFMarketRepository>();
            kernel.Bind<IDataRepository>().To<EFDataRepository>();
            kernel.Bind<IFxRepository>().To<EFFxRepository>();

            //kernel.Bind<ICompanyRepository>().To<FakeCompanyRepository>();
            //kernel.Bind<IMarketRepository>().To<FakeMarketRepository>();
            //kernel.Bind<IDataRepository>().To<FakeDataRepository>();

        }


    }


}