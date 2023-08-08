using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryList {
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, CategoryListVm> {
        private readonly ICatalogDbContext context;
        private readonly IMapper mapper;

        public GetCategoryListQueryHandler(ICatalogDbContext _context, IMapper _mapper)
            => (context, mapper) = (_context, _mapper);

        public async Task<CategoryListVm> Handle(GetCategoryListQuery request, CancellationToken cancellationToken) {
            // get a list of categories,
            // whose every element has been mapped to CategoryLookupDto
            var list = await context.Categories
                .Where((category) => true)
                .ProjectTo<CategoryLookupDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if ( list is null ) {
                throw new NotFoundException(nameof(CategoryEntity), request.ToString());
            }

            return new CategoryListVm {
                Categories = list,
            };
        }
    }
}
