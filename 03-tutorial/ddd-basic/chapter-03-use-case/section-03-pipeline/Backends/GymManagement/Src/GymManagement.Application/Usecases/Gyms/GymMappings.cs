using GymManagement.Application.Usecases.Gyms.Commands.CreateGym;
using GymManagement.Application.Usecases.Gyms.Queries.GetGym;
using GymManagement.Application.Usecases.Gyms.Queries.ListGyms;
using GymManagement.Application.Usecases.Trainers.Commands.AddTrainer;
using GymManagement.Domain.AggregateRoots.Gyms;

namespace GymManagement.Application.Usecases.Gyms;

internal static class GymMappings
{
    public static CreateGymResponse ToResponseCreated(this Gym gym)
    {
        return new CreateGymResponse(gym);
    }

    public static AddTrainerResponse ToResponseAdded(this ErrorOr.Success success)
    {
        return new AddTrainerResponse();
    }

    public static GetGymResponse ToResponse(this Gym gym)
    {
        return new GetGymResponse(gym);
    }

    public static ListGymsResponse ToResponse(this List<Gym> gyms)
    {
        return new ListGymsResponse(gyms);
    }
}
