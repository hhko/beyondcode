namespace DomainTypes_ValueObjects.Case08_ArchitectureTests;

// Case 8. 값 객체 설계 규칙 테스트
//	- 인터페이스 필수 메서드 이름 정의
//	- public sealed 클래스 규칙 테스트
// 	- private 생성자 규칙 테스트
//	- public static Fin<T> Create 메서드 규칙 테스트
//  - public static Error Validate 메서드 규칙 테스트
internal class MathOperations8
{
    public int Divide(int numerator, NonZeroInt8 denominator)
    {
        return numerator / denominator;
    }
}
