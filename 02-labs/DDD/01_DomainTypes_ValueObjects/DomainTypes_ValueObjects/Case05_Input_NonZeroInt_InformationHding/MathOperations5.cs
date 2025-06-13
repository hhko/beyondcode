using DomainTypes_ValueObjects.Case05_Input_NonZeroInt_InformationHding.ValueObjects;

namespace DomainTypes_ValueObjects.Case05_Input_NonZeroInt_InformationHding;

// Case 5. 정보 은닉
//  - 연산자 오버로드(operator overloading)을 통해 값 노출 없이 사용
//  - NonZeroInt는 마치 int처럼 보이지만, 내부적으로는 안전한 규칙을 강제하는 구조
internal partial class MathOperations5
{
    public int Divide(int numerator, NonZeroInt5 denominator)
    {
        // 암시적 타입 변환: 갑 추출
        //int x = denominator;

        // 명시적 타입 변환: 값 추출
        //int x = (int)denominator;

        return numerator / denominator;
    }
}
