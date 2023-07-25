using MediatR;
using WebShop.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Entities;
using WebShop.Domain.Events;

namespace WebShop.Application.CQRS.Catalog.Categories.Commands.UpdateCategory {
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit> {
        private readonly ICatalogDbContext dbContext;

        public UpdateCategoryCommandHandler(ICatalogDbContext db) {
            dbContext = db;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken) {
            var category =
                await dbContext.Categories.FirstOrDefaultAsync(
                    c =>
                    c.Id == request.CategoryId, cancellationToken);

            if ( category is null /*|| category.CreatorID != request.CreatorId*/ )
                throw new NotFoundException(nameof(CategoryEntity), request.CategoryId.ToString());

            int countUpdated = 0;

            if ( request.Details is not null ) {
                countUpdated++;
                category.Details = request.Details;
            }

            if ( request.Title is not null ) {
                countUpdated++;
                category.Title = request.Title;
            }

            if ( request.Image is not null ) {
                countUpdated++;
                category.Image = request.Image;
            }

            category.AddDomainEvent(new CategoryUpdatedEvent(category));

            if ( countUpdated > 0 ) {
                category.LastModified = DateTime.UtcNow;
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
