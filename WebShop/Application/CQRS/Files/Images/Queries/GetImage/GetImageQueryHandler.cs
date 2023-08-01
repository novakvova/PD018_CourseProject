using WebShop.Application.Common.Interfaces;
using WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryList;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Files.Images.Queries.GetImage {
    public class GetImageQueryHandler : IRequestHandler<GetImageQuery, GetImageQueryResult> {
        private readonly IFileService fileService;

        public GetImageQueryHandler(IFileService _fileService)
            => ( fileService ) = ( _fileService );

        public async Task<GetImageQueryResult> Handle(GetImageQuery request, CancellationToken cancellationToken) {
            // get file
            if ( !await fileService.IsFileExistAsync(request.Context, request.FileName) )
                throw new NotFoundException(request.FileName, request.Context);

            return await fileService.GetFileAsync(request.Context, request.FileName);
        }
    }
}
