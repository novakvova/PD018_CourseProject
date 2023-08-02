using Microsoft.Extensions.Logging;
using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;
using WebShop.Domain.Events;
using WebShop.Domain.Events.Category;

namespace WebShop.Application.CQRS.Catalog.Categories.Commands.DeleteCategory {
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit> {
        private readonly ICatalogDbContext dbContext;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        public DeleteCategoryCommandHandler(ICatalogDbContext db, IFileService fileService, ILogger<DeleteCategoryCommandHandler> logger) {
            dbContext = db;
            this.fileService = fileService;
            this.logger = logger;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) {
            var category =
                await dbContext.Categories.FindAsync(new object[] { request.CategoryId }, cancellationToken);

            if ( category is null )
                throw new NotFoundException(request.CategoryId.ToString(), nameof(CategoryEntity));

            dbContext.Categories.Remove(category);

            category.AddDomainEvent(new CategoryDeletedEvent(category));

            await dbContext.SaveChangesAsync(cancellationToken);

            try {
                await fileService.DeleteFileAsync(category.Image);
            }
            catch ( NotFoundException e ) {
                logger.LogWarning($"{nameof(CategoryEntity)} with key {category.Id} was deleted, but image {category.Image} not found");
            }

            return Unit.Value;
        }
    }
}
