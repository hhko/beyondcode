using ErrorOr;

namespace DddGym.Framework.BaseTypes.Application.Events;

public class DomainEventException : Exception
{
    public Error DomainEventError { get; }
    public List<Error> UnderlyingErrors { get; }

    public DomainEventException(
        Error domainEventError, 
        List<Error>? underlyingErrors = null) 
        : base(message: domainEventError.Description)
    {
        DomainEventError = domainEventError;
        UnderlyingErrors = underlyingErrors ?? new();
    }
}
