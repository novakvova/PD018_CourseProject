using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;
using WebShop.Domain.Events;
using WebShop.Domain.Events.Category;

namespace WebShop.Application.CQRS.Catalog.Categories.Commands.CreateCategory {
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int> {
        private readonly ICatalogDbContext dbContext;
        private readonly IFileService fileService;

        public CreateCategoryCommandHandler(ICatalogDbContext db, IFileService fileService) {
            dbContext = db;
            this.fileService = fileService;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) {
            var image = await fileService.UploadImageAsync(
                nameof(CategoryEntity),
                request.ImageContent);

            var category = new CategoryEntity {
                Title = request.Title,
                Image = image,
                Details = request.Details,
            };

            dbContext.Categories.Add(category);

            category.AddDomainEvent(new CategoryCreatedEvent(category));

            await dbContext.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
