using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Catalog.Categories.Commands.CreateCategory;
using WebShop.Application.CQRS.Catalog.Categories.Commands.UpdateCategory;

namespace WebShop.Dto.Catalog.Category {
    public class UpdateCategoryDto : IMapWith<UpdateCategoryCommand> {
        public int? CategoryId { get; set; } = null!;
        public string? Title { get; set; }
        public IFormFile? Image { get; set; }
        public string? Details { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<UpdateCategoryDto, UpdateCategoryCommand>()
                .ForMember(command => command.CategoryId, opt => opt.MapFrom(dto => dto.CategoryId))
                .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(command => command.ImageContent, opt => opt.MapFrom(dto => dto.Image.OpenReadStream()))
                .ForMember(command => command.Details, opt => opt.MapFrom(dto => dto.Details));
        }
    }
}
