using HostName.Application.Abstractions.BaseTypes.Cqrs;

namespace HostName.Application.Usecases.EntityNames.Commands.CommandName;

public sealed record CommandNameCommand(
    string Name,
    Guid SubscriptionId)
    : ICommand<CommandNameResponse>;
