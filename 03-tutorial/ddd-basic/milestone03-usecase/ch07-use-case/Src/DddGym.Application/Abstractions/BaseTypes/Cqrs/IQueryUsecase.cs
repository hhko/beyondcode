
using ErrorOr;
using MediatR;

namespace DddGym.Application.Abstractions.BaseTypes.Cqrs;

public interface IQueryUsecase<in TQuery, TResponse> : IRequestHandler<TQuery, IErrorOr<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : IResponse
{
}