namespace Stock.DAL.Repositories
{
    public class RepositoryFactory
    {

        private static readonly IMarketRepository marketRepository;
        private static readonly ICurrencyRepository currencyRepository;
        private static readonly IAssetRepository assetRepository;
        private static readonly IDataRepository2 dataRepository2;
        private static readonly ITimeframeRepository timeframeRepository;
        private static readonly IQuotationRepository quotationRepository;
        private static readonly IPriceRepository priceRepository;


        static RepositoryFactory()
        {
            marketRepository = new EFMarketRepository();
            dataRepository2 = new EFDataRepository2();
            currencyRepository = new EFCurrencyRepository();
            assetRepository = new EFAssetRepository();
            timeframeRepository = new EFTimeframeRepository();
            quotationRepository = new EFQuotationRepository();
            priceRepository = new EFPriceRepository();
        }


        public static IMarketRepository GetMarketRepository()
        {
            return marketRepository;
        }

        public static ICurrencyRepository GetCurrencyRepository()
        {
            return currencyRepository;
        }

        public static IAssetRepository GetAssetRepository()
        {
            return assetRepository;
        }

        public static ITimeframeRepository GetTimeframeRepository()
        {
            return timeframeRepository;
        }

        public static IDataRepository2 GetDataRepository2()
        {
            return dataRepository2;
        }

        public static IQuotationRepository GetQuotationRepository()
        {
            return quotationRepository;
        }

        public static IPriceRepository GetPriceRepository()
        {
            return priceRepository;
        }

    }
}
