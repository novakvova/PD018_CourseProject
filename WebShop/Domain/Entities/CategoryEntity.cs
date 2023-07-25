namespace WebShop.Domain.Entities;

public class CategoryEntity : BaseAuditableEntity {
    public string Title { get; set; }
    public string? Image { get; set; }
    public string? Details { get; set; }
}
