using System.Data.Entity;

namespace Stock.DAL.Infrastructure
{
    public class UnitTestsDbContext : DbContext
    {
        private static UnitTestsDbContext _instance;

        public UnitTestsDbContext()
        {
            Database.Initialize(false);
        }

        public static UnitTestsDbContext GetInstance()
        {
            return _instance ?? (_instance = new UnitTestsDbContext());
        }

    }
}
