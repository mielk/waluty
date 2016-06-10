using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace Stock.DAL.Repositories
{

    public class EFMarketRepository : IMarketRepository
    {


                                                                #region markets
        public IEnumerable<MarketDto> GetMarkets()
        {

            IEnumerable<MarketDto> markets;

            using (var context = new EFDbContext())
            {
                markets = context.Markets.ToList();
            }

            return markets;
        }


        public MarketDto GetMarketById(int id)
        {

            MarketDto market;
            using (var context = new EFDbContext())
            {
                market = context.Markets.SingleOrDefault(m => m.Id == id);
            }

            return market;
        }

        public MarketDto GetMarketByName(string name)
        {

            MarketDto market;
            using (var context = new EFDbContext())
            {
                market = context.Markets.SingleOrDefault(m => m.Name.Equals(name));
            }

            return market;
        }

        public MarketDto GetMarketBySymbol(string symbol)
        {

            MarketDto market;
            using (var context = new EFDbContext())
            {
                market = context.Markets.SingleOrDefault(m => m.ShortName.Equals(symbol));
            }

            return market;
        }

                                                                #endregion markets


                                                                #region assets
        public IEnumerable<AssetDto> FilterAssets(string q, int limit)
        {
            string lower = q.ToLower();
            IEnumerable<AssetDto> results = null;

            using (EFDbContext context = new EFDbContext())
            {
                results = context.Companies.Where(c => c.Name.ToLower().Contains(lower) ||
                                                  c.ShortName.ToLower().Contains(lower)).Take(limit).ToList();
            }

            return results;

        }

        public AssetDto GetAsset(int id)
        {

            AssetDto company = null;

            using (EFDbContext context = new EFDbContext())
            {
                company = context.Companies.SingleOrDefault(c => c.Id == id);
            }

            return company;

        }
                                                                #endregion assets


                                                                #region fx

        public IEnumerable<FxPairDto> FilterPairs(string q, int limit)
        {
            string lower = q.ToLower();
            IEnumerable<FxPairDto> results = null;

            using (EFDbContext context = new EFDbContext())
            {
                results = context.Pairs.Where(p => p.IsActive && p.Name.ToLower().Contains(lower)).Take(limit).ToList();
            }

            return results;

        }

        public IEnumerable<FxPairDto> GetFxPairs()
        {
            IEnumerable<FxPairDto> results = null;
            using (EFDbContext context = new EFDbContext())
            {
                results = context.Pairs.ToList();
            }

            return results;

        }

        public FxPairDto GetFxPair(int id)
        {

            FxPairDto pair = null;

            using (EFDbContext context = new EFDbContext())
            {
                pair = context.Pairs.SingleOrDefault(p => p.Id == id);
            }

            return pair;

        }

        public FxPairDto GetFxPair(string symbol)
        {

            FxPairDto pair = null;

            using (EFDbContext context = new EFDbContext())
            {
                pair = context.Pairs.SingleOrDefault(p => p.Name == symbol);
            }

            return pair;

        }

                                                                #endregion fx


                                                                #region currencies
        public IEnumerable<CurrencyDto> GetCurrencies()
        {

            IEnumerable<CurrencyDto> markets;

            using (var context = new EFDbContext())
            {
                markets = context.Currencies.ToList();
            }

            return markets;
        }


        public CurrencyDto GetCurrencyById(int id)
        {

            CurrencyDto market;
            using (var context = new EFDbContext())
            {
                market = context.Currencies.SingleOrDefault(m => m.Id == id);
            }

            return market;
        }

        public CurrencyDto GetCurrencyByName(string name)
        {

            CurrencyDto market;
            using (var context = new EFDbContext())
            {
                market = context.Currencies.SingleOrDefault(m => m.Name.Equals(name));
            }

            return market;
        }

        public CurrencyDto GetCurrencyBySymbol(string symbol)
        {

            CurrencyDto market;
            using (var context = new EFDbContext())
            {
                market = context.Currencies.SingleOrDefault(m => m.Symbol.Equals(symbol));
            }

            return market;
        }

                                                                #endregion currencies

    }
}
