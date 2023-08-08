namespace WebShop.Domain.Events.Category;

public class CategoryUpdatedEvent : BaseEvent
{
    public CategoryUpdatedEvent(CategoryEntity category)
    {
        Category = category;
    }

    public CategoryEntity Category { get; }
}
