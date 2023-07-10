using MediatR;

namespace WebShop.Application.MediatR.Catalog.Categories.Commands.CreateCategory {
    public class CreateCategoryCommand : IRequest<Guid> {
        public Guid CreatorID { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Details { get; set; }
    }
}
