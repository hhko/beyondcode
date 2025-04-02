using DddGym.Framework.Utilites;
using LanguageExt;
using LanguageExt.Common;
using System.Diagnostics;

namespace DddGym.Framework.BaseTypes.V2;

public interface IValueObject
{
    //const string SingleValueName = "Value";
    //const string ValidateMethodName = "Validate";
    //const string CreateMethodName = "Create";
}

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

    //Do not remove 'sealed'. It forces DataTransferObject to use this ToString implementation
    public override sealed string ToString()
    {
        return Value.ToString()!;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}

//Alternative approach -> ValueObject with inheritance. 

/// <summary>
/// Each ValueObject should contain at least two public, static methods: "Create" and "Validate".
/// "Create" method should return the ValidationResult of same ValueObjectType and should use the "Validate" method.
/// "Validate" method should return List<Error> and contain all value object validation
/// </summary>
[Serializable]
public abstract class ValueObject : IEquatable<ValueObject>, IValueObject
{
    public const string Value = nameof(Value);

    //For instance the atomic value of a Email is a single character
    /// <summary>
    /// Gets the atomic values of the value object.
    /// </summary>
    /// <returns>The collection of objects representing the value object values.</returns>
    public abstract IEnumerable<object> GetAtomicValues();

    //ValueObjects are equal when their values are equal
    /// <inheritdoc />
    public bool Equals(ValueObject? other)
    {
        return other is not null && ValuesAreEqual(other);
    }

    /// <inheritdoc />
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

    //Make a hash using the whole AtomicValues collection (and the initial value of default(int) which is zero)
    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(default(int), (hashcode, value) => HashCode.Combine(hashcode, value.GetHashCode()));
    }

    /// <summary>
    /// Checks if the values of the specified value object are equal to the values of the current instance.
    /// </summary>
    /// <param name="other">The other value object.</param>
    /// <returns>True if the values of the specified value object are equal to the values of the current instance, otherwise false.</returns>
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


public sealed record class FirstName : ValueObject<string>
{
    public const int MaxLength = 3;

    public static readonly Error Empty = Error.New(
        0,
        $"{nameof(FirstName)} is empty.");

    public static readonly Error TooLong = Error.New(
        //$"{nameof(FirstName)}.{nameof(TooLong)}",
        1,
        $"{nameof(FirstName)} must be at most {MaxLength} characters long.");

    public static readonly Error TooLong2 = Error.New(
        //$"{nameof(FirstName)}.{nameof(TooLong)}",
        2,
        $"{nameof(FirstName)} must be at most {MaxLength} characters long.2");

    private FirstName(string firstName) : base(firstName)
    {
    }

    public static Validation<Error, FirstName> Create(string firstName)
    {
        ManyErrors errors = Validate(firstName);
        return errors.CreateValidation(() => new FirstName(firstName));
    }

    public static ManyErrors Validate(string firstName)
    {
        // public new static Error Empty { get; } = new ManyErrors(Seq.empty<Error>());
        return ((ManyErrors)ManyErrors.Empty)
            .If(string.IsNullOrWhiteSpace(firstName), Empty)
            .If(firstName.Length > MaxLength, TooLong)
            .If(firstName.Length > MaxLength, TooLong2);
    }
}

public static class ManyErrorsUtilities
{
    public static ManyErrors If(
        this ManyErrors error,
        bool condition,
        params Error[] errorsToAdd)
    {
        if (condition)
        {
            foreach (Error errorToAdd in errorsToAdd)
            {
                return new ManyErrors([error, errorToAdd]);
            }
        }

        return error;
    }

    public static Validation<Error, TValueObject> CreateValidation<TValueObject>(
        this ManyErrors errors,
        Func<TValueObject> createValueObject)
        where TValueObject : IValueObject
    {
        if (errors is null)
        {
            throw new ArgumentNullException($"{nameof(errors)} must not be null");
        }

        if (errors.Count != 0)
        {
            return errors;
        }

        return createValueObject.Invoke();
    }
}
