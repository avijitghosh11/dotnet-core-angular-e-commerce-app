using Ekart.Core.Entites;
using Ekart.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Ekart.Infrastructure.Data
{
    // Using Primary Constructure new C# feature
    public class StoreContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);
        }
    }
}
