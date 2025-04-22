using DddGym.Framework.BaseTypes.Cqrs;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateParticipantProfile;

public sealed record CreateParticipantProfileResponse(
    Option<Guid> ParticipantId = default)
    : IResponse;