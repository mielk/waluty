using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;
using Stock.DAL.Infrastructure;
using Stock.Core;

namespace Stock.DAL.Repositories
{
    public class EFPriceRepository : IPriceRepository
    {
        
        private const string PRICE_TABLE_NAME = "prices";


        public IEnumerable<PriceDto> GetPrices(AnalysisDataQueryDefinition queryDef)
        {
            DateTime MIN_DATE = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime MAX_DATE = new DateTime(2100, 1, 1, 0, 0, 0);
            IEnumerable<PriceDto> results;
            using (var context = new DataContext())
            {
                results = context.Prices.Where(p => p.TimeframeId == queryDef.TimeframeId &&
                                                        p.AssetId == queryDef.AssetId &&
                                                        (p.PriceDate.CompareTo(queryDef.StartDate ?? MIN_DATE) >= 0) &&
                                                        (p.PriceDate.CompareTo(queryDef.EndDate ?? MAX_DATE) <= 0)).ToList();
            }

            if (queryDef.Limit > 0)
            {
                if (queryDef.StartDate != null || queryDef.EndDate == null)
                {
                    return results.OrderBy(p => p.PriceDate).Take(queryDef.Limit);
                }
                else if (queryDef.EndDate != null)
                {
                    return results.OrderByDescending(p => p.PriceDate).Take(queryDef.Limit).OrderBy(p => p.PriceDate);
                }
            }

            return results;

        }

        public void UpdatePrices(IEnumerable<PriceDto> prices)
        {

            using (var db = new DataContext())
            {

                foreach (PriceDto dto in prices)
                {
                    var record = db.Prices.SingleOrDefault(d => d.Id == dto.Id);
                    if (record != null)
                    {
                        record.CopyProperties(dto);
                    }
                    else
                    {
                        db.Prices.Add(dto);
                    }
                }
                db.SaveChanges();

            }

        }

    }
}
