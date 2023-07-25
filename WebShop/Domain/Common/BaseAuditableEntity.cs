namespace WebShop.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity {
    public DateTime Created { get; set; }

    // decomment when the user entity is added
    //public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    // decomment when the user entity is added
    //public int? LastModifiedBy { get; set; }
}
