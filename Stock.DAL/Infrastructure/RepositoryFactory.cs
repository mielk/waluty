using Stock.DAL.Repositories;

namespace Stock.DAL.Infrastructure
{
    public class RepositoryFactory
    {

        private static readonly IMarketRepository MarketRepository;
        private static readonly IDataRepository DataRepository;

        static RepositoryFactory()
        {
            MarketRepository = new EFMarketRepository();
            DataRepository = new EFDataRepository();
        }


        public static IMarketRepository GetMarketRepository()
        {
            return MarketRepository;
        }

        public static IDataRepository GetDataRepository()
        {
            return DataRepository;
        }


    }
}
