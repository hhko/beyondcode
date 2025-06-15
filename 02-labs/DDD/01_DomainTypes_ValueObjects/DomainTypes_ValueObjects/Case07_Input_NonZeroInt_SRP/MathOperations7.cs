namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP;

// Case 7. 단일 책임 원칙(SRP)
//	- 객체 생성 책임: Create
//	- 유효성 검사 책임: Validate
//	- 유효성 검사 규칙 책임: If 확장 메서드, Errors 접미사 클래스
internal class MathOperations7
{
    public int Divide(int numerator, NonZeroInt7 denominator)
    {
        return numerator / denominator;
    }
}
