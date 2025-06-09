using SolutionName.Framework.BaseTypes.Application.Cqrs;
using HostName.Domain.AggregateRoots.EntityNames;
using ErrorOr;

namespace HostName.Application.Usecases.EntityNames.Commands.CommandName;

internal sealed class CommandNameCommandUsecase
    : ICommandUsecase<CommandNameCommand, CommandNameResponse>
{
    public async Task<IErrorOr<CommandNameResponse>> Handle(CommandNameCommand command, CancellationToken cancellationToken)
    {
        // return Error
        //     .NotFound(description: "Subscription not found")
        //     .ToErrorOr<CommandNameResponse>();

        // return xyx
        //     .ToResponse()
        //     .ToErrorOr();
    }
}
