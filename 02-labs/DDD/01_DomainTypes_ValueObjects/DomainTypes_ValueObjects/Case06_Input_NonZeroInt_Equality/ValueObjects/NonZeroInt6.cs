namespace DomainTypes_ValueObjects.Case06_Input_NonZeroInt_Equality.ValueObjects;

public readonly struct NonZeroInt6 : IEquatable<NonZeroInt6>
{
    private int Value { get; init; }

    private NonZeroInt6(int value)
    {
        Value = value;
    }

    public static Fin<NonZeroInt6> Create(int value) =>
        value == 0
            ? Error.New("0은 허용되지 않습니다")
            : new NonZeroInt6(value);

    public static explicit operator int(NonZeroInt6 x) =>
        x.Value;

    public static int operator /(int numerator, NonZeroInt6 denominator) =>
        numerator / denominator.Value;

    public override string ToString() =>
        Value.ToString();

    //
    // 동등성
    //  - IEquatable<T>
    //  - object overriding
    //  - operator overloading
    //

    // 동등성: IEquatable<T>
    bool IEquatable<NonZeroInt6>.Equals(NonZeroInt6 other) =>
        Value == other.Value;

    // 동등성: object 기본 구현 재정의(overriding)
    public override bool Equals(object? obj) =>
        obj is NonZeroInt6 other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    // 동등성: 비교 연산자 오버로드(operator overloading)
    // 1. NonZeroInt <-> NonZeroInt 비교
    //      x == y
    public static bool operator ==(NonZeroInt6 left, NonZeroInt6 right) =>
        left.Equals(right);

    public static bool operator !=(NonZeroInt6 left, NonZeroInt6 right) =>
        !(left == right);

    // 2. NonZeroInt == int 비교
    //      x == 6
    public static bool operator ==(NonZeroInt6 left, int right) =>
        left.Value == right;

    public static bool operator !=(NonZeroInt6 left, int right) =>
        !(left == right);

    // 3. int == NonZeroInt 비교
    //      6 == x
    public static bool operator ==(int left, NonZeroInt6 right) =>
        left == right.Value;

    public static bool operator !=(int left, NonZeroInt6 right) =>
        !(left == right);
}
