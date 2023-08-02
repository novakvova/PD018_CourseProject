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
    using global::WebShop.Application.CQRS.Catalog.Categories.Commands.DeleteCategory;
    using global::WebShop.Domain.Events.Category;
    using Microsoft.Extensions.Logging;

    namespace WebShop.Application.CQRS.Catalog.Categories.Commands.DeleteCategory {
        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit> {
            private readonly ICatalogDbContext dbContext;
            private readonly IFileService fileService;
            private readonly ILogger logger;
            private readonly IDateTime dateTimeService;

            public UpdateCategoryCommandHandler(ICatalogDbContext db, IFileService fileService, ILogger<DeleteCategoryCommandHandler> logger, IDateTime dateTimeService) {
                dbContext = db;
                this.fileService = fileService;
                this.logger = logger;
                this.dateTimeService = dateTimeService;
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

                if ( request.ImageContent is not null ) {
                    countUpdated++;

                    try {
                        await fileService.DeleteFileAsync(category.Image);
                    }
                    catch ( NotFoundException e ) {
                        logger.LogWarning($"{nameof(CategoryEntity)} with key {category.Id} was updated, but image {category.Image} not found");
                    }

                    category.Image = await fileService.UploadFileAsync(
                        nameof(CategoryEntity),
                        request.ImageContent);
                }

                category.AddDomainEvent(new CategoryUpdatedEvent(category));

                if ( countUpdated > 0 ) {
                    category.LastModified = dateTimeService.Now;
                    await dbContext.SaveChangesAsync(cancellationToken);
                }

                return Unit.Value;
            }
        }
    }
}
