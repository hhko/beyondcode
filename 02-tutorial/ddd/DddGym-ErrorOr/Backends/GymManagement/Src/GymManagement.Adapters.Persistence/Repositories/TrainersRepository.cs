using GymManagement.Domain.AggregateRoots.Trainers;

namespace GymManagement.Adapters.Persistence.Repositories;

public class TrainersRepository : ITrainersRepository
{
    public Task AddTrainerAsync(Trainer participant)
    {
        throw new NotImplementedException();
    }

    public Task<Trainer?> GetByIdAsync(Guid trainerId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Trainer trainer)
    {
        throw new NotImplementedException();
    }
}
