using System.Xml;
using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategorySearch {
    public class GetCategorySearchQueryHandler : IRequestHandler<GetCategorySearchQuery, CategorySearchVm> {
        private readonly ICatalogDbContext context;
        private readonly IMapper mapper;

        public GetCategorySearchQueryHandler(ICatalogDbContext _context, IMapper _mapper)
            => (context, mapper) = (_context, _mapper);

        public async Task<CategorySearchVm> Handle(GetCategorySearchQuery request, CancellationToken cancellationToken) {
            var query = context.Categories.Where((category) => true);
            var total = await query.CountAsync();
            var pages = (int)Math.Ceiling(total / (double)request.PageSize);
            query = query
                .Skip(( request.Page - 1 ) * request.PageSize)
                .Take(request.PageSize);

            var list = await query
                .ProjectTo<CategorySearchLookupDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CategorySearchVm {
                Categories = list,
                Total = total,
                Pages = pages,
                CurrentPage = request.Page
            };
        }
    }
}
