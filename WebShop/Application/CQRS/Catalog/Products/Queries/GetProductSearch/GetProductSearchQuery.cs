using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.CQRS.Catalog.Products.Queries.GetProductSearch {
    public class GetProductSearchQuery : IRequest<ProductSearchVm> {
        public int Page { get; set; }
        public int PageSize { get; set; } = 2;
    }
}
