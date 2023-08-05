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
            if ( !await fileService.IsImageExistAsync(request.Context, request.FileName, request.Size) )
                throw new NotFoundException(request.FileName, request.Context);

            return await fileService.GetImageAsync(request.Context, request.FileName, request.Size);
        }
    }
}
