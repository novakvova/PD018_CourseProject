using AutoMapper;
using WebShop.Application.Common.Interfaces;
using WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryDetails;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Catalog.Products.Queries.GetProductDetails {
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsVm> {
        private readonly ICatalogDbContext context;
        private readonly IMapper mapper;

        public GetProductDetailsQueryHandler(ICatalogDbContext _context, IMapper _mapper)
            => (context, mapper) = (_context, _mapper);

        public async Task<ProductDetailsVm> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken) {
            // get product by id
            var query = context.Products
                .Where(p => p.Id == request.ProductId);

            // load refered category
            query = query.Include(p => p.Category);

            // take single element
            var entity = await query.SingleOrDefaultAsync();

            if ( entity is null ) {
                throw new NotFoundException(nameof(ProductEntity), request.ProductId.ToString());
            }

            // load pictures uri from second table
            await context.Entry(entity).Collection(nameof(entity.Images)).LoadAsync();

            return mapper.Map<ProductDetailsVm>(entity);
        }
    }
}
