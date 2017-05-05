using System.Data.Entity;

namespace Stock.DAL.Infrastructure
{
    public class OriginalDbContext : DbContext
    {
        private static OriginalDbContext _instance;

        public OriginalDbContext()
        {
            Database.Initialize(false);
        }

        public static OriginalDbContext GetInstance()
        {
            return _instance ?? (_instance = new OriginalDbContext());
        }

    }
}
