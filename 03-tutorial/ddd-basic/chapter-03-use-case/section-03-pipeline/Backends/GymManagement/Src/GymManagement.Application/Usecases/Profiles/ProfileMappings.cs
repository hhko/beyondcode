using GymManagement.Application.Usecases.Users.Queries.ListProfiles;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Profiles;

internal static class ProfileMappings
{
    public static GetProfileResponse ToResponse(this User user)
    {
        return new GetProfileResponse(
            user.AdminId,
            user.ParticipantId,
            user.TrainerId);
    }
}