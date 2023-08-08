using MediatR;

namespace WebShop.Application.CQRS.Catalog.Categories.Commands.CreateCategory {
    public class CreateCategoryCommand : IRequest<int> {
        public int CreatorID { get; set; }
        public string Title { get; set; }
        public Stream ImageContent { get; set; }
        public string? Details { get; set; }
    }
}
