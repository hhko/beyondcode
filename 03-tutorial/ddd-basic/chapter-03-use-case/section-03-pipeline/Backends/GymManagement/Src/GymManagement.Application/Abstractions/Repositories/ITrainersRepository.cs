using GymManagement.Domain.AggregateRoots.Trainers;

namespace GymManagement.Application.Abstractions.Repositories;

public interface ITrainersRepository
{
    //Task AddTrainerAsync(Trainer participant);
    Task<Trainer?> GetByIdAsync(Guid trainerId);
    Task UpdateAsync(Trainer trainer);
}
