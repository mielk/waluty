using System.Data.Entity;
using Stock.DAL.TransferObjects;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Stock.DAL.Infrastructure
{
    public class AnalysisContext : DbContext
    {
        private static AnalysisContext _instance;
        public DbSet<AnalysisTypeDto> AnalysisTypes { get; set; }

        public AnalysisContext()
        {
            Database.Initialize(false);
        }

        public static AnalysisContext GetInstance()
        {
            return _instance ?? (_instance = new AnalysisContext());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnalysisTypeDto>().ToTable("analysis_types");
        }

    }
}
