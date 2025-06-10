namespace GymManagement.Domain.AggregateRoots.Gyms;

public interface IGymsRepository
{
    Task AddGymAsync(Gym gym);

    Task<Gym?> GetByIdAsync(Guid id);

    //Task<bool> ExistsAsync(Guid id);
    Task<List<Gym>> ListSubscriptionGyms(Guid subscriptionId);

    Task UpdateAsync(Gym gym);
}