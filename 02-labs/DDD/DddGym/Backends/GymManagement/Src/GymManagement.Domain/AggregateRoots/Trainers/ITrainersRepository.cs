namespace GymManagement.Domain.AggregateRoots.Trainers;

public interface ITrainersRepository
{
    FinT<IO, Unit> AddTrainerAsync(Trainer participant);
    FinT<IO, Trainer> GetByIdAsync(Guid trainerId);
    FinT<IO, Unit> UpdateAsync(Trainer trainer);
}
