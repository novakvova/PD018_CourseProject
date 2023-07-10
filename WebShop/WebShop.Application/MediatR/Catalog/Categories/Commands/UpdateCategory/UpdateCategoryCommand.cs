using MediatR;

namespace WebShop.Application.MediatR.Catalog.Categories.Commands.UpdateCategory {
    public class UpdateCategoryCommand : IRequest<Unit> {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Details { get; set; }
    }
}
