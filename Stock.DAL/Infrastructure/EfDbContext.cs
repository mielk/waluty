using System.Data.Entity;
using Stock.DAL.TransferObjects;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Stock.DAL.Infrastructure
{
    public class EFDbContext : DbContext
    {

        private static EFDbContext _instance;

        public DbSet<AssetDto> Companies { get; set; }
        
        public DbSet<MarketDto> Markets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetDto>().ToTable("companies");
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