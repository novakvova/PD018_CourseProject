using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.MediatR.Catalog.Categories.Queries.GetCategoryDetails {
    public class GetCategoryDetailsQuery : IRequest<CategoryDetailsVm> {
        public Guid CategoryId { get; set; }
    }
}
