using Microsoft.Extensions.Logging;
using System.Runtime.Serialization;
using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;
using WebShop.Domain.Events;
using WebShop.Domain.Events.Product;

namespace WebShop.Application.CQRS.Catalog.Products.Commands.DeleteProduct {
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit> {
        private readonly ICatalogDbContext dbContext;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        public DeleteProductCommandHandler(ICatalogDbContext db, IFileService fileService, ILogger<DeleteProductCommandHandler> logger) {
            dbContext = db;
            this.fileService = fileService;
            this.logger = logger;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken) {
            var entity =
                await dbContext.Products.FindAsync(new object[] { request.ProductId }, cancellationToken);

            if ( entity is null )
                throw new NotFoundException(request.ProductId.ToString(), nameof(ProductEntity));

            // load pictures uri from second table
            await dbContext.Entry(entity).Collection(nameof(entity.Images)).LoadAsync();

            if ( entity.Images is not null ) {
                foreach ( var image in entity.Images ) {
                    if ( image is null || image.Uri is null )
                        continue;

                    try {
                        await fileService.DeleteImagesAsync(image.Uri);
                    }
                    catch ( NotFoundException e ) {
                        logger.LogWarning($"{nameof(ProductEntity)} with key {entity.Id} was deleted, but image {image} not found");
                    }
                }
            }

            dbContext.Products.Remove(entity);

            entity.AddDomainEvent(new ProductDeletedEvent(entity));

            await dbContext.SaveChangesAsync(cancellationToken);


            return Unit.Value;
        }
    }
}
