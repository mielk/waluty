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
        private const string QuotationsTablePrefix = "quotations_";
        private const string MacdTablePrefix = "macd_";
        private const string AdxTablePrefix = "adx_";


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
                              on quotation.PriceDate equals price.PriceDate

                              select new DataItemDto
                              {
                                  PriceDate = quotation.PriceDate,
                                  AssetId = quotation.AssetId
                                  //Price = new PriceDto {
                                      
                                  //},
                                  //Quotation = new QuotationDto {

                                  //}
                              }).OrderByDescending(q => q.PriceDate).Take(count).OrderBy(q => q.PriceDate).ToList();


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
                              on quotation.PriceDate equals price.PriceDate

                              select new DataItemDto
                              {
                                  PriceDate = quotation.PriceDate,
                                  AssetId = quotation.AssetId
                              }).OrderBy(q => q.PriceDate).ToList();

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
                              on quotation.PriceDate equals price.PriceDate

                              select new DataItemDto
                              {
                                  PriceDate = quotation.PriceDate,
                                  AssetId = quotation.AssetId
                              }).OrderBy(q => q.PriceDate).ToList();

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
                              on quotation.PriceDate equals price.PriceDate

                              select new DataItemDto
                              {
                                  PriceDate = quotation.PriceDate,
                                  AssetId = quotation.AssetId
                              }).OrderBy(q => q.PriceDate).ToList();

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

            return quotations.OrderBy(q => q.PriceDate);

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

            return quotations.OrderBy(q => q.PriceDate);
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

            return quotations.OrderBy(q => q.PriceDate);
        }

        public IEnumerable<DataItemDto> GetFxQuotations(string symbol, DateTime start, DateTime end)
        {

            string query = GetSqlForFxQuotations(symbol, start, end);

            //string sql = "SELECT * FROM fx.{0} WHERE PriceDate >= {1} AND PriceDate <= {2} ORDER BY PriceDate DESC";
            //string query = string.Format(sql, symbol, start.ToString(), end.ToString());
            IEnumerable<DataItemDto> quotations;

            using (var context = new EFDbContext())
            {
                quotations = context.Database.SqlQuery<DataItemDto>(query).ToList();
            }

            return quotations.OrderBy(q => q.PriceDate);

        }

        public IEnumerable<DataItemDto> GetFxQuotationsForAnalysis(string symbol, string analysisType, DateTime lastAnalysisItem, int counter)
        {

            string query = GetSqlForFxQuotations(symbol, analysisType, lastAnalysisItem, counter);

            IEnumerable<DataItemDto> quotations;

            using (var context = new EFDbContext())
            {
                quotations = context.Database.SqlQuery<DataItemDto>(query).ToList();
            }

            return quotations.OrderBy(q => q.PriceDate);

        }

        public IEnumerable<DataItemDto> GetFxQuotationsForAnalysis(string symbol, string analysisType)
        {

            string query = GetSqlForFxQuotations(symbol, analysisType);
           

            IEnumerable<DataItemDto> quotations;

            using (var context = new EFDbContext())
            {
                quotations = context.Database.SqlQuery<DataItemDto>(query).ToList();
            }

            return quotations.OrderBy(q => q.PriceDate);

        }


        private string GetSqlForFxQuotations(string symbol)
        {

            var timeband = symbol.Substring(symbol.IndexOf('_') + 1);

            var sql = "USE fx; " + 
                            "SELECT " + 
                                " '{1}' AS Timeband " +
                                ", q.*" + 
                                ", p.*" + 
                            " FROM" + 
                                " quotations_{0} AS q LEFT JOIN" + 
                                " prices_{0} AS p ON q.PriceDate = p.PriceDate" + 
                            " ORDER BY" + 
                                " q.PriceDate;";

            return string.Format(sql, symbol, timeband);

        }

        private string GetSqlForFxQuotations(string symbol, DateTime startDate, DateTime endDate)
        {

            var timeband = symbol.Substring(symbol.IndexOf('_') + 1);
            var sql = "USE fx; " +
                            "SELECT " +
                                " '{1}' AS Timeband " +
                                ", q.*" +
                                ", p.*" + 
                            " FROM" +
                                " quotations_{0} AS q LEFT JOIN" +
                                " prices_{0} AS p ON q.PriceDate = p.PriceDate" +
                            " WHERE" +
                                " q.PriceDate >= '" + startDate + "' AND" +
                                " q.PriceDate <= '" + endDate + "'" + 
                            " ORDER BY" +
                                " q.PriceDate;";

            return string.Format(sql, symbol, timeband);

        }


        private string GetSqlForFxQuotations(string symbol, string analysisType)
        {
            var timeband = symbol.Substring(symbol.IndexOf('_') + 1);
            var sql = "USE fx; " +
                            "SELECT " +
                                "'{2}' AS Timeband " +
                                ", q.*" +
                                ", p.*" +
                            " FROM" +
                                " quotations_{0} AS q LEFT JOIN" +
                                " {1}_{0} AS p ON q.PriceDate = p.PriceDate" +
                            " ORDER BY" +
                                " q.PriceDate;";

            return string.Format(sql, symbol, analysisType, timeband);

        }

        private string GetSqlForFxQuotations(string symbol, string analysisType, DateTime lastAnalysisItem, int counter)
        {
            var timeband = symbol.Substring(symbol.IndexOf('_') + 1);
            var sql = "USE fx; " +
                            "(SELECT " +
                                "'{2}' AS Timeband " +
                                ", q.* " +
                                ", p.* " +
                            " FROM" +
                                " quotations_{0} AS q LEFT JOIN" +
                                " prices_{0} AS p ON q.PriceDate = p.PriceDate" +
                            " WHERE" +
                                " q.PriceDate <= '" + lastAnalysisItem + "'" +
                            " ORDER BY q.PriceDate DESC" + 
                            " LIMIT " + counter + ") " + 
                            " UNION " + 
                            "(SELECT " +
                                "'{2}' AS Timeband " +
                                ", q.* " +
                                ", p.* " +
                            " FROM" +
                                " quotations_{0} AS q LEFT JOIN" +
                                " {1}_{0} AS p ON q.PriceDate = p.PriceDate" +
                            " WHERE" +
                                " q.PriceDate >= '" + lastAnalysisItem + "'" +
                            " ORDER BY q.PriceDate)";

            return string.Format(sql, symbol, analysisType, timeband);

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

            return prices.OrderBy(p => p.PriceDate);

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

        public object GetDataSetProperties(string symbol)
        {
            //string sql = "SELECT " +
            //                "COUNT(Id) AS Counter, MIN(PriceDate) AS FirstDate, MAX(PriceDate) AS LastDate, " +
            //                "MIN(LowPrice) AS MinPrice, MAX(HighPrice) AS MaxPrice " +
            //             "FROM fx.quotations_" + symbol;
            string sqlCounter = "SELECT COUNT(QuotationId) FROM fx.quotations_" + symbol;
            string sqlMinDate = "SELECT MIN(PriceDate) FROM fx.quotations_" + symbol;
            string sqlMaxDate = "SELECT MAX(PriceDate) FROM fx.quotations_" + symbol;
            string sqlMinPrice = "SELECT MIN(LowPrice) FROM fx.quotations_" + symbol;
            string sqlMaxPrice = "SELECT MAX(HighPrice) FROM fx.quotations_" + symbol;



            using (var context = new EFDbContext())
            {
                int counter = 0;
                DateTime firstDate = new DateTime();
                DateTime lastDate = new DateTime();
                double minPrice = 0d;
                double maxPrice = 0d;
                
                //Counter.
                foreach (var i in context.Database.SqlQuery<String>(sqlCounter))
                {
                    counter = int.Parse(i);
                    break;
                }

                //Min date.
                foreach (var i in context.Database.SqlQuery<String>(sqlMinDate))
                {
                    firstDate = DateTime.Parse(i);
                    break;
                }

                //Max date.
                foreach (var i in context.Database.SqlQuery<String>(sqlMaxDate))
                {
                    lastDate = DateTime.Parse(i);
                    break;
                }

                //Min price.
                foreach (var i in context.Database.SqlQuery<String>(sqlMinPrice))
                {
                    minPrice = double.Parse(i);
                    break;
                }

                //Max price.
                foreach (var i in context.Database.SqlQuery<String>(sqlMaxPrice))
                {
                    maxPrice = double.Parse(i);
                    break;
                }

                return new 
                {
                    counter = counter,
                    firstDate = firstDate,
                    lastDate = lastDate,
                    minPrice = minPrice,
                    maxPrice = maxPrice
                };

            }

        }

        public string toDb(double value)
        {
            return Math.Round(value, 5).ToString().Replace(',', '.');
        }



        public void UpdateQuotation(QuotationDto quotation, string symbol)
        {
            string tableName = QuotationsTablePrefix + symbol;
            string sql = "UPDATE fx." + tableName +
                " SET " +
                    "  OpenPrice = " + toDb(quotation.OpenPrice) +
                    ", HighPrice = " + toDb(quotation.HighPrice) +
                    ", LowPrice = " + toDb(quotation.LowPrice) +
                    ", ClosePrice = " + toDb(quotation.ClosePrice) +
                    ", Volume = " + toDb(quotation.Volume) +
                " WHERE PriceId = " + quotation.Id;

            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sql);
                context.SaveChanges();
            }

        }

        public void AddPrice(PriceDto price, string symbol)
        {
            string tableName = PricesTablePrefix + symbol;

            string sqlRemove = "DELETE FROM fx." + tableName +
                        " WHERE PriceDate = '" + price.PriceDate + "';";
            string sqlInsert = "INSERT INTO fx." + tableName +
                "(AssetId, PriceDate, DeltaClosePrice, PriceDirection3D, PriceDirection2D, " +
                    "PeakByCloseEvaluation, PeakByHighEvaluation, TroughByCloseEvaluation, " +
                    "TroughByLowEvaluation) " +
                "VALUES (" +
                       price.AssetId +
                    ", '" + price.PriceDate + "'" +
                    ", " + toDb(price.DeltaClosePrice) +
                    ", " + price.PriceDirection3D +
                    ", " + price.PriceDirection2D +
                    ", " + toDb(price.PeakByCloseEvaluation) +
                    ", " + toDb(price.PeakByHighEvaluation) +
                    ", " + toDb(price.TroughByCloseEvaluation) +
                    ", " + toDb(price.TroughByLowEvaluation) + ");";

            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sqlRemove);
                context.Database.ExecuteSqlCommand(sqlInsert);
                context.SaveChanges();
            }
        }

        public void UpdatePrice(PriceDto price, string symbol)
        {
            string tableName = PricesTablePrefix + symbol;
            string sql = "UPDATE fx." + tableName +
                " SET " + 
                    "  AssetId = " + price.AssetId +
                    ", PriceDate = '" + price.PriceDate + "'" + 
                    ", DeltaClosePrice = " + toDb(price.DeltaClosePrice) +
                    ", PriceDirection2D = " + price.PriceDirection2D +
                    ", PriceDirection3D = " + price.PriceDirection3D +
                    ", PeakByCloseEvaluation = " + toDb(price.PeakByCloseEvaluation) +
                    ", PeakByHighEvaluation = " + toDb(price.PeakByHighEvaluation) +
                    ", TroughByCloseEvaluation = " + toDb(price.TroughByCloseEvaluation) +
                    ", TroughByLowEvaluation = " + toDb(price.TroughByLowEvaluation) + 
                " WHERE PriceId = " + price.Id;
            
            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sql);
                context.SaveChanges();
            }

        }

        public void AddMacd(MacdDto macd, string symbol)
        {
            string tableName = MacdTablePrefix + symbol;

            string sqlRemove = "DELETE FROM fx." + tableName +
                        " WHERE PriceDate = '" + macd.PriceDate + "';";
            string sqlInsert = "INSERT INTO fx." + tableName +
                "(AssetId, PriceDate, DeltaClosePrice, PriceDirection3D, PriceDirection2D, " +
                    "PeakByCloseEvaluation, PeakByHighEvaluation, TroughByCloseEvaluation, " +
                    "TroughByLowEvaluation) " +
                "VALUES (";// +
                    //   price.AssetId +
                    //", '" + price.PriceDate + "'" +
                    //", " + toDb(price.DeltaClosePrice) +
                    //", " + price.PriceDirection3D +
                    //", " + price.PriceDirection2D +
                    //", " + toDb(price.PeakByCloseEvaluation) +
                    //", " + toDb(price.PeakByHighEvaluation) +
                    //", " + toDb(price.TroughByCloseEvaluation) +
                    //", " + toDb(price.TroughByLowEvaluation) + ");";

            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sqlRemove);
                context.Database.ExecuteSqlCommand(sqlInsert);
                context.SaveChanges();
            }
        }

        public void UpdateMacd(MacdDto macd, string symbol)
        {

        }

        public void AddAdx(AdxDto adx, string symbol)
        {

        }

        public void UpdateAdx(AdxDto adx, string symbol)
        {

        }

        public LastDates GetSymbolLastItems(string symbol, string analysisType)
        {

            string sqlQuotation = "SELECT MAX(PriceDate) FROM quotations_" + symbol + " WHERE OpenPrice > -1;";
            string sqlAnalysisItem = "SELECT MAX(PriceDate) FROM " + analysisType + "_" + symbol + ";";
            DateTime lastQuotation = new DateTime();
            DateTime? lastAnalysisItem = null;

            using (var context = new EFDbContext())
            {
                foreach (var i in context.Database.SqlQuery<String>(sqlQuotation))
                {
                    lastQuotation = DateTime.Parse(i);
                    break;
                }

                foreach (var i in context.Database.SqlQuery<String>(sqlAnalysisItem))
                {

                    try
                    {
                        lastAnalysisItem = DateTime.Parse(i);
                    }
                    catch (ArgumentNullException)
                    {
                        lastAnalysisItem = null;
                    }

                    break;

                }

            }


            return new LastDates
            {
                LastQuotation = lastQuotation,
                LastAnalysisItem = lastAnalysisItem
            };
        }

    }
}
