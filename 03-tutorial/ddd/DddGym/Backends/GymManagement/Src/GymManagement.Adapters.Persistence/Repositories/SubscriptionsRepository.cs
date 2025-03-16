using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Adapters.Persistence.Repositories;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    public Task AddSubscriptionAsync(Subscription subscription)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Subscription?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Subscription subscription)
    {
        throw new NotImplementedException();
    }
}