using AutoMapper;
using AutoMapper.Configuration.Annotations;
using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Catalog.Categories.Commands.CreateCategory;
using WebShop.Application.CQRS.Catalog.Products.Commands.CreateProduct;
using WebShop.WebAPI.Common;

namespace WebShop.Dto.Catalog.Product {
    public class CreateProductDto : IMapWith<CreateProductCommand> {
        public string Title { get; set; } = null!;
        public string Details { get; set; } = null!;
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public IList<IFormFile> Images { get; set; } = null!;

        public void Mapping(Profile profile) {
            profile.CreateMap<CreateProductDto, CreateProductCommand>()
                .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(command => command.Price, opt => opt.MapFrom(dto => dto.Price))
                .ForMember(command => command.CategoryId, opt => opt.MapFrom(dto => dto.CategoryId))
                .ForMember(command => command.Images,
                opt => opt.MapFrom(
                    dto => dto.Images
                    .Select(formFile => formFile.OpenReadStream())
                    .ToList()))
                .ForMember(command => command.Details, opt => opt.MapFrom(dto => dto.Details));
        }
    }
}
