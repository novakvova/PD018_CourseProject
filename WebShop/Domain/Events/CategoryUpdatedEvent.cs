namespace WebShop.Domain.Events;

public class CategoryUpdatedEvent : BaseEvent {
    public CategoryUpdatedEvent(CategoryEntity category) {
        Category = category;
    }

    public CategoryEntity Category { get; }
}
