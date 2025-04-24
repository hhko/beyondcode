using DddGym.Framework.BaseTypes;
using DddGym.Framework.BaseTypes.Utilities;
using LanguageExt;
using LanguageExt.Common;

namespace DddGym.Framework.Tests.Unit.BaseTypes.TestData;

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

    //public static Validation<Error, FirstName> Create(string firstName)
    //{
    //    ManyErrors errors = Validate(firstName);
    //    return errors.CreateValidation(() => new FirstName(firstName));
    //}

    public static Fin<FirstName> Create(string firstName)
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
