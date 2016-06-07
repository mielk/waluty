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

        public FxPairDto GetPair(int id)
        {

            FxPairDto pair = null;

            using (EFDbContext context = new EFDbContext())
            {
                pair = context.Pairs.SingleOrDefault(p => p.Id == id);
            }

            return pair;

        }

        public FxPairDto GetPair(string symbol)
        {

            FxPairDto pair = null;

            using (EFDbContext context = new EFDbContext())
            {
                pair = context.Pairs.SingleOrDefault(p => p.Name == symbol);
            }

            return pair;

        }

                                                                #endregion fx


    }
}
