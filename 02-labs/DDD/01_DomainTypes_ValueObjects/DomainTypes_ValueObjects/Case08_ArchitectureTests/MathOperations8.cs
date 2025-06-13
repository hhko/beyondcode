namespace DomainTypes_ValueObjects.Case08_ArchitectureTests;

internal class MathOperations8
{
    public int Divide(int numerator, NonZeroInt8 denominator)
    {
        return numerator / denominator;
    }
}
