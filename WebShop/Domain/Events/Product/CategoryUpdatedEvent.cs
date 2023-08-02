namespace WebShop.Domain.Events.Product;

public class ProductUpdatedEvent : BaseEvent {
    public ProductUpdatedEvent(ProductEntity product) {
        Product = product;
    }

    public ProductEntity Product { get; }
}
