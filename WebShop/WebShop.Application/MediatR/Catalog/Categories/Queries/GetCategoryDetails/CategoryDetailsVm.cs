using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Domain.Entities.Catalog;

namespace WebShop.Application.MediatR.Catalog.Categories.Queries.GetCategoryDetails {
    public class CategoryDetailsVm : IMapWith<CategoryEntity> {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Details { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<CategoryEntity, CategoryDetailsVm>()
                .ForMember(vm => vm.Title, opt => opt.MapFrom(category => category.Title))
                .ForMember(vm => vm.Image, opt => opt.MapFrom(category => category.Image))
                .ForMember(vm => vm.Details, opt => opt.MapFrom(category => category.Details))
                .ForMember(vm => vm.DateCreated, opt => opt.MapFrom(category => category.DateCreated))
                .ForMember(vm => vm.DateUpdated, opt => opt.MapFrom(category => category.DateUpdated))
                .ForMember(vm => vm.Id, opt => opt.MapFrom(category => category.Id));
        }
    }
}
