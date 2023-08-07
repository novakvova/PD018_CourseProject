namespace WebShop.Domain.Entities;

public class ProductImageEntity : BaseEntity {
    public string? Uri { get; set; }
    public int Priority { get; set; }
    public ProductEntity? Product { get; set; }
}