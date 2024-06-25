using Microsoft.EntityFrameworkCore;
using TechAssess.SupplierService.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TechAssess.SupplierService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                            .Entries()
                            .Where(e => e.Entity is Supplier &&
                                       (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Supplier)entityEntry.Entity).LastEdited = DateTime.Now;
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
