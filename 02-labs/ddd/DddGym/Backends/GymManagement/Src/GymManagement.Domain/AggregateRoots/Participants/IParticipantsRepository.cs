namespace GymManagement.Domain.AggregateRoots.Participants;

public interface IParticipantsRepository
{
    //public Task AddParticipantAsync(Participant participant);
    Task<Participant?> GetByIdAsync(Guid id);
    Task<List<Participant>> ListByIds(List<Guid> ids);
    Task UpdateAsync(Participant participant);
    Task UpdateRangeAsync(List<Participant> participants);
}
