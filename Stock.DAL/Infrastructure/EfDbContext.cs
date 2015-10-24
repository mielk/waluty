using System.Data.Entity;
using Stock.DAL.TransferObjects;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Stock.DAL.Infrastructure
{
    public class EFDbContext : DbContext
    {

        private static EFDbContext _instance;

        public DbSet<CompanyDto> Companies { get; set; }
        public DbSet<PairDto> Pairs { get; set; }
        public DbSet<CurrencyDto> Currencies { get; set; }
        public DbSet<MarketDto> Markets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyDto>().ToTable("companies");
            modelBuilder.Entity<PairDto>().ToTable("pairs");
            modelBuilder.Entity<CurrencyDto>().ToTable("currencies");
            modelBuilder.Entity<MarketDto>().ToTable("markets");
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