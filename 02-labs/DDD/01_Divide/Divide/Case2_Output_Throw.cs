namespace DomainDrivenDesign;

internal class Case2_Output_Throw
{
    // Case 2. 출력 개선: 명시적 예외로 출력
    public int Divide(int x, int y)
    {
        if (y == 0)
            throw new ArgumentException("분모가 0입니다");

        return x / y;
    }
}
