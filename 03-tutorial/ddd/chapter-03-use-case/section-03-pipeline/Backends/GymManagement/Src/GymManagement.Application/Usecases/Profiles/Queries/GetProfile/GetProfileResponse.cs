using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Profiles.Queries.GetProfile;

public sealed record GetProfileResponse(
    Guid? AdminId,
    Guid? ParticipantId,
    Guid? TrainerId)
    : IResponse;