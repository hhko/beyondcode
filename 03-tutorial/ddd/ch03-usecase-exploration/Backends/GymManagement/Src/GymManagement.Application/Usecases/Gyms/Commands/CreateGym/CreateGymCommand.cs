using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Gyms.Commands.CreateGym;

public sealed record CreateGymCommand(
    string Name,
    Guid SubscriptionId)
    : ICommand<CreateGymResponse>;