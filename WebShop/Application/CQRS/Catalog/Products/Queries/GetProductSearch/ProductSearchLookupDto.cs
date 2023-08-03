using WebShop.Application.Common.Mappings;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Catalog.Products.Queries.GetProductSearch {
    public class ProductSearchLookupDto : IMapWith<ProductEntity> {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public IList<string>? Images { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<ProductEntity, ProductSearchLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(product => product.Id))
                .ForMember(dto => dto.Images, opt => opt.MapFrom(product => product.Images.Select(i => i.Uri)))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(product => product.Title));
        }
    }
}
