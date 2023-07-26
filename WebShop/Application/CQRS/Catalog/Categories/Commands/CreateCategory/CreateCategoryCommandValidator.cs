using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.CQRS.Catalog.Categories.Commands.CreateCategory {
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand> {
        public CreateCategoryCommandValidator() {
            RuleFor(c => c.Title)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(c => c.Details)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(200);

            RuleFor(c => c.Image)
                .NotEmpty()
                .Must(BeValidUrl)
                .WithMessage("Invalid URL format.");
        }

        private bool BeValidUrl(string? url) {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && ( uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps );
        }
    }
}
