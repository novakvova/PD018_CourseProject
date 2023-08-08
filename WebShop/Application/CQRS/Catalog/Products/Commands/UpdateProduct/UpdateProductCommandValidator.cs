using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Interfaces;
using WebShop.Application.CQRS.Catalog.Products.Commands.CreateProduct;
using static System.Net.Mime.MediaTypeNames;

namespace WebShop.Application.CQRS.Catalog.Products.Commands.UpdateProduct {
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand> {
        public UpdateProductCommandValidator(ICatalogDbContext db) {
            RuleFor(c => c.Title)
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(c => c.Details)
                .MinimumLength(10)
                .MaximumLength(200);

            RuleFor(c => c.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.CategoryId)
                .MustAsync(async (c, token) =>
                    ( c == null || ( await db.Categories.FindAsync(c) ) != null ))
                .WithMessage(p => $"Category {p.CategoryId} does not exist");
        }
    }
}
