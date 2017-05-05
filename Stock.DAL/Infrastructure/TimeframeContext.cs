using System.Data.Entity;
using Stock.DAL.TransferObjects;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Stock.DAL.Infrastructure
{
    public class TimeframeContext : DbContext
    {
        private static TimeframeContext _instance;
        public DbSet<TimeframeDto> Timeframes { get; set; } 

        public TimeframeContext()
        {
            Database.Initialize(false);
        }

        public static TimeframeContext GetInstance()
        {
            return _instance ?? (_instance = new TimeframeContext());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeframeDto>().ToTable("timeframes");
        }

    }
}
