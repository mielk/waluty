using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace Stock.DAL.Repositories
{
    public class EFMarketRepository : IMarketRepository
    {

        //private static readonly EFDbContext Context = EFDbContext.GetInstance();

        public IEnumerable<MarketDto> GetMarkets()
        {

            IEnumerable<MarketDto> markets;

            using (var context = new EFDbContext())
            {
                markets = context.Markets.ToList();
            }

            return markets;
        }
    }
}
