using LanguageExt;
using LanguageExt.Common;

namespace DomainDrivenDesign;

internal class Case3_Output_Error
{
    // Case 3. 출력 개선: 예외보다는 값으로 출력
    public Fin<int> Divide(int x, int y)
    {
        if (y == 0)
            return Error.New("분모가 0입니다");

        return x / y;
    }
}
