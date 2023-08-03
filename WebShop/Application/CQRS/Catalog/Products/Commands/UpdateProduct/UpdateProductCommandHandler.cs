using Microsoft.Extensions.Logging;
using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;
using WebShop.Domain.Events;
using WebShop.Domain.Events.Category;
using WebShop.Domain.Events.Product;
using static System.Net.Mime.MediaTypeNames;

namespace WebShop.Application.CQRS.Catalog.Products.Commands.UpdateProduct {
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit> {
        private readonly ICatalogDbContext dbContext;
        private readonly IFileService fileService;
        private readonly IDateTime dateTimeService;

        public UpdateProductCommandHandler(ICatalogDbContext db, IFileService fileService, IDateTime dateTimeService) {
            dbContext = db;
            this.fileService = fileService;
            this.dateTimeService = dateTimeService;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
            var product =
                await dbContext.Products.FirstOrDefaultAsync(
                    p =>
                    p.Id == request.ProductId, cancellationToken);

            if ( product is null )
                throw new NotFoundException(nameof(ProductEntity), request.ProductId.ToString());

            int countUpdated = 0;

            if ( request.Details is not null ) {
                countUpdated++;
                product.Details = request.Details;
            }

            if ( request.Title is not null ) {
                countUpdated++;
                product.Title = request.Title;
            }

            if ( request.Price is not null ) {
                countUpdated++;
                product.Price = request.Price;
            }

            if ( request.CategoryId is not null ) {
                var category = await dbContext.Categories
                    .FindAsync(request.CategoryId);

                if ( category is not null ) {
                    countUpdated++;
                    product.Category = category;
                }
            }

            //if ( request.Images.Count > 0 ) {
            //    countUpdated++;

            //    try {
            //        await fileService.DeleteFileAsync(category.Image);
            //    }
            //    catch ( NotFoundException e ) {
            //        logger.LogWarning($"{nameof(CategoryEntity)} with key {category.Id} was updated, but image {category.Image} not found");
            //    }

            //    category.Image = await fileService.UploadFileAsync(
            //        nameof(CategoryEntity),
            //        request.ImageContent);
            //}

            product.AddDomainEvent(new ProductUpdatedEvent(product));

            if ( countUpdated > 0 ) {
                product.LastModified = dateTimeService.Now;
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
