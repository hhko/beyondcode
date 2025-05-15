using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Sessions.Enumerations;

namespace GymManagement.Adapters.Persistence.Repositories;

public class SessionsRepository : ISessionsRepository
{
    public Task AddSessionAsync(Session session)
    {
        throw new NotImplementedException();
    }

    public Task<Session?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Session>> ListByGymIdAsync(Guid gymId, DateTime? startDateTime = null, DateTime? endDateTime = null, List<SessionCategory>? categories = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<Session>> ListByIds(IReadOnlyList<Guid> sessionIds, DateTime? startDateTime = null, DateTime? endDateTime = null, List<SessionCategory>? categories = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<Session>> ListByRoomIdAsync(Guid roomId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync(List<Session> sessions)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Session session)
    {
        throw new NotImplementedException();
    }
}
