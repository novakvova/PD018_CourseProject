using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Catalog.Products.Queries.GetProductSearch;
using WebShop.Dto.Catalog.Category;

namespace WebShop.WebAPI.DTO.Catalog.Product {
    public class GetProductSearchDto : IMapWith<GetProductSearchQuery> {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 2;

        public void Mapping(Profile profile) {
            profile.CreateMap<GetProductSearchDto, GetProductSearchQuery>()
                .ForMember(q => q.Page, opt => opt.MapFrom(dto => dto.Page))
                .ForMember(q => q.PageSize, opt => opt.MapFrom(dto => dto.PageSize));
        }
    }
}
