namespace FunctionalDdd.Framework.BaseTypes;

// TODO: Entity 패키지로 대체

public abstract class Entity : IEntity
{
    public Guid Id { get; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        return ((Entity)obj).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}