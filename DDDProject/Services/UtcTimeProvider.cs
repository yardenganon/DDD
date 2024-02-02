using DDDProject.Domain.Abstractions;

namespace DDDProject.Services;

public class UtcTimeProvider : ITimeProvider
{
    public DateTime Now() => DateTime.UtcNow;
}