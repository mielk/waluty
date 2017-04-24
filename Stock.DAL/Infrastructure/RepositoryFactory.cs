using Stock.DAL.Repositories;

namespace Stock.DAL.Infrastructure
{
    public class RepositoryFactory
    {

        private static readonly IMarketRepository MarketRepository;
        private static readonly IDataRepository DataRepository;
        private static readonly ICurrencyRepository CurrencyRepository;

        static RepositoryFactory()
        {
            MarketRepository = new EFMarketRepository();
            DataRepository = new EFDataRepository();
            CurrencyRepository = new EFCurrencyRepository();
        }


        public static IMarketRepository GetMarketRepository()
        {
            return MarketRepository;
        }

        public static ICurrencyRepository GetCurrencyRepository()
        {
            return CurrencyRepository;
        }

        public static IDataRepository GetDataRepository()
        {
            return DataRepository;
        }


    }
}
