namespace WebShop.Domain.Events.Product;

public class ProductDeletedEvent : BaseEvent {
    public ProductDeletedEvent(ProductEntity product) {
        Product = product;
    }

    public ProductEntity Product { get; }
}
