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
    public class EFQuotationRepository : IQuotationRepository
    {
        
        private const string QUOTATIONS_TABLE_NAME = "quotations";

        public IEnumerable<QuotationDto> GetQuotations(AnalysisDataQueryDefinition queryDef)
        {
            DateTime MIN_DATE = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime MAX_DATE = new DateTime(2100, 1, 1, 0, 0, 0);
            IEnumerable<QuotationDto> results;
            using (var context = new DataContext())
            {
                results = context.Quotations.Where(q => q.TimeframeId == queryDef.TimeframeId &&
                                                        q.AssetId == queryDef.AssetId &&
                                                        (q.PriceDate.CompareTo(queryDef.StartDate ?? MIN_DATE) >= 0) &&
                                                        (q.PriceDate.CompareTo(queryDef.EndDate ?? MAX_DATE) <= 0)).ToList();
            }

            if (queryDef.Limit > 0)
            {
                if (queryDef.StartDate != null || queryDef.EndDate == null)
                {
                    return results.OrderBy(q => q.PriceDate).Take(queryDef.Limit);
                }
                else if (queryDef.EndDate != null)
                {
                    return results.OrderByDescending(q => q.PriceDate).Take(queryDef.Limit).OrderBy(q => q.PriceDate);
                }
            }

            return results;

        }

        public void UpdateQuotations(IEnumerable<QuotationDto> quotations)
        {

            using (var db = new DataContext())
            {

                foreach (QuotationDto dto in quotations)
                {
                    var record = db.Quotations.SingleOrDefault(d => d.QuotationId == dto.QuotationId);
                    if (record != null)
                    {
                        record.CopyProperties(dto);
                    }
                    else
                    {
                        db.Quotations.Add(dto);
                    }
                }
                db.SaveChanges();

            }

        }

    }
}
