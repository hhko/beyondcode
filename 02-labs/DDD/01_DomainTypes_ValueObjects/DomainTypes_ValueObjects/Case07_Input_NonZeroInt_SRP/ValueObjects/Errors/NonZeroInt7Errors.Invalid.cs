namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP.Errors;

public readonly partial struct NonZeroInt7Errors
{
    public static Error Invalid() =>
        Error.New("0은 허용되지 않습니다");
}
