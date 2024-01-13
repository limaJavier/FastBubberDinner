using FastBubberDinner.Application.Common.Interfaces.Services;

namespace FastBubberDinner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}