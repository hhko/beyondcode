namespace DomainTypes_ValueObjects.Case08_ArchitectureTests.Errors;

public sealed partial class NonZeroInt8Errors
{
    public static Error Invalid() =>
        Error.New("0은 허용되지 않습니다");
}
