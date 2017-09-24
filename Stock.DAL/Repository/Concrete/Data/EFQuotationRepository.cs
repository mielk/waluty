using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;
using Stock.DAL.Infrastructure;
using Stock.Core;
using Stock.Utils;

namespace Stock.DAL.Repositories
{
    public class EFQuotationRepository : IQuotationRepository
    {

        private const string QUOTATIONS_TABLE_NAME = "quotations";

        public IEnumerable<QuotationDto> GetQuotations(AnalysisDataQueryDefinition queryDef)
        {
            IEnumerable<QuotationDto> results;
            using (var context = new DataContext())
            {

                results = context.Quotations.Where(q => q.TimeframeId == queryDef.TimeframeId &&
                                                        q.AssetId == queryDef.AssetId && 
                                                        q.IndexNumber > 0).ToList();
            }

            if (queryDef.StartDate != null)
            {
                results = results.Where(q => q.PriceDate.CompareTo((DateTime)queryDef.StartDate) >= 0);
            }

            if (queryDef.EndDate != null)
            {
                results = results.Where(q => q.PriceDate.CompareTo((DateTime)queryDef.EndDate) <= 0);
            }

            if (queryDef.StartIndex != null)
            {
                results = results.Where(q => q.IndexNumber >= queryDef.StartIndex);
            }

            if (queryDef.EndIndex != null)
            {
                results = results.Where(q => q.IndexNumber <= queryDef.EndIndex);
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