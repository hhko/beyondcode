using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Profiles.Queries.GetProfile;

public sealed record GetProfileQuery(
    Guid UserId)
    : IQuery<GetProfileResponse>;