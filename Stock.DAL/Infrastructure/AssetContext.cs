using System.Data.Entity;
using Stock.DAL.TransferObjects;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Stock.DAL.Infrastructure
{
    public class AssetContext : DbContext
    {
        private static AssetContext _instance;
        public DbSet<AssetDto> Assets { get; set; }

        public AssetContext()
        {
            Database.Initialize(false);
        }

        public static AssetContext GetInstance()
        {
            return _instance ?? (_instance = new AssetContext());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetDto>().ToTable("assets");
        }

    }
}
