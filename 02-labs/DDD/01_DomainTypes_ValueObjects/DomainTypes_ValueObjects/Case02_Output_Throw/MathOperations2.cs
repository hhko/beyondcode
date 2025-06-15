namespace DomainTypes_ValueObjects.Case02_Output_Throw;

// Case 2. 출력 개선: 예외
internal class MathOperations2
{
    public int Divide(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("0으로 나눌 수 없습니다");

        return numerator / denominator;
    }
}
