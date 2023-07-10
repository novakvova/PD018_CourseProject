using MediatR;

namespace WebShop.Application.MediatR.Catalog.Categories.Commands.DeleteCategory {
    public class DeleteCategoryCommand : IRequest<Unit> {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
