using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Application.MediatR.Catalog.Categories.Commands.DeleteCategory;
using WebShop.Application.MediatR.Catalog.Categories.Commands.UpdateCategory;

namespace WebShop.Dto.Catalog.Category {
    public class DeleteCategoryDto : IMapWith<DeleteCategoryCommand> {
        public Guid? CategoryId { get; set; } = null!;
        public void Mapping(Profile profile) {
            profile.CreateMap<DeleteCategoryDto, DeleteCategoryCommand>()
                .ForMember(command => command.CategoryId, opt => opt.MapFrom(dto => dto.CategoryId));
        }
    }
}
