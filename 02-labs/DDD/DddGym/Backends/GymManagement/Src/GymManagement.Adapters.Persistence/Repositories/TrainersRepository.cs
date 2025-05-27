using GymManagement.Domain.AggregateRoots.Trainers;

namespace GymManagement.Adapters.Persistence.Repositories;

public class TrainersRepository : ITrainersRepository
{
    public FinT<IO, Unit> AddTrainerAsync(Trainer participant)
    {
        return lift(() => unit);
    }

    //public Task<Trainer?> GetByIdAsync(Guid trainerId)
    public FinT<IO, Trainer> GetByIdAsync(Guid trainerId)
    {
        return lift(() => Trainer.Create(Guid.NewGuid()));
    }

    public FinT<IO, Unit> UpdateAsync(Trainer trainer)
    {
        return lift(() => unit);
    }
}
