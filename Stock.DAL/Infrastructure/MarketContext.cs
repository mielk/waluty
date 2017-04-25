using System.Data.Entity;
using Stock.DAL.TransferObjects;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Stock.DAL.Infrastructure
{
    public class MarketContext : DbContext
    {
        private static MarketContext _instance;
        public DbSet<MarketDto> Markets { get; set; }

        public MarketContext()
        {
            Database.Initialize(false);
        }

        public static MarketContext GetInstance()
        {
            return _instance ?? (_instance = new MarketContext());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarketDto>().ToTable("markets");
        }

    }
}
