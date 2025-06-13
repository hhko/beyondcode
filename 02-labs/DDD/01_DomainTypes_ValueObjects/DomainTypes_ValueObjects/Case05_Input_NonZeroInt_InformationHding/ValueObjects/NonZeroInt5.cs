namespace DomainTypes_ValueObjects.Case05_Input_NonZeroInt_InformationHding.ValueObjects;

public readonly struct NonZeroInt5
{
    private int Value { get; init; }

    private NonZeroInt5(int value)
    {
        Value = value;
    }

    // 유효성 검증 포함된 생성 메서드
    public static Fin<NonZeroInt5> Create(int value) =>
        value == 0
            ? Error.New("0은 허용되지 않습니다")
            : new NonZeroInt5(value);

    // 타입 변환
    //  - implicit 
    //  - explicit 
    //public static implicit operator int(NonZeroInt x) =>
    //    x.Value;
    public static explicit operator int(NonZeroInt5 x) =>
        x.Value;

    // x / NonZeroInt 연산을 지원하는 operator 오버로드
    public static int operator /(int numerator, NonZeroInt5 denominator) =>
        numerator / denominator.Value;

    public override string ToString() =>
        Value.ToString();
}
