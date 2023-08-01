using MediatR;

namespace WebShop.Application.CQRS.Catalog.Categories.Commands.UpdateCategory {
    public class UpdateCategoryCommand : IRequest<Unit> {
        public int CategoryId { get; set; }
        public string? Title { get; set; }
        public Stream? ImageContent { get; set; }
        public string? Details { get; set; }
    }
}
