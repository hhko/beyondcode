using ErrorOr;

namespace DddGym.Framework.BaseTypes.Application.Events;

public static class DomainEventError
{
    public const int DomainEventType = 100;

    public static Error From(string code, string description)
    {
        return Error.Custom(
            type: DomainEventType,
            code: code,
            description: description);
    }
}
