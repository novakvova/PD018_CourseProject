namespace WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryList {
    public class CategoryListVm {
        public IList<CategoryLookupDto> Categories { get; set; } = null!;
    }
}
