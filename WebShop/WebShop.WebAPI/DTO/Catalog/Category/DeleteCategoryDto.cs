using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Catalog.Categories.Commands.DeleteCategory;

namespace WebShop.Dto.Catalog.Category {
    public class DeleteCategoryDto : IMapWith<DeleteCategoryCommand> {
        public int? CategoryId { get; set; } = null!;
        public void Mapping(Profile profile) {
            profile.CreateMap<DeleteCategoryDto, DeleteCategoryCommand>()
                .ForMember(command => command.CategoryId, opt => opt.MapFrom(dto => dto.CategoryId));
        }
    }
}
