using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Application.MediatR.Catalog.Categories.Commands.CreateCategory;

namespace WebShop.Dto.Catalog.Category {
    public class CreateCategoryDto : IMapWith<CreateCategoryCommand> {
        public string Title { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Details { get; set; } = null!;

        public void Mapping(Profile profile) {
            profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>()
                .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(command => command.Image, opt => opt.MapFrom(dto => dto.Image))
                .ForMember(command => command.Details, opt => opt.MapFrom(dto => dto.Details));
        }
    }
}
