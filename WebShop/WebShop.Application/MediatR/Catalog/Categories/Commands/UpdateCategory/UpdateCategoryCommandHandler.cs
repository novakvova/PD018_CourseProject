using MediatR;
using Microsoft.EntityFrameworkCore;
using WebShop.Application.Common.Exceptions;
using WebShop.Application.Interfaces;
using WebShop.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.MediatR.Catalog.Categories.Commands.UpdateCategory {
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit> {
        private readonly ICategoriesDbContext _categoriesDbContext;

        public UpdateCategoryCommandHandler(ICategoriesDbContext categoriesDbContext) {
            this._categoriesDbContext = categoriesDbContext;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken) {
            var entity =
                await _categoriesDbContext.Categories.FirstOrDefaultAsync(
                    c =>
                    c.Id == request.CategoryId, cancellationToken);

            if ( entity is null /*|| entity.CreatorID != request.CreatorId*/ )
                throw new NotFoundException(nameof(CategoryEntity), request.CategoryId);

            int countUpdated = 0;

            if ( request.Details is not null ) {
                countUpdated++;
                entity.Details = request.Details;
            }

            if ( request.Title is not null ) {
                countUpdated++;
                entity.Title = request.Title;
            }

            if ( request.Image is not null ) {
                countUpdated++;
                entity.Image = request.Image;
            }

            if ( countUpdated > 0 ) {
                entity.DateUpdated = DateTime.UtcNow;
                await _categoriesDbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
