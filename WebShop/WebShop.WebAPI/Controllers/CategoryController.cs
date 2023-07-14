using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.MediatR.Catalog.Categories.Queries.GetCategoryDetails;

namespace WebShop.WebAPI.Controllers {
    public class CategoryController : BaseController {
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
    }
}
