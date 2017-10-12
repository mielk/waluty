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
        public DbSet<TrendHitDto> TrendHits { get; set; }
        public DbSet<TrendBreakDto> TrendBreaks { get; set; }
        public DbSet<TrendRangeDto> TrendRanges { get; set; }

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
            modelBuilder.Entity<TrendHitDto>().ToTable("trend_hits");
            modelBuilder.Entity<TrendBreakDto>().ToTable("trend_breaks");
            modelBuilder.Entity<TrendRangeDto>().ToTable("trend_ranges");
        }

    }
}

