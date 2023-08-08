using MediatR;

namespace WebShop.Application.CQRS.Catalog.Products.Commands.DeleteProduct {
    public class DeleteProductCommand : IRequest<Unit> {
        public int ProductId { get; set; }
    }
}
