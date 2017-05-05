namespace Stock.DAL.Repositories
{
    public class RepositoryFactory
    {

        private static readonly IMarketRepository MarketRepository;
        private static readonly ICurrencyRepository CurrencyRepository;
        private static readonly IAssetRepository AssetRepository;
        private static readonly IDataRepository DataRepository;
        private static readonly IDataRepository2 DataRepository2;
        private static readonly ITimeframeRepository TimeframeRepository;


        static RepositoryFactory()
        {
            MarketRepository = new EFMarketRepository();
            DataRepository = new EFDataRepository();
            DataRepository2 = new EFDataRepository2();
            CurrencyRepository = new EFCurrencyRepository();
            AssetRepository = new EFAssetRepository();
            TimeframeRepository = new EFTimeframeRepository();
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

        public static ITimeframeRepository GetTimeframeRepository()
        {
            return TimeframeRepository;
        }

        public static IDataRepository GetDataRepository()
        {
            return DataRepository;
        }

        public static IDataRepository2 GetDataRepository2()
        {
            return DataRepository2;
        }


    }
}
