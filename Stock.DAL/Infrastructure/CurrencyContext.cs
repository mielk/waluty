using System.Data.Entity;
using Stock.DAL.TransferObjects;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Stock.DAL.Infrastructure
{
    public class CurrencyContext : DbContext
    {
        private static CurrencyContext _instance;
        public DbSet<CurrencyDto> Currencies { get; set; }
        public DbSet<FxPairDto> Pairs { get; set; }

        public CurrencyContext()
        {
            Database.Initialize(false);
        }

        public static CurrencyContext GetInstance()
        {
            return _instance ?? (_instance = new CurrencyContext());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyDto>().ToTable("currencies");
            modelBuilder.Entity<FxPairDto>().ToTable("pairs");
        }

    }
}
