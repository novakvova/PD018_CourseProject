using WebShop.Application.Common.Interfaces;

namespace WebShop.Infrastructure.Services;

public class DateTimeService : IDateTime {
    public DateTime Now => DateTime.UtcNow;
}
