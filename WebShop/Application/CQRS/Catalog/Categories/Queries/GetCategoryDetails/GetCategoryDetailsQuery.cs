namespace WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryDetails {
    public class GetCategoryDetailsQuery : IRequest<CategoryDetailsVm> {
        public int CategoryId { get; set; }
    }
}
