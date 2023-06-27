using Microsoft.EntityFrameworkCore;
using WebShop.Domain.Configuration.Catalog;
using WebShop.Domain.Entities.Catalog;

namespace WebShop.EFData {
    public class EFDataContext : DbContext {
        public DbSet<CategoryEntity> Categories { get; set; }
        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);


            builder.ApplyConfiguration(new CategoryConfiguration());

        }
    }
}