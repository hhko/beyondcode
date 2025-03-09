using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateParticipantProfile;

public sealed record CreateParticipantProfileCommand(
    Guid UserId)
    : ICommand;