using DddGym.Application.Usecases.Gyms.Queries.GetGym;
using DddGym.Application.Usecases.Gyms.Queries.ListGyms;
using DddGym.Domain.AggregateRoots.Gyms;

namespace DddGym.Application.Usecases.Gyms;

internal static class GymMappings
{
    public static GetGymResponse ToResponse(this Gym gym)
    {
        return new GetGymResponse(gym);
    }

    public static ListGymsResponse ToResponse(this List<Gym> gyms)
    {
        return new ListGymsResponse(gyms);
    }
}
