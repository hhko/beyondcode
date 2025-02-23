using ErrorOr;
using MediatR;

namespace DddGym.Application.Abstractions.BaseTypes.Cqrs;

public interface ICommand : IRequest<IErrorOr>
{
}

public interface ICommand<out TResponse> : IRequest<IErrorOr<TResponse>>
    where TResponse : IResponse
{
}