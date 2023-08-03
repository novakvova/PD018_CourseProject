using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebShop.Domain.Entities;

namespace WebShop.Application.Common.Interfaces;

public interface ICatalogDbContext {
    DbSet<CategoryEntity> Categories { get; }
    DbSet<ProductEntity> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}
