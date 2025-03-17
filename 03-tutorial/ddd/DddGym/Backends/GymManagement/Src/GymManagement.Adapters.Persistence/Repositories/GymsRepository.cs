using GymManagement.Domain.AggregateRoots.Gyms;

namespace GymManagement.Adapters.Persistence.Repositories;

public class GymsRepository : IGymsRepository
{
    public Task AddGymAsync(Gym gym)
    {
        throw new NotImplementedException();
    }

    public Task<Gym?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Gym>> ListSubscriptionGyms(Guid subscriptionId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Gym gym)
    {
        throw new NotImplementedException();
    }
}