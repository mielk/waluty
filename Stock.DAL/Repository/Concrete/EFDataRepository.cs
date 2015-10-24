using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.DAL.Repositories
{
    public class EFDataRepository : IDataRepository
    {

        private const string PricesTableTemplate = "tm_prices_template";
        private const string PricesTablePrefix = "prices_";


        //private static readonly EFDbContext Context = EFDbContext.GetInstance();

        public IEnumerable<DataItemDto> GetQuotations(int assetId, int timeband, int count)
        {

            IEnumerable<DataItemDto> quotations = null;
            IEnumerable<QuotationDto> tempQuotations = new List<QuotationDto>();
            IEnumerable<PriceDto> tempPrices = new List<PriceDto>();

            using (EFDbContext context = new EFDbContext())
            {
                switch (timeband)
                {
                    case 1:
                        //tempQuotations = context.DailyQuotations.Where(q => q.AssetId == assetId).AsQueryable();
                        //tempPrices = context.DailyPrices.Where(p => p.AssetId == assetId).AsQueryable();
                        break;
                    case 2:
                        //tempQuotations = context.WeeklyQuotations.Where(q => q.AssetId == assetId).AsQueryable();
                        //tempPrices = context.WeeklyPrices.Where(p => p.AssetId == assetId).AsQueryable();
                        break;
                    case 3:
                        //tempQuotations = context.MonthlyQuotations.Where(q => q.AssetId == assetId).AsQueryable();
                        //tempPrices = context.MonthlyPrices.Where(p => p.AssetId == assetId).AsQueryable();
                        break;
                }



                quotations = (from quotation in tempQuotations
                              join price in tempPrices
                              on quotation.PriceDate equals price.ItemDate

                              select new DataItemDto
                              {
                                  ItemDate = quotation.PriceDate,
                                  AssetId = quotation.AssetId
                                  //Price = new PriceDto {
                                      
                                  //},
                                  //Quotation = new QuotationDto {

                                  //}
                              }).OrderByDescending(q => q.ItemDate).Take(count).OrderBy(q => q.ItemDate).ToList();


            }

            return quotations;

        }

        public IEnumerable<DataItemDto> GetQuotations(int companyId, int timeband, DateTime start)
        {

            IEnumerable<DataItemDto> quotations = null;
            IEnumerable<QuotationDto> tempQuotations = new List<QuotationDto>();
            IEnumerable<PriceDto> tempPrices = new List<PriceDto>();

            using (EFDbContext context = new EFDbContext())
            {
                switch (timeband)
                {
                    case 1:
                        //tempQuotations = context.DailyQuotations.Where(q => q.AssetId == companyId && q.AssetId.CompareTo(start) >= 0).AsQueryable();
                        //tempPrices = context.DailyPrices.Where(p => p.AssetId == companyId && p.PriceDate.CompareTo(start) >= 0).AsQueryable();
                        break;
                    case 2:
                        //tempQuotations = context.WeeklyQuotations.Where(q => q.AssetId == companyId && q.AssetId.CompareTo(start) >= 0).AsQueryable();
                        //tempPrices = context.WeeklyPrices.Where(p => p.AssetId == companyId && p.PriceDate.CompareTo(start) >= 0).AsQueryable();
                        break;
                    case 3:
                        //tempQuotations = context.MonthlyQuotations.Where(q => q.AssetId == companyId && q.AssetId.CompareTo(start) >= 0).AsQueryable();
                        //tempPrices = context.MonthlyPrices.Where(p => p.AssetId == companyId && p.PriceDate.CompareTo(start) >= 0).AsQueryable();
                        break;
                }



                quotations = (from quotation in tempQuotations
                              join price in tempPrices
                              on quotation.PriceDate equals price.ItemDate

                              select new DataItemDto
                              {
                                  ItemDate = quotation.PriceDate,
                                  AssetId = quotation.AssetId
                              }).OrderBy(q => q.ItemDate).ToList();

            }

            return quotations;

        }

        public IEnumerable<DataItemDto> GetQuotations(int companyId, int timeband, DateTime start, DateTime end)
        {

            IEnumerable<DataItemDto> quotations = null;
            IEnumerable<QuotationDto> tempQuotations = new List<QuotationDto>();
            IEnumerable<PriceDto> tempPrices = new List<PriceDto>();

            using (EFDbContext context = new EFDbContext())
            {
                switch (timeband)
                {
                    case 1:
                        //tempQuotations = context.DailyQuotations.Where(q => q.AssetId == companyId && q.AssetId.CompareTo(start) >= 0 && q.AssetId.CompareTo(end) <= 0).AsQueryable();
                        //tempPrices = context.DailyPrices.Where(p => p.AssetId == companyId && p.PriceDate.CompareTo(start) >= 0 && p.PriceDate.CompareTo(end) <= 0).AsQueryable();
                        break;
                    case 2:
                        //tempQuotations = context.WeeklyQuotations.Where(q => q.AssetId == companyId && q.AssetId.CompareTo(start) >= 0 && q.AssetId.CompareTo(end) <= 0).AsQueryable();
                        //tempPrices = context.WeeklyPrices.Where(p => p.AssetId == companyId && p.PriceDate.CompareTo(start) >= 0 && p.PriceDate.CompareTo(end) <= 0).AsQueryable();
                        break;
                    case 3:
                        //tempQuotations = context.MonthlyQuotations.Where(q => q.AssetId == companyId && q.AssetId.CompareTo(start) >= 0 && q.AssetId.CompareTo(end) <= 0).AsQueryable();
                        //tempPrices = context.MonthlyPrices.Where(p => p.AssetId == companyId && p.PriceDate.CompareTo(start) >= 0 && p.PriceDate.CompareTo(end) <= 0).AsQueryable();
                        break;
                }



                quotations = (from quotation in tempQuotations
                              join price in tempPrices
                              on quotation.PriceDate equals price.ItemDate

                              select new DataItemDto
                              {
                                  ItemDate = quotation.PriceDate,
                                  AssetId = quotation.AssetId
                              }).OrderBy(q => q.ItemDate).ToList();

            }

            return quotations;

        }

        public IEnumerable<DataItemDto> GetQuotations(int companyId, int timeband)
        {

            IEnumerable<DataItemDto> quotations = null;
            IEnumerable<QuotationDto> tempQuotations = new List<QuotationDto>();
            IEnumerable<PriceDto> tempPrices = new List<PriceDto>();

            using (EFDbContext context = new EFDbContext())
            {
                switch (timeband)
                {
                    case 1:
                        //tempQuotations = context.DailyQuotations.Where(q => q.AssetId == companyId).AsQueryable();
                        //tempPrices = context.DailyPrices.Where(p => p.AssetId == companyId).AsQueryable();
                        break;
                    case 2:
                        //tempQuotations = context.WeeklyQuotations.Where(q => q.AssetId == companyId).AsQueryable();
                        //tempPrices = context.WeeklyPrices.Where(p => p.AssetId == companyId).AsQueryable();
                        break;
                    case 3:
                        //tempQuotations = context.MonthlyQuotations.Where(q => q.AssetId == companyId).AsQueryable();
                        //tempPrices = context.MonthlyPrices.Where(p => p.AssetId == companyId).AsQueryable();
                        break;
                }



                quotations = (from quotation in tempQuotations
                              join price in tempPrices
                              on quotation.PriceDate equals price.ItemDate

                              select new DataItemDto
                              {
                                  ItemDate = quotation.PriceDate,
                                  AssetId = quotation.AssetId
                              }).OrderBy(q => q.ItemDate).ToList();

            }


            return quotations;

        }



        public IEnumerable<DataItemDto> GetFxQuotations(string symbol)
        {
            string query = GetSqlForFxQuotations(symbol);
            IEnumerable<DataItemDto> quotations;

            using (var context = new EFDbContext())
            {
                quotations = context.Database.SqlQuery<DataItemDto>(query).ToList();
            }

            return quotations.OrderBy(q => q.ItemDate);

        }


        public IEnumerable<DataItemDto> GetFxQuotations(string symbol, int count)
        {

            string query = GetSqlForFxQuotations(symbol);
            IEnumerable<DataItemDto> quotations;
            //string sql = "SELECT * FROM fx.{0} ORDER BY PriceDate DESC LIMIT {1}";

            using (var context = new EFDbContext())
            {
                quotations = context.Database.SqlQuery<DataItemDto>(query).ToList();
            }

            return quotations.OrderBy(q => q.ItemDate);
        }


        public IEnumerable<DataItemDto> GetFxQuotations(string symbol, DateTime start)
        {
            string sql = "SELECT * FROM fx.{0} WHERE PriceDate >= {1} ORDER BY PriceDate DESC";
            string query = string.Format(sql, symbol, start.ToString());
            IEnumerable<DataItemDto> quotations;

            using (var context = new EFDbContext())
            {
                quotations = context.Database.SqlQuery<DataItemDto>(query).ToList();
            }

            return quotations.OrderBy(q => q.ItemDate);
        }


        public IEnumerable<DataItemDto> GetFxQuotations(string symbol, DateTime start, DateTime end)
        {

            string sql = "SELECT * FROM fx.{0} WHERE PriceDate >= {1} AND PriceDate <= {2} ORDER BY PriceDate DESC";
            string query = string.Format(sql, symbol, start.ToString(), end.ToString());
            IEnumerable<DataItemDto> quotations;

            using (var context = new EFDbContext())
            {
                quotations = context.Database.SqlQuery<DataItemDto>(query).ToList();
            }

            return quotations.OrderBy(q => q.ItemDate);

        }


        private string GetSqlForFxQuotations(string symbol)
        {

            var sql = "USE fx; " + 
                            "SELECT " + 
                                "  q.AssetId AS AssetId " +
                                ", 'X' AS Timeband " +
                                ", q.PriceDate AS ItemDate" + 
                                ", q.Id AS QuotationId" + 
                                ", q.OpenPrice AS OpenPrice" + 
                                ", q.LowPrice AS LowPrice" + 
                                ", q.HighPrice AS HighPrice" + 
                                ", q.ClosePrice AS ClosePrice" + 
                                ", q.Volume AS Volume" + 
                                ", p.Id AS PriceId" + 
                                ", p.DeltaClosePrice AS DeltaClosePrice" + 
                                ", p.PriceDirection2D AS PriceDirection2D" + 
                                ", p.PriceDirection3D AS PriceDirection3D" + 
                                ", p.PeakByCloseEvaluation AS PeakByCloseEvaluation" + 
                                ", p.PeakByHighEvaluation AS PeakByHighEvaluation" + 
                                ", p.TroughByCloseEvaluation AS TroughByCloseEvaluation" + 
                                ", p.TroughByLowEvaluation AS TroughByLowEvaluation" + 
                            " FROM" + 
                                " quotations_{0} AS q LEFT JOIN" + 
                                " prices_{0} AS p ON q.PriceDate = p.PriceDate" + 
                            " ORDER BY" + 
                                " q.PriceDate;";

            return string.Format(sql, symbol);

        }


        public IEnumerable<PriceDto> GetPrices(string symbol)
        {
            string tableName = PricesTablePrefix + symbol;
            string sql = "SELECT * FROM fx.{0} ORDER BY PriceDate DESC";
            string query = string.Format(sql, tableName);
            IEnumerable<PriceDto> prices;


            //Check if table exists. If not, create it.
            if (!CheckIfTableExists(tableName))
            {
                CreateTable(tableName, PricesTableTemplate);
            }


            using (var context = new EFDbContext())
            {
                prices = context.Database.SqlQuery<PriceDto>(query).ToList();
            }

            return prices.OrderBy(p => p.ItemDate);

        }



        public bool CheckIfTableExists(string tableName)
        {

            using (var context = new EFDbContext())
            {

                bool exists = context.Database
                                     .SqlQuery<int?>(@"
                         SELECT 1 FROM information_schema.tables AS T
                         WHERE table_schema = 'fx' 
                                AND table_name = '" + tableName + "' LIMIT 1")
                                     .SingleOrDefault() != null;


                return exists;


            }

        }


        public bool CreateTable(string tableName, string template)
        {

            string sqlCommand = string.Format("CREATE TABLE {0} LIKE {1}", tableName, template);


            using (var context = new EFDbContext())
            {

                try
                {
                    context.Database.ExecuteSqlCommand(sqlCommand);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
     
        }


        public IEnumerable<String> GetStats()
        {
            string sql = "SELECT symbol FROM fx.last_updates";
            IEnumerable<String> symbols;


            using (var context = new EFDbContext())
            {
                symbols = context.Database.SqlQuery<String>(sql).ToList();
            }

            return symbols;

        }

        public string toDb(double value)
        {
            return Math.Round(value, 5).ToString().Replace(',', '.');
        }

        public void AddPrice(PriceDto price, string symbol)
        {
            string tableName = PricesTablePrefix + symbol;
            string sql = "INSERT INTO fx." + tableName +
                "(AssetId, PriceDate, DeltaClosePrice, PriceDirection3D, PriceDirection2D, " +
                    "PeakByCloseEvaluation, PeakByHighEvaluation, TroughByCloseEvaluation, " +
                    "TroughByLowEvaluation) " +
                "VALUES (" +
                       price.AssetId +
                    ", '" + price.ItemDate + "'" +
                    ", " + toDb(price.DeltaClosePrice) +
                    ", " + price.PriceDirection3D +
                    ", " + price.PriceDirection2D +
                    ", " + toDb(price.PeakByCloseEvaluation) +
                    ", " + toDb(price.PeakByHighEvaluation) +
                    ", " + toDb(price.TroughByCloseEvaluation) +
                    ", " + toDb(price.TroughByLowEvaluation) + ");";

            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sql);
                context.SaveChanges();
            }
        }


        public void UpdatePrice(PriceDto price, string symbol)
        {
            string tableName = PricesTablePrefix + symbol;
            string sql = "UPDATE fx." + tableName +
                " SET " + 
                    "  AssetId = " + price.AssetId + 
                    ", PriceDate = '" + price.ItemDate + "'" + 
                    ", DeltaClosePrice = " + toDb(price.DeltaClosePrice) +
                    ", PriceDirection2D = " + price.PriceDirection2D +
                    ", PriceDirection3D = " + price.PriceDirection3D +
                    ", PeakByCloseEvaluation = " + toDb(price.PeakByCloseEvaluation) +
                    ", PeakByHighEvaluation = " + toDb(price.PeakByHighEvaluation) +
                    ", TroughByCloseEvaluation = " + toDb(price.TroughByCloseEvaluation) +
                    ", TroughByLowEvaluation = " + toDb(price.TroughByLowEvaluation) + 
                " WHERE Id = " + price.Id;
            
            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sql);
                context.SaveChanges();
            }

        }

    }
}
