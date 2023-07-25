namespace WebShop.Domain.Events;

public class CategoryDeletedEvent : BaseEvent {
    public CategoryDeletedEvent(CategoryEntity category) {
        Category = category;
    }

    public CategoryEntity Category { get; }
}
