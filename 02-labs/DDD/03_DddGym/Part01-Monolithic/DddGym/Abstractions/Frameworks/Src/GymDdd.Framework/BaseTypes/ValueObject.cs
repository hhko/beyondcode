using GymDdd.Framework.Utilites;
using System.Diagnostics;

namespace GymDdd.Framework.BaseTypes;

[DebuggerDisplay("{Value}")]
public abstract record class ValueObject<TValue> : IValueObject
    where TValue : notnull
{
    protected ValueObject(TValue value)
    {
        Value = value;
    }

    protected ValueObject()
    {
    }

    public TValue Value { get; }

    public override sealed string ToString()
    {
        return Value.ToString()!;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}

[Serializable]
public abstract class ValueObject : IEquatable<ValueObject>, IValueObject
{
    public const string Value = nameof(Value);

    public abstract IEnumerable<object> GetAtomicValues();

    /// <inheritdoc />
    public bool Equals(ValueObject? other)
    {
        return other is not null && ValuesAreEqual(other);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        if (obj is not ValueObject otherValueObject)
        {
            return false;
        }

        return ValuesAreEqual(otherValueObject);
    }

    public static bool operator ==(ValueObject? first, ValueObject? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    public static bool operator !=(ValueObject? first, ValueObject? second)
    {
        return !(first == second);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(default(int), (hashcode, value) => HashCode.Combine(hashcode, value.GetHashCode()));
    }

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues()
            .SequenceEqual(other.GetAtomicValues());
    }

    public override string ToString()
    {
        return GetAtomicValues()
            .Join(", ");
    }
}

//public abstract class ValueObject
//{
//    public abstract IEnumerable<object?> GetEqualityComponents();

//    public override bool Equals(object? obj)
//    {
//        if (obj is null || obj.GetType() != GetType())
//        {
//            return false;
//        }

//        return ((ValueObject)obj)
//            .GetEqualityComponents()
//            .SequenceEqual(GetEqualityComponents());
//    }

//    public override int GetHashCode()
//    {
//        return GetEqualityComponents()
//            .Select(x => x?.GetHashCode() ?? 0)
//            .Aggregate((x, y) => x ^ y);
//    }
//}