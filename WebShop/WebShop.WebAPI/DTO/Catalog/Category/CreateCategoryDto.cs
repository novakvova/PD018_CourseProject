using AutoMapper;
using AutoMapper.Configuration.Annotations;
using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Catalog.Categories.Commands.CreateCategory;
using WebShop.WebAPI.Common;

namespace WebShop.Dto.Catalog.Category {
    public class CreateCategoryDto : IMapWith<CreateCategoryCommand> {
        public string Title { get; set; } = null!;
        public string Details { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;

        public void Mapping(Profile profile) {
            profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>()
                .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(command => command.ImageContent, opt => opt.MapFrom(dto => dto.Image.OpenReadStream()))
                .ForMember(command => command.Details, opt => opt.MapFrom(dto => dto.Details));
        }
    }
}
