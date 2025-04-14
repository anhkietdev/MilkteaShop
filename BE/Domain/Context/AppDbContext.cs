using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class AppDbContext : DbContext
    {

        public 
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
