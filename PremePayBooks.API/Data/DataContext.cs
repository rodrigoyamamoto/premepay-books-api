using Microsoft.EntityFrameworkCore;
using PremePayBooks.API.Models;

namespace PremePayBooks.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // bug due breaking changes in the ef core 3: DetectChanges should honor store-generated key values when tracking new entities
            // https://github.com/dotnet/efcore/issues/14616
            // https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/breaking-changes#dc
            modelBuilder.Entity<Book>().Property(e => e.Id).ValueGeneratedNever();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}