using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryList;
using WebShop.Application.CQRS.Files.Images.Queries.GetImage;

namespace WebShop.WebAPI.Controllers {
    public class FilesController : BaseController {
        private readonly IMapper mapper;

        public FilesController(IMapper mapper) {
            this.mapper = mapper;
        }

        [HttpGet("{context}/{filename}")]
        public async Task<ActionResult> Get(string context, string filename) {
            // forming query from http request
            var query = new GetImageQuery {
                Context = context,
                FileName = filename
            };

            // get result from Mediator request handler
            var result = await Mediator.Send(query);

            var provider = new FileExtensionContentTypeProvider();
            if ( !provider.TryGetContentType(result.Extention, out var contentType) )
                contentType = "application/octet-stream"; // Default content type if not found

            return File(result.Stream, contentType);
        }
    }
}
