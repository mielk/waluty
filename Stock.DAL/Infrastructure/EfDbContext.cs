using System.Data.Entity;
using Stock.DAL.TransferObjects;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Stock.DAL.Infrastructure
{
    public class EFDbContext : DbContext
    {

        private static EFDbContext _instance;
        public DbSet<CurrencyDto> Currencies { get; set; }
        public DbSet<FxPairDto> Pairs { get; set; }
        public DbSet<AssetDto> Assets { get; set; }
        public DbSet<MarketDto> Markets { get; set; }
        public DbSet<TimeframeDto> Timeframes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarketDto>().ToTable("markets");
            modelBuilder.Entity<CurrencyDto>().ToTable("currencies");
            modelBuilder.Entity<FxPairDto>().ToTable("pairs");
            modelBuilder.Entity<AssetDto>().ToTable("assets");
            modelBuilder.Entity<TimeframeDto>().ToTable("timeframes");
        }

        public EFDbContext()
        {
            Database.Initialize(false);
        }

        public static EFDbContext GetInstance()
        {
            return _instance ?? (_instance = new EFDbContext());
        }

    }
}