using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Catalog.Products.Commands.CreateProduct;
using WebShop.Application.CQRS.Catalog.Products.Commands.UpdateProduct;
using WebShop.Dto.Catalog.Product;

namespace WebShop.WebAPI.DTO.Catalog.Product {
    public class UpdateProductDto : IMapWith<UpdateProductCommand> {
        public int? ProductId { get; set; }
        public string? Title { get; set; } = null!;
        public string? Details { get; set; } = null!;
        public float? Price { get; set; }
        public int? CategoryId { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<UpdateProductDto, UpdateProductCommand>()
                .ForMember(command => command.ProductId, opt => opt.MapFrom(dto => dto.ProductId))
                .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(command => command.CategoryId, opt => opt.MapFrom(dto => dto.CategoryId))
                .ForMember(command => command.Price, opt => opt.MapFrom(dto => dto.Price))
                .ForMember(command => command.Details, opt => opt.MapFrom(dto => dto.Details));
        }
    }
}