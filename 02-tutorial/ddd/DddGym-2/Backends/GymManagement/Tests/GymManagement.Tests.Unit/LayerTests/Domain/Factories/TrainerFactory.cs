using GymManagement.Domain.AggregateRoots.Trainers;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal static class TrainerFactory
{
    public static Trainer CreateTrainer(
        Guid? userId = null,
        Guid? id = null)
    {
        //return new Trainer(
        //    userId: userId ?? DomainConstants.User.Id,
        //    id: id ?? DomainConstants.Trainer.Id);

        return Trainer.Create(
            userId: userId ?? DomainConstants.User.Id,
            id: id ?? DomainConstants.Trainer.Id);
    }
}