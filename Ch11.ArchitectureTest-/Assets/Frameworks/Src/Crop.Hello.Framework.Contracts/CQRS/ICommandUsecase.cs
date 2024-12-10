using Crop.Hello.Framework.Contracts.Results;
using MediatR;

namespace Crop.Hello.Framework.Contracts.CQRS;

public interface ICommandUsecase<in TCommand> : IRequestHandler<TCommand, IResult>
    where TCommand : ICommand
{
}

public interface ICommandUsecase<in TCommand, TResponse> : IRequestHandler<TCommand, IResult<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : IResponse
{
}
