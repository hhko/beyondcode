using GymManagement.Application.Usecases.Gyms.Commands;
using GymManagement.Application.Usecases.Gyms.Queries;
using GymManagement.Domain.AggregateRoots.Gyms;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Gyms;

internal static class GymMappings
{
    public static CreateGymCommand.Response ToCreateGymResponse(this Gym gym)
    {
        return new CreateGymCommand.Response(gym);
    }

    //public static AddTrainerResponse ToResponseTrainerAdded(this ErrorOr.Success _)
    //{
    //    return new AddTrainerResponse();
    //}

    public static GetGymQuery.Response ToGetGymResponse(this Gym gym)
    {
        return new GetGymQuery.Response(gym);
    }

    public static ListGymsQuery.Response ToListGymsResponse(this List<Gym> gyms)
    {
        return new ListGymsQuery.Response(gyms);
    }

    public static ListSessionsQuery.Response ToListSessionsResponse(this List<Session> sessions)
    {
        return new ListSessionsQuery.Response(sessions);
    }
}