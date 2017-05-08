using System.Data.Entity;
using Stock.DAL.TransferObjects;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Stock.DAL.Infrastructure
{
    public class DataContext : DbContext
    {
        private static DataContext _instance;
        public DbSet<QuotationDto> Quotations { get; set; }

        public DataContext()
        {
            Database.Initialize(false);
        }

        public static DataContext GetInstance()
        {
            return _instance ?? (_instance = new DataContext());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuotationDto>().ToTable("quotations");
        }

    }
}
