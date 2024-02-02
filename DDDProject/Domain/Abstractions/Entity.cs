using DDDProject.Domain.Events;

namespace DDDProject.Domain.Abstractions;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    protected IReadOnlyCollection<IDomainEvent> GetDomainEvents => _domainEvents.AsReadOnly();

    protected void ClearEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}