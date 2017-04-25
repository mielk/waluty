using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace Stock.DAL.Repositories
{

    public class EFMarketRepository : IMarketRepository
    {

        public IEnumerable<MarketDto> GetMarkets()
        {
            IEnumerable<MarketDto> markets;
            using (var context = new MarketContext())
            {
                markets = context.Markets.ToList();
            }
            return markets;
        }

        public MarketDto GetMarketById(int id)
        {
            MarketDto market;
            using (var context = new MarketContext())
            {
                market = context.Markets.SingleOrDefault(m => m.Id == id);
            }
            return market;
        }

        public MarketDto GetMarketByName(string name)
        {
            MarketDto market;
            using (var context = new MarketContext())
            {
                market = context.Markets.SingleOrDefault(m => m.Name.Equals(name));
            }
            return market;
        }

        public MarketDto GetMarketBySymbol(string symbol)
        {
            MarketDto market;
            using (var context = new MarketContext())
            {
                market = context.Markets.SingleOrDefault(m => m.ShortName.Equals(symbol));
            }
            return market;
        }

    }

}
