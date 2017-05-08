using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;
using Stock.DAL.Infrastructure;

namespace Stock.DAL.Repositories
{
    public class EFQuotationRepository : IQuotationRepository
    {
        
        private const string QUOTATIONS_TABLE_NAME = "quotations";

        public IEnumerable<QuotationDto> GetQuotations(int assetId, int timeframe)
        {
            IEnumerable<QuotationDto> results;
            using (var context = new DataContext())
            {
                results = context.Quotations.Where(q => q.TimeframeId == timeframe && q.AssetId == assetId).ToList();
            }
            return results;
        }

        public IEnumerable<QuotationDto> GetQuotations(AnalysisDataQueryDefinition queryDef)
        {
            return null;
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
                        record = dto;
                    }
                    else
                    {
                        db.Quotations.Add(dto);
                    }
                }
                db.SaveChanges();

            }

        }

        private string getInsertSql(QuotationDto quotation)
        {
            string sql = "INSERT INTO {0} ({1}) VALUES ({2});";
            string propertyNames = "QuotationId, PriceDate, AssetId, TimeframeId, OpenPrice, HighPrice, LowPrice, ClosePrice, RealClosePrice, Volume, IndexNumber";
            string propertyValues = quotation.QuotationId + ", " + quotation.PriceDate + ", " + quotation.AssetId + ", " + quotation.TimeframeId + ", " + quotation.OpenPrice + ", " +
                                quotation.HighPrice + ", " + quotation.LowPrice + ", " + quotation.ClosePrice + ", NULL, " + quotation.Volume + ", " + quotation.IndexNumber;
            return string.Format(sql, QUOTATIONS_TABLE_NAME, propertyNames, propertyValues);

        }

        private string getUpdateSql(QuotationDto quotation)
        {
            string sql = "UPDATE {0} SET {1} WHERE {2}";
            string settingValues = "PriceDate = " + quotation.PriceDate + ", " + 
                                   "AssetId = " + quotation.AssetId + ", " + 
                                   "TimeframeId = " + quotation.TimeframeId + ", " + 
                                   "OpenPrice = " + quotation.OpenPrice + ", " + 
                                   "HighPrice = " + quotation.HighPrice + ", " + 
                                   "LowPrice = " + quotation.LowPrice + ", " + 
                                   "ClosePrice = " + quotation.ClosePrice + ", " + 
                                   "Volume = " + quotation.Volume + ", " + 
                                   "IndexNumber = " + quotation.IndexNumber;
            string wherePart = "QuotationId = " + quotation.QuotationId;
            return string.Format(sql,QUOTATIONS_TABLE_NAME, settingValues, wherePart);
        }

    }
}
