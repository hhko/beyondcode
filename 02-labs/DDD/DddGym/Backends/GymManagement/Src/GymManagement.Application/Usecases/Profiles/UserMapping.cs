using GymManagement.Application.Usecases.Profiles.Commands;
using GymManagement.Application.Usecases.Profiles.Queries;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles;

public static class UserMapping
{
    public static async ValueTask<Fin<GetProfileQuery.Response>> ToGetProfileResponse(this ValueTask<Fin<User>> task)
    {
        var result = await task;
        return result.Match(
            Succ: user => new GetProfileQuery.Response(
                user.AdminId,
                user.ParticipantId,
                user.TrainerId),
            Fail: Fin<GetProfileQuery.Response>.Fail);
    }

    public static async ValueTask<Fin<CreateAdminProfileCommand.Response>> ToCreateAdminProfileResponse(this ValueTask<Fin<Guid>> task)
    {
        var result = await task;
        return result.Match(
            Succ: adminId => new CreateAdminProfileCommand.Response(adminId),
            Fail: Fin<CreateAdminProfileCommand.Response>.Fail);
    }

    public static async ValueTask<Fin<CreateParticipantProfileCommand.Response>> ToCreateParticipantProfileResponse(this ValueTask<Fin<Guid>> task)
    {
        var result = await task;
        return result.Match(
            Succ: participantId => new CreateParticipantProfileCommand.Response(participantId),
            Fail: Fin<CreateParticipantProfileCommand.Response>.Fail);
    }

    public static async ValueTask<Fin<CreateTrainerProfileCommand.Response>> ToCreateTrainerProfileResponse(this ValueTask<Fin<Guid>> task)
    {
        var result = await task;
        return result.Match(
            Succ: trainerId => new CreateTrainerProfileCommand.Response(trainerId),
            Fail: Fin<CreateTrainerProfileCommand.Response>.Fail);
    }
}
