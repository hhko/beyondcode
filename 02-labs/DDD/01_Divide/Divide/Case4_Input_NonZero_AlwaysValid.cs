using LanguageExt;
using LanguageExt.Common;

namespace DomainDrivenDesign;

internal class Case4_Input_NonZero_AlwaysValid
{
    // Case 4. 입력 개선: Primitive Obsession(NonZeroInt), Always Valid(Create)
    public Fin<int> Divide(int x, NonZeroInt y)
    {
        return x / y.Value;
    }

    public readonly struct NonZeroInt
    {
        public int Value { get; }

        private NonZeroInt(int value)
        {
            Value = value;
        }

        

        // 유효성 검증 포함된 생성 메서드
        public static Fin<NonZeroInt> Create(int value) =>
            value == 0
                ? Error.New("0은 허용되지 않습니다 (분모는 0이 될 수 없습니다)")
                : new NonZeroInt(value);

        //// 암시적 변환 (Optional)
        //public static explicit operator int(NonZeroInt x) => x.Value;

        //// x / NonZeroInt 연산을 지원하는 operator 오버로드
        //public static int operator /(int numerator, NonZeroInt denominator) =>
        //    numerator / denominator.Value;

        public override string ToString() => 
            Value.ToString();
    }
}
