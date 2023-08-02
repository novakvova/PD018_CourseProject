using WebShop.Domain.Entities;

namespace WebShop.Application.Common.Interfaces;

public interface ICatalogDbContext {
    DbSet<CategoryEntity> Categories { get; }
    DbSet<ProductEntity> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
