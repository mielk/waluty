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
        public IEnumerable<CompanyDto> FilterCompanies(string q, int limit)
        {
            string lower = q.ToLower();
            IEnumerable<CompanyDto> results = null;

            using (EFDbContext context = new EFDbContext())
            {
                results = context.Companies.Where(c => c.PairName.ToLower().Contains(lower) ||
                                                  c.Short.ToLower().Contains(lower)).Take(limit).ToList();
            }

            return results;

        }

        public CompanyDto GetCompany(int id)
        {

            CompanyDto company = null;

            using (EFDbContext context = new EFDbContext())
            {
                company = context.Companies.SingleOrDefault(c => c.Id == id);
            }

            return company;

        }
                                                                #endregion assets


                                                                #region fx

        public IEnumerable<PairDto> FilterPairs(string q, int limit)
        {
            string lower = q.ToLower();
            IEnumerable<PairDto> results = null;

            using (EFDbContext context = new EFDbContext())
            {
                results = context.Pairs.Where(p => p.IsActive && p.PairName.ToLower().Contains(lower)).Take(limit).ToList();
            }

            return results;

        }

        public PairDto GetPair(int id)
        {

            PairDto pair = null;

            using (EFDbContext context = new EFDbContext())
            {
                pair = context.Pairs.SingleOrDefault(p => p.Id == id);
            }

            return pair;

        }

        public PairDto GetPair(string symbol)
        {

            PairDto pair = null;

            using (EFDbContext context = new EFDbContext())
            {
                pair = context.Pairs.SingleOrDefault(p => p.PairName == symbol);
            }

            return pair;

        }

                                                                #endregion fx


    }
}
