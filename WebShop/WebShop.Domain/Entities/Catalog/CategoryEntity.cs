using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities.Base;

namespace WebShop.Domain.Entities.Catalog {
    public class CategoryEntity : BaseEntity<int> {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
