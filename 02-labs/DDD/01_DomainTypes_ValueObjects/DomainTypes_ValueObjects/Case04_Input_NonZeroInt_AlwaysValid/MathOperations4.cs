using DomainTypes_ValueObjects.Case04_Input_NonZeroInt_AlwaysValid.ValueObjects;

namespace DomainTypes_ValueObjects.Case04_Input_NonZeroInt_AlwaysValid;

// Case 4. 입력 개선
//  - "0이 아님"이라는 비즈니스 규칙을 포함한 의미 있는 타입(Primitive Obsession: NonZeroInt)
//  - 입력 유효성 검사(validate)를 객체 생성 시점으로 위임하여 이후 비즈니스 로직에서의 복잡성을 줄임(Always Valid: Create)
internal partial class MathOperations4
{
    public int Divide(int numerator, NonZeroInt4 denominator)
    {
        return numerator / denominator.Value;
    }
}
