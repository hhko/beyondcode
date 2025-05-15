using GymManagement.Domain.AggregateRoots.Participants;

namespace GymManagement.Application.Abstractions.Repositories;

public interface IParticipantsRepository
{
    //public Task AddParticipantAsync(Participant participant);
    Task<Participant?> GetByIdAsync(Guid id);
    Task<List<Participant>> ListByIds(List<Guid> ids);
    Task UpdateAsync(Participant participant);
    Task UpdateRangeAsync(List<Participant> participants);
}
