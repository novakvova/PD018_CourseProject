using WebShop.Application.Common.Mappings;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategorySearch {
    public class CategorySearchLookupDto : IMapWith<CategoryEntity> {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Image { get; set; } = null!;

        public void Mapping(Profile profile) {
            profile.CreateMap<CategoryEntity, CategorySearchLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(category => category.Id))
                .ForMember(dto => dto.Image, opt => opt.MapFrom(category => category.Image ?? $"{nameof(CategoryEntity)}/noimage.png"))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(category => category.Title));
        }
    }
}
