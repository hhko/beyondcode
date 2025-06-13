using DomainTypes_ValueObjects.Case08_ArchitectureTests.Framework;
using DomainTypes_ValueObjects.Case08_ArchitectureTests.Errors;

namespace DomainTypes_ValueObjects.Case08_ArchitectureTests;

public sealed class NonZeroInt8
    : IEquatable<NonZeroInt8>
    , IValueObject8
{
    private int Value { get; init; }

    private NonZeroInt8(int value)
    {
        Value = value;
    }

    private NonZeroInt8()
    {

    }

    // 개선 전
    //public static Fin<NonZeroInt> Create(int value) =>
    //    value == 0
    //        ? Error.New("0은 허용되지 않습니다")
    //        : new NonZeroInt(value);

    // 개선 후
    //  - 유효성 검사: Validate
    //  - 값 객체 생성: Create
    public static Fin<NonZeroInt8> Create(int value)
    {
        return Validate(value)
            .CreateValueObject(() => new NonZeroInt8(value));
    }

    public static Error Validate(int value)
    {
        return Error
            .Empty
            .If(value == 0, NonZeroInt8Errors.Invalid());
    }

    public static explicit operator int(NonZeroInt8 x) =>
        x.Value;

    public static int operator /(int numerator, NonZeroInt8 denominator) =>
        numerator / denominator.Value;

    public override string ToString() =>
        Value.ToString();

    bool IEquatable<NonZeroInt8>.Equals(NonZeroInt8 other) =>
        Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is NonZeroInt8 other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public static bool operator ==(NonZeroInt8 left, NonZeroInt8 right) =>
        left.Equals(right);

    public static bool operator !=(NonZeroInt8 left, NonZeroInt8 right) =>
        !(left == right);

    public static bool operator ==(NonZeroInt8 left, int right) =>
        left.Value == right;

    public static bool operator !=(NonZeroInt8 left, int right) =>
        !(left == right);

    public static bool operator ==(int left, NonZeroInt8 right) =>
        left == right.Value;

    public static bool operator !=(int left, NonZeroInt8 right) =>
        !(left == right);
}
