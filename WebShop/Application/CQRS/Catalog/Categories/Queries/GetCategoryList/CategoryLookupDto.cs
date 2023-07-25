using WebShop.Application.Common.Mappings;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryList {
    public class CategoryLookupDto : IMapWith<CategoryEntity> {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public void Mapping(Profile profile) {
            profile.CreateMap<CategoryEntity, CategoryLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(category => category.Id))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(category => category.Title));
        }
    }
}
