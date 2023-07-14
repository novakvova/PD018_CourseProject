using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebShop.Application.Common.Exceptions;
using WebShop.Application.Interfaces;
using WebShop.Domain.Entities.Catalog;

namespace WebShop.Application.MediatR.Catalog.Categories.Queries.GetCategoryDetails {
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, CategoryListVm> {
        private readonly ICategoriesDbContext context;
        private readonly IMapper mapper;

        public GetCategoryListQueryHandler(ICategoriesDbContext _context, IMapper _mapper)
            => (context, mapper) = (_context, _mapper);

        public async Task<CategoryListVm> Handle(GetCategoryListQuery request, CancellationToken cancellationToken) {
            // get a list of categories,
            // whose every element has been mapped to CategoryLookupDto
            var list = await context.Categories
                .Where((category) => true)
                .ProjectTo<CategoryLookupDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if ( list is null ) {
                throw new NotFoundException(nameof(CategoryEntity), -1);
            }

            return new CategoryListVm {
                Categories = list,
            };
        }
    }
}
