using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebShop.Application.Common.Helpers;

namespace WebShop.Application.CQRS.Files.Images.Queries.GetImage {
    public class GetImageQueryValidator : AbstractValidator<GetImageQuery> {
        public GetImageQueryValidator() {
            RuleFor(c => c.Context)
                .NotEmpty()
                .Must(BeSecuredAgainstDirectoryTraversal)
                .WithMessage("Permission denied");
            RuleFor(c => c.FileName)
                .NotEmpty()
                .Must(BeSecuredAgainstDirectoryTraversal)
                .WithMessage("Permission denied");
        }

        private bool BeSecuredAgainstDirectoryTraversal(string? path) {
            return path?.Contains("..") == false;
        }
    }
}
