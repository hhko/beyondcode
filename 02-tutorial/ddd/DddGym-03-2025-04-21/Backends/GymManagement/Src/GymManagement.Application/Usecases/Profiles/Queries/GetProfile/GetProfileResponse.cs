using DddGym.Framework.BaseTypes.Cqrs;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles.Queries.GetProfile;

public sealed record GetProfileResponse(
    Option<Guid> AdminId = default,
    Option<Guid> ParticipantId = default,
    Option<Guid> TrainerId = default)
    : IResponse;