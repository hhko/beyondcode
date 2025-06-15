using DomainTypes_ValueObjects.Case06_Input_NonZeroInt_Equality.ValueObjects;

namespace DomainTypes_ValueObjects.Case06_Input_NonZeroInt_Equality;

// Case 6. 값 객체 동등성 구현
//  - IEquatable<T>
//  - object overriding
//  - operator overloading
internal partial class MathOperations6
{
    public int Divide(int numerator, NonZeroInt6 denominator)
    {
        return numerator / denominator;
    }
}
