using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles.Queries.GetProfile;

//public sealed record GetProfileResponse(
//    Option<Guid> AdminId = default,
//    Option<Guid> ParticipantId = default,
//    Option<Guid> TrainerId = default)
//    : IResponse
//{
//    public static GetProfileResponse Create(User user) =>
//        new(user.AdminId,
//            user.ParticipantId,
//            user.TrainerId);
//}

public sealed record GetProfileResponse(
    Option<Guid> AdminId,
    Option<Guid> ParticipantId,
    Option<Guid> TrainerId)
    : IResponse;