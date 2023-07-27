using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebShop.Application.Common.Helpers;

namespace WebShop.Application.CQRS.Catalog.Categories.Commands.UpdateCategory {
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand> {
        public UpdateCategoryCommandValidator() {
            RuleFor(c => c.Title)
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(c => c.Details)
                .MinimumLength(10)
                .MaximumLength(200);

            RuleFor(c => c.ImageContent)
                .Must(BeImage)
                .WithMessage("File is not image.");
        }

        private bool BeValidUrl(string? url) {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && ( uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps );
        }
        private bool BeFilename(string input) {
            string pattern = @"^[^\s]+\.[^\s]+$";
            return Regex.IsMatch(input, pattern);
        }
        private bool BeImage(Stream? file) {
            if ( file is null )
                return true;

            // read 4 bytes of stream to extract file extention
            var ext = new BinaryReader(file).ReadBytes(4);
            // bring stream pointer back
            file.Position -= ext.Length;

            return ImageHelper.GetImageFormat(ext) != ImageHelper.ImageFormat.unknown;
        }
    }
}
