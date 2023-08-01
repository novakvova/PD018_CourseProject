using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Catalog.Categories.Commands.UpdateCategory;
using WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategorySearch;
using WebShop.Dto.Catalog.Category;

namespace WebShop.WebAPI.DTO.Catalog.Category {
    public class GetCategorySearchDto : IMapWith<GetCategorySearchQuery> {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 2;

        public void Mapping(Profile profile) {
            profile.CreateMap<GetCategorySearchDto, GetCategorySearchQuery>()
                .ForMember(q => q.Page, opt => opt.MapFrom(dto => dto.Page))
                .ForMember(q => q.PageSize, opt => opt.MapFrom(dto => dto.PageSize));
        }
    }
}
