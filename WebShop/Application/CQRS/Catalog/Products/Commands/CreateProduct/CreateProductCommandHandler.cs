using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;
using WebShop.Domain.Events;
using WebShop.Domain.Events.Product;

namespace WebShop.Application.CQRS.Catalog.Products.Commands.CreateProduct {
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int> {
        private readonly ICatalogDbContext dbContext;
        private readonly IFileService fileService;

        public CreateProductCommandHandler(ICatalogDbContext db, IFileService fileService) {
            dbContext = db;
            this.fileService = fileService;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
            List<string> uploadedFiles = new List<string>();

            //foreach ( var formImageStream in request.Images ) {
            //    var image = await fileService.UploadFileAsync(
            //        nameof(ProductEntity),
            //        formImageStream);

            //    uploadedFiles.Add(image);
            //}

            var category = await dbContext.Categories.FindAsync(request.CategoryId);

            var product = new ProductEntity {
                Title = request.Title,
                Details = request.Details,
                Price = request.Price,
                Category = category
            };

            dbContext.Products.Add(product);

            product.AddDomainEvent(new ProductCreatedEvent(product));

            await dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
