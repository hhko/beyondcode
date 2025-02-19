
using ErrorOr;
using MediatR;

namespace DddGym.Application.Abstractions.BaseTypes.Cqrs;

public interface IQuery<out TResponse> : IRequest<IErrorOr<TResponse>>
    where TResponse : IResponse
{
}