using WebShop.Application.Common.Mappings;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryDetails {
    public class CategoryDetailsVm : IMapWith<CategoryEntity> {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Details { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<CategoryEntity, CategoryDetailsVm>()
                .ForMember(vm => vm.Title, opt => opt.MapFrom(category => category.Title))
                .ForMember(vm => vm.Image, opt => opt.MapFrom(category => category.Image))
                .ForMember(vm => vm.Details, opt => opt.MapFrom(category => category.Details))
                .ForMember(vm => vm.Created, opt => opt.MapFrom(category => category.Created))
                .ForMember(vm => vm.LastModified, opt => opt.MapFrom(category => category.LastModified))
                .ForMember(vm => vm.Id, opt => opt.MapFrom(category => category.Id));
        }
    }
}
