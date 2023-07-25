using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;
using WebShop.Domain.Events;

namespace WebShop.Application.CQRS.Catalog.Categories.Commands.DeleteCategory {
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit> {
        private readonly ICatalogDbContext dbContext;

        public DeleteCategoryCommandHandler(ICatalogDbContext db) {
            dbContext = db;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) {
            var category =
                await dbContext.Categories.FindAsync(new object[] { request.CategoryId }, cancellationToken);

            if ( category is null )
                throw new NotFoundException(nameof(CategoryEntity), request.CategoryId.ToString());

            dbContext.Categories.Remove(category);

            category.AddDomainEvent(new CategoryDeletedEvent(category));

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
