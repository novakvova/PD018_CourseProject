using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryDetails;
using WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryList;
using WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategorySearch;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Catalog.Products.Queries.GetProductSearch {
    public class ProductSearchVm {
        /// <summary>
        /// Список отриманих профільтрованих
        /// </summary>
        public IList<ProductSearchLookupDto> Products { get; set; } = null!;

        /// <summary>
        /// Кількість усіх
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Поточна сторінка
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Кількість усіх сторінок
        /// </summary>
        public int Pages { get; set; }
    }
}
