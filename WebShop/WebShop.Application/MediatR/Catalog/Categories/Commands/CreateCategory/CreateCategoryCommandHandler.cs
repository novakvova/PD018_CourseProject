using MediatR;
using WebShop.Application.Interfaces;
using WebShop.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.MediatR.Catalog.Categories.Commands.CreateCategory {
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid> {
        private readonly ICategoriesDbContext _categoriesDbContext;

        public CreateCategoryCommandHandler(ICategoriesDbContext categoriesDbContext) {
            this._categoriesDbContext = categoriesDbContext;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) {
            var category = new CategoryEntity {
                Title = request.Title,
                Image = request.Image,
                Details = request.Details,
            };

            await _categoriesDbContext.Categories.AddAsync(category);
            await _categoriesDbContext.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
