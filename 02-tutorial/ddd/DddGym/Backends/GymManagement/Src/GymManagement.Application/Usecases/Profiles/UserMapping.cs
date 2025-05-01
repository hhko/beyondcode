using GymManagement.Application.Usecases.Profiles.Queries;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles;

public static class UserMapping
{
    public static Fin<GetProfile.Response> ToGetProfileResponse(this Fin<User> userResult)
    {
        return userResult.Match(
            Succ: _ =>
            {
                return new GetProfile.Response(
                    _.AdminId,
                    _.ParticipantId,
                    _.TrainerId);
            },
            Fail: Fin<GetProfile.Response>.Fail);
    }
}
