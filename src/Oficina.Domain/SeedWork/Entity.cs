namespace Oficina.Domain.SeedWork;

public abstract class Entity
{
    private readonly List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => 
        _domainEvents.AsReadOnly();
    
    protected void Raise(DomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);
}