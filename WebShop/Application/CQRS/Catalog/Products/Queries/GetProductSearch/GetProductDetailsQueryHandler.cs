using AutoMapper;
using WebShop.Application.Common.Interfaces;
using WebShop.Application.CQRS.Catalog.Products.Queries.GetProductSearch;

namespace WebShop.Application.CQRS.Catalog.Products.Queries.GetProductDetails {
    public class GetProductSearchQueryHandler : IRequestHandler<GetProductSearchQuery, ProductSearchVm> {
        private readonly ICatalogDbContext context;
        private readonly IMapper mapper;

        public GetProductSearchQueryHandler(ICatalogDbContext _context, IMapper _mapper)
            => (context, mapper) = (_context, _mapper);

        public async Task<ProductSearchVm> Handle(GetProductSearchQuery request, CancellationToken cancellationToken) {
            // apply filters from request
            var query = context.Products.Where((product) => true);

            // get total count
            var total = await query.CountAsync();

            // get total pages count
            var pages = (int)Math.Ceiling(total / (double)request.PageSize);

            // paginate request
            query = query
                .Skip(( request.Page - 1 ) * request.PageSize)
                .Take(request.PageSize);

            var list = await query
                .ProjectTo<ProductSearchLookupDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProductSearchVm {
                Products = list,
                Total = total,
                Pages = pages,
                CurrentPage = request.Page
            };
        }
    }
}
