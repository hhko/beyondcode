namespace GymManagement.Domain.AggregateRoots.Trainers;

public interface ITrainersRepository
{
    Task AddTrainerAsync(Trainer participant);
    Task<Trainer?> GetByIdAsync(Guid trainerId);
    Task UpdateAsync(Trainer trainer);
}
