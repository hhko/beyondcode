using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Users.Queries.ListProfiles;

public sealed record GetProfileResponse(
    Guid? AdminId,
    Guid? ParticipantId,
    Guid? TrainerId)
    : IResponse;