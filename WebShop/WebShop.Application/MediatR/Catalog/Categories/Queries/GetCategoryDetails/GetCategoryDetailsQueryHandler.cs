using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebShop.Application.Common.Exceptions;
using WebShop.Application.Interfaces;
using WebShop.Domain.Entities.Catalog;

namespace WebShop.Application.MediatR.Catalog.Categories.Queries.GetCategoryDetails {
    public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, CategoryDetailsVm> {
        private readonly ICategoriesDbContext context;
        private readonly IMapper mapper;

        public GetCategoryDetailsQueryHandler(ICategoriesDbContext _context, IMapper _mapper)
            => (context, mapper) = (_context, _mapper);

        public async Task<CategoryDetailsVm> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken) {
            // get category by id
            var entity = await context.Categories
                .FirstOrDefaultAsync(category
                => category.Id == request.CategoryId, cancellationToken);

            if ( entity is null ) {
                throw new NotFoundException(nameof(CategoryEntity), request.CategoryId);
            }

            return mapper.Map<CategoryDetailsVm>(entity);
        }
    }
}
