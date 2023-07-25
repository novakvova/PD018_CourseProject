using MediatR;

namespace WebShop.Application.CQRS.Catalog.Categories.Commands.DeleteCategory {
    public class DeleteCategoryCommand : IRequest<Unit> {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
