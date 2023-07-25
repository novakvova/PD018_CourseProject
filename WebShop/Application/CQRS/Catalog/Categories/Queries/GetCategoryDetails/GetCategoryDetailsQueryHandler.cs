using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryDetails {
    public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, CategoryDetailsVm> {
        private readonly ICatalogDbContext context;
        private readonly IMapper mapper;

        public GetCategoryDetailsQueryHandler(ICatalogDbContext _context, IMapper _mapper)
            => (context, mapper) = (_context, _mapper);

        public async Task<CategoryDetailsVm> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken) {
            // get category by id
            var entity = await context.Categories
                .FirstOrDefaultAsync(category
                => category.Id == request.CategoryId, cancellationToken);

            if ( entity is null ) {
                throw new NotFoundException(nameof(CategoryEntity), request.CategoryId.ToString());
            }

            return mapper.Map<CategoryDetailsVm>(entity);
        }
    }
}
