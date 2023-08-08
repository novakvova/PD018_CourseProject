using MediatR;

namespace WebShop.Application.CQRS.Catalog.Products.Commands.CreateProduct {
    public class CreateProductCommand : IRequest<int> {
        public string? Title { get; set; }
        public IList<Stream> Images { get; set; } = null!;
        public string? Details { get; set; }
        public float? Price { get; set; }
        public int CategoryId { get; set; }
    }
}
