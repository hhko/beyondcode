using GymManagement.Application.Usecases.Gyms.Commands.CreateGym;
using GymManagement.Application.Usecases.Gyms.Queries.GetGym;
using GymManagement.Application.Usecases.Gyms.Queries.ListGyms;
using GymManagement.Application.Usecases.Gyms.Queries.ListSessionse;
using GymManagement.Domain.AggregateRoots.Gyms;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Gyms;

internal static class GymMappings
{
    public static CreateGymResponse ToResponseCreated(this Gym gym)
    {
        return new CreateGymResponse(gym);
    }

    //public static AddTrainerResponse ToResponseTrainerAdded(this ErrorOr.Success _)
    //{
    //    return new AddTrainerResponse();
    //}

    public static GetGymResponse ToResponse(this Gym gym)
    {
        return new GetGymResponse(gym);
    }

    public static ListGymsResponse ToResponse(this List<Gym> gyms)
    {
        return new ListGymsResponse(gyms);
    }

    public static ListSessionsResponse ToResponse(this List<Session> sessions)
    {
        return new ListSessionsResponse(sessions);
    }
}