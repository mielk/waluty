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
        private const string QuotationsTablePrefix = "quotations_";
        private const string PricesTablePrefix = "prices_";
        private const string MacdTablePrefix = "macd_";
        private const string AdxTablePrefix = "adx_";
        private const string CandlestickTablePrefix = "candlesticks_";
        private const string TrendlinesTablePrefix = "trendlines_";
        private const string AnalysisInfoTable = "_log_analysis";
        private const string ExtremaEvaluationTable = "_extrema_evaluation";



        public IEnumerable<DataItemDto> GetDataItems(string symbol, DateTime? startDate, DateTime? endDate, IEnumerable<string> analysisType)
        {

            Dictionary<long, DataItemDto> dictItems = getFxQuotationsAsDataItemsDictionary(symbol, startDate, endDate);

            if (analysisType.Contains("prices"))
            {
                IEnumerable<PriceDto> prices = GetPrices(symbol, startDate, endDate);
                foreach (var price in prices)
                {
                    try{
                        DataItemDto did = null;
                        dictItems.TryGetValue(price.PriceDate.Ticks, out did);
                        if (did != null)
                        {
                            did.Price = price;
                        }
                    }catch (Exception){}
                }
            }

            if (analysisType.Contains("macd"))
            {
                IEnumerable<MacdDto> macds = GetMacds(symbol, startDate, endDate);
                foreach (var macd in macds)
                {
                    try
                    {
                        DataItemDto did = null;
                        dictItems.TryGetValue(macd.PriceDate.Ticks, out did);
                        if (did != null)
                        {
                            did.Macd = macd;
                        }
                    }
                    catch (Exception) { }
                }
            }

            if (analysisType.Contains("adx"))
            {
                IEnumerable<AdxDto> adxs = GetAdxs(symbol, startDate, endDate);
                foreach (var adx in adxs)
                {
                    try
                    {
                        DataItemDto did = null;
                        dictItems.TryGetValue(adx.PriceDate.Ticks, out did);
                        if (did != null)
                        {
                            did.Adx = adx;
                        }
                    }
                    catch (Exception) { }
                }
            }

            if (analysisType.Contains("candlesticks"))
            {
                IEnumerable<CandlestickDto> candlesticks = GetCandlesticks(symbol, startDate, endDate);
                foreach (var candle in candlesticks)
                {
                    try
                    {
                        DataItemDto did = null;
                        dictItems.TryGetValue(candle.PriceDate.Ticks, out did);
                        if (did != null)
                        {
                            did.Candlestick = candle;
                        }
                    }
                    catch (Exception) { }
                }
            }


            return dictItems.Values;


        }


        private Dictionary<long, DataItemDto> getFxQuotationsAsDataItemsDictionary(string symbol, DateTime? startDate, DateTime? endDate)
        {
            Dictionary<long, DataItemDto> dictItems = new Dictionary<long, DataItemDto>();
            IEnumerable<DataItemDto> quotations;
            if (endDate != null){
                quotations = GetFxQuotations(symbol, (DateTime)startDate, (DateTime)endDate);
            } else if (startDate != null){
                quotations = GetFxQuotations(symbol, (DateTime)startDate);
            } else {
                quotations = GetFxQuotations(symbol);
            }


            foreach (var item in quotations)
            {
                dictItems.Add(item.PriceDate.Ticks, item);
            }

            return dictItems;

        }

        //private static readonly EFDbContext Context = EFDbContext.GetInstance();

        public IEnumerable<DataItemDto> GetQuotations(int assetId, int timeframe, int count)
        {

            IEnumerable<DataItemDto> quotations = null;
            IEnumerable<QuotationDto> tempQuotations = new List<QuotationDto>();
            IEnumerable<PriceDto> tempPrices = new List<PriceDto>();

            using (EFDbContext context = new EFDbContext())
            {
                switch (timeframe)
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

        public IEnumerable<DataItemDto> GetQuotations(int companyId, int timeframe, DateTime start)
        {

            IEnumerable<DataItemDto> quotations = null;
            IEnumerable<QuotationDto> tempQuotations = new List<QuotationDto>();
            IEnumerable<PriceDto> tempPrices = new List<PriceDto>();

            using (EFDbContext context = new EFDbContext())
            {
                switch (timeframe)
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

        public IEnumerable<DataItemDto> GetQuotations(int companyId, int timeframe, DateTime start, DateTime end)
        {

            IEnumerable<DataItemDto> quotations = null;
            IEnumerable<QuotationDto> tempQuotations = new List<QuotationDto>();
            IEnumerable<PriceDto> tempPrices = new List<PriceDto>();

            using (EFDbContext context = new EFDbContext())
            {
                switch (timeframe)
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

        public IEnumerable<DataItemDto> GetQuotations(int companyId, int timeframe)
        {

            IEnumerable<DataItemDto> quotations = null;
            IEnumerable<QuotationDto> tempQuotations = new List<QuotationDto>();
            IEnumerable<PriceDto> tempPrices = new List<PriceDto>();

            using (EFDbContext context = new EFDbContext())
            {
                switch (timeframe)
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
            string query = GetSqlForFxQuotations(symbol, start, null);
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

        public IEnumerable<DataItemDto> GetFxQuotations(string symbol, bool isSimulation)
        {

            //If this is not for simulation return normal set of quotations for a given symbol.
            if (!isSimulation) return GetFxQuotations(symbol);

            string query = GetSqlForPlainFxQuotations(symbol);
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

        public IEnumerable<ExtremumDto> GetExtrema(string symbol, DateTime startDate, DateTime endDate)
        {

            IEnumerable<ExtremumDto> extrema;

            string query = "SELECT * FROM fx._extrema_evaluation WHERE symbol = '{0}' AND pricedate BETWEEN '{1}' AND '{2}';";
            query = string.Format(query, symbol, startDate, endDate);

            using (var context = new EFDbContext())
            {
                extrema = context.Database.SqlQuery<ExtremumDto>(query).ToList();
            }

            return extrema;

        }



        public IEnumerable<PriceDto> GetPrices(string symbol, DateTime? startDate, DateTime? endDate)
        {

            string query = GetSqlForFxCalculations(symbol, PricesTablePrefix, startDate, endDate);
            IEnumerable<PriceDto> prices;

            using (var context = new EFDbContext())
            {
                prices = context.Database.SqlQuery<PriceDto>(query).ToList();
            }

            return prices.OrderBy(q => q.PriceDate);

        }

        public IEnumerable<MacdDto> GetMacds(string symbol, DateTime? startDate, DateTime? endDate)
        {

            string query = GetSqlForFxCalculations(symbol, MacdTablePrefix, startDate, endDate);
            IEnumerable<MacdDto> macds;

            using (var context = new EFDbContext())
            {
                macds = context.Database.SqlQuery<MacdDto>(query).ToList();
            }

            return macds.OrderBy(q => q.PriceDate);

        }

        public IEnumerable<AdxDto> GetAdxs(string symbol, DateTime? startDate, DateTime? endDate)
        {

            string query = GetSqlForFxCalculations(symbol, AdxTablePrefix, startDate, endDate);
            IEnumerable<AdxDto> adxs;

            using (var context = new EFDbContext())
            {
                adxs = context.Database.SqlQuery<AdxDto>(query).ToList();
            }

            return adxs.OrderBy(q => q.PriceDate);

        }

        public IEnumerable<CandlestickDto> GetCandlesticks(string symbol, DateTime? startDate, DateTime? endDate)
        {

            string query = GetSqlForFxCalculations(symbol, AdxTablePrefix, startDate, endDate);
            IEnumerable<CandlestickDto> candlesticks;

            using (var context = new EFDbContext())
            {
                candlesticks = context.Database.SqlQuery<CandlestickDto>(query).ToList();
            }

            return candlesticks.OrderBy(q => q.PriceDate);

        }



        private string GetSqlForPlainFxQuotations(string symbol)
        {
            var timeframe = symbol.Substring(symbol.IndexOf('_') + 1);

            var sql = "USE fx; " +
                            "SELECT " +
                                " '{1}' AS Timeframe " +
                                ", q.*" +
                            " FROM" +
                                " quotations_{0} AS q" +
                            " ORDER BY" +
                                " q.PriceDate;";

            return string.Format(sql, symbol, timeframe);

        }


        private string GetSqlForFxCalculations(string symbol, string tableName, DateTime? startDate, DateTime? endDate)
        {

            string timeframe = symbol.Substring(symbol.IndexOf('_') + 1);
            string where = (startDate != null ? "a.PriceDate >= '" + startDate + "'" + (endDate != null ? " AND a.PriceDate <= '" + endDate + "'" : string.Empty) : string.Empty);
            string sql = "USE fx; " +
                            "SELECT " +
                                " '{2}' AS Timeframe " +
                                ", a.*" +
                            " FROM" +
                                " {0}{1} AS a" +
                                (where.Length > 0 ? " WHERE " + where : string.Empty) +
                            " ORDER BY" +
                                " a.PriceDate;";

            return string.Format(sql, tableName, symbol, timeframe);

        }

        private string GetSqlForFxQuotations(string symbol)
        {

            var timeframe = symbol.Substring(symbol.IndexOf('_') + 1);

            var sql = "USE fx; " + 
                            "SELECT " + 
                                " '{1}' AS Timeframe " +
                                ", q.*" + 
                                ", p.*" +
                                ", m.*" + 
                            " FROM" + 
                                " quotations_{0} AS q" +
                                " LEFT JOIN prices_{0} AS p ON q.PriceDate = p.PriceDate" +
                                " LEFT JOIN macd_{0} AS m ON q.PriceDate = m.PriceDate" +
                            " ORDER BY" + 
                                " q.PriceDate;";

            return string.Format(sql, symbol, timeframe);

        }

        private string GetSqlForFxQuotations(string symbol, DateTime startDate, DateTime? endDate)
        {

            var timeframe = symbol.Substring(symbol.IndexOf('_') + 1);
            var sql = "USE fx; " +
                            "SELECT " +
                                " '{1}' AS Timeframe " +
                                ", q.*" +
                                ", p.*" +
                                ", m.*" + 
                            " FROM" +
                                " quotations_{0} AS q " + 
                                " LEFT JOIN prices_{0} AS p ON q.PriceDate = p.PriceDate" +
                                " LEFT JOIN macd_{0} AS m ON q.PriceDate = m.PriceDate" +
                            " WHERE" +
                                " q.PriceDate >= '" + startDate + "' " +
                                (endDate == null ? "" : "AND q.PriceDate <= '" + endDate + "'") + 
                            " ORDER BY" +
                                " q.PriceDate;";

            return string.Format(sql, symbol, timeframe);

        }


        private string GetSqlForFxQuotations(string symbol, string analysisType)
        {
            var timeframe = symbol.Substring(symbol.IndexOf('_') + 1);
            var sql = "USE fx; " +
                            "SELECT " +
                                "'{2}' AS Timeframe " +
                                ", q.*" +
                            " FROM" +
                                " quotations_{0} AS q" +
                            " ORDER BY" +
                                " q.PriceDate;";

            return string.Format(sql, symbol, analysisType, timeframe);

        }

        private string GetSqlForFxQuotations(string symbol, string analysisType, DateTime lastAnalysisItem, int counter)
        {
            var timeframe = symbol.Substring(symbol.IndexOf('_') + 1);
            var sql = "USE fx; " +
                            "(SELECT " +
                                "'{2}' AS Timeframe " +
                                ", q.* " +
                                ", p.* " +
                            " FROM" +
                                " quotations_{0} AS q LEFT JOIN" +
                                " {1}_{0} AS p ON q.PriceDate = p.PriceDate" +
                            " WHERE" +
                                " q.PriceDate <= '" + lastAnalysisItem + "'" +
                            " ORDER BY q.PriceDate DESC" + 
                            " LIMIT " + counter + ") " + 
                            " UNION " + 
                            "(SELECT " +
                                "'{2}' AS Timeframe " +
                                ", q.* " +
                                ", p.* " +
                            " FROM" +
                                " quotations_{0} AS q LEFT JOIN" +
                                " {1}_{0} AS p ON q.PriceDate = p.PriceDate" +
                            " WHERE" +
                                " q.PriceDate >= '" + lastAnalysisItem + "'" +
                            " ORDER BY q.PriceDate)";

            return string.Format(sql, symbol, analysisType, timeframe);

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


        public void UpdateQuotation(QuotationDto quotation, string symbol)
        {
            string tableName = QuotationsTablePrefix + symbol;
            string sql = "UPDATE fx." + tableName +
                " SET " +
                    "  OpenPrice = " + quotation.OpenPrice.ToDbString() +
                    ", HighPrice = " + quotation.HighPrice.ToDbString() +
                    ", LowPrice = " + quotation.LowPrice.ToDbString() +
                    ", ClosePrice = " + quotation.ClosePrice.ToDbString() +
                    ", Volume = " + quotation.Volume.ToDbString() +
                " WHERE QuotationId = " + quotation.Id;

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
                    "TroughByLowEvaluation, PriceGap) " +
                "VALUES (" +
                       price.AssetId +
                    ", '" + price.PriceDate + "'" +
                    ", " + price.DeltaClosePrice.ToDbString() +
                    ", " + price.PriceDirection3D +
                    ", " + price.PriceDirection2D +
                    ", " + price.PeakByCloseEvaluation.ToDbString() +
                    ", " + price.PeakByHighEvaluation.ToDbString() +
                    ", " + price.TroughByCloseEvaluation.ToDbString() +
                    ", " + price.TroughByLowEvaluation.ToDbString() +
                    ", " + price.PriceGap.ToDbString() + ");";


            //Add info about extrema.
            RemoveExtrema(symbol, price.PriceDate);
            AddExtremum(price.PeakByClose);
            AddExtremum(price.PeakByHigh);
            AddExtremum(price.TroughByClose);
            AddExtremum(price.TroughByLow);


            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sqlRemove);
                context.SaveChanges();
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
                    ", DeltaClosePrice = " + price.DeltaClosePrice.ToDbString() +
                    ", PriceDirection2D = " + price.PriceDirection2D +
                    ", PriceDirection3D = " + price.PriceDirection3D +
                    ", PeakByCloseEvaluation = " + price.PeakByCloseEvaluation.ToDbString() +
                    ", PeakByHighEvaluation = " + price.PeakByHighEvaluation.ToDbString() +
                    ", TroughByCloseEvaluation = " + price.TroughByCloseEvaluation.ToDbString() +
                    ", TroughByLowEvaluation = " + price.TroughByLowEvaluation.ToDbString() +
                    ", PriceGap = " + price.PriceGap.ToDbString() + 
                " WHERE PriceId = " + price.Id;


            //Update info about extrema.
            UpdateExtremum(price.PeakByClose);
            UpdateExtremum(price.PeakByHigh);
            UpdateExtremum(price.TroughByClose);
            UpdateExtremum(price.TroughByLow);


            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sql);
                context.SaveChanges();
            }

        }

        private void RemoveExtrema(string symbol, DateTime date)
        {
            string sqlDelete = string.Format("DELETE FROM fx.{0} " +
                                " WHERE " +
                                    " Symbol = '" + symbol + "' AND " +
                                    " PriceDate = '" + date + "';", ExtremaEvaluationTable);

            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sqlDelete);
                context.SaveChanges();
            }

        }

        private void AddExtremum(ExtremumDto extremum)
        {

            if (extremum == null) return;

            string sqlInsert = string.Format(extremum.InsertSql(), ExtremaEvaluationTable);

            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sqlInsert);
                context.SaveChanges();
            }

        }


        public void UpdateExtremum(ExtremumDto extremum)
        {

            if (extremum == null) return;

            string sql = string.Format(extremum.Cancelled ? extremum.RemoveSql() : extremum.UpdateSql(), ExtremaEvaluationTable);

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
                "(AssetId, PriceDate, MA13, EMA13, MA26, EMA26, MACDLine, SignalLine, Histogram, HistogramAvg, " + 
                "HistogramExtremum, DeltaHistogram, DeltaHistogramPositive, DeltaHistogramNegative, DeltaHistogramZero, " + 
                "HistogramDirection2D, HistogramDirection3D, HistogramDirectionChanged, HistogramToOX, " +
                "HistogramRow, OxCrossing, MacdPeak, LastMACDPeak, MACDPeakSlope, MACDTrough, LastMACDTrough, MACDTroughSlope) " + 
                "VALUES (" +
                    macd.AssetId +
                 ", '" + macd.PriceDate + "'" +
                 ", " + macd.Ma13.ToDbString() +
                 ", " + macd.Ema13.ToDbString() +
                 ", " + macd.Ma26.ToDbString() +
                 ", " + macd.Ema26.ToDbString() +
                 ", " + macd.MacdLine.ToDbString() +
                 ", " + macd.SignalLine.ToDbString() +
                 ", " + macd.Histogram.ToDbString() +
                 ", " + macd.HistogramAvg.ToDbString() +
                 ", " + macd.HistogramExtremum.ToDbString() +
                 ", " + macd.DeltaHistogram.ToDbString() +
                 ", " + macd.DeltaHistogramPositive +
                 ", " + macd.DeltaHistogramNegative +
                 ", " + macd.DeltaHistogramZero +
                 ", " + macd.HistogramDirection2D +
                 ", " + macd.HistogramDirection3D +
                 ", " + macd.HistogramDirectionChanged +
                 ", " + macd.HistogramToOx +
                 ", " + macd.HistogramRow +
                 ", " + macd.OxCrossing.ToDbString() +
                 ", " + macd.MacdPeak +
                 ", " + macd.LastMacdPeak.ToDbString() +
                 ", " + macd.MacdPeakSlope.ToDbString() +
                 ", " + macd.MacdTrough +
                 ", " + macd.LastMacdTrough.ToDbString() +
                 ", " + macd.MacdTroughSlope.ToDbString() +");";

            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sqlRemove);
                context.Database.ExecuteSqlCommand(sqlInsert);
                context.SaveChanges();
            }
        }

        public void UpdateMacd(MacdDto macd, string symbol)
        {
            string tableName = MacdTablePrefix + symbol;

            string sqlUpdate = "UPDATE fx." + tableName +
                " SET " +
                    "MA13 = " + macd.Ma13.ToDbString() +
                    ", EMA13 = " + macd.Ema13.ToDbString() +
                    ", MA26 = " + macd.Ma26.ToDbString() +
                    ", EMA26 = " + macd.Ema26.ToDbString() +
                    ", MACDLine = " + macd.MacdLine.ToDbString() +
                    ", SignalLine = " + macd.SignalLine.ToDbString() +
                    ", Histogram = " + macd.Histogram.ToDbString() +
                    ", HistogramAvg = " + macd.HistogramAvg.ToDbString() +
                    ", HistogramExtremum = " + macd.HistogramExtremum.ToDbString() +
                    ", DeltaHistogram = " + macd.DeltaHistogram.ToDbString() +
                    ", DeltaHistogramPositive = " + macd.DeltaHistogramPositive +
                    ", DeltaHistogramNegative = " + macd.DeltaHistogramNegative +
                    ", DeltaHistogramZero = " + macd.DeltaHistogramZero +
                    ", HistogramDirection2D = " + macd.HistogramDirection2D +
                    ", HistogramDirection3D = " + macd.HistogramDirection3D +
                    ", HistogramDirectionChanged = " + macd.HistogramDirectionChanged +
                    ", HistogramToOX = " + macd.HistogramToOx +
                    ", HistogramRow = " + macd.HistogramRow +
                    ", OxCrossing = " + macd.OxCrossing.ToDbString() +
                    ", MacdPeak = " + macd.MacdPeak +
                    ", LastMACDPeak = " + macd.LastMacdPeak.ToDbString() +
                    ", MACDPeakSlope = " + macd.MacdPeakSlope.ToDbString() +
                    ", MACDTrough = " + macd.MacdTrough +
                    ", LastMACDTrough = " + macd.LastMacdTrough.ToDbString() +
                    ", MACDTroughSlope = " + macd.MacdTroughSlope.ToDbString() + 
                 " WHERE PriceDate = '" + macd.PriceDate + "';";

            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sqlUpdate);
                context.SaveChanges();
            }
        }

        public void AddAdx(AdxDto adx, string symbol)
        {

        }

        public void UpdateAdx(AdxDto adx, string symbol)
        {

        }

        public DateTime? GetAnalysisLastCalculation(string symbol, string analysisType)
        {
            string sqlAnalysisItem = "SELECT MAX(PriceDate) FROM " + analysisType + "_" + symbol + ";";

            using (var context = new EFDbContext())
            {

                foreach (var i in context.Database.SqlQuery<String>(sqlAnalysisItem))
                {

                    try
                    {
                        return DateTime.Parse(i);
                    }
                    catch (ArgumentNullException)
                    {
                        return null;
                    }

                }

            }

            return null;

        }

        public DateTime? GetLastQuotationDate(string symbol)
        {
            string sqlQuotation = "SELECT MAX(PriceDate) FROM quotations_" + symbol + " WHERE OpenPrice > -1;";
            using (var context = new EFDbContext())
            {
                foreach (var i in context.Database.SqlQuery<String>(sqlQuotation))
                {
                    return DateTime.Parse(i);
                }
            }

            return null;

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

        


        public void AddAnalysisInfo(AnalysisDto analysis)
        {

            string sqlInsert = "INSERT INTO fx." + AnalysisInfoTable +
                "(AnalysisName, Symbol, FirstAnalysedItemDate, LastAnalysedItemDate, AnalysedUnits, " +
                    "AnalysisStart, AnalysisEnd, AnalysisTotalTime) " +
                "VALUES ('" + analysis.Type + "'" +
                    ", '" + analysis.Symbol + "'" +
                    ", '" + analysis.FirstItemDate + "'" +
                    ", '" + analysis.LastItemDate + "'" +
                    ", " + analysis.AnalyzedItems +
                    ", '" + analysis.AnalysisStart + "'" +
                    ", '" + analysis.AnalysisEnd + "'" +
                    ", " + analysis.AnalysisTotalTime.ToDbString() + ");";

            using (var context = new EFDbContext())
            {
                context.Database.ExecuteSqlCommand(sqlInsert);
                context.SaveChanges();
            }
        }

    }
}
