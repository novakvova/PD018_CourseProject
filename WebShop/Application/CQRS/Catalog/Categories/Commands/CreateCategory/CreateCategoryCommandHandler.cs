using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;
using WebShop.Domain.Events;

namespace WebShop.Application.CQRS.Catalog.Categories.Commands.CreateCategory {
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int> {
        private readonly ICatalogDbContext dbContext;

        public CreateCategoryCommandHandler(ICatalogDbContext db) {
            dbContext = db;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) {
            var category = new CategoryEntity {
                Title = request.Title,
                Image = request.Image,
                Details = request.Details,
            };

            dbContext.Categories.Add(category);

            category.AddDomainEvent(new CategoryCreatedEvent(category));

            await dbContext.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
