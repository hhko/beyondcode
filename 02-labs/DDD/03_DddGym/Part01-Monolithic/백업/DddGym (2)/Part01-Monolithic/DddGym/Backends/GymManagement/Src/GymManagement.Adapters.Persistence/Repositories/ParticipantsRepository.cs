using GymManagement.Domain.AggregateRoots.Participants;

namespace GymManagement.Adapters.Persistence.Repositories;

public class ParticipantsRepository : IParticipantsRepository
{
    public FinT<IO, Unit> AddParticipantAsync(Participant participant)
    {
        return IO.lift(() => unit);
    }

    public Task<Participant?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Participant>> ListByIds(List<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Participant participant)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(List<Participant> participants)
    {
        throw new NotImplementedException();
    }
}
