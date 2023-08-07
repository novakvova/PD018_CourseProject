using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategorySearch;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Catalog.Products.Queries.GetProductSearch {
    public class ProductSearchLookupDto : IMapWith<ProductEntity> {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Details { get; set; } = null!;
        public float Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CategorySearchLookupDto Category { get; set; } = null!;
        public IList<string>? Images { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<ProductEntity, ProductSearchLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(product => product.Id))
                .ForMember(dto => dto.Images, opt => opt.MapFrom(product => product.Images.Select(i => i.Uri)))
                .ForMember(dto => dto.Category, opt => opt.MapFrom(product => product.Category))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(product => product.Price))
                .ForMember(dto => dto.CreatedAt, opt => opt.MapFrom(product => product.Created))
                .ForMember(dto => dto.UpdatedAt, opt => opt.MapFrom(product => product.LastModified))
                .ForMember(dto => dto.Details, opt => opt.MapFrom(product => product.Details))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(product => product.Title));
        }
    }
}
