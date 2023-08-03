using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Catalog.Products.Commands.DeleteProduct;

namespace WebShop.Dto.Catalog.Product {
    public class DeleteProductDto : IMapWith<DeleteProductCommand> {
        public int? ProductId { get; set; } = null!;
        public void Mapping(Profile profile) {
            profile.CreateMap<DeleteProductDto, DeleteProductCommand>()
                .ForMember(command => command.ProductId, opt => opt.MapFrom(dto => dto.ProductId));
        }
    }
}
