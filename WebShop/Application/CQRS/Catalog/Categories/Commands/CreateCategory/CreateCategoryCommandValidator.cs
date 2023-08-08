using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebShop.Application.Common.Helpers;
using WebShop.Application.Common.Interfaces;

namespace WebShop.Application.CQRS.Catalog.Categories.Commands.CreateCategory {
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand> {
        public CreateCategoryCommandValidator(IFileService fileService) {
            RuleFor(c => c.Title)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(c => c.Details)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(200);

            RuleFor(c => c.ImageContent)
                .NotEmpty()
                .MustAsync(async (s, ct) => await fileService.IsImage(s) == true)
                .WithMessage("Image is corrupted, in a bad format or has another problem");
        }
    }
}
