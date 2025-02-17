namespace DddGym.Domain.Abstractions.BaseTypes;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }
}