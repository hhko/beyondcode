using GymManagement.Application.Usecases.Profiles.Queries.GetProfile;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles;

public static class UserMapping
{
    public static Fin<GetProfileResponse> ToGetProfileResponse(this Fin<User> userResult)
    {
        return userResult.Match(
            Succ: _ =>
            {
                return new GetProfileResponse(
                    _.AdminId,
                    _.ParticipantId,
                    _.TrainerId);
            },
            Fail: Fin<GetProfileResponse>.Fail);
    }
}
