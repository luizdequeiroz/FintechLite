using Host.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Host.Api.Data
{
    public class FintechLiteDbContext : DbContext
    {
        public FintechLiteDbContext(DbContextOptions<FintechLiteDbContext> options)
            : base(options)
        {

        }

        public DbSet<Account> Accounts => Set<Account>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
