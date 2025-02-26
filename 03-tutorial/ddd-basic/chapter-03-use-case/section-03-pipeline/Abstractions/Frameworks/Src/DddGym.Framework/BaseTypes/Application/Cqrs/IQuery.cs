
using ErrorOr;
using MediatR;

namespace DddGym.Framework.BaseTypes.Application.Cqrs;

public interface IQuery<out TResponse> : IRequest<IErrorOr<TResponse>>
    where TResponse : IResponse
{
}