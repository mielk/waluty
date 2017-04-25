using System.Data.Entity;
using Stock.DAL.TransferObjects;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Stock.DAL.Infrastructure
{
    public class EFDbContext : DbContext
    {

        private static EFDbContext _instance;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
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