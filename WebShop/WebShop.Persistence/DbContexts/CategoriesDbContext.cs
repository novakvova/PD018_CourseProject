using Microsoft.EntityFrameworkCore;
using WebShop.Application.Interfaces;
using WebShop.Domain.Entities.Catalog;
using WebShop.Domain.Configuration.Catalog;

namespace MyShop.Persistence.DbContexts {
    public class CategoriesDbContext : DbContext, ICategoriesDbContext {
        public DbSet<CategoryEntity> Categories { get; set; }

        public CategoriesDbContext(DbContextOptions<CategoriesDbContext> options) : base(options) {
        }
        protected override void OnModelCreating(ModelBuilder builder) {
            builder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(builder);
        }
        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
