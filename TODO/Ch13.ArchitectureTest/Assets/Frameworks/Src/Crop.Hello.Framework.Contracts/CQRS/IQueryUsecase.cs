using Crop.Hello.Framework.Contracts.Results;
using MediatR;

namespace Crop.Hello.Framework.Contracts.CQRS;

public interface IQueryUsecase<in TQuery, TResponse> : IRequestHandler<TQuery, IResult<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : IResponse
{
}
