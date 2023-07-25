using System.Reflection;
using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebShop.Infrastructure.Data.Configurations;

namespace WebShop.Infrastructure.Data;

public class CatalogDbContext : DbContext, ICatalogDbContext {
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<CategoryEntity> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.ApplyConfiguration(new CategoryConfiguration());
        base.OnModelCreating(builder);
    }
    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
