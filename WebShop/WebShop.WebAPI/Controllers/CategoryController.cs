using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.MediatR.Catalog.Categories.Commands.CreateCategory;
using WebShop.Application.MediatR.Catalog.Categories.Commands.DeleteCategory;
using WebShop.Application.MediatR.Catalog.Categories.Commands.UpdateCategory;
using WebShop.Application.MediatR.Catalog.Categories.Queries.GetCategoryDetails;
using WebShop.Application.Dto;

namespace WebShop.WebAPI.Controllers {
    public class CategoryController : BaseController {
        private readonly IMapper mapper;

        public CategoryController(IMapper mapper) {
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CategoryListVm>> GetAll() {
            // forming query from http request
            var query = new GetCategoryListQuery {
                // in future here can be pagination or filters
            };

            // get result from Mediator request handler
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryListVm>> Get(Guid id) {
            // forming query from http request
            var query = new GetCategoryDetailsQuery {
                CategoryId = id
            };

            // get result from Mediator request handler
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCategoryDto dto) {
            // map received from request dto to cqrs command
            var command = mapper.Map<CreateCategoryCommand>(dto);
            command.CreatorID = UserId;
            var categoryId = await Mediator.Send(command);

            return Ok(categoryId);
        }

        [HttpPatch]
        public async Task<ActionResult> Update([FromBody] UpdateCategoryDto dto) {
            var command = mapper.Map<UpdateCategoryCommand>(dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] DeleteCategoryDto dto) {
            var command = mapper.Map<DeleteCategoryCommand>(dto);
            await Mediator.Send(command);
            return Ok();
        }
    }
}
