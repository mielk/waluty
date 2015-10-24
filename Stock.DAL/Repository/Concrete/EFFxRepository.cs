using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.DAL.Repositories
{
    public class EFFxRepository : IFxRepository
    {

        public IEnumerable<PairDto> FilterPairs(string q, int limit)
        {
            string lower = q.ToLower();
            IEnumerable<PairDto> results = null;

            using (EFDbContext context = new EFDbContext())
            {
                results = context.Pairs.Where(p => p.PairName.ToLower().Contains(lower)).Take(limit).ToList();
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

        

    }
}
