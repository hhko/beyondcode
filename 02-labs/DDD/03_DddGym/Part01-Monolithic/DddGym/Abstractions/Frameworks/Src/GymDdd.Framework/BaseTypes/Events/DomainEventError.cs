namespace GymDdd.Framework.BaseTypes.Events;

// TODO: DomainError            100
// DONE: DomainEventError       101
// TODO: ApplicationError       200

// TODO: Error에 Exception 담은 후 로그 출력 -> Serilog 예외 출력 적용

public static class DomainEventError
{
    public const int DomainEventType = 200;

    //public static Error From(string code, string description)
    //{
    //    return Error.Custom(
    //        type: DomainEventType,
    //        code: code,
    //        description: description);
    //}

    public static Error From(string message)
    {
        return Error.New(
            message);
    }
}
