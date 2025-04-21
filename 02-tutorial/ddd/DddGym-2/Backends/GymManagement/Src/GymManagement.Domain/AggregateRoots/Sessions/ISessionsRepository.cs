using GymManagement.Domain.AggregateRoots.Sessions.Enumerations;

namespace GymManagement.Domain.AggregateRoots.Sessions;

public interface ISessionsRepository
{
    Task AddSessionAsync(Session session);
    Task<Session?> GetByIdAsync(Guid id);
    Task<List<Session>> ListByIds(
        IReadOnlyList<Guid> sessionIds,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null,
        List<SessionCategory>? categories = null);

    Task<List<Session>> ListByGymIdAsync(
        Guid gymId,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null,
        List<SessionCategory>? categories = null);

    Task UpdateAsync(Session session);

    Task<List<Session>> ListByRoomIdAsync(Guid roomId);
    Task RemoveRangeAsync(List<Session> sessions);
}
