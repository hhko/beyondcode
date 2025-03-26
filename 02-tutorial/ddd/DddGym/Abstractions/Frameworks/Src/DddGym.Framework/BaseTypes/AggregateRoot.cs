using DddGym.Framework.BaseTypes.Events;

namespace DddGym.Framework.BaseTypes;

// TODO: Aggregate Root 패키지로 대체

public abstract class AggregateRoot : Entity
{
    protected readonly List<IDomainEvent> _domainEvents = [];

    protected AggregateRoot(Guid id) : base(id)
    {
    }

    protected AggregateRoot()
    {
    }

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }
}