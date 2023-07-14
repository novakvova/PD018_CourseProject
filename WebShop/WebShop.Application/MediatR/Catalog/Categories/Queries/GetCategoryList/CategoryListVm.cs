using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Domain.Entities.Catalog;

namespace WebShop.Application.MediatR.Catalog.Categories.Queries.GetCategoryDetails {
    public class CategoryListVm {
        public IList<CategoryLookupDto> Categories { get; set; } = null!;
    }
}
