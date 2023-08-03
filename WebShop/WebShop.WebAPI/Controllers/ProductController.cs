using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.CQRS.Catalog.Categories.Commands.DeleteCategory;
using WebShop.Application.CQRS.Catalog.Products.Commands.CreateProduct;
using WebShop.Application.CQRS.Catalog.Products.Commands.DeleteProduct;
using WebShop.Application.CQRS.Catalog.Products.Queries.GetProductDetails;
using WebShop.Application.CQRS.Catalog.Products.Queries.GetProductSearch;
using WebShop.Dto.Catalog.Category;
using WebShop.Dto.Catalog.Product;
using WebShop.WebAPI.DTO.Catalog.Product;

namespace WebShop.WebAPI.Controllers {
    public class ProductController : BaseController {
        private readonly IMapper mapper;

        public ProductController(IMapper mapper) {
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ProductSearchVm>> Search([FromQuery] GetProductSearchDto search) {
            var query = mapper.Map<GetProductSearchQuery>(search);

            // get result from Mediator request handler
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsVm>> Get(int id) {
            // forming query from http request
            var query = new GetProductDetailsQuery {
                ProductId = id
            };

            // get result from Mediator request handler
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateProductDto dto) {
            // map received from request dto to cqrs command
            var command = mapper.Map<CreateProductCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] DeleteProductDto dto) {
            var command = mapper.Map<DeleteProductCommand>(dto);
            await Mediator.Send(command);
            return Ok();
        }
    }
}
