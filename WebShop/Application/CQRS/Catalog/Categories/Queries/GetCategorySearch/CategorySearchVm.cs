namespace WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategorySearch {
    public class CategorySearchVm {
        /// <summary>
        /// Список отриманих профільтрованих категорій
        /// </summary>
        public IList<CategorySearchLookupDto> Categories { get; set; } = null!;

        /// <summary>
        /// Кількість усіх категорій
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
