using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.CQRS.Catalog.Products.Commands.CreateProduct;
using WebShop.Dto.Catalog.Product;

namespace WebShop.WebAPI.Controllers {
    public class ProductController : BaseController {
        private readonly IMapper mapper;

        public ProductController(IMapper mapper) {
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateProductDto dto) {
            // map received from request dto to cqrs command
            var command = mapper.Map<CreateProductCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }
    }
}
