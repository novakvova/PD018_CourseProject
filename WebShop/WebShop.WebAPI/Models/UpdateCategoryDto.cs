using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Application.MediatR.Catalog.Categories.Commands.UpdateCategory;

namespace WebShop.WebAPI.Models {
    public class UpdateCategoryDto : IMapWith<UpdateCategoryCommand> {
        public Guid? CategoryId { get; set; } = null!;
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Details { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<UpdateCategoryDto, UpdateCategoryCommand>()
                .ForMember(command => command.CategoryId, opt => opt.MapFrom(dto => dto.CategoryId))
                .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(command => command.Image, opt => opt.MapFrom(dto => dto.Image))
                .ForMember(command => command.Details, opt => opt.MapFrom(dto => dto.Details));
        }
    }
}
