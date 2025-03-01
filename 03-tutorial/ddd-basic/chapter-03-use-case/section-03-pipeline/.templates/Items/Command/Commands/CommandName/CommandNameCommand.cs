using SolutionName.Framework.BaseTypes.Application.Cqrs;

namespace HostName.Application.Usecases.EntityNames.Commands.CommandName;

public sealed record CommandNameCommand()
    : ICommand<CommandNameResponse>;