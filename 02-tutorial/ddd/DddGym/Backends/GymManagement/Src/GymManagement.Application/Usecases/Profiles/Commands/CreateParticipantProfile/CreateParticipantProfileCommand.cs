using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateParticipantProfile;

public sealed record CreateParticipantProfileCommand(
    Guid UserId)
    : ICommand2<CreateParticipantProfileResponse>;