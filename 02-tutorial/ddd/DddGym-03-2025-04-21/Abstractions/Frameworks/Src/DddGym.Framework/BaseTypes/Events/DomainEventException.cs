
using LanguageExt;
using LanguageExt.Common;

namespace DddGym.Framework.BaseTypes.Events;

// DONE: DomainEventException 필요성 이해
//  - 이벤트 핸들러에서는 실패일 때 예외를 throw해야 합니다.
//  - 왜?
//      이벤트는 과거 지행이다.
//      이벤트 핸들러는 상태 변경이 성공 후 발생하기 때문에 반드시 성공해야 한다.

public class DomainEventException : Exception
{
    public Error DomainEventError { get; }
    //public List<Error> UnderlyingErrors { get; }


    //public DomainEventException(
    //    Error domainEventError,
    //    List<Error>? underlyingErrors = null)
    //    : base(message: domainEventError.Description)
    //{
    //    DomainEventError = domainEventError;
    //    UnderlyingErrors = underlyingErrors ?? new();
    //}

    public Error UnderlyingErrors { get; }

    public DomainEventException(
        Error domainEventError,
        Option<Error> underlyingErrors = default)
        : base(message: domainEventError.Message)
    {
        DomainEventError = domainEventError;
        UnderlyingErrors = underlyingErrors.IfNone(Error.Empty);
    }
}
