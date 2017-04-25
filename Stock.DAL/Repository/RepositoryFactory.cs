namespace Stock.DAL.Repositories
{
    public class RepositoryFactory
    {

        private static readonly IMarketRepository MarketRepository;
        private static readonly ICurrencyRepository CurrencyRepository;
        private static readonly IAssetRepository AssetRepository;
        private static readonly IDataRepository DataRepository;


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

        public static IAssetRepository GetAssetRepository()
        {
            return AssetRepository;
        }

        public static IDataRepository GetDataRepository()
        {
            return DataRepository;
        }


    }
}
