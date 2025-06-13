namespace DomainTypes_ValueObjects.Case03_Output_Error;

// Case 3. 출력 개선: 예측할 수 없는 예외 대신, 예측 가능한 값으로 반환
internal class MathOperations3
{
    // finite
    public Fin<int> Divide(int numerator, int denominator)
    {
        if (denominator == 0)
            return Error.New("0으로 나눌 수 없습니다");

        return numerator / denominator;
    }
}
