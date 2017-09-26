using System.Data.Entity;
using Stock.DAL.TransferObjects;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Stock.DAL.Infrastructure
{
    public class TrendlineContext : DbContext
    {
        private static TrendlineContext _instance;
        public DbSet<TrendlineDto> Trendlines { get; set; }

        public TrendlineContext()
        {
            Database.Initialize(false);
        }

        public static TrendlineContext GetInstance()
        {
            return _instance ?? (_instance = new TrendlineContext());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrendlineDto>().ToTable("trendlines");
        }

    }
}
