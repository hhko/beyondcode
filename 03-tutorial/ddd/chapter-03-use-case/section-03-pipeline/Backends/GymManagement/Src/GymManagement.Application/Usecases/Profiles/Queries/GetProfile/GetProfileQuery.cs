using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Profiles.Queries.GetProfile;

public sealed record GetProfileQuery(
    Guid UserId)
    : IQuery<GetProfileResponse>;