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

namespace WebShop.Application.MediatR.Catalog.Categories.Commands.DeleteCategory {
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit> {
        private readonly ICategoriesDbContext _categoriesDbContext;

        public DeleteCategoryCommandHandler(ICategoriesDbContext categoriesDbContext) {
            this._categoriesDbContext = categoriesDbContext;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) {
            var entity =
                await _categoriesDbContext.Categories.FindAsync(new object[] { request.CategoryId }, cancellationToken);

            if ( entity is null /*|| entity.CreatorID != request.CreatorId*/ )
                throw new NotFoundException(nameof(CategoryEntity), request.CategoryId);

            _categoriesDbContext.Categories.Remove(entity);

            await _categoriesDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
