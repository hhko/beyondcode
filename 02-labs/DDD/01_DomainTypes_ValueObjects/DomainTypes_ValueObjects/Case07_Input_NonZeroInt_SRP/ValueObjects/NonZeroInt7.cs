using DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP.Framework;
using DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP.Errors;

namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP;

public readonly partial struct NonZeroInt7
    : IEquatable<NonZeroInt7>
    , IValueObject7
{
    private int Value { get; init; }

    private NonZeroInt7(int value)
    {
        Value = value;
    }

    // 개선 전
    //public static Fin<NonZeroInt> Create(int value) =>
    //    value == 0
    //        ? Error.New("0은 허용되지 않습니다")
    //        : new NonZeroInt(value);

    // 개선 후
    //  - 유효성 검사: Validate
    //  - 값 객체 생성: Create
    public static Fin<NonZeroInt7> Create(int value)
    {
        return Validate(value)
            .CreateValueObject(() => new NonZeroInt7(value));
    }

    public static Error Validate(int value)
    {
        return Error
            .Empty
            .If(value == 0, NonZeroInt7Errors.Invalid());
    }

    public static explicit operator int(NonZeroInt7 x) =>
        x.Value;

    public static int operator /(int numerator, NonZeroInt7 denominator) =>
        numerator / denominator.Value;

    public override string ToString() =>
        Value.ToString();

    bool IEquatable<NonZeroInt7>.Equals(NonZeroInt7 other) =>
        Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is NonZeroInt7 other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public static bool operator ==(NonZeroInt7 left, NonZeroInt7 right) =>
        left.Equals(right);

    public static bool operator !=(NonZeroInt7 left, NonZeroInt7 right) =>
        !(left == right);

    public static bool operator ==(NonZeroInt7 left, int right) =>
        left.Value == right;

    public static bool operator !=(NonZeroInt7 left, int right) =>
        !(left == right);

    public static bool operator ==(int left, NonZeroInt7 right) =>
        left == right.Value;

    public static bool operator !=(int left, NonZeroInt7 right) =>
        !(left == right);
}
