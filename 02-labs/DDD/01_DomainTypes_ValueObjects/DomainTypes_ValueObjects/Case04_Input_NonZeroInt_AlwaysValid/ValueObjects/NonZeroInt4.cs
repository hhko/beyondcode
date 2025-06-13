namespace DomainTypes_ValueObjects.Case04_Input_NonZeroInt_AlwaysValid.ValueObjects;

public readonly struct NonZeroInt4
{
    public int Value { get; }

    private NonZeroInt4(int value)
    {
        Value = value;
    }

    // 유효성 검증 포함된 생성 메서드
    public static Fin<NonZeroInt4> Create(int value) =>
        value == 0
            ? Error.New("0은 허용되지 않습니다")
            : new NonZeroInt4(value);

    public override string ToString() =>
        Value.ToString();
}
