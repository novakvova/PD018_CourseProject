namespace WebShop.Domain.Events.Category;

public class CategoryCreatedEvent : BaseEvent
{
    public CategoryCreatedEvent(CategoryEntity category)
    {
        Category = category;
    }

    public CategoryEntity Category { get; }
}
