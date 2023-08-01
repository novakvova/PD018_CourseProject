using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebShop.Application.Common.Helpers;

namespace WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategorySearch {
    public class GetCategorySearchValidator : AbstractValidator<GetCategorySearchQuery> {
        public GetCategorySearchValidator() {
            RuleFor(c => c.Page)
                .GreaterThan(0);

            RuleFor(c => c.PageSize)
                .GreaterThanOrEqualTo(1);
        }
    }
}
