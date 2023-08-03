using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.CQRS.Catalog.Products.Commands.UpdateProduct {
    public class UpdateProductCommand : IRequest<Unit> {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        //public IList<Stream> Images { get; set; } = null!;
        public string? Details { get; set; }
        public float? Price { get; set; }
        public int? CategoryId { get; set; }
    }
}
