using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateTrainerProfile;

public sealed record CreateTrainerProfileCommand(
    Guid UserId)
    : ICommand;