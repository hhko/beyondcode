using ErrorOr;
using MediatR;

namespace DddGym.Framework.BaseTypes.Application.Cqrs;

public interface IQueryUsecase<in TQuery, TResponse> : IRequestHandler<TQuery, IErrorOr<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : IResponse;