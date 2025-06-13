using DomainTypes_ValueObjects.Case06_Input_NonZeroInt_Equality.ValueObjects;

namespace DomainTypes_ValueObjects.Case06_Input_NonZeroInt_Equality;

internal partial class MathOperations6
{
    public int Divide(int numerator, NonZeroInt6 denominator)
    {
        return numerator / denominator;
    }
}
