using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebShop.Application.Common.Helpers;

namespace WebShop.Application.CQRS.Catalog.Products.Queries.GetProductSearch {
    public class GetProductSearchValidator : AbstractValidator<GetProductSearchQuery> {
        public GetProductSearchValidator() {
            RuleFor(c => c.Page)
                .GreaterThan(0);

            RuleFor(c => c.PageSize)
                .GreaterThanOrEqualTo(1);
        }
    }
}
