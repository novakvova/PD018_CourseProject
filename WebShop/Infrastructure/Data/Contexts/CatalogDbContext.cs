using System.Reflection;
using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebShop.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebShop.Persistance.Data.Contexts;

public class CatalogDbContext : DbContext, ICatalogDbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ProductEntity> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new ProductImagesConfiguration());
        base.OnModelCreating(builder);
    }
    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
    public new EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class {
        return base.Entry(entity);
    }
}
