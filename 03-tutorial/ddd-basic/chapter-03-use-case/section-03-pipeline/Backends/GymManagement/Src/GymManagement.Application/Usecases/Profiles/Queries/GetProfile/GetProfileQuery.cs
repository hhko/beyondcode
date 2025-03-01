using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Users.Queries.ListProfiles;

public sealed record GetProfileQuery(
    Guid UserId)
    : IQuery<GetProfileResponse>;