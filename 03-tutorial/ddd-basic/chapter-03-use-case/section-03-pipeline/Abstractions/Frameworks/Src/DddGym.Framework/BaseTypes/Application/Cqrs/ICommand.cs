using ErrorOr;
using MediatR;

namespace DddGym.Framework.BaseTypes.Application.Cqrs;

public interface ICommand : IRequest<IErrorOr>
{
}

public interface ICommand<out TResponse> : IRequest<IErrorOr<TResponse>>
    where TResponse : IResponse
{
}