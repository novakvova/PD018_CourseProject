using WebShop.Domain.Entities;

namespace WebShop.Application.Common.Interfaces;

public interface ICatalogDbContext {
    DbSet<CategoryEntity> Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
