using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;
using WebShop.Domain.Events;
using WebShop.Domain.Events.Product;
using static System.Net.Mime.MediaTypeNames;

namespace WebShop.Application.CQRS.Catalog.Products.Commands.CreateProduct {
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int> {
        private readonly ICatalogDbContext dbContext;
        private readonly IFileService fileService;

        public CreateProductCommandHandler(ICatalogDbContext db, IFileService fileService) {
            dbContext = db;
            this.fileService = fileService;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
            // find category by id from request
            var category = await dbContext.Categories.FindAsync(request.CategoryId);

            // create product entity
            var product = new ProductEntity {
                Title = request.Title,
                Details = request.Details,
                Price = request.Price,
                Category = category
            };

            dbContext.Products.Add(product);

            // notify about creation subscribers
            product.AddDomainEvent(new ProductCreatedEvent(product));

            // upload each image from request
            List<ProductImageEntity> uploadedFiles = new List<ProductImageEntity>();

            int priority = 0;
            foreach ( var formImageStream in request.Images ) {
                var imageUri = await fileService.UploadFileAsync(
                    nameof(ProductEntity),
                    formImageStream);

                var imageEntity = new ProductImageEntity() {
                    Priority = priority++,
                    Uri = imageUri,
                    Product = product
                };

                uploadedFiles.Add(imageEntity);
            }

            // set product images to uploaded images
            product.Images = uploadedFiles;

            // persist all changes to db
            await dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
