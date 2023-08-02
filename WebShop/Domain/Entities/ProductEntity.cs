namespace WebShop.Domain.Entities;

public class ProductEntity : BaseAuditableEntity {
    public string? Title { get; set; }
    public string? Details { get; set; }
    public float? Price { get; set; }
    public CategoryEntity? Category { get; set; }
}