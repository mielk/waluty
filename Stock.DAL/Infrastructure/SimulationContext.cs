using System.Data.Entity;
using Stock.DAL.TransferObjects;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Stock.DAL.Infrastructure
{
    public class SimulationContext : DbContext
    {
        private static SimulationContext _instance;
        public DbSet<AnalysisTimestampDto> AnalysisTimestamps { get; set; }
        public DbSet<SimulationDto> Simulations { get; set; }

        public SimulationContext()
        {
            Database.Initialize(false);
        }

        public static SimulationContext GetInstance()
        {
            return _instance ?? (_instance = new SimulationContext());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnalysisTimestampDto>().ToTable("last_updates");
            modelBuilder.Entity<SimulationDto>().ToTable("simulations");
        }

    }
}
