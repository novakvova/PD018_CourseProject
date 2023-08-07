using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebShop.Application.Common.Helpers;
using WebShop.Application.Common.Interfaces;

namespace WebShop.Application.CQRS.Catalog.Categories.Commands.UpdateCategory {
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand> {
        public UpdateCategoryCommandValidator(IFileService fileService) {
            RuleFor(c => c.Title)
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(c => c.Details)
                .MinimumLength(10)
                .MaximumLength(200);

            RuleFor(c => c.ImageContent)
                .MustAsync(async (s, ct) => {
                    if (s == null)
                        return true;

                    return await fileService.IsImage(s) == true;
                })
                .WithMessage("Image is corrupted, in a bad format or has another problem");
        }
    }
}
