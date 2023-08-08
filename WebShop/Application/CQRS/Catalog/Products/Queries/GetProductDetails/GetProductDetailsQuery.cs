using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.CQRS.Catalog.Products.Queries.GetProductDetails {
    public class GetProductDetailsQuery : IRequest<ProductDetailsVm> {
        public int ProductId { get; set; }
    }
}
