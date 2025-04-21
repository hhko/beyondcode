using GymManagement.Application.Usecases.Profiles.Queries.GetProfile;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles;

internal static class ProfileMappings
{
    //public static GetProfileResponse ToResponse(this User user)
    //{
    //    return new GetProfileResponse(
    //        user.AdminId,
    //        user.ParticipantId,
    //        user.TrainerId);
    //}

    public static GetProfileResponse ToResponse(this Fin<User> user)
    {
        return user.Match(
            Succ: _ =>
                new GetProfileResponse(
                    _.AdminId,
                    _.ParticipantId,
                    _.TrainerId),
            Fail: _ =>
                new GetProfileResponse());
    }
}