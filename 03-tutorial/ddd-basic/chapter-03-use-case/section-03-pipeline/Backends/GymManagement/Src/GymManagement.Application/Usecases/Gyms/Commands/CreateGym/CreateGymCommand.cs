using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Gyms.Commands.CreateGym;

public sealed record CreateGymCommand(
    string Name,
    Guid SubscriptionId)
    : ICommand<CreateGymResponse>;