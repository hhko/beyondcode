using ErrorOr;
using MediatR;

namespace DddGym.Framework.BaseTypes.Cqrs;

public interface IQuery<out TResponse> : IRequest<IErrorOr<TResponse>>
    where TResponse : IResponse;